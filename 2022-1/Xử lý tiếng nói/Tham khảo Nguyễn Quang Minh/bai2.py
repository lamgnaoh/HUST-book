import math
import matplotlib.pyplot as plt

# Cau a
def Xn(n):
    A = 5
    F = 50
    Fs = 500
    return A*math.sin(2 * math.pi * F * n / Fs)

n = list(range(0, 50))
xn = [Xn(i) for i in n]

plt.stem(n, xn)
plt.xlabel('n')
plt.ylabel('x(n)')
plt.title(r'Plot of CT signal x(n) = Asin(2piFn/Fs)')
plt.xlim([1, 50])
plt.show()

# Cau b
plt.magnitude_spectrum(xn, color='green')
plt.title(r'Plot Magnitude Spectrum')
plt.show()