using System;
using System.Runtime.InteropServices;

namespace IRSlingerCsharp
{
    public class IRSlingerCsharp: IIRSlingerCsharp
    {
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
            throw new InvalidOperationException("pigpio sending nec pulse error!");
        }

        [DllImport("irslinger_start.so", 
            CharSet = CharSet.Ansi, 
            EntryPoint = "SendNec", 
            CallingConvention = CallingConvention.StdCall, 
            BestFitMapping = true,
            ThrowOnUnmappableChar = true)] 
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
            [MarshalAs(UnmanagedType.LPStr)]string codes
        );

        public void SendRawMsg
        (
            UInt32 broadcomOutPin,
            int frequency,
            double dutyCycle,
            int[] codes
        )
        {
            var retCode = SendRaw(broadcomOutPin, frequency, dutyCycle, codes);
            if (retCode == 0)
                return;
            throw new InvalidOperationException("pigpio sending raw pulse error!");
        }

        [DllImport("irslinger_start.so", EntryPoint = "SendRaw", CallingConvention = CallingConvention.StdCall)]
        private static extern int SendRaw
        (
            UInt32 outPin,
            int frequency,
            double dutyCycle,
            int[] codes
        );
    }
}
