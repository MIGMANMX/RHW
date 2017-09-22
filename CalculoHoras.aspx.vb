
Imports RHLogica

Partial Class CalculoHoras
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
        TxFechaFin.Text = DateAdd(DateInterval.Day, 13, FIngreso.SelectedDate).ToString("yyyy-MM-dd")
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If wucEmpleados2.idEmpleado <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                Dim ec As New ctiCalculo
                GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(TxFechaFin.Text), "yyyy-dd-MM"))
                ec = Nothing
                GridView1.DataBind()
            Else
                Lmsg.Text = "Error: Falta Capturar Fecha"
            End If
        Else
            Lmsg.Text = "Error: Falta Capturar Empleado"
        End If


    End Sub
    Protected Sub GridView1_PageIndexChanging1(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Dim ec As New ctiCalculo
        GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(TxFechaFin.Text), "yyyy-dd-MM"))
        ec = Nothing
        GridView1.DataBind()
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        HTotales()
        HTrabajadas()
        DDescansados()
        DDescansadosTrabajados()
    End Sub
    Public Sub HTotales()

    End Sub
    Public Sub HTrabajadas()

    End Sub
    Public Sub DDescansados()

    End Sub
    Public Sub DDescansadosTrabajados()

    End Sub
End Class
