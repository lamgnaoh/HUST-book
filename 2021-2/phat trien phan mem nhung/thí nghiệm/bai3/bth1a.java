import java.io.*;
import java.net.URL;
import java.net.HttpURLConnection;


public class bth1a{
    private static final String keyApi = "T7H40F0X82VGW7L5";
    private static final String thinkSpeakUrl = "https://api.thingspeak.com";


    public static void updateDataUsingUrl(String urlStr){
        URL url;
        String line;
        int responseCode;
        BufferedReader reader;
        HttpURLConnection conn = null;
        StringBuilder responseBody = new StringBuilder();
        try {
            url = new URL(urlStr);
            conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");

            /* Get response code from connection */
            responseCode = conn.getResponseCode();
            System.out.println("GET Response Code : " + responseCode);
            if (responseCode == HttpURLConnection.HTTP_OK) {
                reader = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line = reader.readLine()) != null) {
                    responseBody.append(line);
                }
                System.out.println(responseBody.toString());
                reader.close();
            }
            conn.disconnect();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    public static void main(String[] args) {
        int field1 = 15;
        int field2 = 01;
       //TODO: Hoàn thiện url của request
        String url = thinkSpeakUrl + "/update?api_key=" + keyApi + "&field1=" + field1 + "&field2=" + field2;
        updateDataUsingUrl(url);
    }
}