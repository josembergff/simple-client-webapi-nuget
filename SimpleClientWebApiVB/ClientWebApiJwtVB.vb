Imports SimpleClientWebApi

Public Class ClientWebApiJwtVB
    Public NotificationsMessage As String
    Public Notifications As String()

    Public Function GetApi(Of T1 As New)(ByVal chave As String, ByVal uri As String) As T1

        Dim client As ClientWebApiJwt = New ClientWebApiJwt(chave, uri)

        Dim response As T1 = client.Get(Of T1, Object)()

        getNotifications(client)

        Return response
    End Function

    Public Function PostApi(Of T1 As New, T2 As New)(ByVal chave As String, ByVal uri As String, ByRef send As T2) As T1

        Dim client As ClientWebApiJwt = New ClientWebApiJwt(chave, uri)

        Dim response As T1 = client.Post(Of T1, T2)(send)

        getNotifications(client)

        Return response

    End Function

    Private Sub getNotifications(ByVal client As ClientWebApiJwt)
        If Not client.IsValid() Then
            NotificationsMessage = client.NotificationsMessage
            Notifications = client.Notifications
        End If
    End Sub

End Class
