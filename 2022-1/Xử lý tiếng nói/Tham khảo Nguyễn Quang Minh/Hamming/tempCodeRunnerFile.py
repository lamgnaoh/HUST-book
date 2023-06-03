import numpy as np
import matplotlib.pyplot as plt
import math

f1 = 300
f2 = 350
fs = 1500
n = np.arange(0,256)
pi = math.pi

xn = np.sin((2*pi*f1*n)/fs) + np.sin((2*pi*f2*n)/fs)
res=xn*np.hamming(256)
plt.plot(range(256), res)
plt.xlabel('Time in ms')
plt.xlim([0, 255])
wh=2* np.hamming(256)
plt.plot(wh)
plt.show()
