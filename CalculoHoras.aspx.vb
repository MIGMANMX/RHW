
Imports RHLogica

Partial Class CalculoHoras
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    'Datos de los campos de texto
    Dim FIn As Date
    Dim FFn As Date

    'Variable global
    Dim Fech As Date
    Dim FechCorta As String

    'Variables para operaciones
    Dim FechaIn As Date
    Dim FechaFn As Date
    Dim Tipo As Integer
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
        'HTrabajadas()
        'DDescansados()
        'DDescansadosTrabajados()
    End Sub
    Public Sub HTotales()
        'Asignar los datos de los campos de texto a Variables
        ' FIn = Convert.ToDateTime(TxFechaInicio.ToString("yyyy-MM-dd"))
        FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        'Igualar Fecha de inicio a la Variable Global
        Fech = FIn

        'Inicio del ciclo de comparacion
        While (Fech <= FFn)

            Dim acceso As New ctiCalculo
            Dim datos() As String = acceso.datosCalculo(wucEmpleados2.idEmpleado, Fech)
            TxHtotales.Text = datos(1)
            ' = FechCorta
            'datos(2) = Tipo

            'If Tipo = 1 And FechCorta.ToString("yyyy-MM-dd") = Fech Then
            '    datos(1) = FechaIn.ToString("HH:mm:ss.fff")
            'ElseIf Tipo = 4 And FechCorta.ToString("yyyy-MM-dd") = Fech Then
            '    datos(1) = FechaFn.ToString("HH:mm:ss.fff")
            'End If


            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")
        End While



        'Operacion de resta de totales
        'Dim Res As TimeSpan = FechaFn - FechaIn
        'TxHtotales.Text = Res.ToString
    End Sub
    Public Sub HTrabajadas()

    End Sub
    Public Sub DDescansados()

    End Sub
    Public Sub DDescansadosTrabajados()

    End Sub
End Class
