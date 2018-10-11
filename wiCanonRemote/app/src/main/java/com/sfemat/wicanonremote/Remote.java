package com.sfemat.wicanonremote;

import android.app.Activity;
import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;

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
        Remote.opzione = temp.getText().toString();
        System.out.println(Remote.opzione);
        new ClientThread().run();
    }
}

class ClientThread implements Runnable
{
    public static final String SERVER_IP = "192.168.1.1"; // 192.168.1.116
    public static final int SERVER_PORT = 1027;
    private Socket socket;

    @Override
    public void run()
    {
        try
        {
            socket = new Socket(SERVER_IP, SERVER_PORT);

            DataOutputStream out = new DataOutputStream(socket.getOutputStream());
            out.writeChars(Remote.opzione);
            out.flush();
            socket.close();

        }
        catch (UnknownHostException e)
        {
            e.printStackTrace();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }
}