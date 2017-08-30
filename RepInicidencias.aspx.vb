
Imports Microsoft.Reporting.WebForms
Imports System.Globalization
Partial Class RepInicidencias
    Inherits System.Web.UI.Page

    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TFInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")


        Ffin.Text = DateAdd(DateInterval.Day, 6, FIngreso.SelectedDate).ToString("yyyy-MM-dd")

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        tSuc.Text = wucSucursales.sucursal
        FIngreso.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        tSuc.Text = wucSucursales.sucursal
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Dim c As CultureInfo = New CultureInfo("es-MX")

        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            If TFInicio.Text <> "" Then
                Dim dt1 As DateTime
                dt1 = Format(CDate(TFInicio.Text), "yyyy-MM-dd")
                Dim dt2 As DateTime
                dt2 = Format(CDate(Ffin.Text), "yyyy-MM-dd")

                Dim p As New ReportParameter("Fech1", dt1)
                ReportViewer1.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech2", dt2)
                ReportViewer1.LocalReport.SetParameters(p)

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                ReportViewer1.LocalReport.SetParameters(p)

                ReportViewer1.ServerReport.Refresh()
            Else
                Mens.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Mens.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        wucSucursales.ddlAutoPostBack = 0
        tSuc.Text = ""
        TFInicio.Text = ""
        Ffin.Text = ""
        Mens.Text = ""
    End Sub
End Class
