# # importing libraries
# import numpy as np
# import matplotlib.pyplot as plt
# # Function to generate exponential signals e**(at)
# def exponential(a, n):
#     expo =[]
#     for sample in n:
#         expo.append( np.sin(a * sample))
#     return (expo)
         
# a = 2
# UL = 1
# LL = -1
# n = np.arange(LL, UL, 0.1)
# x = exponential(a, n)
# plt.stem(n, x)
# plt.set_xlabel('n')
# plt.set_xticks(np.arange(LL, UL, 0.2))
# # plt.set_yticks([0, UL, 1])
# plt.set_ylabel('x[n]')
# plt.title('Sin Signal e**(an)')
# plt.savefig("Sin.png")
import numpy as np

import matplotlib.pyplot as plt

# n = np.arange(-10,10,1);

# dt = 0.1/10

# x = np.sin(2 * np.pi * 10 * n * dt)

# plt.set_xlabel('n');

# plt.set_ylabel('x[n]');

# plt.stem(n, x);

# plt.savefig("Sin.png")


# Function to plot Impulse signal d(a)
def unit_impulse(a, n):
	delta =[]
	for sample in n:
		if sample == a:
			delta.append(1)
		else:
			delta.append(0)
			
	return delta

def unit_step(a, n):
    unit =[]
    for sample in n:
        if sample<a:
            unit.append(0)
        else:
            unit.append(1)
    return(unit)

def exponential(a, n):
    expo =[]
    for sample in n:
        expo.append(1 / np.exp(a * sample))
    return (expo)

fig, ax = plt.subplots(2, 2)

a = 0 # Enter delay or advance
UL = 5
LL = -5
n = np.arange(LL, UL, 1)
d = unit_impulse(a, n)
ax[0][0].stem(n, d)
ax[0][0].set_xlabel('n')
ax[0][0].set_xticks(np.arange(LL, UL, 1))
ax[0][0].set_yticks([0, 1])
ax[0][0].set_ylabel('d[n]')
# ax[0][0].title('Unit sequence')
# ax[][].savefig("UnitSequence.png")

# plot unit step function u[n-a]
a = 2 # Enter delay or advance
UL = 10
LL = -10
n = np.arange(LL, UL, 1)
unit = unit_step(a, n)
ax[0][1].stem(n, unit)
ax[0][1].set_xlabel('n')
ax[0][1].set_xticks(np.arange(LL, UL, 1))
ax[0][1].set_yticks([0, 1])
ax[0][1].set_ylabel('u[n]')
# ax[0][1].title('Unit step u[n-a]')

a = 2
UL = 1
LL = -1
n = np.arange(LL, UL, 0.1)
x = exponential(a, n)
ax[1][0].stem(n, x)
ax[1][0].set_xlabel('n')
ax[1][0].set_xticks(np.arange(LL, UL, 0.2))
# ax[][].set_yticks([0, UL, 1])
ax[1][0].set_ylabel('x[n]')
# ax[1][0].title('Exponential Signal e**(an)')

n = np.arange(-10,10,1);

dt = 0.1/10

x = np.sin(2 * np.pi * 10 * n * dt)

ax[1][1].set_xlabel('n');

ax[1][1].set_ylabel('x[n]');

ax[1][1].stem(n, x);

plt.show()