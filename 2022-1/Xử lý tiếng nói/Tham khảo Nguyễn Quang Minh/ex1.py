# importing libraries
import numpy as np
import matplotlib.pyplot as plt



# Function to plot Impulse signal d(a)
def unit_impulse(a, n):
	delta =[]
	for sample in n:
		if sample == a:
			delta.append(1)
		else:
			delta.append(0)
			
	return delta

a = 0 # Enter delay or advance
UL = 5
LL = -5
n = np.arange(LL, UL, 1)
d = unit_impulse(a, n)
plt.stem(n, d)
plt.xlabel('n')
plt.xticks(np.arange(LL, UL, 1))
plt.yticks([0, 1])
plt.ylabel('d[n]')
plt.title('Unit sequence')
plt.savefig("UnitSequence.png")



