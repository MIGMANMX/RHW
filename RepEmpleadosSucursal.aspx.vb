
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class _RepEmpleadosSucursal
    Inherits System.Web.UI.Page

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        wucSucursales.ddlAutoPostBack = 0
        tSuc.Text = ""
        Mens.Text = ""
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        tSuc.Text = wucSucursales.sucursal
        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()

        '''''''''''''Ocultar sucursales a Gerentes
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC

        If datos(0) = 2 Then
            wucSucursales.idSucursal = datos(1)
            wucSucursales.Visible = False
            Suc.Visible = False


        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            Dim p As New ReportParameter("sucursal", wucSucursales.sucursal)
            ReportViewer1.LocalReport.SetParameters(p)

            ReportViewer1.ServerReport.Refresh()

        Else
            Mens.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        tSuc.Text = wucSucursales.sucursal
    End Sub
End Class
