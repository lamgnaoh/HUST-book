import matplotlib.pyplot as plt
import numpy as np
from opensoundscape.audio import Audio
from opensoundscape.spectrogram import Spectrogram

audio_object = Audio.from_file("Xe.wav")
spectrogram_object = Spectrogram.from_audio(audio_object)
spectrogram_object.plot()