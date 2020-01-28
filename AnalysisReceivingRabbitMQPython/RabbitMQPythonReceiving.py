import pika

print('started...')

def callback(ch, method, properties, body):
   print(" [x] %r" % body)
 
	
if __name__ == '__main__':

   credentials = pika.PlainCredentials('ferasg', '123456')
   parameters = pika.ConnectionParameters(credentials=credentials,host='192.168.43.215') 
   
   connection = pika.BlockingConnection(parameters)
   channel = connection.channel()

   channel.exchange_declare(exchange='trackWithStitchedBeamData', exchange_type='fanout', durable=True)

   result = channel.queue_declare(queue='', exclusive=True)
   queue_name = result.method.queue

   channel.queue_bind(exchange='trackWithStitchedBeamData', queue=queue_name)

   print(' [*] Waiting for logs. To exit press CTRL+C')
   channel.basic_consume(queue = queue_name, on_message_callback = callback, auto_ack=True)

   channel.start_consuming()
