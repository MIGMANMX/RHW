
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class RepPrestamoEmp
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Lmsg.Text = ""
        Session("idz_e") = ""

        '''''''''''''Ocultar sucursales a Gerentes
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC
        'If wucEmpleados2.idEmpleado = 0 Then
        If datos(0) = 2 Then
            wucSucursales.idSucursal = datos(1)
            wucSucursales.Visible = False
            suc.Visible = False
            'wucEmpleados2.ddlDataSource(datos(1))
            'wucEmpleados2.ddlAutoPostBack = True
            'If IsNumeric(grdSR.Text) Then
            '    grdSR.Text = ""
        End If
        'End If
        'Else
        '    wucEmpleados2.ddlAutoPostBack = True
        'End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''

        tSuc.Text = wucSucursales.sucursal
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim gvds As New ctiWUC
        Dim acceso As New ctiCatalogos
        'wucEmpleados2.ddlDataSource(wucSucursales.idSucursal)
        'gvds = Nothing
        'wucEmpleados2.ddlAutoPostBack = True
        'If IsNumeric(grdSR.Text) Then
        '    grdSR.Text = ""
        'End If
        tSuc.Text = wucSucursales.sucursal
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
        wucSucursales.ddlAutoPostBack = 0
        tSuc.Text = ""
        TxFechaInicio.Text = ""
        TxFechaFin2.Text = ""
        TxFechaFin.Text = ""
        Lmsg.Text = ""
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")
        tSuc.Text = wucSucursales.sucursal
        Repo.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
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

                'p = New ReportParameter("idempleado", idEmpleadoTX.Text)
                'Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                Repo.LocalReport.SetParameters(p)

                'p = New ReportParameter("empleado", TxEmpleado.Text)
                'Repo.LocalReport.SetParameters(p)

                Repo.ServerReport.Refresh()
            Else
                Lmsg.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Lmsg.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub
End Class
