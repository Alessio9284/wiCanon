package com.sfemat.wicanonremote;

import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;

class ClientThread implements Runnable
{
    public static String SERVER_IP = "192.168.1.1"; // 192.168.1.116
    public static final int SERVER_PORT = 1027;
    private Socket socket;

    @Override
    public void run()
    {
        try
        {
            //Crea un nuovo socket
            socket = new Socket(SERVER_IP, SERVER_PORT);
            //Crea uno stream di output verso il socket
            DataOutputStream out = new DataOutputStream(socket.getOutputStream());
            //Scrivici il carattere
            out.writeChars(Remote.opzione);
            //Chiudi il socket
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