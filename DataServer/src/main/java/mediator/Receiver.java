package mediator;

import com.rabbitmq.client.*;

import java.nio.charset.StandardCharsets;

public class Receiver
{
	private static RequestHandler handler;
	private static final String RPC_QUEUE_NAME = "data_queue";
	
	public static void main(String[] argv) throws Exception
	{
		handler = new RequestHandler();
		
		ConnectionFactory factory = new ConnectionFactory();
		factory.setHost("localhost");
		
		try (Connection connection = factory.newConnection();
			 Channel channel = connection.createChannel())
		{
			channel.queueDeclare(RPC_QUEUE_NAME, false, false, false, null);
			channel.queuePurge(RPC_QUEUE_NAME);
			
			channel.basicQos(10);
			
			System.out.println(" [x] Awaiting data requests");
			
			Object monitor = new Object();
			DeliverCallback deliverCallback = (consumerTag, delivery) ->
			{
				AMQP.BasicProperties replyProps = new AMQP.BasicProperties.Builder()
						.correlationId(delivery.getProperties().getCorrelationId())
						.build();
				
				String response = "";
				
				try
				{
					String message = new String(delivery.getBody(), StandardCharsets.UTF_8);
					System.out.println("request==> " + message);
					response = handler.filterRequest(message);
					System.out.println("response==> " + response);
				}
				catch (RuntimeException e)
				{
					System.out.println(" [.] " + e);
				}
				finally
				{
					channel.basicPublish("", delivery.getProperties().getReplyTo(), replyProps, response.getBytes("UTF-8"));
					channel.basicAck(delivery.getEnvelope().getDeliveryTag(), false);
					// RabbitMq consumer worker thread notifies the RPC server owner thread
					synchronized (monitor)
					{
						monitor.notify();
					}
				}
			};
			
			channel.basicConsume(RPC_QUEUE_NAME, false, deliverCallback, (consumerTag -> {}));
			// Wait and be prepared to consume the message from RPC client.
			while (true)
			{
				synchronized (monitor)
				{
					try
					{
						monitor.wait();
					}
					catch (InterruptedException e)
					{
						e.printStackTrace();
					}
				}
			}
		}
	}
}
