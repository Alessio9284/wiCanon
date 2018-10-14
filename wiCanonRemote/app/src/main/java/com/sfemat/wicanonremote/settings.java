package com.sfemat.wicanonremote;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class settings extends AppCompatActivity {
    EditText e;
    Button b;
    SharedPreferences sharedPref;
    SharedPreferences.Editor editor;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
        b = (Button)findViewById(R.id.button);
        e  = (EditText)findViewById(R.id.editText);
        //Inserisci l'ip memorizzato nel campo
        try{
            e.setText(sharedPref.getString("ipp",""));
        }
        catch (Exception e){

        }

        //Alla pressione del pulsante imposta l'IP inserito nel campo di testo e lo salva per i prossimi avvii dell'app
        b.setOnClickListener(
                new View.OnClickListener()
                {
                    public void onClick(View view)
                    {
                        ClientThread.SERVER_IP = e.getText().toString();
                        Intent intent = new Intent(settings.this, Remote.class);

                        sharedPref= getSharedPreferences("ip", Context.MODE_PRIVATE);
                        editor=sharedPref.edit();
                        editor.putString("ipp", e.getText().toString());
                        editor.commit();
                        startActivity(intent);
                    }
                });
    }
}
