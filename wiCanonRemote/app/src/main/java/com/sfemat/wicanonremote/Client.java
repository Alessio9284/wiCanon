package com.sfemat.wicanonremote;

import android.os.AsyncTask;
import android.widget.TextView;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.Socket;
import java.net.UnknownHostException;

public class Client extends AsyncTask<Void, Void, Void>
{
    private String dstAddress;
    private int dstPort;
    TextView textResponse;
    private int DIM_BUF = 1024;
    private String response = "";

    Client(String addr, int port, TextView textResponse)
    {
        dstAddress = addr;
        dstPort = port;
        this.textResponse = textResponse;
    }

    @Override
    public Void doInBackground(Void... arg0)
    {
        Socket socket = null;

        try
        {
            int bytesRead;
            byte[] buffer = new byte[DIM_BUF];
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream(DIM_BUF);

            socket = new Socket(dstAddress, dstPort);

            InputStream inputStream = socket.getInputStream();

            /*
             * notice: inputStream.read() will block if no data return
             */
            while ((bytesRead = inputStream.read(buffer)) != -1)
            {
                byteArrayOutputStream.write(buffer, 0, bytesRead);
                response += byteArrayOutputStream.toString("UTF-8");
            }

        }
        catch (UnknownHostException e)
        {
            // TODO Auto-generated catch block
            e.printStackTrace();
            response = "UnknownHostException: " + e.toString();
        }
        catch (IOException e)
        {
            // TODO Auto-generated catch block
            e.printStackTrace();
            response = "IOException: " + e.toString();
        }
        finally
        {
            if (socket != null)
            {
                try
                {
                    socket.close();
                }
                catch (IOException e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
            }
        }
        return null;
    }

    @Override
    protected void onPostExecute(Void result)
    {
        textResponse.setText(response);
        super.onPostExecute(result);
    }
}