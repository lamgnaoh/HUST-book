import com.rabbitmq.client.AMQP;
import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import java.io.IOException;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.TimeoutException;
public class RPCClient implements AutoCloseable {
    private Connection connection;
    private Channel channel;
    private String requestQueueName = "rpc_queue"; // ten queue
    public RPCClient() throws IOException,TimeoutException{
        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost"); // ket noi den broker may local.neu muon ket noi broker tren may khac thi thau bang dia
        // chi ip may do
        connection = factory.newConnection();
        channel = connection.createChannel();
    }
    @Override
    public void close() throws Exception {
        connection.close();
    }
    public String call(String message) throws IOException,InterruptedException {
        final String corrId = UUID.randomUUID().toString();
        String replyQueueName = channel.queueDeclare().getQueue();
        AMQP.BasicProperties props = new AMQP.BasicProperties
                .Builder()
                .correlationId(corrId)
                .replyTo(replyQueueName)
                .build();
        channel.basicPublish("", requestQueueName, props,
                message.getBytes("UTF-8"));
        final BlockingQueue<String> response = new
                ArrayBlockingQueue<>(1);
        String ctag = channel.basicConsume(replyQueueName, true,
                (consumerTag, delivery) -> {
                    if
                    (delivery.getProperties().getCorrelationId().equals(corrId)) {
                        response.offer(new String(delivery.getBody(), "UTF-8"));
                    }
                }, consumerTag -> {
                });
        String result = response.take();
        channel.basicCancel(ctag);
        return result;
    }

    public static void main(String[] args) {
        try(RPCClient fibonacciRpc = new RPCClient()){
            for (int i = 0; i < 32; i++) {
                String i_str = Integer.toString(i);
                System.out.println(" [x] Requesting fib(" + i_str +
                        ")");
                String response = fibonacciRpc.call(i_str);
                System.out.println(" [.] Got '" + response + "'");
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (TimeoutException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

