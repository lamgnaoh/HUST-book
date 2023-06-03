# importing libraries
import numpy as np
import matplotlib.pyplot as plt
# Function to generate exponential signals e**(at)
def exponential(a, n):
    expo =[]
    for sample in n:
        expo.append(np.exp(a * sample))
    return (expo)
         
a = 1
UL = 1
LL = -1
n = np.arange(LL, UL, 1)
x = exponential(a, n)
plt.stem(n, x)
plt.xlabel('n')
plt.xticks(np.arange(LL, UL, 2))
# plt.yticks([0, UL, 1])
plt.ylabel('x[n]')
plt.title('Exponential Signal e**(an)')
plt.savefig("Exponential.png")