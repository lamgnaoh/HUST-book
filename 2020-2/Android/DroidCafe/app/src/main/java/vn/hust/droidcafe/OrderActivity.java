package vn.hust.droidcafe;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.RadioButton;
import android.widget.Toast;

public class OrderActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_order);
    }

    public void onRadioButtonClicked(View view) {
        boolean checked = ((RadioButton) view).isChecked();
        // Check which radio button was clicked.
        switch (view.getId()) {
            case R.id.sameday:
                if (checked)
                    // Same day service
                    displayToast("Same day messenger service");
                break;
            case R.id.nextday:
                if (checked)
                    // Next day delivery
                    displayToast("Next day ground delivery");
                break;
            case R.id.pickup:
                if (checked)
                    // Pick up
                    displayToast("onRadioButtonClicked");
                break;
            default:
                // Do nothing.
                break;
        }
    }
    public void displayToast(String message){
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show();
    }

}