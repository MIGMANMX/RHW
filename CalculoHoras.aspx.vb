
Imports System.Data.SqlClient
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
    Dim HoraIn As Date
    Dim HoraFn As Date

    Dim HI As Integer
    Dim HF As Integer

    Dim MI As Integer
    Dim MF As Integer
    ' Dim tsDiferencia As TimeSpan = TimeSpan.Zero
    Dim tsDiferencia As Integer
    ' Dim Acum As TimeSpan = TimeSpan.Zero
    Dim Acum As Integer
    Dim con As Integer
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
        TxFechaFin.Text = DateAdd(DateInterval.Day, 16, FIngreso.SelectedDate).ToString("yyyy-MM-dd")
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
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        If wucEmpleados2.idEmpleado <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                Dim ec As New ctiCalculo
                GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(TxFechaFin.Text), "yyyy-dd-MM"))
                ' GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(TxFechaFin.Text), "yyyy-MM-dd"))

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
        FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        'Igualar Fecha de inicio a la Variable Global
        Fech = FIn

        'Inicio del ciclo de comparacion
        While (Fech <= FFn)
            'Conexion y busqueda de registros
            Using dbC As New SqlConnection
                dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbC.Open()
                Dim cmd As New SqlCommand("Select inicio,fin From vm_Jornada where fecha>=@fecha AND fecha <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' AND idempleado=@idempleado Order BY fecha ", dbC)
                'Dim cmd As New SqlCommand("Select * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec ", dbC)

                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("fecha", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                While rdr.Read
                    'Lectura de registros
                    ReDim dsP(3)
                    dsP(0) = rdr("inicio").ToString
                    dsP(1) = rdr("fin").ToString


                    HoraIn = dsP(0)
                    HoraFn = dsP(1)
                    HI = Convert.ToInt32(HoraIn.ToString("HH"))
                    HF = Convert.ToInt32(HoraFn.ToString("HH"))

                    'Diferencia de horas
                    tsDiferencia = 0
                    tsDiferencia = HF - HI
                End While

                'Limpiar variables
                HI = 0
                HF = 0

                rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
            End Using
            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")
            'Acumulador de horas
            Acum = tsDiferencia + Acum
            tsDiferencia = 0
        End While
        TxHorasTrabajadas.Text = Acum

    End Sub
    Public Sub HTrabajadas()
        'Asignar los datos de los campos de texto a Variables
        FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        'Igualar Fecha de inicio a la Variable Global
        Fech = FIn

        'Inicio del ciclo de comparacion
        While (Fech < FFn)
            'Borramos datos de las variables
            tsDiferencia = 0
            HI = 0
            HF = 0
            'Conexion y busqueda de registros
            Using dbC As New SqlConnection
                dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbC.Open()
                Dim cmd As New SqlCommand("Select * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' AND idempleado=@idempleado Order BY chec ", dbC)
                'Dim cmd As New SqlCommand("Select * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec ", dbC)

                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("chec", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                While rdr.Read
                    'Lectura de registros
                    ReDim dsP(3)
                    dsP(0) = rdr("idchequeo").ToString
                    dsP(1) = rdr("chec").ToString
                    dsP(2) = rdr("tipo").ToString
                    'Condicion de E/S
                    If dsP(2) = "Entrada" Then
                        'Consultar Hora
                        Dim acceso As New ctiCalculo
                        Dim datos() As String = acceso.datosHora(dsP(0))

                        HoraIn = datos(0)

                        HI = Convert.ToInt32(HoraIn.ToString("HH"))
                        MI = Convert.ToInt32(HoraIn.ToString("mm"))

                        If MI > 5 Then
                            HI = HI + 1
                        End If
                    ElseIf dsP(2) = "Salida" Then
                        'Consultar Hora
                        Dim acceso As New ctiCalculo
                        Dim datos() As String = acceso.datosHora(dsP(0))

                        HoraFn = datos(0)

                        HF = Convert.ToInt32(HoraFn.ToString("HH"))
                        MF = Convert.ToInt32(HoraFn.ToString("mm"))
                        If MF > 49 Then
                            HF = HF + 1
                        End If
                    End If

                End While
                'Diferencia de horas
                tsDiferencia = 0
                tsDiferencia = HF - HI
                'Limpiar variables
                HI = 0
                HF = 0
                'Insertar en tabla
                'Dim Hora As New ctiCalculo
                'Dim dato() As String = Hora.agregarHora(Format(CDate(Fech), "yyyy-MM-dd"), wucEmpleados2.idEmpleado, HoraIn.ToShortTimeString, HoraFn.ToShortTimeString, tsDiferencia)

                rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
            End Using
            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")
            'Acumulador de horas
            Acum = tsDiferencia + Acum
            tsDiferencia = 0
        End While
        TxHorasTrabajadas.Text = Acum
    End Sub
    Public Sub DDescansados()

    End Sub
    Public Sub DDescansadosTrabajados()

    End Sub


End Class
