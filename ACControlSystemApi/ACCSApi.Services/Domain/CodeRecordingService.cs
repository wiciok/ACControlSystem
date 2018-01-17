using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ACCSApi.Model;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace ACCSApi.Services.Domain
{
    public class CodeRecordingService : ICodeRecordingService
    {
        private const int ShortAndLongPulseBoundary = 1000;
        private const int MaxAcCommandLength = 10000;
        private readonly IACDevice _currentAcDevice;
        private readonly GpioPin _inputPin;

        public CodeRecordingService(IACDeviceService acDeviceService, IHostDeviceService hostDeviceService)
        {
            _currentAcDevice = acDeviceService.GetCurrentDevice();
            var currentRaspberryPiDevice = hostDeviceService.GetCurrentDevice();

            //todo: restore this after deploy to raspberry!
            //temporary!!!
            //_inputPin = Pi.Gpio.Pins.Single(x => x.HeaderPinNumber == currentRaspberryPiDevice.BoardInPin);
            //_inputPin.PinMode = GpioPinDriveMode.Input;
        }

        private List<Tuple<byte, double>> RecordCode()
        {
            var pulseList = new List<Tuple<byte, double>>();
            var value = true;
            var numOnes = 0;
            var previousValue = false;

            while (value)
            {
                value = _inputPin.Read();
            }

            var stopwatch = Stopwatch.StartNew();
            while (numOnes < MaxAcCommandLength)
            {
                if (value != previousValue)
                {
                    var pulseLength = stopwatch.Elapsed;
                    stopwatch.Restart();
                    pulseList.Add(new Tuple<byte, double>(previousValue ? (byte)1 : (byte)0, pulseLength.TotalMilliseconds * 1000));
                }

                numOnes = value ? numOnes + 1 : 0;

                previousValue = value;
                value = _inputPin.Read();
            }
            stopwatch.Stop();

            return pulseList;
        }

        public void ResetCurrentAcDeviceNecCodeSettings()
        {
            if (_currentAcDevice == null)
                throw new CurrentACDeviceNotSetException();

            _currentAcDevice.NecCodeSettings = null;
        }

        public RawCode RecordRawCode()
        {
            var pulseList = RecordCode();
            var rawCode = new RawCode { Code = pulseList.Select(x => (int)x.Item2).ToArray() };

            return rawCode;
        }

        public NecCode RecordNecCode()
        {
            if (_currentAcDevice == null)
                throw new CurrentACDeviceNotSetException();

            var necCode = new NecCode();
            var pulseList = RecordCode();

            if (_currentAcDevice.NecCodeSettingsSaved)
            {
                pulseList.RemoveRange(0, 2);
                pulseList.Remove(pulseList.Last());
                necCode.Code = BuildStringCode(pulseList, ShortAndLongPulseBoundary);
            }
            else
            {
                var necCodeSettings = RegisterNecCodeSettings(pulseList);
                necCode.Code = BuildStringCode(pulseList, ShortAndLongPulseBoundary);

                SaveNecCodeSettingsToAcDeviceObject(necCodeSettings);
            }
            return necCode;
        }

        private NecCodeSettings RegisterNecCodeSettings(List<Tuple<byte, double>> pulseList)
        {
            bool necSendTrailingPulse;

            pulseList.RemoveRange(0, 2);

            if (pulseList.Count % 2 == 1)
            {
                necSendTrailingPulse = true;
                pulseList.Remove(pulseList.Last());
            }
            else
            {
                necSendTrailingPulse = false;
            }

            //determine duration of pulses

            var necOneGapDuration =
                (int)Math.Round(
                    pulseList
                        .Where(x => x.Item2 >= ShortAndLongPulseBoundary && x.Item1 == 1)
                        .Select(t => t.Item2)
                        .Average());

            var zeroGapDuration =
                (int)Math.Round(
                    pulseList
                        .Where(x => x.Item2 < ShortAndLongPulseBoundary && x.Item1 == 1)
                        .Select(t => t.Item2)
                        .Average());

            var zeroPulseAndOnePulseDuration =
                (int)Math.Round(
                    pulseList
                        .Where(x => x.Item1 == 0)
                        .Select(t => t.Item2)
                        .Average());

            var averageShortPulse = (zeroGapDuration + zeroPulseAndOnePulseDuration) / 2;

            var necOnePulseDuration = averageShortPulse;
            var necZeroPulseDuration = averageShortPulse;
            var necZeroGapDuration = averageShortPulse;
            var necLeadingPulseDuration = (int)pulseList[0].Item2;
            var necLeadingGapDuration = (int)pulseList[1].Item2;

            var necCodeSettings = new NecCodeSettings
            (
                leadingPulseDuration: necLeadingPulseDuration,
                leadingGapDuration: necLeadingGapDuration,
                onePulseDuration: necOnePulseDuration,
                oneGapDuration: necOneGapDuration,
                zeroPulseDuration: necZeroPulseDuration,
                zeroGapDuration: necZeroGapDuration,
                sendTrailingPulse: necSendTrailingPulse
            );

            return necCodeSettings;
        }

        private void SaveNecCodeSettingsToAcDeviceObject(NecCodeSettings settings)
        {
            _currentAcDevice.NecCodeSettings = settings;
        }

        private static string BuildStringCode(IEnumerable<Tuple<byte, double>> pulseList, int threshold)
        {
            var code = pulseList.Select(x => x.Item2).ToList();
            var codeBuilder = new StringBuilder();
            for (var i = 0; i < code.Count; i += 2)
            {
                codeBuilder.Append(code[i + 1] >= threshold ? 1 : 0);
            }
            return codeBuilder.ToString();
        }
    }
}
