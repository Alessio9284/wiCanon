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

    Private Sub wiCanon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        Thread.Sleep(3000)
        Visible = True

        thread = New Thread(AddressOf ThreadTask) With {
            .IsBackground = True
        }
        thread.Start()
        ' browser.Load("https://www.google.it")
    End Sub

    Private Sub ThreadTask()
        Try

            server = New TcpListener(IPAddress.Parse(IP_SERVER), PORT)

            server.Start()

            While True
                Try
                    Debug.Print("Utente da accettare")
                    client = server.AcceptTcpClient()
                    Debug.Print("Utente accettato")

                    dati = client.GetStream()
                    Debug.Print("Dato prelevato")

                    Dim bytes(client.ReceiveBufferSize) As Byte
                    dati.Read(bytes, 0, client.ReceiveBufferSize)

                    Dim numero As String = Encoding.ASCII.GetString(bytes)
                    Debug.Print("Dato memorizzato: " + numero)

                    If numero.Contains("1") Then
                        ' browser.Load("https://www.raiplay.it/dirette/rai1")
                    End If
                    If numero.Contains("2") Then
                        ' browser.Load("https://www.raiplay.it/dirette/rai2")
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