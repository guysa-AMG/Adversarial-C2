import socket as sk


client = sk.socket(sk.AF_INET,sk.SOCK_STREAM)

client.connect(("127.0.0.1",8888))

while True:
    client.send("/".encode())

    rea = input("cmd: ")
    client.send(rea.encode())
    print(client.recv(1024).decode())
