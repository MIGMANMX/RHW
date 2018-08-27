Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica
Partial Class _RepIAusentismo
    Inherits System.Web.UI.Page
    'Dim IR As Integer
    'Dim E As Integer
    'Dim S As Integer
    'Dim P As Integer
    Dim IA As Double = 0.0
    Dim F As Double = 0.0
    Dim HT As Double = 0.0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Session("idz_e") = ""
        tSuc.Text = wucSucursales.sucursal
        Mens.Text = ""
        Repo.ServerReport.Refresh()

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
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado

        idEmpleadoTX.Text = wucEmpleados2.idEmpleado.ToString

        Dim dsP As New ctiCatalogos
        Dim sr As String()

        sr = dsP.datosEmpleado(wucEmpleados2.idEmpleado)

        TxEmpleado.Text = sr(0)

    End Sub
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TxFechaInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        FIngreso.Visible = False
        TxFechaFin.Text = DateAdd(DateInterval.Day, 13, FIngreso.SelectedDate).ToString("yyyy-MM-dd")
    End Sub
    Protected Sub FFinal_SelectionChanged(sender As Object, e As EventArgs) Handles FFinal.SelectionChanged
        TxFechaFin.Text = FFinal.SelectedDate.ToString("yyyy-MM-dd")
        FFinal.Visible = False
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")

        Repo.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then

                Ausentismo()

                Dim dt1 As Date
                dt1 = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
                Dim dtf As Date
                dtf = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

                Dim dt2 As Date
                dt2 = DateAdd(DateInterval.Day, 1, dtf)
                dt2 = Format(CDate(dt2), "yyyy-MM-dd")
                TxFechaFin2.Text = dt2



                Dim p As New ReportParameter("Fecha1", dt1)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fecha2", dtf)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("idempleado", idEmpleadoTX.Text)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("empleado", TxEmpleado.Text)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("IA", IA)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("F", F)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("HT", HT)
                Repo.LocalReport.SetParameters(p)



                Repo.ServerReport.Refresh()
            Else
                Mens.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Mens.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub
    Public Sub Ausentismo()
        If wucEmpleados2.idEmpleado <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                '''''''''Botones
                HTotales()
                DDescansados()
                DFestivosTrabajados()
                DDescansadosTrabajados()
                CalculoHoras()
                '''''''''''
                Dim FechaFinal As Date
                Dim FechaFinal2 As Date
                FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
                FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")
                Dim ec As New ctiCalculo

                falt()
                hrs()
                'F = 2
                'HT = 85
                IA = (F / HT)

                IAt.Text = IA
                'Ft.Text = F
                'H.Text = HT
            Else
                Mens.Text = "Error: Falta Capturar Fecha"
            End If
        Else
            Mens.Text = "Error: Falta Capturar Empleado"
        End If

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
            Using dbC As New Data.SqlClient.SqlConnection
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
        'TxHtotales.Text = Acum
    End Sub
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
        'TxDDescasados.Text = Acum
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

        ' TextBox3.Text = Acum
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
        ' TextBox2.Text = Acum
        Acum = 0
    End Sub
    Public Sub CalculoHoras()
        Dim dbc As New SqlConnection
        dbc.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc.Open()
        Dim dbc2 As New SqlConnection
        dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc2.Open()
        Dim cmd2 As New SqlCommand("", dbc2)

        ''''''''''
        'Dim dbc3 As New SqlConnection
        'dbc3.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        'dbc3.Open()
        'Dim cmd3 As New SqlCommand("", dbc3)
        ''''''''''
        'LIMPIAR TABLA TEMPORAL
        cmd2 = New SqlCommand("DELETE FROM Temp_Calculo", dbc2)
        cmd2.ExecuteNonQuery()
        Dim rdr2 As SqlDataReader

        Dim FechaInicial, FechaFinal, Fecha As Date
        FechaInicial = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FechaFinal = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")
        Fecha = FechaInicial
        Dim IniDiaN, FinDiaC, FinDiaN, IniDiaSig, SigDia, IniHorario, FinHorario, Checada As Date
        Dim ChqIni, ChqFin, ChqEnt, ChqSal, IniTol, FinTol, IniPuntual, FinPuntual, Detalle, Horario, IniJ, FinJ, MinSalC, AltHorario As String
        Dim incremento As Boolean = False
        Dim entrada, salida, entradanom, salidanom, calc, calcnom, puntualidad, acum, acumnom, hextCerrador, ininom, finnom, hrsextras, Minutos As Integer
        Dim AcumHrs As Integer = 0
        ''''''''
        'Dim HExtras As Integer = 0
        ''''''''
        While Fecha <= FechaFinal
            MinSalC = "" : Minutos = 0 : incremento = False
            ChqIni = "" : ChqFin = "" : calc = 0 : calcnom = 0 : entrada = 0 : salida = 0 : Detalle = "" : Horario = "" : puntualidad = 0 : hextCerrador = 0 : IniJ = "" : FinJ = "" : hrsextras = 0 : entradanom = 0 : salidanom = 0 : AltHorario = ""

            IniDiaN = Left(Fecha, 10) + " 05:01:00"
            FinDiaN = Left(Fecha, 10) + " 23:59:59"
            SigDia = DateAdd(DateInterval.Day, 1, Fecha).ToString("yyyy-MM-dd")
            IniDiaSig = Left(SigDia, 10) + " 00:00:01"
            FinDiaC = Left(SigDia, 10) + " 05:00:00"

            'Sacar las horas extras del dia
            'cmd3 = New SqlCommand("SELECT cantidad FROM Particulares WHERE idempleado=", dbc2)
            'cmd3.ExecuteNonQuery()
            'Dim rdr3 As SqlDataReader = cmd3.ExecuteReader
            'If rdr3.Read Then
            '    HExtras = rdr3("cantidad").ToString
            'End If
            'rdr3.Close()

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
                MinSalC = rdr("chec").ToString.Substring(3, 2)
                Minutos = CInt(MinSalC)
                'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                If ChqSal.Contains(":") Then
                    'Eliminar ":" cuando son horas menores a 10
                    salida = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
                Else
                    salida = CInt(ChqSal)
                End If
                'Si el empleado sale antes de la 1, en hora de salida se guardaba 0
                If salida = 0 And Minutos >= 50 Then
                    incremento = True
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

                'Se le pone en hora de salida = 1, revisar línea 1475
                If incremento And rdr("cierre") = 0 Then
                    salida = 1
                End If

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

                If Detalle = "DESCANSO LABORADO" And entrada = 18 And salida = 1 Then
                    hextCerrador = 1
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
                    AltHorario = "ENTRADA"
                End If

                'Revisar si se le respeta su hora de salida aunque haya checado antes
                If rdr("completarfin") Then
                    'Obtener la hora de fin de jornada
                    FinJ = rdr("fin").ToString.Substring(0, 2)
                    salida = CInt(FinJ)
                    AltHorario = "SALIDA"
                End If

                'Completar su hora de salida
                If rdr("completarhsal") Then
                    'Obtener su checada de salida y sumarle 1 a la hora
                    FinJ = ChqSal
                    salida = CInt(FinJ + 1)
                    AltHorario = "HORA"
                End If

                If rdr("completar") And rdr("completarfin") Then
                    AltHorario = "ENTRADA Y SALIDA"
                End If


                'Revisar si tiene horas extras autorizadas
                IniJ = rdr("inicio").ToString.Substring(0, 2)
                ininom = CInt(IniJ)
                FinJ = rdr("fin").ToString.Substring(0, 2)
                finnom = CInt(FinJ)
                cmd2 = New SqlCommand("SELECT * FROM vm_Particulares WHERE fecha=@Fecha AND idempleado=@idempleado AND verificado = 1", dbc2)
                cmd2.Parameters.AddWithValue("Fecha", Format(CDate(Fecha), "yyyy-MM-dd"))
                cmd2.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                rdr2 = cmd2.ExecuteReader
                If rdr2.Read Then
                    entradanom = entrada
                    'Validación para solamente pagar horas extras a los que hayan completado su jornada de 8 hrs.
                    'Horario normal
                    If salida >= 6 Then
                        If (salida - entrada) > 8 Then
                            hrsextras = CInt(rdr2("cantidad"))
                            salidanom = FinJ + hrsextras
                        Else
                            salidanom = salida
                        End If
                        'Horario cerrador
                    Else
                        If ((24 - entrada) + salida) > 8 Then
                            hrsextras = CInt(rdr2("cantidad"))
                            salidanom = FinJ + hrsextras
                        Else
                            salidanom = salida
                        End If
                    End If
                Else
                    entradanom = entrada
                    If salida < 6 Then
                        If ((24 - entrada) + salida) > 8 Then
                            salidanom = FinJ
                        Else
                            salidanom = salida
                        End If
                    Else
                        If FinJ < 6 And salida > 6 Then
                            salidanom = salida
                        Else
                            If (salida - entrada) > 8 Or (salida > FinJ) Then
                                salidanom = FinJ
                            Else
                                salidanom = salida
                            End If
                        End If
                    End If
                End If
                AcumHrs = AcumHrs + hrsextras
                rdr2.Close()

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

            If entradanom <> 0 Then
                If salidanom < 6 Then
                    'Cálculo para sumar horas de siguiente día
                    calcnom = (24 - entradanom) + salidanom
                Else
                    calcnom = salidanom
                    calcnom = salidanom - entradanom
                End If
                calcnom = calcnom + hextCerrador

            End If
            'Detalle = salidanom
            'Si su hora de entrada es igual a su hora de salida (solo checó una vez) poner Calc en 0
            If ChqIni = ChqFin And AltHorario = "" Then calc = 0 : calcnom = 0
            'Si su hora de entrada están en la misma hora, porner Calc en 0
            If ChqIni <> "" And AltHorario = "" Then
                If CDate(ChqIni).Hour = CDate(ChqFin).Hour Then calc = 0 : calcnom = 0
            End If
            rdr.Close()

            acum = acum + calc
            acumnom = acumnom + calcnom

            cmd2 = New SqlCommand("INSERT INTO Temp_Calculo(fecha,entrada,salida,hrstrab,hrstrabnom,puntualidad,detalle,clockin,clockout,horario,althorario,hrsextras) VALUES(@fecha," & entrada & "," & salida & "," & calc & ", " & calcnom & " , " & puntualidad & ",'" & Detalle & "','" & ChqIni & "','" & ChqFin & "','" & Horario & "','" & AltHorario & "','" & hrsextras & "')", dbc2)
            cmd2.Parameters.AddWithValue("fecha", Fecha)
            cmd2.ExecuteNonQuery()
            cmd2.Dispose()
            Fecha = DateAdd(DateInterval.Day, 1, Fecha).ToString("yyyy-MM-dd")

        End While
        'TxHorasExtras.Text = AcumHrs
        dbc.Close() : dbc.Dispose()
        'Lmsg.Text = IniTol
        'TxHorasTrabajadas.Text = acum
        ' TxHtotales.Text = acumnom
    End Sub
    Public Sub hrs()
        Dim dsP As New ctiCatalogos
        Dim datos() As String = dsP.datoshrs()
        dsP = Nothing
        If datos(0).StartsWith("Error") Then
            Mens.CssClass = "error"
            Mens.Text = datos(0)
        Else
            H.Text = datos(0)
            HT = datos(0)
        End If

    End Sub
    Public Sub falt()
        Dim dsP As New ctiCatalogos
        Dim datos() As String = dsP.datosfalt()
        dsP = Nothing
        If datos(0).StartsWith("Error") Then
            Mens.CssClass = "error"
            Mens.Text = datos(0)
        Else
            Ft.Text = datos(0)
            F = datos(0)
        End If

    End Sub
End Class
