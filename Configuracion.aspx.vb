
Imports RHLogica

Partial Class Configuracion
    Inherits System.Web.UI.Page
    Public gvPos As Integer
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
        'If datos(0).StartsWith("Error") Then
        '    Lmsg.CssClass = "error"
        '    Lmsg.Text = datos(0)
        'Else
        txtdia.Text = datos(0)
        DropDownList1.SelectedValue = datos(0)
        txthora.Text = datos(1)
        'End If
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
        Dim ap As New ctiConfiguracion
        'txt1.Text = DropH.SelectedValue
        Dim r As String = ap.actualizarHorarios(DropDownList1.SelectedValue, txthora.Text)
        If r.StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
            '
        End If
    End Sub

End Class
