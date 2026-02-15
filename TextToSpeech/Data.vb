Public Class Data

    Public Shared Sub SetData(ByVal data As String)
        _this_data = data
        RaiseEvent DataChanged(data)
    End Sub

    Public Shared Sub SetDataWithoutRaisingEvent(ByVal data As String)
        _this_data = data
    End Sub

    Public Shared Function GetData() As String
        Return _this_data
    End Function

    Public Shared Sub ClearData()
        _this_data = ""
        RaiseEvent DataCleared()
    End Sub

    Public Shared Sub ClearDataWithoutRaisingEvent()
        _this_data = ""
    End Sub

    Public Shared Event DataChanged(ByVal data As String)

    Public Shared Event DataCleared()

    Private Shared _this_data As String
End Class
