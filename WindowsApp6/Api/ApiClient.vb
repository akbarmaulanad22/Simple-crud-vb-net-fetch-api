Imports System.Net.Http

Imports System.Threading.Tasks

Imports Newtonsoft.Json

Public Class ApiClient

    Private ReadOnly client As HttpClient

    Public Sub New()

        client = New HttpClient()
        client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoxLCJ1c2VybmFtZSI6ImFhZWVlIiwiZXhwIjoxNzQ4NDkzMTkyLCJpYXQiOjE3NDg0MDY3OTIsImlzcyI6Im15YXBwIiwic3ViIjoiMSJ9.EdGYLxwH5GQuiTypFbrY5A25vc70ScTbq-AjvytymtU")

    End Sub

    ' --- GET ---
    Public Async Function GetDataAsync(Of T)(url As String) As Task(Of T)

        Dim response As HttpResponseMessage = Await client.GetAsync(url)

        response.EnsureSuccessStatusCode()

        Dim jsonString As String = Await response.Content.ReadAsStringAsync()

        Return JsonConvert.DeserializeObject(Of T)(jsonString)

    End Function

    ' --- POST ---
    Public Async Function PostDataAsync(Of T)(url As String, data As Object) As Task(Of T)
        Dim json = JsonConvert.SerializeObject(data)
        Dim content = New StringContent(json, Nothing, "application/json")

        Dim response = Await client.PostAsync(url, content)
        response.EnsureSuccessStatusCode()

        Dim responseJson = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of T)(responseJson)
    End Function

    ' --- PUT ---
    Public Async Function PutDataAsync(Of T)(url As String, data As Object) As Task(Of T)
        Dim json = JsonConvert.SerializeObject(data)
        Dim content = New StringContent(json, Nothing, "application/json")

        Dim response = Await client.PutAsync(url, content)
        response.EnsureSuccessStatusCode()

        Dim responseJson = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of T)(responseJson)
    End Function

    ' --- DELETE ---
    Public Async Function DeleteDataAsync(url As String) As Task(Of String)
        Dim response = Await client.DeleteAsync(url)
        response.EnsureSuccessStatusCode()
        Return Await response.Content.ReadAsStringAsync()
    End Function

End Class