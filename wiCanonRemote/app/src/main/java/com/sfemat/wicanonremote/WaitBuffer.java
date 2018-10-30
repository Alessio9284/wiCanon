package com.sfemat.wicanonremote;

import android.os.AsyncTask;

public class WaitBuffer extends AsyncTask<Void, Integer, Void> {
    @Override
    protected Void doInBackground(Void... voids) {
        try {
            synchronized(this){
                this.wait(2000);
                //Crea e avvia un nuovo thread
                new ClientThread().run();
                }
        } catch(InterruptedException ex){

        }
        return null;
    }
}
