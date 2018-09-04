Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class _EncuestaSalida
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Lmsg.Text = "" : gvPos = 0
        Session("idz_e") = ""

        Lmsg.Visible = True
        Lmsg0.Visible = False
    End Sub
    Protected Sub wucEmpleados2_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado

        idEmpleadoTX.Text = wucEmpleados2.idEmpleado.ToString
        'Dim dsP As New ctiCatalogos
        'Dim sr As String()
        'sr = dsP.datosEmpleado(wucEmpleados2.idEmpleado)
        'TxEmpleado.Text = sr(0)

    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim gvds As New ctiWUC
        Dim acceso As New ctiCatalogos
        wucEmpleados2.ddlDataSource(wucSucursales.idSucursal)
        gvds = Nothing
        wucEmpleados2.ddlAutoPostBack = True
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
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
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

        Dim gc As New ctiCatalogos
        Dim r() As String = gc.agregarEncuestaSalida(wucEmpleados2.idEmpleado, TxFechaInicio.Text, DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, DropDownList5.SelectedValue, DropDownList6.SelectedValue, DropDownList7.SelectedValue, DropDownList8.SelectedValue, DropDownList9.SelectedValue, CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, CheckBox4.Checked, CheckBox5.Checked, CheckBox6.Checked, CheckBox7.Checked, CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, CheckBox11.Checked, CheckBox12.Checked, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text)
        gc = Nothing

        If r(0).StartsWith("Error") Then
            Lmsg.CssClass = "error"
            Lmsg.Visible = False
            Lmsg0.Visible = True
            Lmsg0.Text = "Ya existe este registro"

            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosEncuestaSalida(wucEmpleados2.idEmpleado)
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                wucEmpleados2.idEmpleado = datos(0)
                TxFechaInicio.Text = datos(1)
                DropDownList1.SelectedValue = datos(2)
                DropDownList2.SelectedValue = datos(3)
                DropDownList3.SelectedValue = datos(4)
                DropDownList4.SelectedValue = datos(5)
                DropDownList5.SelectedValue = datos(6)
                DropDownList6.SelectedValue = datos(7)
                DropDownList7.SelectedValue = datos(8)
                DropDownList8.SelectedValue = datos(9)
                DropDownList9.SelectedValue = datos(10)
                CheckBox1.Checked = datos(11)
                CheckBox2.Checked = datos(12)
                CheckBox3.Checked = datos(13)
                CheckBox4.Checked = datos(14)
                CheckBox5.Checked = datos(15)
                CheckBox6.Checked = datos(16)
                CheckBox7.Checked = datos(17)
                CheckBox8.Checked = datos(18)
                CheckBox9.Checked = datos(19)
                CheckBox10.Checked = datos(20)
                CheckBox11.Checked = datos(21)
                CheckBox12.Checked = datos(22)
                TextBox1.Text = datos(23)
                TextBox2.Text = datos(24)
                TextBox3.Text = datos(25)
                TextBox4.Text = datos(26)
                TextBox5.Text = datos(27)
                'Reporte
                reporte()
            End If

        Else
            Lmsg.Visible = True
            Lmsg0.Visible = False
            Lmsg.CssClass = "correcto"
            Lmsg.Visible = True
            Lmsg0.Visible = False
            'Reporte
            reporte()
            'Limpiar
            wucEmpleados2.idEmpleado = 0
            TxFechaInicio.Text = ""
            DropDownList1.SelectedValue = 0
            DropDownList2.SelectedValue = 0
            DropDownList3.SelectedValue = 0
            DropDownList4.SelectedValue = 0
            DropDownList5.SelectedValue = 0
            DropDownList6.SelectedValue = 0
            DropDownList7.SelectedValue = 0
            DropDownList8.SelectedValue = 0
            DropDownList9.SelectedValue = 0
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
            CheckBox7.Checked = False
            CheckBox8.Checked = False
            CheckBox9.Checked = False
            CheckBox10.Checked = False
            CheckBox11.Checked = False
            CheckBox12.Checked = False
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""


        End If
        Lmsg.Text = r(0)
    End Sub
    Public Sub reporte()
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")

        Repo.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" Then

                'Dim dt1 As Date
                'dt1 = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")

                'Dim p As New ReportParameter("Fecha1", dt1)
                'Repo.LocalReport.SetParameters(p)

                'p = New ReportParameter("idempleado", idEmpleadoTX.Text)
                'Repo.LocalReport.SetParameters(p)

                'p = New ReportParameter("sucursal", wucSucursales.sucursal)
                'Repo.LocalReport.SetParameters(p)

                Dim p As New ReportParameter("empleado", TxEmpleado.Text)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("idempleado", wucEmpleados2.idEmpleado)
                Repo.LocalReport.SetParameters(p)

                Repo.ServerReport.Refresh()
            Else
                Lmsg.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Lmsg.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub

End Class
