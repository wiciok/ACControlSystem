using System;
using System.Runtime.InteropServices;

namespace IRSlingerCsharp
{
    public interface IIRSlingerCsharp
    {
        void SendNecMsg
        (
            UInt32 broadcomOutPin,
            int frequency,
            double dutyCycle,
            int leadingPulseDuration,
            int leadingGapDuration,
            int onePulse,
            int zeroPulse,
            int oneGap,
            int zeroGap,
            bool sendTrailingPulse,
            string codes
         );

        void SendRawMsg
        (
            UInt32 broadcomOutPin,
            int frequency,
            double dutyCycle,
            string codes
        );
    }


    public class IRSlingerCsharp: IIRSlingerCsharp
    {
        [DllImport("irslinger_start.so", EntryPoint = "SendNec")] //todo: dopasować rozszerzenie pliku, itp.
        private static extern int SendNec
        (
            UInt32 outPin,
            int frequency,
            double dutyCycle,
            int leadingPulseDuration,
            int leadingGapDuration,
            int onePulse,
            int zeroPulse,
            int oneGap,
            int zeroGap,
            int sendTrailingPulse,
            string codes
        );



        public void SendNecMsg
        (
            UInt32 broadcomOutPin,
            int frequency,
            double dutyCycle,
            int leadingPulseDuration,
            int leadingGapDuration,
            int onePulse,
            int zeroPulse,
            int oneGap,
            int zeroGap,
            bool sendTrailingPulse,
            string codes
        )
        {
            int sendTrailingPulseInt = sendTrailingPulse ? 1 : 0;
            int retCode = SendNec(broadcomOutPin, frequency, dutyCycle, leadingPulseDuration, leadingGapDuration, onePulse, zeroPulse, oneGap, zeroGap, sendTrailingPulseInt, codes);

            if (retCode == 0)
                return;
            else
                throw new InvalidOperationException("pigpio sending nec pulse error!");
        }

        [DllImport("irslinger_start.so", EntryPoint = "SendRaw")] //todo: dopasować rozszerzenie pliku, itp.
        private static extern int SendRaw
        (
            UInt32 outPin,
            int frequency,
            double dutyCycle,
            string codes
        );


        public void SendRawMsg
        (
            UInt32 broadcomOutPin,
            int frequency,
            double dutyCycle,
            string codes
        )
        {
            int retCode = SendRaw(broadcomOutPin, frequency, dutyCycle, codes);
            if (retCode == 0)
                return;
            else
                throw new InvalidOperationException("pigpio sending raw pulse error!");
        }
    }
}
