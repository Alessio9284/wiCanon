Imports System.Threading

Public Class Funzioni
    Public volume As Integer = 256
    Public channels() As String = {
        "http://mediapolisevent.rai.it/relinker/relinkerServlet.htm?cont=2606803",
        "http://bit.ly/rai2qdr",
        "http://bit.ly/rai3qdr",
        "http://bit.ly/rete4qdr",
        "http://bit.ly/canale5qdr",
        "https://live3-mediaset-it.akamaized.net/content/hls_clr_xo/live/channel(ch02)/index.m3u8",
        "http://bit.ly/la7qdr",
        "http://par-1.ml/hls/36b35538-7b62-fedb-14ed-efbb87852f8e.m3u8",
        "http://par-1.ml/hls/59c39cf8-3388-ebfc-1dcd-d660c566f7c0.m3u8",
        "http://skyianywhere2-i.akamaihd.net/hls/live/200275/tg24/playlist.m3u8",
        "https://live3-mediaset-it.akamaized.net/content/hls_clr_xo/live/channel(ch05)/index.m3u8"
    }
    Public currentChannelString As String
    Public currentChannelInteger As Integer = 0

    Public Sub close_openVLC(volumee As Integer, canalee As String, Optional audio As String = "")
        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
        Thread.Sleep(1000)
        Process.Start("CMD", "/C cd C:\Program Files\VideoLAN\VLC & vlc " + audio + " --fullscreen --volume=" +
                      Chr(34) + volumee.ToString + Chr(34) + " " + canalee)
    End Sub

    Public Sub changeVolume(v As Boolean)
        'Volume
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

        close_openVLC(volume, currentChannelString)
    End Sub

    Public Sub mute()
        'Chiudi VLC e riaprilo mutando il volume
        close_openVLC(volume, currentChannelString, "--noaudio")
    End Sub

    'Restituisce l'indirizzo del canale
    Public Function GetChannelIP(canale As Integer)
        Return channels(canale)
    End Function

    Public Sub pag(queue As Boolean)
        Debug.Print("Canale corrente: " + (currentChannelInteger + 1).ToString)

        'Se è 0 e si vuole retrocedere di canale si va all'ultimo
        If (currentChannelInteger = 0 And queue = False) Then
            currentChannelInteger = channels.Length - 1

            'Se è l'ultimo canale e si vuole avanzare si va al primo
        ElseIf (currentChannelInteger = (channels.Length - 1) And queue = True) Then
            currentChannelInteger = 0

            'Caso normale modifica la variabile del canale corrente in base al comando
        Else
            If (queue) Then
                currentChannelInteger += 1
            Else
                currentChannelInteger -= 1
            End If
        End If

        Debug.Print("Canale modificato: " + (currentChannelInteger + 1).ToString)

        close_openVLC(volume, GetChannelIP(currentChannelInteger))
    End Sub

    Public Sub Command(comando As String)
        Select Case comando
            Case "s"
                Process.Start("CMD", "/C taskkill /im vlc.exe /f")
                Environment.Exit(0)
            Case "m"
                changeVolume(False)
            Case "p"
                changeVolume(True)
            Case "ps"
                pag(True)
            Case "pg"
                pag(False)
            Case "l"
                mute()
        End Select
    End Sub
End Class