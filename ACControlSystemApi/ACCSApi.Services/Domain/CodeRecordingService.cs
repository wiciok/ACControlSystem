using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Utils;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace ACCSApi.Services.Domain
{
    //todo: make sure that proper conversion between readed bool valuea and byte is performed

    public class CodeRecordingService : ICodeRecordingService
    {
        private const int ShortAndLongPulseThreshold = 1000;
        private readonly IACDeviceRepository _acDeviceRepository;
        private readonly IRaspberryPiDeviceRepository _raspberryPiDeviceRepository;
        private IACDevice _currentAcDevice;
        private IRaspberryPiDevice _currentRaspberryPiDevice;
        private GpioPin _inputPin;

        public CodeRecordingService(IACDeviceRepository acDeviceRepository, IRaspberryPiDeviceRepository raspberryPiDeviceRepository)
        {
            _acDeviceRepository = acDeviceRepository;
            _raspberryPiDeviceRepository = raspberryPiDeviceRepository;
            Init();
        }

        private void Init()
        {
            _currentAcDevice = _acDeviceRepository.CurrentACDevice;
            _currentRaspberryPiDevice = _raspberryPiDeviceRepository.CurrentDevice;

            _inputPin = Pi.Gpio.Pins.Single(x => x.HeaderPinNumber == _currentRaspberryPiDevice.BoardInPin);
            _inputPin.PinMode = GpioPinDriveMode.Input;
        }

        private List<Tuple<byte, double>> RecordCode()
        {
            var pulseList = new List<Tuple<byte, double>>();
            byte value = 1;
            var numOnes = 0;
            byte previousValue = 0;

            while (value == 1)
            {
                value = (byte)(_inputPin.Read() ? 1 : 0);
            }

            const int maxCommandLength = 10000;

            var stopwatch = Stopwatch.StartNew();
            while (numOnes < maxCommandLength)
            {
                if (value != previousValue)
                {
                    var pulseLength = stopwatch.Elapsed;
                    stopwatch.Restart();
                    pulseList.Add(new Tuple<byte, double>(previousValue, pulseLength.TotalMilliseconds * 1000));
                }

                numOnes = value == 1 ? numOnes + 1 : 0;

                previousValue = value;
                value = (byte)(_inputPin.Read() ? 1 : 0);
            }
            stopwatch.Stop();

            return pulseList;
        }

        public RawCode RecordRawCode()
        {
            var pulseList = RecordCode();
            var rawCode = new RawCode { Code = pulseList.Select(x => (int)x.Item2).ToArray() };

            return rawCode;
        }

        public NecCode RecordNecCode()
        {
            NecCode necCode;
            var pulseList = RecordCode();

            if (_currentAcDevice.NecPulseDurationSettingsSaved)
            {
                necCode = new NecCode()
                {
                    LeadingPulseDuration = _currentAcDevice.LeadingPulseDuration ?? throw new InvalidOperationException(),
                    LeadingGapDuration = _currentAcDevice.LeadingGapDuration ?? throw new InvalidOperationException(),
                    ZeroGapDuration = _currentAcDevice.ZeroGapDuration ?? throw new InvalidOperationException(),
                    ZeroPulseDuration = _currentAcDevice.ZeroPulseDuration ?? throw new InvalidOperationException(),
                    OneGapDuration = _currentAcDevice.OneGapDuration ?? throw new InvalidOperationException(),
                    OnePulseDuration = _currentAcDevice.OnePulseDuration ?? throw new InvalidOperationException(),
                    SendTrailingPulse = _currentAcDevice.SendTrailingPulse ?? throw new InvalidOperationException()
                };
                pulseList.RemoveRange(0, 2);
                pulseList.Remove(pulseList.Last());
                necCode.Code = BuildStringCode(pulseList, ShortAndLongPulseThreshold);
            }
            else
            {
                necCode = new NecCode();

                necCode.LeadingPulseDuration = (int)pulseList[0].Item2;
                necCode.LeadingGapDuration = (int)pulseList[1].Item2;
                pulseList.RemoveRange(0, 2);

                if (pulseList.Count % 2 == 1)
                {
                    necCode.SendTrailingPulse = true;
                    pulseList.Remove(pulseList.Last());
                }
                else
                {
                    necCode.SendTrailingPulse = false;
                }

                //determine duration of pulses

                necCode.OneGapDuration =
                    (int)Math.Round(
                        pulseList
                        .Where(x => x.Item2 >= ShortAndLongPulseThreshold && x.Item1 == 1)
                        .Select(t => t.Item2)
                        .Average());

                var zeroGapDuration =
                    (int)Math.Round(
                        pulseList
                        .Where(x => x.Item2 < ShortAndLongPulseThreshold && x.Item1 == 1)
                        .Select(t => t.Item2)
                        .Average());

                var zeroPulseAndOnePulseDuration =
                    (int)Math.Round(
                        pulseList
                        .Where(x => x.Item1 == 0)
                        .Select(t => t.Item2)
                        .Average());

                var averageShortPulse = (zeroGapDuration + zeroPulseAndOnePulseDuration) / 2;

                necCode.OnePulseDuration = averageShortPulse;
                necCode.ZeroPulseDuration = averageShortPulse;
                necCode.ZeroGapDuration = averageShortPulse;

                //determine logical representation of code
                necCode.Code = BuildStringCode(pulseList, ShortAndLongPulseThreshold);

                SaveNecCodeSettingsToAcDeviceObject(necCode);
            }
            return necCode;
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

        private void SaveNecCodeSettingsToAcDeviceObject(NecCode code)
        {
            _currentAcDevice.LeadingGapDuration = code.LeadingGapDuration;
            _currentAcDevice.LeadingPulseDuration = code.LeadingPulseDuration;
            _currentAcDevice.OneGapDuration = code.OneGapDuration;
            _currentAcDevice.OnePulseDuration = code.OnePulseDuration;
            _currentAcDevice.ZeroPulseDuration = code.ZeroPulseDuration;
            _currentAcDevice.ZeroGapDuration = code.ZeroGapDuration;
            _currentAcDevice.SendTrailingPulse = code.SendTrailingPulse;

            _acDeviceRepository.Update(_currentAcDevice);
        }
           

        private static string ConvertByteArrayToString(byte[] array)
        {
            var strBuilder = new StringBuilder();
            foreach (var character in array)
            {
                strBuilder.Append(character);
            }
            return strBuilder.ToString();
        }
    }
}
