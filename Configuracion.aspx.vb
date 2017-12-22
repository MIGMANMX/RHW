
Imports RHLogica

Partial Class Configuracion
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Dim cont As Integer = 0
    Dim cont2 As Integer = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"

        End If
        Lmsg.Text = "" : gvPos = 0
        Session("idz_e") = ""


        Dim dsP As New ctiConfiguracion
        Dim datos() As String = dsP.datosHorario()
        dsP = Nothing
        TextBox1.Text = datos(0)
        TextBox2.Text = datos(1)
    End Sub
    Protected Sub Horario_Click(sender As Object, e As EventArgs) Handles Horario.Click
        MultiView1.ActiveViewIndex = 0
    End Sub
    Protected Sub Datos_Click(sender As Object, e As EventArgs) Handles Datos.Click
        MultiView1.ActiveViewIndex = 1
    End Sub
    Protected Sub Info_Click(sender As Object, e As EventArgs) Handles Info.Click
        MultiView1.ActiveViewIndex = 2
    End Sub
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        validarchecSem()
        validarchecHora()
        Dim ap As New ctiConfiguracion
        Dim r As String = ap.actualizarHorarios(TextBox1.Text, TextBox2.Text)
        If r.StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
        End If
    End Sub
    Public Sub validarchecSem()
        cont = 0
        'TextBox1.Text = ""
        If CheckBox1.Checked = True Then
            cont = 1 + cont
            TextBox1.Text = "Monday"
        End If
        If CheckBox2.Checked = True Then
            cont = 1 + cont
            TextBox1.Text = "Tuesday"
        End If
        If CheckBox3.Checked = True Then
            cont = 1 + cont
            TextBox1.Text = "Wednesday"
        End If
        If CheckBox4.Checked = True Then
            cont = 1 + cont
            TextBox1.Text = "Thursday"
        End If
        If CheckBox5.Checked = True Then
            cont = 1 + cont
            TextBox1.Text = "Friday"
        End If
        If CheckBox6.Checked = True Then
            cont = 1 + cont
            TextBox1.Text = "Saturday"
        End If
        If CheckBox7.Checked = True Then
            cont = 1 + cont
            TextBox1.Text = "Sunday"
        End If
        validar()
    End Sub
    Public Sub validarchecHora()
        cont2 = 0
        'TextBox2.Text = ""
        If CheckBox8.Checked = True Then
            cont2 = 1 + cont2
            TextBox2.Text = "08:00"
        End If
        If CheckBox9.Checked = True Then
            cont2 = 1 + cont2
            TextBox2.Text = "09:00"
        End If
        If CheckBox10.Checked = True Then
            cont2 = 1 + cont2
            TextBox2.Text = "10:00"
        End If
        If CheckBox11.Checked = True Then
            cont2 = 1 + cont2
            TextBox2.Text = "11:00"
        End If
        If CheckBox12.Checked = True Then
            cont2 = 1 + cont2
            TextBox2.Text = "12:00"
        End If
        validar2()
    End Sub
    Public Sub validar()
        If cont < 2 Then
            'Mens.Text = "Error: Solo debe seleccionar un dia"
        End If
    End Sub
    Public Sub validar2()
        If cont2 < 2 Then
            'Mens.Text = "Error: Solo debe seleccionar una hora"
        End If
    End Sub
End Class
