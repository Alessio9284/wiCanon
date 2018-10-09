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
    Dim channels() As String = {"http://bit.ly/rai1qdr", "http://bit.ly/rai2qdr"}


    Private Sub wiCanon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        Thread.Sleep(3000)
        Visible = True

        thread = New Thread(AddressOf ThreadTask) With {
            .IsBackground = True
        }
        thread.Start()


    End Sub
    Private Function GetChannelIP(k As String)
        Return channels(Convert.ToInt32(k) - 1)
    End Function
    Private Function Command(k As String)
        Select Case k
            Case "s"
                Application.Exit()
            Case "vm"
                'Audioswitcher.audioapi
            Case "vp"
                'Audioswitcher.audioapi
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

                    Dim numero As String = Encoding.ASCII.GetString(bytes)
                    Debug.Print("Dato memorizzato: " + numero)
                    Process.Start("CMD", "/C taskkill /im vlc.exe /f")
                    Threading.Thread.Sleep(1000)
                    Dim chars() As Char = numero
                    For Each c As Char In chars
                        If IsNumeric(c) Then
                            Process.Start("CMD", "/C  cd C:\Program Files\VideoLAN\VLC & vlc --fullscreen " + GetChannelIP(numero))
                        Else
                            ' In base alla lettera chiude l'applicazione o alza il volume ecc..
                            Command(numero)
                        End If
                        Exit For
                    Next


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