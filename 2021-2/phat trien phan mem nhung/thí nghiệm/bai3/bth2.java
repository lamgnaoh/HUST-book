import java.io.*;
import java.net.URL;
import java.net.HttpURLConnection;
import org.json.JSONObject;
import org.json.JSONArray;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;


public class bth2{
    /* Define Key API and URL of Thingspeak cloud */
    private static final String keyApi = "T7H40F0X82VGW7L5";
    private static final String thinkSpeakUrl = "https://api.thingspeak.com";

    public static String getDataFromChannel(String urlStr) {
        URL url;
        String line;
        int responseCode;
        BufferedReader reader;
        HttpURLConnection conn = null;
        StringBuilder responseBody = new StringBuilder();
        try {
            url = new URL(urlStr);
            conn = (HttpURLConnection) url.openConnection();
            conn.setRequestProperty("Content-Type", "application/json; charset=UTF-8");
            conn.setRequestProperty("Accept", "application/json");
            conn.setRequestMethod("GET");
            responseCode = conn.getResponseCode();
            System.out.println("GET Response Code : " + responseCode);
            if (responseCode == HttpURLConnection.HTTP_OK) {
                reader = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line = reader.readLine()) != null) {
                    responseBody.append(line);
                }
                reader.close();
                conn.disconnect();
                return responseBody.toString();
            } else {
                return "";
            }
        } catch (Exception e) {
            e.printStackTrace();
            return "";
        }
    }
    public static ArrayList<Object> parseData(String data) {
            try {
                ArrayList<Object> list = new ArrayList<Object>();
                JSONObject jsonObject = new JSONObject(data);
                JSONArray channel = jsonObject.getJSONArray("feeds");
                for (int i = 0; i < channel.length(); i++) {
                    JSONObject jo = channel.getJSONObject(i);
                    Map<String, String> m = new HashMap<String, String>();
                    if (!jo.isNull("field1")) {
                        String field1 = jo.getString("field1");
                        m.put("field1", field1);
                    }
                    if (!jo.isNull("field2")) {
                        String field2 = jo.getString("field2");
                        m.put("field2", field2);
                    }
                    list.add(m);
                }
    
                return list;
    
            } catch (Exception e) {
                e.printStackTrace();
                return null;
            }
        }
        public static void main(String args[]) {
            int numChannel = 1529099; /* Channel ID */
            int numRecord = 2; /* Number of record want to get */
            String urlStr = "https://api.thingspeak.com/channels/" + numChannel + "/feeds.json?results=" + numRecord;
            String data = getDataFromChannel(urlStr);
            System.out.println(data);
            System.out.println(parseData(data));
        }
    }

