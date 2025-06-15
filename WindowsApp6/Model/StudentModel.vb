Imports Newtonsoft.Json

Public Class StudentModel

    Public Property Id As Integer

    <JsonProperty("user_id")>
    Public Property UserId As Integer

    Public Property NIM As String

    Public Property Name As String

    <JsonProperty("date_of_birth")>
    Public Property DateOfBirth As String

    Public Property Address As String

    <JsonProperty("study_program_id")>
    Public Property StudyProgramId As Integer

    <JsonProperty("class_year")>
    Public Property ClassYear As String

End Class
