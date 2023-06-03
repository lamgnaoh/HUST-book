import math
import numpy as np
import matplotlib.pyplot as plt

def divComplex( z1, z2):
  return z1 / z2

pi = math.pi
n = np.arange(0, 2*pi+0.1, pi/12)
hz = []
for i in range(len(n)):
  comp1 = complex(0.87, 0)
  comp2 = complex(math.cos(n[i]), math.sin(n[i]))
  tmp = divComplex(comp1, comp2)
  a = complex(1,0)
  b = complex(1 - tmp.real, tmp.imag)
  res = divComplex(a, b)
  hz.append(res)

plt.plot(n, hz)
plt.axis([0, 2*pi+0.1, 0, 8])
plt.show()

pi = math.pi
n = np.arange(0, 2*pi+0.1, pi/12)
hz = []
for i in range(len(n)):
  temp = math.sqrt((1-math.cos(n[i]))**2 + (math.sin(n[i]))**2)
  hz.append(temp)
plt.plot(n, hz)
plt.axis([0, 2*pi+0.1, 0, 2])
plt.show()