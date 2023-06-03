import numpy as np
import matplotlib.pyplot as plot
import librosa
import math

# Get x values of the sine wave
time = np.arange(0, 10, 0.1)
# Amplitude of the sine wave is sine of a variable like time
amplitude = np.sin(time)


def sgn(x):
    if x == 0: return 0
    return x / abs(x)
# Su dung A
def A_compress(xn, A):
    res = []
    for x in xn:
        if np.abs(x) < 1/A:
            res.append(sgn(x) * A * np.abs(x) / (1 + math.log(A)))
        else:
            res.append(sgn(x) * (1 + math.log(A*np.abs(x))) / (1 + math.log(A)))
    
    return np.array(res)

def A_decompress(yn, A):
    res = []
    for y in yn:
        if np.abs(y) < 1 / (1 + math.log(A)):
            res.append(sgn(y) * abs(y) * (1 + math.log(A)) / A)
        else:
            res.append(sgn(y) * math.exp(-1 + np.abs(y)* (1 + math.log(A))) / A)
    
    return np.array(res)

# Su dung A
# encode
compress_A = A_compress(amplitude, 87.56)
plot.plot(time, compress_A)
plot.show()
# decode
expand_A = A_decompress(compress_A, 87.56)
plot.plot(time, expand_A)
plot.show()