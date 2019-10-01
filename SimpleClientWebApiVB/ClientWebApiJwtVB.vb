Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports System.Text


Public Class ClientWebApiJwtVB
    Public NotificationsMessage As String
    Public Notifications As String()
    Private _uriWebApi, _keyJwt, _typeKey As String
    Private _client As HttpClient

    Public Sub New(ByVal keyJwt As String, ByVal uriWebApi As String, Optional ByVal typeKey As String = "")
        _keyJwt = keyJwt
        _uriWebApi = uriWebApi
        _client = New HttpClient()
        _typeKey = typeKey

        If String.IsNullOrEmpty(typeKey) Then
            _typeKey = "Bearer"
        End If

        _client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue(_typeKey, keyJwt)
    End Sub


    Public Function GetApi(Of T1 As New)(Optional ByVal route As String = "") As T1
        Dim valueReturn As T1 = New T1()
        Dim urlApi = _uriWebApi

        If Not String.IsNullOrEmpty(route) Then
            urlApi = _uriWebApi + "/" + route
        End If

        Dim result = _client.GetAsync(urlApi).Result

        If result.StatusCode = System.Net.HttpStatusCode.OK Then
            Dim reponseClient = result.Content.ReadAsStringAsync().Result
            valueReturn = JsonConvert.DeserializeObject(Of T1)(reponseClient)
        End If

        Return valueReturn

    End Function

    Public Function PostApi(Of T1 As New, T2 As New)(ByVal objectSend As T2, Optional ByVal route As String = "") As T1

        Dim valueReturn As T1 = New T1()
        Dim urlApi = _uriWebApi

        If Not String.IsNullOrEmpty(route) Then
            urlApi = _uriWebApi + "/" + route
        End If
        Dim serializedEmailContract = JsonConvert.SerializeObject(objectSend)

        Dim content = New StringContent(serializedEmailContract, Encoding.UTF8, "application/json")
        Dim result = _client.PostAsync(urlApi, content).Result

        If result.StatusCode = System.Net.HttpStatusCode.OK Then
            Dim reponseClient = result.Content.ReadAsStringAsync().Result
            valueReturn = JsonConvert.DeserializeObject(Of T1)(reponseClient)
        End If

        Return valueReturn

    End Function


End Class
