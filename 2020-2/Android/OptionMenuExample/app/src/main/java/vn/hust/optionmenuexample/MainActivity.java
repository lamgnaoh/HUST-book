package vn.hust.optionmenuexample;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.main_menu,menu);
        return super.onCreateOptionsMenu(menu);
    }
//    xử lý sự kiện khi người dùng bấm vào các menu item

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
//        lấy ra id của đối tượng menu selected
        int itemId = item.getItemId();
        switch (itemId){
            case R.id.action_download:
                Log.v("TAG","action download");
                break;
            case R.id.action_favorites:
                Log.v("TAG","action favorite");break;
            case R.id.action_setting:
                Log.v("TAG","action setting");
                break;

        }

        return super.onOptionsItemSelected(item);
    }
}