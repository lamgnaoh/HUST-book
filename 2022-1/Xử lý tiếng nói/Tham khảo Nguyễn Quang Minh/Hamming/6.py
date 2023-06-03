from cProfile import label
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

N=256
fre=np.linspace(0,fs/2,N//2)
x=np.fft.fft(xn,N)
print(x)
max_x=np.max(np.abs(x))
xdb=20*np.log10(np.abs(x)/max_x)
plt.plot(fre,xdb[0:128],color='b', label='Without Hamming')
y=np.fft.fft(res,N)
max_y=np.max(np.abs(y))
ydb=20*np.log10(np.abs(y)/max_y)
plt.plot(fre,ydb[0:128],color='r', label='With Hamming')
plt.ylabel('Amplitude in db')
plt.xlabel('Frequency in Hz')
plt.legend(loc='best')
plt.show()