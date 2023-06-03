import numpy as np
import matplotlib.pyplot as plot

#from wavenet import mu_law_encode, mu_law_decode

QUANT_LEVELS = 255


# A set of mu law encode/decode functions implemented
# in numpy
def manual_mu_law_encode(signal, quantization_channels):
    # Manual mu-law companding and mu-bits quantization
    mu = quantization_channels - 1

    magnitude = np.log1p(mu * np.abs(signal)) / np.log1p(mu)
    signal = np.sign(signal) * magnitude

    # Map signal from [-1, +1] to [0, mu-1]
    signal = (signal + 1) / 2 * mu + 0.5
    quantized_signal = signal.astype(np.int32)

    return quantized_signal


def manual_mu_law_decode(signal, quantization_channels):
    # Calculate inverse mu-law companding and dequantization
    mu = quantization_channels - 1
    y = signal.astype(np.float32)

    y = 2 * (y / mu) - 1
    x = np.sign(y) * (1.0 / mu) * ((1.0 + mu)**abs(y) - 1.0)
    return x
time        = np.arange(0, 10, 0.1);

 

# Amplitude of the sine wave is sine of a variable like time

amplitude   = np.sin(time)
encode=manual_mu_law_encode(amplitude,QUANT_LEVELS)
decode=manual_mu_law_decode(encode,QUANT_LEVELS)
plot.plot(time, amplitude)
plot.title("raw")
plot.grid(True, which='both')
plot.axhline(y=0, color='k')
plot.show()
plot.plot(time,encode)
plot.plot(time, amplitude)
plot.title("encode")

plot.grid(True, which='both')
plot.axhline(y=0, color='k')
plot.show()
plot.plot(time,decode)

plot.title("decode")
plot.grid(True, which='both')
plot.axhline(y=0, color='k')
plot.show()