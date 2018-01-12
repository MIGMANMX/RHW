
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class RepCalculoHoras
    Inherits System.Web.UI.Page
    Public gvPos As Integer

    '''''''''Lineas para cambiar
    '88
    '108
    '243
    '280
    '785
    '919
    '920
    '1058
    '1059
    '1086
    '1087
    '1107
    '1137
    '1138
    '1156
    '1157
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Lmsg.Text = "" : gvPos = 0
        Session("idz_e") = ""

        '''''''''''''Ocultar sucursales a Gerentes
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC
        If wucEmpleados2.idEmpleado = 0 Then
            If datos(0) = 2 Then
                wucSucursales.idSucursal = datos(1)
                wucSucursales.Visible = False
                suc.Visible = False
                wucEmpleados2.ddlDataSource(datos(1))
                wucEmpleados2.ddlAutoPostBack = True
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""
                End If
            End If
        Else
            wucEmpleados2.ddlAutoPostBack = True
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado

        idEmpleadoTX.Text = wucEmpleados2.idEmpleado.ToString

        Dim dsP As New ctiCatalogos
        Dim sr As String()

        sr = dsP.datosEmpleado(wucEmpleados2.idEmpleado)

        TxEmpleado.Text = sr(0)
        GridView1.Visible = False
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
                '''''''''Botones
                HTotales()
                'HTrabajadasCont()
                HorasExtras()
                DDescansados()
                DFestivosTrabajados()
                DDescansadosTrabajados()
                'CalculoHrsTrab()
                CalculoHoras()
                '''''''''''
                Dim FechaFinal As Date
                Dim FechaFinal2 As Date
                FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
                FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")
                Dim ec As New ctiCalculo

                'GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
                GridView1.DataSource = ec.gvChequeo()
                GridView1.Visible = True
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

        'GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
        GridView1.DataSource = ec.gvChequeo()

        ec = Nothing
        GridView1.DataBind()
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        HTotales()
        'HTrabajadasCont()
        HorasExtras()
        DDescansados()
        DFestivosTrabajados()
        DDescansadosTrabajados()
        'CalculoHrsTrab()
        CalculoHoras()
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
                ' Dim cmd As New SqlCommand("Select inicio,fin,jornada From vm_Jornada where idempleado=@idempleado AND fecha BETWEEN '" & Fech.ToString("yyyy-dd-MM") & "'  AND '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-dd-MM") & "' Order BY fecha", dbC)
                Dim cmd As New SqlCommand("Select inicio,fin,jornada From vm_Jornada where idempleado=@idempleado AND fecha BETWEEN '" & Fech.ToString("yyyy-MM-dd") & "'  AND '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' Order BY fecha", dbC)

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
                'Dim cmd As New SqlCommand("Select jornada From vm_Jornada where idempleado=@idempleado AND fecha = '" & Fech.ToString("yyyy-dd-MM") & "' Order BY fecha", dbC)
                Dim cmd As New SqlCommand("Select jornada From vm_Jornada where idempleado=@idempleado AND fecha = '" & Fech.ToString("yyyy-MM-dd") & "' Order BY fecha", dbC)

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
                'Dim cmd As New SqlCommand("Select jornada From vm_Jornada where idempleado=@idempleado AND fecha = '" & Fech.ToString("yyyy-dd-MM") & "' Order BY fecha", dbC)
                Dim cmd As New SqlCommand("Select jornada From vm_Jornada where idempleado=@idempleado AND fecha = '" & Fech.ToString("yyyy-MM-dd") & "' Order BY fecha", dbC)

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

                    Dim FechaF As Date
                    FechaF = Format(CDate(FF), "yyyy-MM-dd")
                    'Saber si es DESCANSO
                    If dsP(0) = "DESCANSO" Then

                        'Consulta si trabajo segun la fecha
                        Dim acceso As New ctiCalculo
                        Dim datos() As String = acceso.ConsultaAsistencia(wucEmpleados2.idEmpleado, F.ToString("yyyy-MM-dd"), FechaF.ToString("yyyy-MM-dd"))
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
            'Dim cmd As New SqlCommand("Select dia,fecha From Dia_Festivo where fecha BETWEEN '" & FIn.ToString("yyyy-dd-MM") & "'  AND '" & FFn.ToString("yyyy-dd-MM") & "' Order BY fecha ", dbC)
            Dim cmd As New SqlCommand("Select dia,fecha From Dia_Festivo where fecha BETWEEN '" & FIn.ToString("yyyy-MM-dd") & "'  AND '" & FFn.ToString("yyyy-MM-dd") & "' Order BY fecha ", dbC)
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
                Dim datos() As String = acceso.ConsultaAsistencia(wucEmpleados2.idEmpleado, Format(CDate(F), "yyyy/MM/dd"), Format(CDate(FF), "yyyy/MM/dd"))
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
        Dim c As CultureInfo = New CultureInfo("es-MX")
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
                Dim DT As Integer = 0
                Dim FT As Integer = 0

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

                If TextBox2.Text <> "" Then
                    DT = TextBox2.Text
                Else
                    DT = 0
                End If
                If TextBox3.Text <> "" Then
                    FT = TextBox3.Text
                Else
                    FT = 0
                End If
                Dim p As New ReportParameter("Fecha1", dt1.ToString("yyyy-MM-dd"))
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fecha2", dtf.ToString("yyyy-MM-dd"))
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("empleado", TxEmpleado.Text)
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("HTotales", Htotales)
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("HTrabajadas", HTrabajadas)
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("DDescansados", DDescansados)
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("HExtras", HExtras)
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("DDTrabajados", DT)
                repo.LocalReport.SetParameters(p)

                p = New ReportParameter("DFTrabajados", FT)
                repo.LocalReport.SetParameters(p)

                repo.ServerReport.Refresh()
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

                    TxId.Text = datos(2).ToString

                    grdSR.Text = e.CommandArgument.ToString
                    GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                    Dim gvp As New clsCTI
                    gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                    gvp = Nothing
                Else


                    TxId.Text = datos(2).ToString

                    grdSR.Text = e.CommandArgument.ToString
                    GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                    Dim gvp As New clsCTI
                    gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                    gvp = Nothing

                End If

            End If
        End If
    End Sub
    'Public Sub CalculoHrsTrab()
    '    Dim FIn, FFn, Checada As Date
    '    Dim HEntrada, HSalida, ChqEnt, ChqSal As String
    '    Dim FHEntrada, FHEntrada2, FHPuntual, FinTolerancia, FHSalida, FHSalida2, FHSalida3, HoraEnt, Salida, IniTolerancia, IniDia, SigDia, IniDia2, FFnCierre As String
    '    Dim calc, ent, sal, acum, entrada As Integer
    '    Dim cerrador As Boolean
    '    Dim puntualidad, hrstarde, minuto As Integer
    '    Dim incidencia, idjornada, detalle As String
    '    Dim revisar, clockin, clockout As String

    '    'FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
    '    'FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

    '    Dim dbc As New SqlConnection
    '    dbc.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
    '    dbc.Open()
    '    Dim dbc2 As New SqlConnection
    '    dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
    '    dbc2.Open()
    '    Dim cmd2 As New SqlCommand("", dbc2)

    '    'Dim rdr2 As SqlDataReader

    '    'LIMPIAR TABLA TEMPORAL
    '    cmd2 = New SqlCommand("DELETE FROM Temp_Calculo", dbc2)
    '    cmd2.ExecuteNonQuery()

    '    Dim cmd As New SqlCommand("SELECT * FROM vm_Jornada WHERE fecha >=@FIn AND fecha <=@FFn  AND idempleado=@idempleado Order BY fecha ASC", dbc)
    '    cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '    cmd.Parameters.AddWithValue("FIn", Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"))
    '    cmd.Parameters.AddWithValue("FFn", Format(CDate(TxFechaFin.Text), "yyyy-MM-dd"))
    '    Dim rdr As SqlDataReader = cmd.ExecuteReader

    '    While rdr.Read
    '        puntualidad = 0
    '        calc = 0
    '        'Filtar Jornadas
    '        idjornada = rdr("idjornada").ToString
    '        'Guardar si jornada es de cierre o no
    '        cerrador = rdr("cierre").ToString
    '        detalle = ""

    '        'If Not CBool(rdr("ausente")) Then
    '        'Calculos de Puntualidad, tolerancia y retardo
    '        HEntrada = Left(rdr("inicio").ToString, 2) - 1
    '        HoraEnt = Left(rdr("inicio").ToString, 2)
    '        FHEntrada = Left(rdr("fecha").ToString, 11) + rdr("checkini").ToString
    '        'FHEntrada = Left(rdr("fecha").ToString, 11) + HEntrada + ":45:00"
    '        FHPuntual = Left(rdr("fecha").ToString, 11) + HoraEnt + ":00:59"
    '        IniTolerancia = Left(rdr("fecha").ToString, 11) + HoraEnt + ":01:00"
    '        FinTolerancia = Left(rdr("fecha").ToString, 11) + rdr("tolerancia").ToString
    '        FHEntrada2 = Left(rdr("fecha").ToString, 11) + "06:00:00"

    '        'Cálculos de salida
    '        'Salida = Left(rdr("check").ToString, 2) - 1
    '        FHSalida = Left(rdr("fecha").ToString, 11) + rdr("checkfin").ToString
    '        FHSalida2 = Left(rdr("fecha").ToString, 11) + Left(rdr("fin").ToString, 8)
    '        FHSalida3 = Left(rdr("fecha").ToString, 11) + "23:59:59"

    '        'Cálculos de salida CERRADOR
    '        SigDia = DateAdd(DateInterval.Day, 1, rdr("fecha"))

    '        FFnCierre = SigDia + " " + rdr("checkfin").ToString

    '        IniDia = SigDia + " 05:59:00"
    '        IniDia2 = SigDia + " 00:00:01"
    '        'End If
    '        sal = 0 : ent = 0
    '        clockin = "" : clockout = ""
    '        'If idjornada <> "21" And idjornada <> "22" And idjornada <> "24" And idjornada <> "36" And idjornada <> "9" Then

    '        '********************** C E R R A D O R *************************************************************************

    '        If cerrador Then
    '            If Not CBool(rdr("ausente")) Then
    '                entrada = 0
    '                'REVISAR SI LLEGO PUNTUAL
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHPuntual), "yyyy-MM-dd HH:mm:ss"))
    '                Dim rdr2 As SqlDataReader = cmd2.ExecuteReader

    '                If rdr2.Read Then
    '                    entrada = 1
    '                    puntualidad = 1
    '                    detalle = "PUNTUALIDAD"
    '                    'Obtener la hora del campo chec
    '                    Checada = rdr2("chec").ToString
    '                    clockin = CStr(Checada)
    '                    ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                    'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                    If ChqEnt.Contains(":") Then
    '                        'Eliminar ":" cuando son horas menores a 10
    '                        ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                    Else
    '                        ent = CInt(ChqEnt)
    '                    End If
    '                    If ent = HEntrada Then
    '                        ent = ent + 1
    '                    End If
    '                    'Lmsg.Text = "Puntual"
    '                Else
    '                    'REVISAR SI LLEGO EN TOLERANCIA
    '                    rdr2.Close()
    '                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                    cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                    cmd2.Parameters.AddWithValue("FHin", Format(CDate(IniTolerancia), "yyyy-MM-dd HH:mm:ss"))
    '                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
    '                    rdr2 = cmd2.ExecuteReader
    '                    If rdr2.Read Then
    '                        entrada = 1
    '                        puntualidad = 0
    '                        detalle = "ASISTENCIA"
    '                        'Obtener la hora del campo chec
    '                        Checada = rdr2("chec").ToString
    '                        clockin = CStr(Checada)
    '                        ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                        'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                        If ChqEnt.Contains(":") Then
    '                            'Eliminar ":" cuando son horas menores a 10
    '                            ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                        Else
    '                            ent = CInt(ChqEnt)
    '                        End If
    '                        'Lmsg.Text = ent
    '                    Else
    '                        'TRAER LA PRIMER CHECADA SI LLEGO TARDE
    '                        rdr2.Close()
    '                        puntualidad = 0
    '                        'detalle = "ASISTENCIA"
    '                        cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                        cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                        cmd2.Parameters.AddWithValue("FHin", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
    '                        cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida), "yyyy-MM-dd HH:mm:ss"))
    '                        rdr2 = cmd2.ExecuteReader
    '                        If rdr2.Read Then
    '                            entrada = 1
    '                            puntualidad = 0
    '                            detalle = "ASISTENCIA"
    '                            'Obtener la hora del campo chec3
    '                            Checada = rdr2("chec").ToString
    '                            clockin = CStr(Checada)
    '                            ChqEnt = rdr2("chec").ToString.Substring(0, 2)

    '                            'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                            'revisar = CStr(Checada)
    '                            If ChqEnt.Contains(":") Then
    '                                'Eliminar ":" cuando son horas menores a 10
    '                                ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                            Else
    '                                ent = CInt(ChqEnt)
    '                            End If

    '                            'Descontarle las horas que llega tarde
    '                            If CInt(HoraEnt) = ent Then
    '                                ent = ent + 1
    '                            Else
    '                                hrstarde = CInt(ChqEnt) - CInt(HoraEnt)
    '                                ent = ent + hrstarde
    '                            End If
    '                        Else
    '                            rdr2.Close()
    '                            ' ///// TRAER PRIMER CHECADA AUNQUE NO COINCIDA CON SU HORARIO

    '                            cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                            cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                            cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada2), "yyyy-MM-dd HH:mm:ss"))
    '                            cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                            'Dim rdr2 As SqlDataReader = cmd2.ExecuteReader
    '                            rdr2 = cmd2.ExecuteReader
    '                            Dim minutos As Date
    '                            If rdr2.Read Then
    '                                puntualidad = 0
    '                                detalle = "ASISTENCIA"
    '                                'Obtener la hora del campo chec3
    '                                Checada = rdr2("chec").ToString
    '                                clockin = CStr(Checada)
    '                                ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                                'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                                If ChqEnt.Contains(":") Then
    '                                    'Eliminar ":" cuando son horas menores a 10
    '                                    ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                                Else
    '                                    ent = CInt(ChqEnt)
    '                                End If

    '                                minutos = CDate(clockin)

    '                                If minutos.Minute >= 6 Then
    '                                    ent = ent + 1
    '                                End If
    '                            End If
    '                            rdr2.Close()
    '                        End If
    '                    End If
    '                End If
    '                rdr2.Close()

    '                'REVISAR SALIDA. Desde su horario de salida hasta las 6 am
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHIn AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FFnCierre), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(IniDia), "yyyy-MM-dd HH:mm:ss"))
    '                rdr2 = cmd2.ExecuteReader
    '                If rdr2.Read Then
    '                    'Agregarle una hora a su hora de salida.
    '                    'ChqSal = rdr2("chec").ToString.Substring(11, 2)

    '                    'Obtener la hora del campo chec3
    '                    Checada = rdr2("chec").ToString
    '                    clockout = CStr(Checada)
    '                    ChqSal = rdr2("chec").ToString.Substring(0, 2)
    '                    revisar = rdr2("chec").ToString
    '                    If ChqSal.Contains(":") Then
    '                        sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                    Else
    '                        sal = CInt(ChqSal)
    '                    End If
    '                    If sal = 0 Then sal = 1

    '                Else
    '                    rdr2.Close()

    '                    'REVISAR SALIDA ANTES DE SU HORARIO DE SALIDA. Desde 00:00:01 hasta su Horario de salida.
    '                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                    cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                    cmd2.Parameters.AddWithValue("FHIn", Format(CDate(IniDia2), "yyyy-MM-dd HH:mm:ss"))
    '                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FFnCierre), "yyyy-MM-dd HH:mm:ss"))
    '                    rdr2 = cmd2.ExecuteReader
    '                    If rdr2.Read Then

    '                        'Obtener la hora del campo chec3
    '                        Checada = rdr2("chec").ToString
    '                        clockout = CStr(Checada)
    '                        ChqSal = rdr2("chec").ToString.Substring(0, 2)

    '                        If ChqSal.Contains(":") Then
    '                            sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                        Else
    '                            sal = CInt(ChqSal)
    '                        End If
    '                        'sal = sal - 1
    '                        If sal = 0 Then sal = 1
    '                    Else
    '                        rdr2.Close()

    '                        'REVISAR SALIDA. Traer última checada después de su hora de salida.
    '                        cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                        cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                        cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHSalida2), "yyyy-MM-dd HH:mm:ss"))
    '                        cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                        rdr2 = cmd2.ExecuteReader
    '                        If rdr2.Read Then

    '                            'Obtener la hora del campo chec3
    '                            Checada = rdr2("chec").ToString
    '                            clockout = CStr(Checada)
    '                            ChqSal = rdr2("chec").ToString.Substring(0, 2)

    '                            If ChqSal.Contains(":") Then
    '                                sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                            Else
    '                                sal = CInt(ChqSal)
    '                            End If
    '                        End If
    '                    End If

    '                End If
    '                rdr2.Close()

    '                If sal = 0 Or ent = 0 Then
    '                    calc = 0
    '                Else
    '                    ' Si es cerrador se le agrega una hora
    '                    calc = (24 - ent) + sal + 1
    '                End If

    '            Else
    '                '//////////     CALCULAR HORAS TRABAJADAS CUANDO NO COINCIDE SU HORARIO CON LAS CHECADAS     ///////////

    '                'SACAR LA PRIMER CHECADA AUNQUE NO COINCIDA CON SU HORARIO
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada2), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                Dim rdr2 As SqlDataReader = cmd2.ExecuteReader
    '                Dim minutos As Date
    '                If rdr2.Read Then
    '                    puntualidad = 0
    '                    'Obtener la hora del campo chec3
    '                    Checada = rdr2("chec").ToString
    '                    clockin = CStr(Checada)
    '                    ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                    'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                    If ChqEnt.Contains(":") Then
    '                        'Eliminar ":" cuando son horas menores a 10
    '                        ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                    Else
    '                        ent = CInt(ChqEnt)
    '                    End If

    '                    minutos = CDate(clockin)

    '                    If minutos.Minute >= 6 Then
    '                        ent = ent + 1
    '                    End If
    '                End If
    '                rdr2.Close()

    '                ' TRAER ULTIMA CHECADA DEL DIA
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec DESC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHEntrada), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                rdr2 = cmd2.ExecuteReader
    '                If rdr2.Read Then
    '                    Checada = rdr2("chec").ToString
    '                    clockout = CStr(Checada)
    '                    ChqSal = rdr2("chec").ToString.Substring(0, 2)
    '                    If ChqSal.Contains(":") Then
    '                        sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                    Else
    '                        sal = CInt(ChqSal)
    '                    End If
    '                Else
    '                    rdr2.Close()

    '                    ' BUSCAR ULTIMA CHECADA DEL SIGUIENTE DIA HASTA ANTES DE LAS 6 AM
    '                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec DESC", dbc2)
    '                    cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                    cmd2.Parameters.AddWithValue("FHIn", Format(CDate(IniDia2), "yyyy-MM-dd HH:mm:ss"))
    '                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(IniDia), "yyyy-MM-dd HH:mm:ss"))
    '                    rdr2 = cmd2.ExecuteReader
    '                    If rdr2.Read Then
    '                        Checada = rdr2("chec").ToString
    '                        clockout = CStr(Checada)
    '                        ChqSal = rdr2("chec").ToString.Substring(0, 2)
    '                        If ChqSal.Contains(":") Then
    '                            sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                        Else
    '                            sal = CInt(ChqSal)
    '                        End If
    '                    End If
    '                End If
    '                rdr2.Close()
    '            End If

    '        Else

    '            '********************** N O R M A L *************************************************************************

    '            If Not CBool(rdr("ausente")) Then
    '                'entrada = 0
    '                'REVISAR SI LLEGO PUNTUAL
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHPuntual), "yyyy-MM-dd HH:mm:ss"))
    '                Dim rdr2 As SqlDataReader = cmd2.ExecuteReader

    '                If rdr2.Read Then
    '                    'entrada = 1
    '                    puntualidad = 1
    '                    detalle = "PUNTUALIDAD"
    '                    'Obtener la hora del campo chec
    '                    Checada = rdr2("chec").ToString
    '                    clockin = CStr(Checada)
    '                    ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                    'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                    If ChqEnt.Contains(":") Then
    '                        'Eliminar ":" cuando son horas menores a 10
    '                        ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                    Else
    '                        ent = CInt(ChqEnt)
    '                    End If
    '                    If ent = HEntrada Then
    '                        ent = ent + 1
    '                    End If
    '                Else

    '                    'REVISAR SI LLEGO EN TOLERANCIA
    '                    rdr2.Close()
    '                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                    cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                    cmd2.Parameters.AddWithValue("FHin", Format(CDate(IniTolerancia), "yyyy-MM-dd HH:mm:ss"))
    '                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
    '                    rdr2 = cmd2.ExecuteReader
    '                    If rdr2.Read Then
    '                        puntualidad = 0
    '                        detalle = "ASISTENCIA"
    '                        'Obtener la hora del campo chec
    '                        Checada = rdr2("chec").ToString
    '                        clockin = CStr(Checada)
    '                        ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                        'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                        If ChqEnt.Contains(":") Then
    '                            'Eliminar ":" cuando son horas menores a 10
    '                            ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                        Else
    '                            ent = CInt(ChqEnt)
    '                        End If
    '                        'Lmsg.Text = ent
    '                    Else

    '                        'SACAR LA PRIMER CHECADA SI LLEGO TARDE
    '                        rdr2.Close()
    '                        cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                        cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                        cmd2.Parameters.AddWithValue("FHin", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
    '                        cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida), "yyyy-MM-dd HH:mm:ss"))
    '                        rdr2 = cmd2.ExecuteReader
    '                        If rdr2.Read Then
    '                            'entrada = 1
    '                            puntualidad = 0
    '                            detalle = "ASISTENCIA"
    '                            'Obtener la hora del campo chec3
    '                            Checada = rdr2("chec").ToString
    '                            clockin = CStr(Checada)
    '                            ChqEnt = rdr2("chec").ToString.Substring(0, 2)

    '                            'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                            revisar = CStr(Checada)
    '                            If ChqEnt.Contains(":") Then
    '                                'Eliminar ":" cuando son horas menores a 10
    '                                ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                            Else
    '                                ent = CInt(ChqEnt)
    '                            End If

    '                            'Descontarle las horas que llega tarde
    '                            If CInt(HoraEnt) = ent Then
    '                                ent = ent + 1
    '                            Else
    '                                hrstarde = CInt(ChqEnt) - CInt(HoraEnt)
    '                                ent = ent + hrstarde
    '                            End If
    '                        Else
    '                            rdr2.Close()
    '                            'SACAR LA PRIMER CHECADA AUNQUE NO COINCIDA CON SU HORARIO
    '                            cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                            cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                            cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada2), "yyyy-MM-dd HH:mm:ss"))
    '                            cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                            rdr2 = cmd2.ExecuteReader
    '                            Dim minutos As Date
    '                            If rdr2.Read Then
    '                                puntualidad = 0
    '                                detalle = "ASISTENCIA"
    '                                'Obtener la hora del campo chec3
    '                                Checada = rdr2("chec").ToString
    '                                clockin = CStr(Checada)
    '                                ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                                'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                                If ChqEnt.Contains(":") Then
    '                                    'Eliminar ":" cuando son horas menores a 10
    '                                    ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                                Else
    '                                    ent = CInt(ChqEnt)
    '                                End If

    '                                minutos = CDate(clockin)

    '                                If minutos.Minute >= 6 Then
    '                                    ent = ent + 1
    '                                End If
    '                            End If
    '                            rdr2.Close()
    '                        End If

    '                    End If
    '                End If
    '                rdr2.Close()

    '                'REVISAR SALIDA. Traer checada de su salida, hasta 23:59:59
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec DESC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHSalida2), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                rdr2 = cmd2.ExecuteReader
    '                If rdr2.Read Then
    '                    'Agregarle una hora a su hora de salida.
    '                    Checada = rdr2("chec").ToString
    '                    clockout = CStr(Checada)
    '                    ChqSal = rdr2("chec").ToString.Substring(0, 2)
    '                    'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
    '                    'ChqEnt = CStr(Checada).Substring(11, 2)
    '                    If ChqSal.Contains(":") Then
    '                        sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                    Else
    '                        sal = CInt(ChqSal)
    '                    End If
    '                    'sal = sal + 1
    '                Else
    '                    rdr2.Close()

    '                    ' TRAER ULTIMA CHECADA DEL DIA
    '                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec DESC", dbc2)
    '                    cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                    cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHEntrada), "yyyy-MM-dd HH:mm:ss"))
    '                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                    rdr2 = cmd2.ExecuteReader
    '                    If rdr2.Read Then
    '                        Checada = rdr2("chec").ToString
    '                        clockout = CStr(Checada)
    '                        ChqSal = rdr2("chec").ToString.Substring(0, 2)
    '                        If ChqSal.Contains(":") Then
    '                            sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                        Else
    '                            sal = CInt(ChqSal)
    '                        End If
    '                    End If
    '                End If
    '                rdr2.Close()

    '            Else
    '                '//////////     CALCULAR HORAS TRABAJADAS CUANDO NO COINCIDE SU HORARIO CON LAS CHECADAS     ///////////

    '                'SACAR LA PRIMER CHECADA AUNQUE NO COINCIDA CON SU HORARIO
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada2), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                Dim rdr2 As SqlDataReader = cmd2.ExecuteReader
    '                Dim minutos As Date
    '                If rdr2.Read Then
    '                    puntualidad = 0
    '                    'Obtener la hora del campo chec3
    '                    Checada = rdr2("chec").ToString
    '                    clockin = CStr(Checada)
    '                    ChqEnt = rdr2("chec").ToString.Substring(0, 2)
    '                    'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
    '                    If ChqEnt.Contains(":") Then
    '                        'Eliminar ":" cuando son horas menores a 10
    '                        ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
    '                    Else
    '                        ent = CInt(ChqEnt)
    '                    End If

    '                    minutos = CDate(clockin)

    '                    If minutos.Minute >= 6 Then
    '                        ent = ent + 1
    '                    End If
    '                End If
    '                rdr2.Close()

    '                ' TRAER ULTIMA CHECADA DEL DIA
    '                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec DESC", dbc2)
    '                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
    '                cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHEntrada), "yyyy-MM-dd HH:mm:ss"))
    '                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
    '                rdr2 = cmd2.ExecuteReader
    '                If rdr2.Read Then
    '                    Checada = rdr2("chec").ToString
    '                    clockout = CStr(Checada)
    '                    ChqSal = rdr2("chec").ToString.Substring(0, 2)
    '                    If ChqSal.Contains(":") Then
    '                        sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
    '                    Else
    '                        sal = CInt(ChqSal)
    '                    End If
    '                End If
    '                rdr2.Close()
    '            End If
    '            If sal = 0 Or ent = 0 Then
    '                calc = 0
    '            Else
    '                calc = sal - ent
    '            End If
    '        End If

    '        acum = acum + calc

    '        If detalle = "" Then
    '            If Not CBool(rdr("ausente")) Then
    '                detalle = "FALTA"
    '            Else
    '                detalle = rdr("jornada").ToString
    '            End If
    '        End If

    '        Dim horario As String
    '        horario = rdr("jornada").ToString

    '        cmd2 = New SqlCommand("INSERT INTO Temp_Calculo(fecha,entrada,salida,hrstrab,puntualidad,detalle,clockin,clockout,horario) VALUES(@fecha," & CStr(ent) & "," & CStr(sal) & "," & CStr(calc) & "," & CStr(puntualidad) & ",'" & detalle & "', '" & clockin & "','" & clockout & "', '" & horario & "')", dbc2)
    '        Dim Fech As Date
    '        Fech = rdr("fecha").ToString
    '        cmd2.Parameters.AddWithValue("fecha", Fech.ToString("yyyy-MM-dd"))
    '        cmd2.ExecuteNonQuery()
    '        cmd2.Dispose()

    '    End While

    '    rdr.Close()
    '    'Lmsg.Text = minuto
    '    TxHorasTrabajadas.Text = acum
    'End Sub
    Public Sub CalculoHoras()
        Dim dbc As New SqlConnection
        dbc.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc.Open()
        Dim dbc2 As New SqlConnection
        dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc2.Open()
        Dim cmd2 As New SqlCommand("", dbc2)

        'LIMPIAR TABLA TEMPORAL
        cmd2 = New SqlCommand("DELETE FROM Temp_Calculo", dbc2)
        cmd2.ExecuteNonQuery()

        Dim FechaInicial, FechaFinal, Fecha As Date
        FechaInicial = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FechaFinal = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")
        Fecha = FechaInicial
        Dim IniDiaN, FinDiaC, FinDiaN, IniDiaSig, SigDia, IniHorario, FinHorario, Checada As Date
        Dim ChqIni, ChqFin, ChqEnt, ChqSal, IniTol, FinTol, IniPuntual, FinPuntual, Detalle, Horario, IniJ, FinJ As String

        Dim entrada, salida, calc, puntualidad, acum, hextCerrador As Integer


        While Fecha <= FechaFinal
            ChqIni = "" : ChqFin = "" : calc = 0 : entrada = 0 : salida = 0 : Detalle = "" : Horario = "" : puntualidad = 0 : hextCerrador = 0 : IniJ = "" : FinJ = ""

            IniDiaN = Left(Fecha, 10) + " 05:01:00"
            FinDiaN = Left(Fecha, 10) + " 23:59:59"
            SigDia = DateAdd(DateInterval.Day, 1, Fecha).ToString("yyyy-MM-dd")
            IniDiaSig = Left(SigDia, 10) + " 00:00:01"
            FinDiaC = Left(SigDia, 10) + " 05:00:00"

            'BUSCAR PRIMERA CHECADA DEL DIA
            Dim cmd As New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @Ini AND @Fin AND idempleado = @idempleado ORDER BY chec ASC", dbc)
            cmd.Parameters.AddWithValue("Ini", Format(CDate(IniDiaN), "yyyy-MM-dd HH:mm:ss"))
            cmd.Parameters.AddWithValue("Fin", Format(CDate(FinDiaN), "yyyy-MM-dd HH:mm:ss"))
            cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.Read Then
                'Obtener la hora del campo chec
                ChqIni = rdr("chec").ToString
                ChqEnt = rdr("chec").ToString.Substring(0, 2)
                'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                If ChqEnt.Contains(":") Then
                    'Eliminar ":" cuando son horas menores a 10
                    entrada = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
                Else
                    entrada = CInt(ChqEnt)
                End If
            End If
            rdr.Close()

            'BUSCAR ULTIMA CHECADA DEL DIA SIGUIENTE
            cmd = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @Ini AND @Fin AND idempleado = @idempleado ORDER BY chec DESC", dbc)
            cmd.Parameters.AddWithValue("Ini", Format(CDate(IniDiaSig), "yyyy-MM-dd HH:mm:ss"))
            cmd.Parameters.AddWithValue("Fin", Format(CDate(FinDiaC), "yyyy-MM-dd HH:mm:ss"))
            cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
            rdr = cmd.ExecuteReader
            If rdr.Read Then
                ChqFin = rdr("chec").ToString
                ChqSal = rdr("chec").ToString.Substring(0, 2)
                'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                If ChqSal.Contains(":") Then
                    'Eliminar ":" cuando son horas menores a 10
                    salida = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
                Else
                    salida = CInt(ChqSal)
                End If
            Else
                rdr.Close()
                'BUSCAR ULTIMA CHECADA DEL DIA NORMAL
                cmd = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @Ini AND @Fin AND idempleado = @idempleado ORDER BY chec DESC", dbc)
                cmd.Parameters.AddWithValue("Ini", Format(CDate(IniDiaN), "yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("Fin", Format(CDate(FinDiaN), "yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                rdr = cmd.ExecuteReader
                If rdr.Read Then
                    ChqFin = rdr("chec").ToString
                    ChqSal = rdr("chec").ToString.Substring(0, 2)
                    'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                    If ChqSal.Contains(":") Then
                        'Eliminar ":" cuando son horas menores a 10
                        salida = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
                    Else
                        salida = CInt(ChqSal)
                    End If
                End If
            End If
            rdr.Close()

            'BUSCAR HORARIO DEL DIA
            cmd = New SqlCommand("SELECT * FROM vm_Jornada WHERE fecha=@Fecha AND idempleado=@idempleado", dbc)
            cmd.Parameters.AddWithValue("Fecha", Format(CDate(Fecha), "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
            rdr = cmd.ExecuteReader
            If rdr.Read Then
                Horario = rdr("jornada").ToString
                IniHorario = CDate(rdr("inicio").ToString)
                FinHorario = CDate(rdr("checkfin").ToString)
                IniPuntual = rdr("checkini").ToString
                FinPuntual = Left(rdr("inicio").ToString, 2)
                FinPuntual = FinPuntual + ":00:59"
                IniTol = Left(rdr("inicio").ToString, 2)
                IniTol = IniTol + ":01:00"
                FinTol = rdr("tolerancia").ToString

                If ChqIni <> "" Then
                    'Revisa donde entra la checada comparando con su horario
                    If (CDate(ChqIni) >= CDate(IniPuntual)) And (CDate(ChqIni) <= CDate(FinPuntual)) Then
                        Detalle = "PUNTUALIDAD"
                        Checada = CDate(ChqIni)
                        If Checada.Minute <> 0 Then
                            entrada = entrada + 1
                        End If
                        puntualidad = 1
                    ElseIf (CDate(ChqIni) >= CDate(IniTol)) And (CDate(ChqIni) <= CDate(FinTol)) Then
                        Detalle = "TOLERANCIA"
                    ElseIf (CDate(ChqIni) > CDate(FinTol)) Then
                        Detalle = "RETARDO"
                        Checada = CDate(ChqIni)
                        If Checada.Minute >= 6 Then
                            entrada = entrada + 1
                        End If
                    ElseIf CDate(ChqIni) < CDate(IniPuntual) Then
                        Detalle = "ASISTENCIA"
                        Checada = CDate(ChqIni)
                        If Checada.Minute >= 6 Then
                            entrada = entrada + 1
                        End If
                    Else
                        Detalle = "ASISTENCIA"
                    End If
                End If

                If ChqIni <> "" Then
                    If CDate(ChqIni) > CDate(IniPuntual) And CBool(rdr("ausente").ToString) Then
                        Checada = CDate(ChqIni)
                        If Checada.Minute >= 6 Then
                            entrada = entrada + 1
                        End If
                    End If
                End If

                If ChqIni <> "" Then
                    'Para la salida de cerrador, permite la salida desde 15 min antes. Esto viene en la variable FinHorario
                    If (CDate(ChqFin) >= CDate(FinHorario)) And (CDate(ChqFin) <= CDate(rdr("fin").ToString)) Then
                        salida = salida + 1
                    End If
                End If

                If Detalle = "" Then
                    'Si no tiene descanso en su horario y no tiene checada, pone Falta
                    If Not CBool(rdr("ausente")) Then
                        Detalle = "FALTA"
                    Else
                        Detalle = rdr("jornada").ToString
                    End If
                End If

                'Si Tiene checadas y en su horario tiene descanso, pone Descanso laborado
                If ChqIni <> "" And CBool(rdr("ausente").ToString) And salida <> entrada Then
                    Detalle = "DESCANSO LABORADO"
                End If

                'A los cerradores que entran después de las 6 pm y salen después de la 1 se les agrega una hora
                If ChqFin <> "" Then
                    If IniHorario.Hour >= 18 And CDate(ChqFin).Hour < 2 Then
                        hextCerrador = 1
                    End If
                End If

                'Revisar si se le respeta su hora de entrada aunque haya checado después
                If rdr("completar") Then
                    'Obtener la hora de inicio de jornada
                    IniJ = rdr("inicio").ToString.Substring(0, 2)
                    entrada = CInt(IniJ)
                    Detalle = "PUNTUALIDAD"
                End If

                'Revisar si se le respeta su hora de salida aunque haya checado antes
                If rdr("completarfin") Then
                    'Obtener la hora de fin de jornada
                    FinJ = rdr("fin").ToString.Substring(0, 2)
                    salida = CInt(FinJ)
                End If

                'Completar su hora de salida
                If rdr("completarhsal") Then
                    'Obtener su checada de salida y sumarle 1 a la hora
                    FinJ = ChqSal
                    salida = CInt(FinJ + 1)
                End If

            End If

            If entrada <> 0 Then
                If salida < 6 Then
                    'Cálculo para sumar horas de siguiente día
                    calc = (24 - entrada) + salida
                Else
                    calc = salida - entrada
                End If
                calc = calc + hextCerrador
            End If

            'Si su hora de entrada es igual a su hora de salida (solo checó una vez) poner Calc en 0
            If ChqIni = ChqFin Then calc = 0

            'Si su hora de entrada están en la misma hora, porner Calc en 0
            If ChqIni <> "" Then
                If CDate(ChqIni).Hour = CDate(ChqFin).Hour Then calc = 0
            End If
            rdr.Close()

            acum = acum + calc

            cmd2 = New SqlCommand("INSERT INTO Temp_Calculo(fecha,entrada,salida,hrstrab,puntualidad,detalle,clockin,clockout,horario) VALUES(@fecha," & entrada & "," & salida & "," & calc & "," & puntualidad & ",'" & Detalle & "','" & ChqIni & "','" & ChqFin & "','" & Horario & "')", dbc2)
            cmd2.Parameters.AddWithValue("fecha", Fecha)
            cmd2.ExecuteNonQuery()
            cmd2.Dispose()
            Fecha = DateAdd(DateInterval.Day, 1, Fecha).ToString("yyyy-MM-dd")

        End While
        dbc.Close() : dbc.Dispose()
        'Lmsg.Text = IniTol
        TxHorasTrabajadas.Text = acum
    End Sub
End Class
