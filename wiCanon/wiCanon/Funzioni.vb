﻿Imports System.Threading

Public Class Funzioni
    Public volume As Integer
    Public channels() As String = {"http://mediapolisevent.rai.it/relinker/relinkerServlet.htm?cont=2606803", "http://cobra-iptv.noip.me:8000/live/Denis/Denis/4661.m3u8", "https://raiquattro1-i.akamaihd.net/hls/live/253230/rai4sec_1_1800/chunklist.m3u8", "http://bit.ly/rai1qdr", "http://bit.ly/rai2qdr", "https://live3-mediaset-it.akamaized.net/content/hls_clr_xo/live/channel(ch02)/index.m3u8"}
    Public currentChannel As String

    Public Sub changeVolume(v As Boolean)
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
        Thread.Sleep(1000)
        Process.Start("CMD", "/C  cd C:\Program Files (x86)\VideoLAN\VLC & vlc --fullscreen --volume=" +
                      Chr(34) + volume.ToString + Chr(34) + " " + currentChannel)
        ' \Program Files
    End Sub

    Public Sub mute()
        Process.Start("CMD", "/C taskkill /im vlc.exe /f")
        Thread.Sleep(1000)
        Process.Start("CMD", "/C  cd C:\Program Files (x86)\VideoLAN\VLC & vlc --noaudio --fullscreen --volume=" +
                      Chr(34) + volume.ToString + Chr(34) + " " + currentChannel)
        ' \Program Files
    End Sub

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
            Case "l"
                mute()
        End Select
    End Function
End Class