
Imports Microsoft.Reporting.WebForms

Partial Class _RepHorario
    Inherits System.Web.UI.Page
    Dim dt1 As Date
    Dim dt2 As Date
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TFInicio.Text = FIngreso.SelectedDate.ToString("dd/MM/yyyy")

        Dim fe As New Date
        fe = Convert.ToDateTime(TFInicio.Text)

        dt1 = Convert.ToDateTime(FIngreso.SelectedDate.ToString("MM/dd/yyyy"))
        Ffin.Text = DateAdd(DateInterval.Day, 7, fe).ToString("dd/MM/yyyy")

        dt2 = Convert.ToDateTime(DateAdd(DateInterval.Day, 7, fe).ToString("MM/dd/yyyy"))
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        tSuc.Text = wucSucursales.sucursal
        FIngreso.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        'FIngreso1.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()
    End Sub
    'Protected Sub FIngreso1_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso1.SelectionChanged
    '    TFFinal0.Text = FIngreso1.SelectedDate.ToString("dd/MM/yyyy")
    'End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        tSuc.Text = wucSucursales.sucursal
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            If TFInicio.Text <> "" Then
                'Dim dt1 As Date
                'dt1 = (TFInicio.Text)
                'Dim dt2 As Date
                'dt2 = (TFFinal0.Text)

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
        'TFFinal0.Text = ""
        TFInicio.Text = ""
        Ffin.Text = ""
        Mens.Text = ""
    End Sub
End Class
