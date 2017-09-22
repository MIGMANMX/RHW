
Imports Microsoft.Reporting.WebForms
Imports System.Globalization


Partial Class _RepHorario
    Inherits System.Web.UI.Page
    'Dim dt1 As Date
    'Dim dt2 As Date
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TFInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")

        'Dim fe As String
        'fe = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        Ffin.Text = DateAdd(DateInterval.Day, 6, FIngreso.SelectedDate).ToString("yyyy-MM-dd")

        'dt1 = (FIngreso.SelectedDate.ToString("dd/MM/yyyy"))
        'dt2 = (DateAdd(DateInterval.Day, 7, fe))
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
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")
        ''DateTimeFormat es el objeto que contiene las especificaciones de formato
        ''para nuestra referencia cultural
        'Dim format As DateTimeFormatInfo = c.DateTimeFormat
        'Dim f As Date = New Date()
        ''el método ToString recibe como parámetro el formato de salida, y el formato de cultura
        'Response.Write(f.ToString("D", format))


        Mens.Text = ""
        ReportViewer1.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            If TFInicio.Text <> "" Then
                Dim dt1 As DateTime
                dt1 = Format(CDate(TFInicio.Text), "yyyy-MM-dd")
                Dim dt2 As DateTime
                dt2 = Format(CDate(Ffin.Text), "yyyy-MM-dd")

                ' Dim d1 As String = "2017-07-31"
                'Dim d2 As String = "2017-08-7"

                'TFInicio.Text = d1
                'Ffin.Text = d2


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
    Protected Sub Ffin_TextChanged(sender As Object, e As EventArgs) Handles Ffin.TextChanged

    End Sub
End Class
