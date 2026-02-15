Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Application.ProductName & " - bereit"
        AddHandler Data.DataChanged, Sub(ByVal data As String)
                                         Dim mi As MethodInvoker = Sub()
                                                                       Me.Text = Application.ProductName & " - " & data
                                                                   End Sub
                                         If Me.InvokeRequired Then
                                             BeginInvoke(mi)
                                         Else
                                             mi.Invoke()
                                         End If
                                         TextToSpeech.Data.ClearData()
                                     End Sub
    End Sub

    Private Sub btn_speak_Click(sender As System.Object, e As System.EventArgs) Handles btn_speak.Click
        Dim t As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf ThreadAction))
        Data.SetData("spricht")
        Data.SetDataWithoutRaisingEvent(RichTextBox1.Text)
        t.Start()
    End Sub

    Private Sub btn_end_Click(sender As System.Object, e As System.EventArgs) Handles btn_end.Click
        End
    End Sub

    Private Sub ThreadAction()
        Dim SAPI As Object = CreateObject("sapi.spvoice")
        SAPI.Speak(Data.GetData())
        Data.SetData("bereit")
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Data.SetData("speichert")
        Dim FileName As String = ""
        Dim SFD As New SaveFileDialog()
        With SFD
            .Title = "Als Audio speichern"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
            .FileName = "sapi.wav"
            .Filter = "Wavesound|*.wav|Alle Dateien|*.*"
            If .ShowDialog() = DialogResult.OK Then
                FileName = .FileName
            Else : Return
            End If
        End With
        Dim oFileStream As Object = CreateObject("SAPI.SpFileStream")
        oFileStream.Format.Type = 39 'SAFT48kHz16BitStereo
        oFileStream.Open(FileName, 3) 'SSFMCreateForWrite
        Dim oVoice As Object = CreateObject("SAPI.SpVoice")
        oVoice.AudioOutputStream = oFileStream
        oVoice.Speak(RichTextBox1.Text)
        oFileStream.Close()
        Data.SetData("bereit")
    End Sub
End Class
