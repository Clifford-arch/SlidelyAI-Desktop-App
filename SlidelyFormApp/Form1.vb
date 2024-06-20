Imports SlidelyFormApp

Public Class Form1
    Private WithEvents btnCreateSubmission As New Button()
    Private WithEvents btnViewSubmissions As New Button()
    Private WithEvents lblDescription As New Label()
    Private WithEvents lblInstructions As New Label()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        ' Set properties for lblDescription
        lblDescription.Text = "Welcome to Submission Management:"
        lblDescription.AutoSize = True
        lblDescription.Location = New Point(20, 20)

        ' Set properties for btnViewSubmissions
        btnViewSubmissions.Text = "VIEW SUBMISSIONS (CTRL + V)"
        btnViewSubmissions.Size = New Size(300, 40)
        btnViewSubmissions.Location = New Point(40, 50)
        btnViewSubmissions.BackColor = Color.FromArgb(255, 255, 150)
        btnViewSubmissions.FlatStyle = FlatStyle.Flat
        btnViewSubmissions.FlatAppearance.BorderSize = 0
        Dim pathViewSubmissions As New Drawing2D.GraphicsPath()
        Dim rectViewSubmissions As New Rectangle(0, 0, btnViewSubmissions.Width, btnViewSubmissions.Height)
        Dim radiusViewSubmissions As Integer = 20 ' Adjust the radius as needed
        pathViewSubmissions.AddArc(rectViewSubmissions.Left, rectViewSubmissions.Top, radiusViewSubmissions * 2, radiusViewSubmissions * 2, 180, 90)
        pathViewSubmissions.AddArc(rectViewSubmissions.Right - radiusViewSubmissions * 2, rectViewSubmissions.Top, radiusViewSubmissions * 2, radiusViewSubmissions * 2, 270, 90)
        pathViewSubmissions.AddArc(rectViewSubmissions.Right - radiusViewSubmissions * 2, rectViewSubmissions.Bottom - radiusViewSubmissions * 2, radiusViewSubmissions * 2, radiusViewSubmissions * 2, 0, 90)
        pathViewSubmissions.AddArc(rectViewSubmissions.Left, rectViewSubmissions.Bottom - radiusViewSubmissions * 2, radiusViewSubmissions * 2, radiusViewSubmissions * 2, 90, 90)
        pathViewSubmissions.CloseFigure()
        btnViewSubmissions.Region = New Region(pathViewSubmissions)

        ' Set properties for btnCreateSubmission
        btnCreateSubmission.Text = "CREATE NEW SUBMISSION (CTRL + N)"
        btnCreateSubmission.Size = New Size(300, 40)
        btnCreateSubmission.Location = New Point(40, 100)
        btnCreateSubmission.BackColor = Color.FromArgb(173, 216, 230)
        btnCreateSubmission.FlatStyle = FlatStyle.Flat
        btnCreateSubmission.FlatAppearance.BorderSize = 0
        Dim pathCreateSubmission As New Drawing2D.GraphicsPath()
        Dim rectCreateSubmission As New Rectangle(0, 0, btnCreateSubmission.Width, btnCreateSubmission.Height)
        Dim radiusCreateSubmission As Integer = 20 ' Adjust the radius as needed
        pathCreateSubmission.AddArc(rectCreateSubmission.Left, rectCreateSubmission.Top, radiusCreateSubmission * 2, radiusCreateSubmission * 2, 180, 90)
        pathCreateSubmission.AddArc(rectCreateSubmission.Right - radiusCreateSubmission * 2, rectCreateSubmission.Top, radiusCreateSubmission * 2, radiusCreateSubmission * 2, 270, 90)
        pathCreateSubmission.AddArc(rectCreateSubmission.Right - radiusCreateSubmission * 2, rectCreateSubmission.Bottom - radiusCreateSubmission * 2, radiusCreateSubmission * 2, radiusCreateSubmission * 2, 0, 90)
        pathCreateSubmission.AddArc(rectCreateSubmission.Left, rectCreateSubmission.Bottom - radiusCreateSubmission * 2, radiusCreateSubmission * 2, radiusCreateSubmission * 2, 90, 90)
        pathCreateSubmission.CloseFigure()
        btnCreateSubmission.Region = New Region(pathCreateSubmission)

        ' Add controls to form
        Me.Controls.Add(lblDescription)
        Me.Controls.Add(lblInstructions)
        Me.Controls.Add(btnCreateSubmission)
        Me.Controls.Add(btnViewSubmissions)

        ' Set form size to accommodate controls
        Me.Size = New Size(380, 200)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission.PerformClick()
        End If
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        ' Show CreateSubmissionForm when btnCreateSubmission is clicked
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub
End Class
