Imports System.Threading

Public Class Funzioni
    Public volume As Integer
    Public channels() As String = {
        "http://mediapolisevent.rai.it/relinker/relinkerServlet.htm?cont=2606803",
        "http://bit.ly/rai2qdr",
        "http://bit.ly/rai3qdr",
        "http://bit.ly/rete4qdr",
        "http://bit.ly/canale5qdr",
        "https://live3-mediaset-it.akamaized.net/content/hls_clr_xo/live/channel(ch02)/index.m3u8",
        "http://195.154.134.236:9000/live/alaa1/alaa1/9981.ts",
        "http://par-1.ml/hls/36b35538-7b62-fedb-14ed-efbb87852f8e.m3u8",
        "http://par-1.ml/hls/59c39cf8-3388-ebfc-1dcd-d660c566f7c0.m3u8",
        "http://skyianywhere2-i.akamaihd.net/hls/live/200275/tg24/playlist.m3u8"
    }
    Public currentChannel As String

    Public Sub changeVolume(v As Boolean)
        'volume
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
        'Chiudi eventuali VLC aperti e riaprilo cambiando volume
        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
        Thread.Sleep(1000)
        Process.Start("CMD", "/C cd C:\Program Files\VideoLAN\VLC & vlc --fullscreen --volume=" +
                      Chr(34) + volume.ToString + Chr(34) + " " + currentChannel)
    End Sub

    Public Sub mute()
        'Chiudi VLC aperti e riaprilo mutando il volume
        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
        Thread.Sleep(1000)
        Process.Start("CMD", "/C cd C:\Program Files\VideoLAN\VLC & vlc --noaudio --fullscreen --volume=" +
                      Chr(34) + volume.ToString + Chr(34) + " " + currentChannel)
    End Sub

    'Restituisce l'indirizzo del canale
    Public Function GetChannelIP(k As String)
        Return channels(Convert.ToInt32(k) - 1)
    End Function



    Public Function Command(k As Char)
        Select Case k
            Case "s"
                Environment.Exit(0)
            Case "m"
                changeVolume(False)
            Case "p"
                changeVolume(True)
            Case "ps"
                ' da fare
            Case "pg"
                ' da fare
            Case "l"
                mute() ' da fare il bottone
        End Select
    End Function
End Class