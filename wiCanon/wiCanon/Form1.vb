Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class WiCanon

    Private IP_SERVER As String = "127.0.0.1" '192.168.1.62
    Private PORT As Integer = 1027
    Dim server As TcpListener
    Dim client As TcpClient
    Dim dati As NetworkStream
    Dim thread As Thread
    Dim func As New Funzioni


    Private Sub wiCanon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        Thread.Sleep(3000)
        Visible = True

        func.volume = 256

        thread = New Thread(AddressOf ThreadTask) With {
            .IsBackground = True
        }
        thread.Start()
    End Sub

    Private Sub ThreadTask()
        Try
            server = New TcpListener(IPAddress.Parse(IP_SERVER), PORT)
            server.Start()

            Debug.Print("Utente da accettare")
            client = server.AcceptTcpClient()
            Debug.Print("Utente accettato")

            While True
                Try
                    dati = client.GetStream()
                    Debug.Print("Dato prelevato")

                    Dim bytes(client.ReceiveBufferSize) As Byte
                    dati.Read(bytes, 0, client.ReceiveBufferSize)
                    Dim opzione As String
                    opzione = Encoding.ASCII.GetString(bytes)

                    If (opzione = "") Then

                    Else
                        Debug.Print("Dato memorizzato: " + opzione)
                        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
                        Thread.Sleep(1000)

                        If IsNumeric(opzione) Then
                            Debug.Print("ka")
                            Process.Start("CMD", "/C  cd C:\Program Files (x86)\VideoLAN\VLC & vlc --fullscreen --volume " +
                                          func.volume.ToString + " " + func.GetChannelIP(opzione))
                            ' Program Files
                            func.currentChannel = func.GetChannelIP(opzione)
                        Else
                            ' In base alla lettera chiude l'applicazione o alza il volume ecc..
                            func.Command(opzione)

                        End If
                    End If
                Catch e As Exception
                    Debug.Print("Errore Qui!")
                End Try
            End While
        Catch e As SocketException
            Debug.Print("Errore Qua!")
        Finally
            server.Stop()
        End Try
    End Sub
End Class