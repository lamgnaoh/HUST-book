import socket

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
HOST = '192.168.16.100'  # ip of raspberry pi
PORT = 12345
print('Socket created')

try:
    s.bind((HOST, PORT))
except socket.error:
    print('Bind failed')

s.listen(1)
print('Socket awaiting messages')
(conn, addr) = s.accept()
print('Connected')
while True:
    data = conn.recv(1024)
    print('I sent a message back in response to: ' + data)

