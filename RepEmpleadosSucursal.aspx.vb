
Imports Microsoft.Reporting.WebForms

Partial Class _RepEmpleadosSucursal
    Inherits System.Web.UI.Page

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        wucSucursales.ddlAutoPostBack = 0
        tSuc.Text = ""
        Mens.Text = ""
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        tSuc.Text = wucSucursales.sucursal
        Mens.Text = ""
        RepoEmSuc.ServerReport.Refresh()
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Mens.Text = ""
        RepoEmSuc.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            Dim p As New ReportParameter("sucursal", wucSucursales.sucursal)
            RepoEmSuc.LocalReport.SetParameters(p)

            RepoEmSuc.ServerReport.Refresh()

        Else
            Mens.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        tSuc.Text = wucSucursales.sucursal
    End Sub
End Class
