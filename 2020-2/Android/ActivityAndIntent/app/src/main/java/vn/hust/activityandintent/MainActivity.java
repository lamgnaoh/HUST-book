package vn.hust.activityandintent;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {
    EditText edtText1;
    EditText edtText2;
    Button calculateBtn;
    TextView resultText;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        edtText1 = findViewById(R.id.edit_text_1);
        edtText2 = findViewById(R.id.edit_text_2);
        calculateBtn = findViewById(R.id.cal_btn);
        calculateBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this,CalculateAvtivity.class);
                intent.putExtra("param1",Integer.parseInt(edtText1.getText().toString()));
                intent.putExtra("param2",Integer.parseInt(edtText2.getText().toString()));
                startActivityForResult(intent,123);
            }
        });

    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(requestCode ==  123){
            if(resultCode == Activity.RESULT_OK){
                int result = data.getIntExtra("Result",0);
                resultText = findViewById(R.id.result_text_view);
                resultText.setText(String.valueOf(result));
            }
        }
    }
}