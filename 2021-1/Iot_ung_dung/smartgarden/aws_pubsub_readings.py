from AWSIoTPythonSDK.MQTTLib import AWSIoTMQTTClient
import serial
#from rpi_lcd import LCD
from time import sleep

# Get serial to fetch data from arduino
ser = serial.Serial('/dev/ttyUSB0', 9600)
#lcd = LCD()

def customCallback(client, userdata, message):
	print("Received a new message: ")
	print(message.payload)
	print("from topic: ")
	print(message.topic)
	print("--------------\n\n")

host = "a290uc2ksy4m1j-ats.iot.us-west-2.amazonaws.com"

rootCAPath = "/home/pi/Templates/Project/smartgarden/rootca.pem"
certificatePath = "/home/pi/Templates/Project/smartgarden/certificate.pem.crt"
privateKeyPath = "/home/pi/Templates/Project/smartgarden/private.pem.key"

my_rpi = AWSIoTMQTTClient("basicPubSub")
my_rpi.configureEndpoint(host, 8883)
my_rpi.configureCredentials(rootCAPath, privateKeyPath, certificatePath)

my_rpi.configureOfflinePublishQueueing(-1)  # Infinite offline Publish queueing
my_rpi.configureDrainingFrequency(2)  # Draining: 2 Hz
my_rpi.configureConnectDisconnectTimeout(10)  # 10 sec
my_rpi.configureMQTTOperationTimeout(5)  # 5 sec

# Connect and subscribe to AWS IoT
my_rpi.connect()
my_rpi.subscribe("smartgarden/readings", 1, customCallback)

sleep(2)


# Publish to the same topic in a loop forever
loopCount = 0
while True:
	# lấy dữ liệu từ arduino
	temp = float(ser.readline())
	hum = float(ser.readline())
	soil = int(ser.readline())







	sleep(2)
	loopCount = loopCount+1
	message = {}
	message["id"] = "id_smartgarden"
	import datetime as datetime
	now = datetime.datetime.now()
	message["datetimeid"] = now.isoformat()
	message["temperature"] = temp
	message["humidity"] = hum
	message["moisture"] = soil

	import json
	# chuyển dữ liệu từ object về dạng json và publish đến aws iot
	my_rpi.publish("smartgarden/readings", json.dumps(message), 1)
