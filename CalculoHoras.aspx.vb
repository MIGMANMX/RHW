
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class CalculoHoras
    Inherits System.Web.UI.Page
    Public gvPos As Integer

    '''''''''Lineas para cambiar
    '88
    '108
    '243
    '280
    '785
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Lmsg.Text = "" : gvPos = 0
        Session("idz_e") = ""

        reporte.ServerReport.Refresh()

        wuc.Visible = False
        obs.Visible = False
        btnActualizar.Visible = False
    End Sub
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado

        idEmpleadoTX.Text = wucEmpleados2.idEmpleado.ToString

        Dim dsP As New ctiCatalogos
        Dim sr As String()

        sr = dsP.datosEmpleado(wucEmpleados2.idEmpleado)

        TxEmpleado.Text = sr(0)

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
        TxFechaFin.Text = DateAdd(DateInterval.Day, 14, FIngreso.SelectedDate).ToString("yyyy-MM-dd")
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

                Dim FechaFinal As Date
                Dim FechaFinal2 As Date
                FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
                FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")
                Dim ec As New ctiCalculo

                GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
                'GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(FechaFinal2), "yyyy-MM-dd"), 3)

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
        Dim FechaFinal As Date
        Dim FechaFinal2 As Date
        FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
        FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")

        GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
        'GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(FechaFinal2), "yyyy-MM-dd"), 3)

        ec = Nothing
        GridView1.DataBind()
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        HTotales()
        HTrabajadasCont()
        HorasExtras()
        DDescansados()
        DFestivosTrabajados()
        DDescansadosTrabajados()
    End Sub
    Public Sub HTotales()
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date

        'Variables para operaciones
        Dim HoraIn As Date
        Dim HoraFn As Date

        Dim HI As Integer
        Dim HF As Integer
        Dim tsDiferencia As Integer

        Dim Acum As Integer
        'Asignar los datos de los campos de texto a Variables
        FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        'Igualar Fecha de inicio a la Variable Global
        Fech = FIn

        'Inicio del ciclo de comparacion
        While (Fech <= FFn)
            'Borramos datos de las variables
            tsDiferencia = 0
            HI = 0
            HF = 0
            'Conexion y busqueda de registros
            Using dbC As New SqlConnection
                dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbC.Open()
                Dim cmd As New SqlCommand("Select inicio,fin,jornada From vm_Jornada where idempleado=@idempleado AND fecha BETWEEN '" & Fech.ToString("yyyy-dd-MM") & "'  AND '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' Order BY fecha", dbC)


                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("fecha", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                If rdr.Read Then
                    'Lectura de registros
                    ReDim dsP(3)
                    dsP(0) = rdr("inicio").ToString
                    dsP(1) = rdr("fin").ToString
                    dsP(2) = rdr("jornada").ToString
                    Dim cadena As String
                    cadena = dsP(2)

                    HoraIn = dsP(0)
                    HoraFn = dsP(1)
                    HI = Convert.ToInt32(HoraIn.ToString("HH"))
                    HF = Convert.ToInt32(HoraFn.ToString("HH"))

                    'Saber si es SUSPENSION/VACACIONES/INCAPACIDAD/DESCANSO
                    If Not cadena = "DESCANSO" Or cadena = "SUSPENSION" Or cadena = "VACACIONES" Or cadena = "INCAPACIDAD" Then
                        'Diferencia de horas
                        tsDiferencia = 0
                        tsDiferencia = HF - HI

                        If tsDiferencia < 1 Then
                            Dim HF_ As Integer = 0
                            HF_ = 24 + HF
                            tsDiferencia = HF_ - HI
                        End If
                        'Limpiar variables
                        HI = 0
                        HF = 0
                    Else
                        tsDiferencia = 0
                    End If
                End If
                rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
            End Using
            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")
            'Acumulador de horas
            Acum = tsDiferencia + Acum
            tsDiferencia = 0
        End While
        TxHtotales.Text = Acum
    End Sub
    Public Sub HTrabajadasCont()
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date
        'Variables para operaciones
        Dim HoraIn As Date
        Dim HoraFn As Date
        Dim HI As Integer
        Dim HF As Integer
        Dim MI As Integer
        Dim MF As Integer
        Dim tsDiferencia As Integer
        Dim Acum As Integer


        'Valores de fechas obtenidas

        'Asignar los datos de los campos de texto a Variables
        FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        'Igualar Fecha de inicio a la Variable Global
        Fech = FIn

        'Inicio del ciclo de comparacion
        While (Fech <= FFn)
            'Borramos datos de las variables
            tsDiferencia = 0
            HI = 0
            HF = 0
            'Conexion y busqueda de registros
            Using dbC As New SqlConnection
                dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbC.Open()

                'Hora de Entrada
                Dim cmd As New SqlCommand("Select TOP (1) * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' AND idempleado=@idempleado Order BY chec ASC ", dbC)
                'Dim cmd As New SqlCommand("Select TOP (1) * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec ASC", dbC)
                'Dim cmd As New SqlCommand("Select TOP (1) * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' AND idempleado=@idempleado Order BY chec ASC ", dbC)
                Dim cmd As New SqlCommand("Select TOP (1) * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec ASC", dbC)

                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("chec", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                While rdr.Read
                    'Lectura de registros
                    ReDim dsP(4)
                    dsP(0) = rdr("idchequeo").ToString
                    dsP(1) = rdr("chec").ToString
                    dsP(2) = rdr("tipo").ToString
                    dsP(3) = rdr("idincidencia").ToString
                    'Valor de fecha Inicial
                    'Consultar Hora

                    Dim acceso As New ctiCalculo
                    Dim datos() As String = acceso.datosHora(dsP(0))

                    HoraIn = datos(0)
                    'Valores de Horas y Minutos
                    HI = Convert.ToInt32(HoraIn.ToString("HH"))
                    MI = Convert.ToInt32(HoraIn.ToString("mm"))
                    'No descontar la hora si el retardo no es culpa del empleado
                    ' If dsP(3) = "" And dsP(3) <> 6 Then
                    'Despues de 05 min es una hora extra
                    If MI > 5 Then
                        HI = HI + 1
                    End If
                    '  End If

                End While
                rdr.Close() : rdr = Nothing


                'Hora de Salida
                'cmd.CommandText = "Select TOP (1) * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' AND idempleado=@idempleado Order BY chec DESC "
                cmd.CommandText = "Select TOP (1) * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec DESC "

                'cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                'cmd.Parameters.AddWithValue("chec", Fech)
                Dim rdr2 As SqlDataReader = cmd.ExecuteReader
                Dim dsP2 As String()
                While rdr2.Read
                    'Lectura de registros
                    ReDim dsP2(4)
                    dsP2(0) = rdr2("idchequeo").ToString
                    dsP2(1) = rdr2("chec").ToString
                    dsP2(2) = rdr2("tipo").ToString
                    ' dsP(3) = rdr("idincidencia").ToString
                    'Valor de fecha Final
                    'Consultar Hora
                    Dim acceso As New ctiCalculo
                    Dim datos() As String = acceso.datosHora(dsP2(0))

                    HoraFn = datos(0)
                    'Valores de Horas y Minutos
                    HF = Convert.ToInt32(HoraFn.ToString("HH"))
                    MF = Convert.ToInt32(HoraFn.ToString("mm"))
                    'Despues de 50m se puede checar la salida
                    If MF > 49 Then
                        HF = HF + 1
                    End If
                End While

                'Diferencia de horas
                tsDiferencia = 0
                tsDiferencia = HF - HI
                'Limpiar variables
                HI = 0
                HF = 0

                rdr2.Close() : rdr2 = Nothing
                cmd.Dispose() : dbC.Close() : dbC.Dispose()

            End Using
            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")
            'Acumulador de horas
            Acum = tsDiferencia + Acum
            tsDiferencia = 0
        End While
        TxHorasTrabajadas.Text = Acum
    End Sub
    'Public Sub HTrabajadas()
    '    'Datos de los campos de texto
    '    Dim FIn As Date
    '    Dim FFn As Date

    '    'Variable global
    '    Dim Fech As Date
    '    'Variables para operaciones
    '    Dim HoraIn As Date
    '    Dim HoraFn As Date
    '    Dim HI As Integer
    '    Dim HF As Integer
    '    Dim MI As Integer
    '    Dim MF As Integer
    '    Dim tsDiferencia As Integer
    '    Dim Acum As Integer

    '    'Asignar los datos de los campos de texto a Variables
    '    FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
    '    FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

    '    'Igualar Fecha de inicio a la Variable Global
    '    Fech = FIn

    '    'Inicio del ciclo de comparacion
    '    While (Fech <= FFn)
    '        'Borramos datos de las variables
    '        tsDiferencia = 0
    '        HI = 0
    '        HF = 0
    '        'Conexion y busqueda de registros
    '        Using dbC As New SqlConnection
    '            dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
    '            dbC.Open()
    '            'Dim cmd As New SqlCommand("Select * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' AND idempleado=@idempleado Order BY chec ", dbC)
    '            Dim cmd As New SqlCommand("Select * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec ", dbC)

    '            cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '            cmd.Parameters.AddWithValue("chec", Fech)
    '            Dim rdr As SqlDataReader = cmd.ExecuteReader
    '            Dim dsP As String()
    '            While rdr.Read
    '                'Lectura de registros
    '                ReDim dsP(3)
    '                dsP(0) = rdr("idchequeo").ToString
    '                dsP(1) = rdr("chec").ToString
    '                dsP(2) = rdr("tipo").ToString
    '                'Condicion de E/S
    '                If dsP(2) = "Entrada" Then
    '                    'Consultar Hora
    '                    Dim acceso As New ctiCalculo
    '                    Dim datos() As String = acceso.datosHora(dsP(0))

    '                    HoraIn = datos(0)

    '                    HI = Convert.ToInt32(HoraIn.ToString("HH"))
    '                    MI = Convert.ToInt32(HoraIn.ToString("mm"))

    '                    If MI > 5 Then
    '                        HI = HI + 1
    '                    End If
    '                ElseIf dsP(2) = "Salida" Then
    '                    'Consultar Hora
    '                    Dim acceso As New ctiCalculo
    '                    Dim datos() As String = acceso.datosHora(dsP(0))

    '                    HoraFn = datos(0)

    '                    HF = Convert.ToInt32(HoraFn.ToString("HH"))
    '                    MF = Convert.ToInt32(HoraFn.ToString("mm"))
    '                    If MF > 49 Then
    '                        HF = HF + 1
    '                    End If
    '                End If

    '            End While
    '            'Diferencia de horas
    '            tsDiferencia = 0
    '            tsDiferencia = HF - HI
    '            'Limpiar variables
    '            HI = 0
    '            HF = 0
    '            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
    '        End Using
    '        'Acumulador de fecha
    '        Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")
    '        'Acumulador de horas
    '        Acum = tsDiferencia + Acum
    '        tsDiferencia = 0
    '    End While
    '    TxHorasTrabajadas.Text = Acum
    'End Sub
    Public Sub DDescansados()
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date

        'Variables para operaciones
        Dim Acum As Integer = 0

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
                Dim cmd As New SqlCommand("Select jornada From vm_Jornada where idempleado=@idempleado AND fecha = '" & Fech.ToString("yyyy-dd-MM") & "' Order BY fecha", dbC)

                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("fecha", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                If rdr.Read Then
                    'Lectura de registros
                    ReDim dsP(1)

                    dsP(0) = rdr("jornada").ToString

                    'Saber si es DESCANSO
                    If dsP(0) = "DESCANSO" Then
                        Acum = Acum + 1
                    Else
                        Acum = Acum
                    End If
                End If
                rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
            End Using
            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")

        End While
        TxDDescasados.Text = Acum
        Acum = 0
    End Sub
    Public Sub DDescansadosTrabajados()
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date

        'Variables para operaciones
        Dim Acum As Integer = 0

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
                Dim cmd As New SqlCommand("Select jornada From vm_Jornada where idempleado=@idempleado AND fecha = '" & Fech.ToString("yyyy-dd-MM") & "' Order BY fecha", dbC)

                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("fecha", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                If rdr.Read Then
                    'Lectura de registros
                    ReDim dsP(1)

                    dsP(0) = rdr("jornada").ToString

                    'Formato para las fechas
                    Dim F As Date
                    F = Fech
                    F = Format(CDate(Fech), "yyyy-MM-dd")
                    Dim FF As Date
                    FF = DateAdd(DateInterval.Day, 1, F).ToString("yyyy-MM-dd")

                    'Saber si es DESCANSO
                    If dsP(0) = "DESCANSO" Then

                        'Consulta si trabajo segun la fecha
                        Dim acceso As New ctiCalculo
                        Dim datos() As String = acceso.ConsultaAsistencia(wucEmpleados2.idEmpleado, F, FF)
                        Dim dat As Integer = datos(0)
                        If datos(0) > 0 Then
                            'Acumular el dia
                            Acum = Acum + 1
                        Else
                            Acum = Acum
                        End If
                    End If
                End If
                rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
            End Using
            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")

        End While
        TextBox2.Text = Acum
        Acum = 0
    End Sub
    Public Sub DFestivosTrabajados()
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date

        'Variables para operaciones
        Dim Acum As Integer = 0

        'Asignar los datos de los campos de texto a Variables
        FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        'Fecha Fin
        Dim FFin As Date
        FFin = DateAdd(DateInterval.Day, 1, FFn).ToString("yyyy-MM-dd")
        'Igualar Fecha de inicio a la Variable Global
        Fech = FIn


        'Conexion y busqueda de registros
        Using dbC As New SqlConnection
                dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbC.Open()
            Dim cmd As New SqlCommand("Select dia,fecha From Dia_Festivo where fecha BETWEEN '" & FIn.ToString("yyyy-dd-MM") & "'  AND '" & FFn.ToString("yyyy-dd-MM") & "' Order BY fecha ", dbC)
            cmd.Parameters.AddWithValue("fecha", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
            'Lectura de registros
            While rdr.Read
                ReDim dsP(2)
                'Obtener valores
                dsP(0) = rdr("dia").ToString
                dsP(1) = rdr("fecha").ToString

                'Formato para las fechas
                Dim F As Date
                F = dsP(1)
                F = Format(CDate(dsP(1)), "yyyy-MM-dd")
                Dim FF As Date
                FF = DateAdd(DateInterval.Day, 1, F).ToString("yyyy-MM-dd")

                'Consulta si trabajo segun la fecha
                Dim acceso As New ctiCalculo
                Dim datos() As String = acceso.ConsultaAsistencia(wucEmpleados2.idEmpleado, F, FF)
                If datos(0) <> 0 Then
                    'Acumular el dia
                    Acum = Acum + 1
                Else
                    Acum = Acum
                End If

            End While
            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        End Using

        TextBox3.Text = Acum
        Acum = 0
    End Sub
    Public Sub HorasExtras()
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date

        'Variables para operaciones
        Dim Acum As Integer = 0

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
                Dim cmd As New SqlCommand("Select cantidad From Particulares where idempleado=@idempleado AND fecha = '" & Fech.ToString("yyyy-MM-dd") & "' AND tipo ='HExtras'Order BY fecha", dbC)

                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("fecha", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                If rdr.Read Then
                    'Lectura de registros
                    ReDim dsP(1)

                    dsP(0) = rdr("cantidad").ToString
                    Dim cant As Integer = 0
                    cant = dsP(0)
                    Acum = Acum + cant
                    cant = 0
                End If
                rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
            End Using
            'Acumulador de fecha
            Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")

        End While
        TxHorasExtras.Text = Acum
        Acum = 0
    End Sub
    Protected Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")

        reporte.ServerReport.Refresh()
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
                Dim Htotales As Integer = 0
                Dim HTrabajadas As Integer = 0
                Dim DDescansados As Integer = 0
                Dim HExtras As Integer = 0

                If TxHorasTrabajadas.Text <> "" Then
                    HTrabajadas = TxHorasTrabajadas.Text
                Else
                    HTrabajadas = 0
                End If

                If TxHtotales.Text <> "" Then
                    Htotales = TxHtotales.Text
                Else
                    Htotales = 0
                End If
                If TxDDescasados.Text <> "" Then
                    DDescansados = TxDDescasados.Text
                Else
                    DDescansados = 0
                End If
                If TxHorasExtras.Text <> "" Then
                    HExtras = TxHorasExtras.Text
                Else
                    HExtras = 0
                End If

                Dim p As New ReportParameter("Fech1", dt1)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech2", dtf)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("idempleado", idEmpleadoTX.Text)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("HTotales", Htotales)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("HTrabajadas", HTrabajadas)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("DDescansados", DDescansados)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("HExtras", HExtras)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("empleado", TxEmpleado.Text)
                reporte.LocalReport.SetParameters(p)

                reporte.ServerReport.Refresh()
            Else
                Lmsg.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Lmsg.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If

            Dim dsP As New ctiCalculo
            Dim datos() As String = dsP.datosCheqIncidencias(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                If datos(0) <> "" Then
                    wucIncidencias.idIncidencia = CInt(datos(0))
                    TxObservacion.Text = datos(1).ToString
                    TxId.Text = datos(2).ToString

                    grdSR.Text = e.CommandArgument.ToString
                    GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                    Dim gvp As New clsCTI
                    gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                    gvp = Nothing
                Else

                    wucIncidencias.idIncidencia = 0
                    TxObservacion.Text = datos(1).ToString
                    TxId.Text = datos(2).ToString

                    grdSR.Text = e.CommandArgument.ToString
                    GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                    Dim gvp As New clsCTI
                    gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                    gvp = Nothing

                End If
                obs.Visible = True
                wuc.Visible = True
                btnActualizar.Visible = True
            End If
        End If
    End Sub
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim idA As Integer = CInt(TxId.Text)
        If wucIncidencias.idIncidencia <> 0 Then
            Dim FechaFinal As Date
            Dim FechaFinal2 As Date
            FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
            FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")

            Dim ap As New ctiCalculo
            Dim r As String = ap.actualizarIncidencias(TxId.Text, wucIncidencias.idIncidencia, TxObservacion.Text)
            GridView1.DataSource = ap.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
            'GridView1.DataSource = ap.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(FechaFinal2), "yyyy-MM-dd"), 3)

            ap = Nothing
            GridView1.DataBind()
            If r.StartsWith("Error") Then
                Lmsg.CssClass = "error"
            Else
                Lmsg.CssClass = "correcto"

            End If
            Lmsg.Text = r


        Else
            Lmsg.Text = "Error : Falta seleccionar incidencia"
        End If
    End Sub
    Public Function RetardoNA() As Boolean
        'Dim Ret As Boolean = False

        ''Datos de los campos de texto
        'Dim FIn As Date
        'Dim FFn As Date
        ''Variable global
        'Dim Fech As Date

        ''Asignar los datos de los campos de texto a Variables
        'FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        'FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        ''Igualar Fecha de inicio a la Variable Global
        'Fech = FIn

        ''Inicio del ciclo de comparacion
        'While (Fech <= FFn)
        '    'Conexion y busqueda de registros
        '    Using dbC As New SqlConnection
        '        dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        '        dbC.Open()
        '        Dim cmd As New SqlCommand("Select inicio,fin,jornada From vm_Jornada where idempleado=@idempleado AND fecha BETWEEN '" & Fech.ToString("yyyy-MM-dd") & "'  AND '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' Order BY fecha", dbC)
        '        'Dim cmd As New SqlCommand("Select * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec ", dbC)

        '        cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
        '        cmd.Parameters.AddWithValue("fecha", Fech)
        '        Dim rdr As SqlDataReader = cmd.ExecuteReader
        '        Dim dsP As String()
        '        If rdr.Read Then
        '            'Lectura de registros
        '            ReDim dsP(3)
        '            dsP(0) = rdr("inicio").ToString
        '            dsP(1) = rdr("fin").ToString
        '            dsP(2) = rdr("jornada").ToString

        '            HoraIn = dsP(0)
        '            HoraFn = dsP(1)
        '            HI = Convert.ToInt32(HoraIn.ToString("HH"))
        '            HF = Convert.ToInt32(HoraFn.ToString("HH"))

        '            'Saber si es SUSPENSION/VACACIONES/INCAPACIDAD/DESCANSO
        '            If dsP(2) <> "SUSPENSION" Or dsP(2) <> "VACACIONES" Or dsP(2) <> "INCAPACIDAD" Or dsP(2) <> "DESCANSO" Then
        '                'Diferencia de horas
        '                tsDiferencia = 0
        '                tsDiferencia = HF - HI
        '                'Limpiar variables
        '                HI = 0
        '                HF = 0
        '                If tsDiferencia < 1 Then
        '                    tsDiferencia = 0
        '                End If
        '            Else
        '                tsDiferencia = 0
        '            End If
        '        End If
        '        rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        '    End Using
        '    'Acumulador de fecha
        '    Fech = DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd")
        '    'Acumulador de horas

        'End While


        'Return Ret
    End Function
End Class
