using System;

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
            int[] codes
        );
    }
}
