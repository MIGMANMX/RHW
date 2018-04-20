
Imports RHLogica

Partial Class ConFHor
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"

        End If
        Mens.Text = "" : gvPos = 0
        Session("idz_e") = ""

        Dim dsP As New ctiConfiguracion
        Dim datos() As String = dsP.datosHorario()
        dsP = Nothing
        TextBox2.Text = datos(0)
        CheckBox1.Checked = datos(1)
        CheckBox2.Checked = datos(2)
        CheckBox3.Checked = datos(3)
        CheckBox4.Checked = datos(4)
        CheckBox5.Checked = datos(5)
        CheckBox6.Checked = datos(6)
        CheckBox7.Checked = datos(7)
    End Sub
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim ap As New ctiConfiguracion
        Dim r As String = ap.actualizarDiaCaptura(TextBox2.Text, CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, CheckBox4.Checked, CheckBox5.Checked, CheckBox6.Checked, CheckBox7.Checked)
        If r.StartsWith("Error") Then
            Mens.CssClass = "error"
        Else
            Mens.CssClass = "correcto"
        End If
    End Sub
End Class
