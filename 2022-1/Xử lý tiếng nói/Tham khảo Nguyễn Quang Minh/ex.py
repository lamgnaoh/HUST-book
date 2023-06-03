import numpy, scipy, matplotlib.pyplot as plt, IPython.display as ipd
import librosa, librosa.display
import matplotlib.pyplot as plt
import numpy as np
import wave
import sys

spf = wave.open("Xe.wav", "r")
audio11, sr = librosa.load("Xe.wav")
# gets the frame rate
FRAME_SIZE = 1024
HOP_LENGTH = 512
zcr_audio = librosa.feature.zero_crossing_rate(audio11, frame_length=FRAME_SIZE, hop_length=HOP_LENGTH)[0]
frames = range(len(zcr_audio))
t =librosa.frames_to_time(frames, hop_length=HOP_LENGTH)

# Extract Raw Audio from Wav File
signal = spf.readframes(-1)
signal = np.fromstring(signal, "int16")
fs = spf.getframerate()

# If Stereo
if spf.getnchannels() == 2:
    print("Just mono files")
    sys.exit(0)

Time = np.linspace(0, len(signal) / fs, num=len(signal))
# plt.figure(figsize=(10, 5))
# plt.title("Signal of Xe.wav...")
# plt.xlabel('Time(s)')
# plt.plot(Time, signal)
# plt.show()

plt.rcParams['figure.figsize'] = (20, 10)
plt.subplot(411)

plt.title("xe.wav")
plt.plot(Time, signal)

plt.subplot(412)
plt.specgram(signal, Fs=fs, cmap="gray")
plt.ylabel("Frequency(Hz)")
plt.xlabel("Time(s)")

plt.subplot(413)
plt.psd(signal, 256, fs)
plt.ylabel("Power")
plt.xlabel("Time(s)")

plt.subplot(414)
plt.plot(t, zcr_audio, color="y")
plt.xlabel("Time(s)")
plt.ylim(0, 1)
plt.show()