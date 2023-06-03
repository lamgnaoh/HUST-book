import numpy as np
import matplotlib.pyplot as plt
import math

f1 = 300
f2 = 350
fs = 1500
n = np.arange(0,256)
pi = math.pi

xn =np.sin((2*pi*f1*n)/fs) + np.sin((2*pi*f2*n)/fs) 

plt.plot(range(256),xn)
plt.xlabel('n')
plt.ylabel('x(n)')
plt.xlim([0, 255])
plt.xlabel('Time in ms')
wh=2* np.hamming(255)
plt.plot(wh)
plt.show()


