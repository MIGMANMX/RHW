
Imports System.Globalization
Imports Microsoft.Reporting.WebForms

Partial Class RepIncidenciasT
    Inherits System.Web.UI.Page

    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Dim c As CultureInfo = New CultureInfo("es-MX")

        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()

        If TFInicio.Text <> "" Then
            Dim dt1 As DateTime
            dt1 = Format(CDate(TFInicio.Text), "yyyy-MM-dd")
            Dim dt2 As DateTime
            dt2 = Format(CDate(Ffin.Text), "yyyy-MM-dd")

            Dim p As New ReportParameter("Fecha1", dt1)
            ReportViewer1.LocalReport.SetParameters(p)

            p = New ReportParameter("Fecha2", dt2)
            ReportViewer1.LocalReport.SetParameters(p)

            ReportViewer1.ServerReport.Refresh()
        Else
            Mens.Text = "Error: Debes capturar una fecha"
        End If
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        TFInicio.Text = ""
        Ffin.Text = ""
        Mens.Text = ""
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        FIngreso.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()
    End Sub
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TFInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")


        Ffin.Text = DateAdd(DateInterval.Day, 6, FIngreso.SelectedDate).ToString("yyyy-MM-dd")

    End Sub
End Class
