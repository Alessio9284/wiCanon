package com.sfemat.wicanonremote;

import android.os.AsyncTask;
import android.util.Log;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;


class ClientThread extends Thread
{
    public static String SERVER_IP = "192.168.1.1"; // 192.168.1.116
    public static final int SERVER_PORT = 1027;

    public void run(){
        try {
            //Crea socket
            DatagramSocket udpSocket = new DatagramSocket(SERVER_PORT);
            InetAddress serverAddr = InetAddress.getByName(SERVER_IP);
            //Costruisci pacchetto con i dati
            byte[] buf = (Remote.opzione).getBytes();
            Remote.opzione = "";
            DatagramPacket packet = new DatagramPacket(buf, buf.length, serverAddr, SERVER_PORT);
            //Invialo e chiudi socket
            udpSocket.send(packet);
            udpSocket.close();

        } catch (SocketException e) {
            Log.e("Udp:", "Socket Error:", e);
        } catch (IOException e) {
            Log.e("Udp Send:", "IO Error:", e);
        }
    }
}