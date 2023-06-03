from scipy.signal import tf2zpk
import numpy as np
import math
import matplotlib.pyplot as plt

F = 10000
Fk = 2500
T = 1 / F
ok = 100 #Nua dai thong
zk = math.exp(-ok*T)
Ok = 2*math.pi*Fk*T
b = [1, -2*zk*math.cos(Ok), math.pow(zk, 2)]
a = [1 - 2*zk*math.cos(Ok) + math.pow(zk, 2), 0, 0]

z, p, k = tf2zpk(b, a)

# print(z, p)
# Ve cac diem khong
x = [data.real for data in z]
y = [data.imag for data in z]

# plot the complex numbers
plt.scatter(x, y)
plt.ylabel('Imaginary')
plt.xlabel('Real')
plt.title('Zero Points')
plt.show()

# Ve cac diem cuc
x = [data.real for data in p]
y = [data.imag for data in p]

# plot the complex numbers
plt.scatter(x, y)
plt.ylabel('Imaginary')
plt.xlabel('Real')
plt.title('Zero Points')
plt.show()
