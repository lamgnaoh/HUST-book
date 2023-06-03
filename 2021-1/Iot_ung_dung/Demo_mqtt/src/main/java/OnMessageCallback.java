import org.eclipse.paho.client.mqttv3.IMqttDeliveryToken;
import org.eclipse.paho.client.mqttv3.MqttCallback;
import org.eclipse.paho.client.mqttv3.MqttMessage;

public class OnMessageCallback implements MqttCallback {
    // ngat ket noi
    @Override
    public void connectionLost(Throwable throwable) {
        // co the code sao cho ket noi thu lai 3 lan .neu sau 3 lan khong reconnect duoc thi lam gi
        System.out.println("disconnect , you can reconnect");
    }
    // khi nhan duoc du lieu :
    @Override
    public void messageArrived(String topic, MqttMessage mqttMessage) throws Exception {
        System.out.println("Received message topic :" + topic);
        System.out.println("received message QoS :"+ mqttMessage.getQos());
        System.out.println("Received message content : " + new String(mqttMessage.getPayload()));
    }
    // khi gui thanh cong du lieu :
    @Override
    public void deliveryComplete(IMqttDeliveryToken iMqttDeliveryToken) {
        System.out.println("delivery completed : " + iMqttDeliveryToken.isComplete());
    }
}
