Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class StudentView
    Private Sub StudentView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim newStudent As New StudentModel()
        Dim inputForm As New StudentForm(newStudent, isEdit:=False, Me)
        inputForm.ShowDialog()
    End Sub

    Public Async Sub RefreshData()
        Try
            ' Inisialisasi API Client
            Dim apiClient As New ApiClient()

            ' Panggil API
            Dim url As String = "http://localhost:8081/api/v1/student"
            Dim students As List(Of StudentModel) = Await apiClient.GetDataAsync(Of List(Of StudentModel))(url)

            ' Tampilkan di DataGridView
            dgvStudents.DataSource = students

        Catch ex As Exception
            MessageBox.Show("Error saat memuat data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgvStudents.SelectedRows.Count > 0 Then
            Dim selectedStudent As StudentModel = dgvStudents.SelectedRows(0).DataBoundItem
            Dim editForm As New StudentForm(selectedStudent, isEdit:=True, Me)
            editForm.ShowDialog()
        Else
            MessageBox.Show("Pilih data yang ingin diedit.")
        End If
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvStudents.SelectedRows.Count > 0 Then
            Dim selectedStudent As StudentModel = dgvStudents.SelectedRows(0).DataBoundItem

            If MessageBox.Show("Yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Try
                    Dim apiClient As New ApiClient()

                    Await apiClient.DeleteDataAsync($"http://localhost:8081/api/v1/student/{selectedStudent.Id}")
                    MessageBox.Show("Data berhasil dihapus.")
                    RefreshData()
                Catch ex As Exception
                    MessageBox.Show("Gagal menghapus: " & ex.Message)
                End Try
            End If
        Else
            MessageBox.Show("Pilih data yang ingin dihapus.")
        End If
    End Sub
End Class