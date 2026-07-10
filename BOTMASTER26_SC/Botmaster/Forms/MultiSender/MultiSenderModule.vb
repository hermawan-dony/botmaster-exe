Imports System.Web.UI.WebControls
Imports Newtonsoft
Imports Newtonsoft.Json
Module MultiSenderModule
    Public WaWebviewChannels As New List(Of WaChannel)
    Public Function IsChannelExist(ByVal ChannelName As String) As Boolean
        Dim Result As Boolean = False
        For Each n In WaWebviewChannels
            If n.AccountName = ChannelName Then
                Result = True
            End If
        Next
        Return Result
    End Function
    Public Sub SaveChannels()
        Dim tempLst As New List(Of String)
        For Each wa In WaWebviewChannels
            tempLst.Add(wa.AccountName)
        Next
        IO.File.WriteAllText(ClsSpecialDirectories.GetAccountFolder & "channels.json", JsonConvert.SerializeObject(tempLst))
    End Sub
    Public Sub LoadChannels()
        Dim tempLst As New List(Of String)
        If IO.File.Exists(ClsSpecialDirectories.GetAccountFolder & "channels.json") Then
            tempLst = JsonConvert.DeserializeObject(Of List(Of String))(IO.File.ReadAllText(ClsSpecialDirectories.GetAccountFolder & "channels.json"))
            For Each acc In tempLst
                Dim NewWa As New WaChannel
                NewWa.AccountName = acc
                NewWa.WA = New WAWebview2
                NewWa.WA.Int(NewWa.AccountName)
                WaWebviewChannels.Add(NewWa)
                FrmAccounts.AddBrowsertoTab(NewWa)
            Next
        End If
    End Sub
End Module
Public Class WaChannel
    Public AccountName As String
    Public WA As WAWebview2
End Class