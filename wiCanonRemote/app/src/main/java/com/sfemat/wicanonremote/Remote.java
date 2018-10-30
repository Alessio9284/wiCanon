package com.sfemat.wicanonremote;

import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

public class Remote extends AppCompatActivity
{
    public static String opzione;
    private WaitBuffer wait;
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_remote);
        //Imposta stringa vuota altrimenti aggiungeva il numero del canale a null (es. null6)
        Remote.opzione = "";
    }

    /*
        Funzione richiamata da qualsiasi pulsante premuto

     */
    public void onClick(View v) throws InterruptedException {
        Button temp = (Button) v.findViewById(v.getId());
        //Aggiunge il carattere del pulsante premuto ad una stringa utilizzata come buffer
        Remote.opzione += temp.getText().toString();

        //Crea una notifica toast che mostri il tasto/i premuto/i
        Toast.makeText(getApplicationContext(), Remote.opzione, Toast.LENGTH_SHORT).show();
        //Aspetta 3 secondi nel caso si voglia aggiungere un altro carattere da spedire
        if(wait!=null)
            wait.cancel(true);
        wait = new WaitBuffer();
        synchronized(wait){
            wait.execute();
        }

    }
}

