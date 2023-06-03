import boto3
from boto3.dynamodb.conditions import Key, Attr
import datetime as dt
from datetime import date

def login():
	try:
		# kết nối db 
		dynamodb = boto3.resource('dynamodb', region_name='us-west-2')
		table = dynamodb.Table('SmartGarden_login')
		# trả về 1 http 200 response chứa các items
		response = table.scan()
		# lấy ra các items trong bảng theo dạng json
		items = response['Items']

		return items
	except:
		import sys
		print(sys.exc_info()[0])
		print(sys.exc_info()[1])

def get_data():
	try:
		dynamodb = boto3.resource('dynamodb', region_name='us-west-2')
		table = dynamodb.Table('SmartGarden_readings')
		# format date theo kiểu yyyy-mm-dd
		startdate = date.today().isoformat()
		# trả về 1 http 200 response chứa các item thỏa mãn thời gian trong startdate bằng với thời gian trong db
		response = table.query(KeyConditionExpression=Key('id').eq('id_smartgarden') & Key('datetimeid').begins_with(startdate),
				ScanIndexForward=False
		)
		# lấy ra các items trong bảng dưới dạng json
		items = response['Items']

		n=1 # get latest data
		data = items[:n]
		print(data)
		return data
	except:
		import sys
		print(sys.exc_info()[0])
		print(sys.exc_info()[1])

def get_chart_data():
	try:

		dynamodb = boto3.resource('dynamodb', region_name='us-west-2')
		table = dynamodb.Table('SmartGarden_readings')

		startdate = date.today().isoformat()
		response = table.query(KeyConditionExpression=Key('id').eq('id_smartgarden') & Key('datetimeid').begins_with(startdate),
				ScanIndexForward=False
		)
		# trả về 1 mảng các object 
		items = response['Items']

		n=15 # limit to last 15 items
		# lấy các item từ 0 -> 14
		data = items[:n]
		# đảo chiều các item
		data_reversed = data[::-1]
		return data_reversed
	except:
		import sys
		print(sys.exc_info()[0])
		print(sys.exc_info()[1])

def get_status():
	try:
		dynamodb = boto3.resource('dynamodb', region_name='us-west-2')
		table = dynamodb.Table('SmartGarden_status')

		startdate = date.today().isoformat()
		response = table.query(KeyConditionExpression=Key('id').eq('id_status') & Key('datetimeid').begins_with(startdate),
				ScanIndexForward=False
		)

		items = response['Items']

		n=1
		data = items[:n]
		return data
	except:
		import sys
		print(sys.exc_info()[0])
		print(sys.exc_info()[1])

def send_status(status):
	try:
		# print("status", status)
		dynamodb = boto3.resource('dynamodb', region_name='us-west-2')
		table = dynamodb.Table('SmartGarden_status')

		now = dt.datetime.now()
		new_item = {
			"id": "id_status",
			'datetimeid': now.isoformat(),
			'status': status
		}
		table.put_item(Item = new_item)

	except:
		import sys
		print(sys.exc_info()[0])
		print(sys.exc_info()[1])
