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
    Dim volume As Integer
    Dim channels() As String = {"http://bit.ly/rai1qdr", "http://bit.ly/rai2qdr"}
    Dim currentChannel As String

    Private Sub wiCanon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        Thread.Sleep(3000)
        Visible = True

        volume = 256

        thread = New Thread(AddressOf ThreadTask) With {
            .IsBackground = True
        }
        thread.Start()


    End Sub
    Private Sub changeVolume(v As Boolean)
        If (v) Then
            If (volume + 10 > 1024) Then
            Else
                volume -= 10
            End If

        Else
            If (volume - 10 < 0) Then
            Else
                volume -= 10
            End If
        End If
        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
        Threading.Thread.Sleep(1000)
        Process.Start("CMD", "/C  cd C:\Program Files\VideoLAN\VLC & vlc --fullscreen --volume=" + Chr(34) + volume.ToString + Chr(34) + " " + currentChannel)

    End Sub
    Private Sub Mute()
        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
        Threading.Thread.Sleep(1000)
        Process.Start("CMD", "/C  cd C:\Program Files\VideoLAN\VLC & vlc --noaudio --fullscreen --volume=" + Chr(34) + volume.ToString + Chr(34) + " " + currentChannel)


    End Sub
    Private Function GetChannelIP(k As String)
        Return channels(Convert.ToInt32(k) - 1)
    End Function
    Private Function Command(k As Char)
        Select Case k
            Case "s"
                Environment.Exit(0)
            Case "m"
                changeVolume(False)
            Case "p"
                changeVolume(True)
            Case "l"
                Mute()
        End Select
    End Function
    Private Sub ThreadTask()
        Try

            server = New TcpListener(IPAddress.Parse(IP_SERVER), PORT)

            server.Start()
            client = server.AcceptTcpClient()
            While True
                Try
                    Debug.Print("Utente da accettare")

                    Debug.Print("Utente accettato")

                    dati = client.GetStream()
                    Debug.Print("Dato prelevato")

                    Dim bytes(client.ReceiveBufferSize) As Byte
                    dati.Read(bytes, 0, client.ReceiveBufferSize)
                    Dim numero(1) As String
                    numero(0) = Encoding.ASCII.GetString(bytes)
                    If (numero(0) = "") Then

                    Else


                        Debug.Print("Dato memorizzato: " + numero(0))
                        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
                        Threading.Thread.Sleep(1000)

                        If IsNumeric(numero(0)) Then
                            Debug.Print("ka")
                            Process.Start("CMD", "/C  cd C:\Program Files\VideoLAN\VLC & vlc --fullscreen --volume " + volume.ToString + " " + GetChannelIP(numero(0)))
                            currentChannel = GetChannelIP(numero(0))
                        Else
                            ' In base alla lettera chiude l'applicazione o alza il volume ecc..
                            Command(numero(0))

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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)
    End Sub
End Class