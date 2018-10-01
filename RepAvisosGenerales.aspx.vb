
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class _RepAvisosGenerales
    Inherits System.Web.UI.Page

    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TFInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        FIngreso.Visible = False

        Ffin.Text = DateAdd(DateInterval.Day, 6, FIngreso.SelectedDate).ToString("yyyy-MM-dd")

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        FIngreso.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()

    End Sub

    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Dim c As CultureInfo = New CultureInfo("es-MX")

        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()

        If TFInicio.Text <> "" Then
                Dim dt1 As DateTime
                dt1 = Format(CDate(TFInicio.Text), "yyyy-MM-dd")
                Dim dt2 As DateTime
                dt2 = Format(CDate(Ffin.Text), "yyyy-MM-dd")

                Dim p As New ReportParameter("Fech1", dt1)
                ReportViewer1.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech2", dt2)
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
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FIngreso.Visible = True Then
            FIngreso.Visible = False
        ElseIf FIngreso.Visible = False Then
            FIngreso.Visible = True
        End If
    End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        If FFinal.Visible = True Then
            FFinal.Visible = False
        ElseIf FFinal.Visible = False Then
            FFinal.Visible = True
        End If
    End Sub
    Protected Sub FFinal_SelectionChanged(sender As Object, e As EventArgs) Handles FFinal.SelectionChanged
        Ffin.Text = FFinal.SelectedDate.ToString("yyyy-MM-dd")
        FFinal.Visible = False
    End Sub
End Class
