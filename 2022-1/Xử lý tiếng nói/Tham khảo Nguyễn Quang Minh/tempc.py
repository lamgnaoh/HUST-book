from msilib.sequence import tables
import scipy.signal as sc

a=[1,-0.3,0.2,-0.1,0.05]
b=[1]
x=[ 2.5,     0.75,   -0.5,    -0.875,  -0.625,   2.3125,  0.875 ]
t=sc.lfilter(b,a,x)
print(t)