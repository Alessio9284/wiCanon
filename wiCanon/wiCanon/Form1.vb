Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class WiCanon

    Private IP_SERVER As String = "127.0.0.1"
    Private PORT As Integer = 1027
    Dim server As TcpListener
    Dim client As TcpClient
    Dim dati As NetworkStream
    Dim thread As Thread
    Dim func As New Funzioni

    Private Sub wiCanon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False

        'Crea e avvia thread per accettare connessioni
        thread = New Thread(AddressOf serverThread) With {
            .IsBackground = True
        }
        thread.Start()
    End Sub

    Public Sub serverThread()
        'Crea client udp
        Dim udpClient As New UdpClient(PORT)

        While True
            'Accetta qualsiasi ip
            Dim RemoteIpEndPoint As New IPEndPoint(IPAddress.Any, 0)
            Dim receiveBytes As Byte()

            'Riceve i dati
            receiveBytes = udpClient.Receive(RemoteIpEndPoint)
            Dim opzione As String = Encoding.ASCII.GetString(receiveBytes)

            Debug.Print("Dato memorizzato: " + opzione)

            'Chiudi VLC in caso sia già aperto
            Process.Start("CMD", "/C taskkill /im vlc.exe /f")

            'Se è un canale
            If IsNumeric(opzione) Then
                'Se non abbiamo l'IP corrispondente non fare nulla
                If (Convert.ToInt32(opzione) > func.channels.Length) Then

                    'Se ce l'abbiamo vai a quel canale
                Else
                    func.currentChannelInteger = Convert.ToInt32(opzione) - 1
                    func.currentChannelString = func.GetChannelIP(func.currentChannelInteger)
                    func.close_openVLC(func.volume, func.currentChannelString)
                End If
                'Altrimenti esegui la funzione correlata alla lettera
            Else
                ' In base alla lettera chiude l'applicazione o alza il volume ecc..
                func.Command(opzione)
            End If
        End While
    End Sub
End Class