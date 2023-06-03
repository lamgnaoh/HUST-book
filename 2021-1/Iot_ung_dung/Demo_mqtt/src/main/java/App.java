import org.eclipse.paho.client.mqttv3.MqttClient;
import org.eclipse.paho.client.mqttv3.MqttConnectOptions;
import org.eclipse.paho.client.mqttv3.MqttException;
import org.eclipse.paho.client.mqttv3.MqttMessage;
import org.eclipse.paho.client.mqttv3.persist.MemoryPersistence;
import java.nio.charset.StandardCharsets;

public class App {
    public static void main(String[] args) {
        //subcribe topic
        String subTopic = "/ktmt/iot" ;
        //publish topic
        String pubTopic = "/ktmt/pub";
        String content  = "Hello World";
        // qos cua pub , sub
        int qos = 2;
        // Dia chi broker hivemq
        String broker = "tcp://broker.hivemq.com:1883";

        String clientId = "client1";
        MemoryPersistence persistence = new MemoryPersistence();
        try {
            // tao mot client
            MqttClient client = new MqttClient(broker,clientId,persistence);
            // tao 1 option cho ket noi :
            MqttConnectOptions connOpts = new MqttConnectOptions();
            /*
            * thiết lập phiên liên tục hay không . nếu có (true) broker không lưu trũ bất cứ cái gì cho khách hàng và
            * xóa tất cả các thông tin từ bất kì phiên liên tục nào trước đó
            * nếu không (false) , broker lưu trữ tất cả các subcribe của client và các message bị bỏ lỡ cho client
            * đã đăng kí với qos 1,2
            * */
            connOpts.setCleanSession(true);
            /*
            * co che bat dong bo : ben gui cu gui, ben nhan se nhan duoc du lieu sau moi lan subcribe topic gui du lieu
            * nen can 1 ham callback xu li du lieu ma client nhan duoc sau moi lan subcribe do
            *
            * */
            client.setCallback(new OnMessageCallback());
            System.out.println("connecting to the broker : " + broker);
            //ket noi den broker
            client.connect(connOpts);
            System.out.println("Publishing message: " + content);
            //subcribe (lang nghe) tren topic /ktmt/iot
            client.subscribe(subTopic);
            // tao mot doi tuong du lieu de gui di (publish)
            MqttMessage message = new MqttMessage(content.getBytes());
            message.setQos(qos);
            // publish(gui) du lieu tren topic /ktmt/pub
            client.publish(pubTopic,message);
            System.out.println("Message published");
            client.disconnect();
            System.out.println("disconnected");
            client.close();

            System.exit(0);
        } catch (MqttException e) {
            e.printStackTrace();
        }

    }
}
