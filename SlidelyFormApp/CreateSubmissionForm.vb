Imports System.Net.Http
Imports Newtonsoft.Json ' Ensure Newtonsoft.Json is installed via NuGet Package Manager

Public Class CreateSubmissionForm
    Private stopwatchRunning As Boolean = False
    Private stopwatchTime As TimeSpan = TimeSpan.Zero
    Private WithEvents timer As New Timer With {.Interval = 1000}

    Private WithEvents btnToggleStopwatch As New Button()
    Private WithEvents btnSubmit As New Button()
    Private WithEvents txtName As New TextBox()
    Private WithEvents txtEmail As New TextBox()
    Private WithEvents txtPhoneNumber As New TextBox()
    Private WithEvents txtGithubLink As New TextBox()
    Private WithEvents txtStopwatchTime As New TextBox()

    Private WithEvents lblName As New Label()
    Private WithEvents lblEmail As New Label()
    Private WithEvents lblPhoneNumber As New Label()
    Private WithEvents lblGithubLink As New Label()
    Private WithEvents lblDescription As New Label()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize timer when form loads
        timer.Start()
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Handle key events (Ctrl + T for toggle stopwatch, Ctrl + S for submit)
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        ' Toggle stopwatch start/stop
        If stopwatchRunning Then
            timer.Stop()
        Else
            timer.Start()
        End If
        stopwatchRunning = Not stopwatchRunning
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        ' Update stopwatch time display
        stopwatchTime = stopwatchTime.Add(TimeSpan.FromSeconds(1))
        txtStopwatchTime.Text = stopwatchTime.ToString("hh\:mm\:ss")
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Handle submission button click (async)
        Dim submission As New Submission With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .Phone = txtPhoneNumber.Text,
            .GithubLink = txtGithubLink.Text,
            .StopwatchTime = txtStopwatchTime.Text
        }

        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, System.Text.Encoding.UTF8, "application/json")

        Try
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"Failed to save submission. Error: {response.ReasonPhrase}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnToggleStopwatch_Paint(sender As Object, e As PaintEventArgs) Handles btnToggleStopwatch.Paint
        ' Custom painting for toggle stopwatch button
        Dim buttonPath As New Drawing2D.GraphicsPath()
        Dim radius As Integer = 20
        buttonPath.AddArc(New Rectangle(0, 0, radius, radius), 180, 90)
        buttonPath.AddArc(New Rectangle(btnToggleStopwatch.Width - radius, 0, radius, radius), -90, 90)
        buttonPath.AddArc(New Rectangle(btnToggleStopwatch.Width - radius, btnToggleStopwatch.Height - radius, radius, radius), 0, 90)
        buttonPath.AddArc(New Rectangle(0, btnToggleStopwatch.Height - radius, radius, radius), 90, 90)
        buttonPath.CloseAllFigures()
        btnToggleStopwatch.Region = New Region(buttonPath)

        ' Clear the background and draw button text
        e.Graphics.Clear(btnToggleStopwatch.BackColor)
        TextRenderer.DrawText(e.Graphics, btnToggleStopwatch.Text, btnToggleStopwatch.Font, btnToggleStopwatch.ClientRectangle, btnToggleStopwatch.ForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
    End Sub
End Class
