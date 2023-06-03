import matplotlib.pyplot as plt
import numpy as np
import wave
import sys


spf = wave.open("A96.wav", "r")

# Extract Raw Audio from Wav File
signal = spf.readframes(-1)
signal = np.fromstring(signal, "Int16")
fs = spf.getframerate()

# If Stereo
if spf.getnchannels() == 2:
    print("Just mono files")
    sys.exit(0)


Time = np.linspace(0, len(signal) / fs, num=len(signal))

plt.figure(1)
plt.title("Xe")


     
    # label of x-axis
plt.xlabel("Time(s)")
plt.plot(Time, signal)
plt.show()