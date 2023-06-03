package vn.hust.contextualmenuexample;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.ContextMenu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {
    ImageView imageView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        imageView = findViewById(R.id.image_context);
//        dang ki context menu cho image view
        registerForContextMenu(imageView);
        ListView listView = findViewById(R.id.list_view);
        List<String> items = new ArrayList<>();
        for(int i = 0; i< 50 ; i++){
            items.add("Item " + i) ;
        }

//        khoi tao doi tuong adapter de quy dinh cach hien thi du lieu trong list
        ArrayAdapter<String> adapter = new ArrayAdapter<>(MainActivity.this, android.R.layout.simple_list_item_1,items);
        listView.setAdapter(adapter);
        registerForContextMenu(listView);

    }

    @Override
    public void onCreateContextMenu(ContextMenu menu, View v, ContextMenu.ContextMenuInfo menuInfo) {
//        view v: đối tượng đăng kí context menu
        super.onCreateContextMenu(menu, v, menuInfo);
        getMenuInflater().inflate(R.menu.context_menu,menu);
    }

    @Override
    public boolean onContextItemSelected(@NonNull MenuItem item) {
        int itemId = item.getItemId();
        AdapterView.AdapterContextMenuInfo menuInfo = (AdapterView.AdapterContextMenuInfo) item.getMenuInfo();
        int position = menuInfo.position;

        switch (itemId){
            case R.id.action_edit:
                Log.v("Tag" , "action edit " + position);
                break;
            case R.id.action_delete:
                Log.v("Tag" , "action delete " + position);
                break;
            case R.id.action_download:
                Log.v("Tag" , "action download " + position);
                break;
        }

        return super.onContextItemSelected(item);
    }
}