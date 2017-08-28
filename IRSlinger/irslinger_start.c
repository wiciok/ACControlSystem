#include <stdio.h>
#include <inttypes.h>
#include "irslinger.h"

int SendNec
(
	uint32_t outPin, 				// The Broadcom pin number the signal will be sent on
	int frequency, 					// The frequency of the IR signal in Hz
	double dutyCycle, 				// The duty cycle of the IR signal. 0.5 means for every cycle, the LED will turn on for half the cycle time, and off the other half
	int leadingPulseDuration, 		// The duration of the beginning pulse in microseconds
	int leadingGapDuration, 		// The duration of the gap in microseconds after the leading pulse
	int onePulse, 					// The duration of a pulse in microseconds when sending a logical 1
	int zeroPulse, 					// The duration of a pulse in microseconds when sending a logical 0
	int oneGap, 					// The duration of the gap in microseconds when sending a logical 1
	int zeroGap, 					// The duration of the gap in microseconds when sending a logical 0
	int sendTrailingPulse,			// 1 = Send a trailing pulse with duration equal to "onePulse", 0 = Don't send a trailing pulse
	const char* codes
)
{
	return irSling(outPin, frequency, dutyCycle, leadingPulseDuration, leadingGapDuration, onePulse, zeroPulse, oneGap, zeroGap, sendTrailingPulse, codes);
}

int SendRaw
(
	uint32_t outPin, 	   // The Broadcom pin number the signal will be sent on
	int frequency, 		   // The frequency of the IR signal in Hz
	double dutyCycle,	   // The duty cycle of the IR signal. 0.5 means for every cycle, the LED will turn on for half the cycle time, and off the other half
	int* codes
)
{
	return irSlingRaw(outPin, frequency, dutyCycle, codes, sizeof(codes) / sizeof(int));
}
