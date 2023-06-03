import numpy as np
from numpy.polynomial.polynomial import polyval as npp_polyval
from scipy import signal
from scipy import special, optimize, fft as sp_fft
import operator
import matplotlib.pyplot as plt 
from scipy.io import wavfile
import math
import matplotlib 
import wave
from scipy.signal import lfilter, hamming
from librosa import lpc
spf = wave.open("A96.wav", "r") # http://www.linguistics.ucla.edu/people/hayes/103/Charts/VChart/ae.wav

    # Get file as numpy array.
  # Get file as numpy arra
fs, signal_x = wavfile.read('A96.wav')
signal_x = np.array(signal_x, dtype=float)
N=len(signal_x)
w=np.hamming(N);
x1=signal_x*w
x1=lfilter([1], [1., 0.63], x1)
A= lpc(x1,int(12))
rts = np.roots(A)
rts = [r for r in rts if np.imag(r) >= 0]
angz = np.arctan2(np.imag(rts), np.real(rts))
frqs = sorted(angz * (fs / (2 * math.pi)))
print(signal_x)
print(frqs)
#plt.show()