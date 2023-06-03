package vn.hust.activityandintent;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class CalculateAvtivity extends AppCompatActivity {
    TextView textView1;
    TextView textView2;
    TextView textView3;
    int param1 , param2 , res ;
    Button finishBtn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_calculate_avtivity);

        Intent intent = getIntent();
        param1 = intent.getIntExtra("param1",0);
        param2 = intent.getIntExtra("param2",0);
        res = param1 + param2 ;
        textView1 = findViewById(R.id.param1);
        textView2 = findViewById(R.id.param2);
        textView3 = findViewById(R.id.param3);
        textView1.setText("Value 1 : " + param1);
        textView2.setText("Value 2 : " + param2);
        textView3.setText("Result : " + res );
        Intent reply = new Intent(CalculateAvtivity.this,MainActivity.class);
        reply.putExtra("Result", res);
//        set activity result to OK
        setResult(Activity.RESULT_OK,reply);

        finishBtn = findViewById(R.id.btn_finish);
        finishBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                finish();
            }
        });

    }
}