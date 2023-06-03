import socket

HOST = '192.168.16.119'
PORT = 12345
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((HOST, PORT))

s.send('match'.encode())
reply = s.recv(1024)
if reply=='Terminate':
    pass
print(reply)