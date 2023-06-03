import numpy as np 
import matplotlib.pyplot as plt
import math
from scipy import signal

xn = []
for i in range(-5, 9):
    if i > -1:
        xn.append(math.pow(0.8, i))
    else:
        xn.append(0)
    
hn = []
for i in range(-5, 9):
    if i > -1: hn.append(1)

    else: hn.append(0)

fig, ax = plt.subplots(3)
x = range(-5, 9)
ax[0].stem(x,xn, 'r', ) 


ax[1].stem(x,hn, 'r', )

cn = []
for i in range(-5, 9):
  if i > -1:
    cn.append((1 - math.pow(0.8, i + 1)) / 0.2) 
  else: 
    cn.append(0)

# print(xn)
# print(hn)
# print(cn)
ax[2].stem(x, cn, 'r',)
plt.show()