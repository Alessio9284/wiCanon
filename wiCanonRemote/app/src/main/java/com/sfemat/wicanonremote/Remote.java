package com.sfemat.wicanonremote;

import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class Remote extends AppCompatActivity
{
    public static String opzione;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_remote);
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);
    }

    public void onClick(View v)
    {
        Button temp = (Button) v.findViewById(v.getId());
        //Memorizza il carattere del pulsante premuto
        Remote.opzione = temp.getText().toString();
        System.out.println(Remote.opzione);
        //Crea e avvia un thread
        new ClientThread().run();
    }
}

