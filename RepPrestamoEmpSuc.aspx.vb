
Imports System.Globalization
Imports Microsoft.Reporting.WebForms

Partial Class RepPrestamoEmpSuc
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"

        End If
        Lmsg.Text = ""
        Session("idz_e") = ""


    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FIngreso.Visible = True Then
            FIngreso.Visible = False
        ElseIf FIngreso.Visible = False Then
            FIngreso.Visible = True
        End If
    End Sub
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TxFechaInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        FIngreso.Visible = False
        TxFechaFin.Text = DateAdd(DateInterval.Day, 13, FIngreso.SelectedDate).ToString("yyyy-MM-dd")
    End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        If FFinal.Visible = True Then
            FFinal.Visible = False
        ElseIf FFinal.Visible = False Then
            FFinal.Visible = True
        End If
    End Sub
    Protected Sub FFinal_SelectionChanged(sender As Object, e As EventArgs) Handles FFinal.SelectionChanged
        TxFechaFin.Text = FFinal.SelectedDate.ToString("yyyy-MM-dd")
        FFinal.Visible = False
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click

        tSuc.Text = ""
        TxFechaInicio.Text = ""
        TxFechaFin2.Text = ""
        TxFechaFin.Text = ""
        Lmsg.Text = ""
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")

        Repo.ServerReport.Refresh()

        If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then

            Dim dt1 As Date
            dt1 = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
            Dim dtf As Date
            dtf = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

            Dim dt2 As Date
            dt2 = DateAdd(DateInterval.Day, 1, dtf)
            dt2 = Format(CDate(dt2), "yyyy-MM-dd")
            TxFechaFin2.Text = dt2

            Dim p As New ReportParameter("Fech1", dt1)
            Repo.LocalReport.SetParameters(p)

            p = New ReportParameter("Fech2", dtf)
            Repo.LocalReport.SetParameters(p)


            Repo.ServerReport.Refresh()
        Else
            Lmsg.Text = "Error: Debes capturar una fecha"
        End If

    End Sub
End Class
