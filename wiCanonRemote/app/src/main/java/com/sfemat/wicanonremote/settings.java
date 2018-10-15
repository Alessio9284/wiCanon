package com.sfemat.wicanonremote;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
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
            SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
            e.setText(preferences.getString("ip", ""));
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

                        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(settings.this);
                        SharedPreferences.Editor editor = preferences.edit();
                        editor.putString("ip",ClientThread.SERVER_IP);
                        editor.apply();

                        Intent intent = new Intent(settings.this, Remote.class);
                        startActivity(intent);
                    }
                });
    }
}
