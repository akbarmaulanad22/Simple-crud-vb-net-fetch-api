Public Class StudentForm
    Private student As StudentModel
    Private isEdit As Boolean = False
    Private apiClient As ApiClient = New ApiClient()
    Private parentForm As StudentView

    Public Sub New(student As StudentModel, isEdit As Boolean, parentForm As StudentView)
        InitializeComponent()
        Me.student = student
        Me.isEdit = isEdit
        Me.parentForm = parentForm

        If isEdit Then
            LoadDataToForm()
        End If
    End Sub

    Private Sub LoadDataToForm()
        txtUserId.Text = student.UserId.ToString()
        txtNim.Text = student.NIM
        txtName.Text = student.Name
        txtDateOfBirth.Text = student.DateOfBirth
        txtAddress.Text = student.Address
        txtStudyProgramId.Text = student.StudyProgramId.ToString()
        txtClassYear.Text = student.ClassYear
    End Sub

    Private Sub SaveStudent()
        student.UserId = Integer.Parse(txtUserId.Text)
        student.NIM = txtNim.Text
        student.Name = txtName.Text
        student.DateOfBirth = txtDateOfBirth.Text
        student.Address = txtAddress.Text
        student.StudyProgramId = Integer.Parse(txtStudyProgramId.Text)
        student.ClassYear = txtClassYear.Text
    End Sub

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveStudent()

        Try
            If isEdit Then
                ' UPDATE
                Await apiClient.PutDataAsync(Of StudentModel)(
                    $"http://localhost:8081/api/v1/student/{student.Id}", student)
            Else
                ' CREATE
                Await apiClient.PostDataAsync(Of StudentModel)(
                    "http://localhost:8081/api/v1/student", student)
            End If

            MessageBox.Show("Data berhasil disimpan.")
            parentForm.RefreshData()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub StudentForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class