Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class Prenomina
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Dim idempleado As Integer = 0
    Dim empleado As String = ""
    Dim idpuesto As Integer = 0
    Dim m As Integer = 0
    ''''Salarios
    Dim horasN As Single = 0.0
    Dim horasE As Single = 0.0
    Dim horasET As Single = 0.0
    Dim diaF As Single = 0.0
    Dim primaD As Single = 0.0
    Dim diaD As Single = 0.0

    ''''Salarios
    Dim d(13) As Integer

    ''''Funciones
    Dim HTotalesT As Integer = 0
    Dim HorasExtrasT As Integer = 0
    Dim HorasExtrasTriplesT As Integer = 0
    Dim DDescansadosT As Integer = 0
    Dim DFestivosTrabajadosT As Integer = 0
    Dim DDescansadosTrabajadosT As Integer = 0
    Dim TotalHorasT As Integer = 0
    Dim ImporteNormalT As Single = 0.0
    Dim TiempoExtraT As Single = 0.0
    Dim TiempoExtraTipleT As Single = 0.0
    Dim DiaFestivoT As Single = 0.0
    Dim SeptimoDiaT As Single = 0.0
    Dim PrimaDominicalT As Single = 0.0
    Dim DiaDescansoT As Single = 0.0
    Dim ImporteTotalT As Single = 0.0

    '''''Variables de salario
    Dim hora As Single = 0.0
    Dim extra As Single = 0.0
    Dim extratiple As Single = 0.0
    Dim diafes As Single = 0.0
    Dim diades As Single = 0.0
    Dim primadom As Single = 0.0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Mens.Text = "" : gvPos = 0
        Session("idz_e") = ""
        Repo.ServerReport.Refresh()
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
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TxFechaInicio.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        FIngreso.Visible = False
        TxFechaFin.Text = DateAdd(DateInterval.Day, 13, FIngreso.SelectedDate).ToString("yyyy-MM-dd")
    End Sub
    Protected Sub FFinal_SelectionChanged(sender As Object, e As EventArgs) Handles FFinal.SelectionChanged
        TxFechaFin.Text = FFinal.SelectedDate.ToString("yyyy-MM-dd")
        FFinal.Visible = False
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        idempleado = 0
        empleado = ""
        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                '''''''''''Segunda conexion: Limpiar tabla y Registrar datos
                Dim dbc2 As New SqlConnection
                dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbc2.Open()
                Dim cmd2 As New SqlCommand("", dbc2)
                ''''''''''''''''''''''''''''''''''''''
                'LIMPIAR TABLA TEMPORAL
                cmd2 = New SqlCommand("DELETE FROM Prenomina", dbc2)
                cmd2.ExecuteNonQuery()
                '''''''''''Tercera conexion: Empleados por sucursal
                Dim dbc3 As New SqlConnection
                dbc3.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbc3.Open()
                'Consulta de empleados por sucursal
                Dim cmd3 As New SqlCommand("Select idempleado,empleado,idpuesto FROM vm_EmpleadoSucursal  WHERE idsucursal =@idsucursal", dbc3)
                cmd3.Parameters.AddWithValue("idsucursal", wucSucursales.idSucursal)
                Dim rdr3 As SqlDataReader = cmd3.ExecuteReader
                'Inicio de ciclo
                While rdr3.Read
                    '''''''''Empleado
                    idempleado = rdr3("idempleado").ToString
                    idpuesto = rdr3("idpuesto").ToString
                    empleado = rdr3("empleado").ToString
                    '''''''''Funciones
                    ''Dias 1 - 14
                    ''''''''''
                    'HTotales()
                    Variables()
                    Temp_Horas()

                    CalculoHoras()
                    HorasExtras()
                    HorasExtrasTriples()
                    DFestivosTrabajados()
                    DDescansadosTrabajados()
                    TotalHoras()
                    ImporteNormal()
                    TiempoExtra()
                    TiempoExtraTiple()
                    DiaFestivo()
                    SeptimoDia()
                    PrimaDominical()
                    DiaDescanso()
                    ImporteTotal()
                    '''''''''''

                    '''''''Insertar datos
                    cmd2 = New SqlCommand("INSERT INTO Prenomina(idempleado, dia1, dia2, dia3, dia4, dia5, dia6, dia7, dia8, dia9, dia10, dia11, dia12, dia13, dia14, HorasNormales, HorasExtras, HorasTriples, DiasFestivosTrabajados, " _
                   & "DiasDescansadosTrabajados, TotalHoras, ImporteNormal, TiempoExtra, TiempoTriple, DiaFestivo, SeptimoDia, PrimaDominical, DiaDescanso, ImporteTotal, FechaIncio, FechaFin)" _
                   & "VALUES(@idempleado, @dia1, @dia2, @dia3, @dia4, @dia5, @dia6, @dia7, @dia8, @dia9, @dia10, @dia11, @dia12 , @dia13 , @dia14, @HorasNormales, @HorasExtras, @HorasTriples, @DiasFestivosTrabajados, " _
                   & "@DiasDescansadosTrabajados, @TotalHoras, @ImporteNormal, @TiempoExtra, @TiempoTriple, @DiaFestivo, @SeptimoDia, @PrimaDominical, @DiaDescanso, @ImporteTotal, @FechaIncio, @FechaFin)", dbc2)


                    cmd2.Parameters.AddWithValue("idempleado", idempleado)
                    ''''''''''Dias 
                    cmd2.Parameters.AddWithValue("dia1", d(0))
                    cmd2.Parameters.AddWithValue("dia2", d(1))
                    cmd2.Parameters.AddWithValue("dia3", d(2))
                    cmd2.Parameters.AddWithValue("dia4", d(3))
                    cmd2.Parameters.AddWithValue("dia5", d(4))
                    cmd2.Parameters.AddWithValue("dia6", d(5))
                    cmd2.Parameters.AddWithValue("dia7", d(6))
                    cmd2.Parameters.AddWithValue("dia8", d(7))
                    cmd2.Parameters.AddWithValue("dia9", d(8))
                    cmd2.Parameters.AddWithValue("dia10", d(9))
                    cmd2.Parameters.AddWithValue("dia11", d(10))
                    cmd2.Parameters.AddWithValue("dia12", d(11))
                    cmd2.Parameters.AddWithValue("dia13", d(12))
                    cmd2.Parameters.AddWithValue("dia14", d(13))
                    ''''''''''''
                    cmd2.Parameters.AddWithValue("HorasNormales", HTotalesT)
                    cmd2.Parameters.AddWithValue("HorasExtras", HorasExtrasT)
                    cmd2.Parameters.AddWithValue("HorasTriples", HorasExtrasTriplesT)
                    cmd2.Parameters.AddWithValue("DiasFestivosTrabajados", DFestivosTrabajadosT)
                    cmd2.Parameters.AddWithValue("DiasDescansadosTrabajados", DDescansadosTrabajadosT)
                    cmd2.Parameters.AddWithValue("TotalHoras", TotalHorasT)
                    cmd2.Parameters.AddWithValue("ImporteNormal", ImporteNormalT)
                    cmd2.Parameters.AddWithValue("TiempoExtra", TiempoExtraT)
                    cmd2.Parameters.AddWithValue("TiempoTriple", TiempoExtraTipleT)
                    cmd2.Parameters.AddWithValue("DiaFestivo", DiaFestivoT)
                    cmd2.Parameters.AddWithValue("SeptimoDia", SeptimoDiaT)
                    cmd2.Parameters.AddWithValue("PrimaDominical", PrimaDominicalT)
                    cmd2.Parameters.AddWithValue("DiaDescanso", DiaDescansoT)
                    cmd2.Parameters.AddWithValue("ImporteTotal", ImporteTotalT)
                    cmd2.Parameters.AddWithValue("FechaIncio", Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"))
                    cmd2.Parameters.AddWithValue("FechaFin", Format(CDate(TxFechaFin.Text), "yyyy-MM-dd"))

                    cmd2.ExecuteNonQuery()
                    cmd2.Dispose()
                End While
                ''''LLenar
                Dim c As CultureInfo = New CultureInfo("es-MX")
                Repo.ServerReport.Refresh()
                Dim p As New ReportParameter("sucursal", wucSucursales.sucursal)
                Repo.LocalReport.SetParameters(p)

                '''''''''''''''''Fechas del Rango'''''''''''''''''''''''''''''''''
                Dim dt1 As Date
                dt1 = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")

                p = New ReportParameter("Fech1", dt1)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech2", DateAdd(DateInterval.Day, 1, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech3", DateAdd(DateInterval.Day, 2, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech4", DateAdd(DateInterval.Day, 3, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech5", DateAdd(DateInterval.Day, 4, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech6", DateAdd(DateInterval.Day, 5, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech7", DateAdd(DateInterval.Day, 6, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech8", DateAdd(DateInterval.Day, 7, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech9", DateAdd(DateInterval.Day, 8, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech10", DateAdd(DateInterval.Day, 9, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech11", DateAdd(DateInterval.Day, 10, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech12", DateAdd(DateInterval.Day, 11, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech13", DateAdd(DateInterval.Day, 12, dt1))
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech14", DateAdd(DateInterval.Day, 13, dt1))
                Repo.LocalReport.SetParameters(p)

                Repo.ServerReport.Refresh()
                ''''''''''''''''''''''''''''''''''''''''''''''''''
            Else
                Mens.Text = "Error: Falta Capturar Fecha"
            End If
        Else
            Mens.Text = "Error: Falta Capturar Sucursal"
        End If
    End Sub
    Private Sub Variables()
        hora = 0.0
        extra = 0.0
        extratiple = 0.0
        diafes = 0.0
        diades = 0.0
        primadom = 0.0
        Using dbC As New SqlConnection
            dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
            dbC.Open()
            Dim cmd As New SqlCommand("SELECT hora,extra,extratiple,diafestivo,diadescanso,primadominical FROM Salarios WHERE idpuesto = @idpuesto AND idsucursal=@idsucursal", dbC)
            cmd.Parameters.AddWithValue("idpuesto", idpuesto)
            cmd.Parameters.AddWithValue("idsucursal", wucSucursales.idSucursal)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            Dim dsP As String()
            If rdr.Read Then
                hora = rdr("hora").ToString
                extra = rdr("extra").ToString
                extratiple = rdr("extratiple").ToString
                diafes = rdr("diafestivo").ToString
                diades = rdr("diadescanso").ToString
                primadom = rdr("primadominical").ToString
            Else
                ' Mens.Text = "Error: no se encuentra los salarios."
            End If
            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        End Using
    End Sub
    Public Sub HTotales()
        HTotalesT = 0
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

                cmd.Parameters.AddWithValue("idempleado", idempleado)
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
        HTotalesT = Acum
    End Sub
    Public Sub CalculoHoras()
        HTotalesT = 0
        Dim dbc As New SqlConnection
        dbc.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc.Open()
        'Dim dbc2 As New SqlConnection
        'dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        'dbc2.Open()
        'Dim cmd2 As New SqlCommand("", dbc2)

        ''LIMPIAR TABLA TEMPORAL
        'cmd2 = New SqlCommand("DELETE FROM Temp_Calculo", dbc2)
        'cmd2.ExecuteNonQuery()

        Dim FechaInicial, FechaFinal, Fecha As Date
        FechaInicial = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FechaFinal = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")
        Fecha = FechaInicial
        Dim IniDiaN, FinDiaC, FinDiaN, IniDiaSig, SigDia, IniHorario, FinHorario, Checada As Date
        Dim ChqIni, ChqFin, ChqEnt, ChqSal, IniTol, FinTol, IniPuntual, FinPuntual, Detalle, Horario, IniJ, FinJ As String

        Dim entrada, salida, calc, puntualidad, acum, hextCerrador As Integer

        m = 0
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
            cmd.Parameters.AddWithValue("idempleado", idempleado)
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
            cmd.Parameters.AddWithValue("idempleado", idempleado)
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
                cmd.Parameters.AddWithValue("idempleado", idempleado)
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
            cmd.Parameters.AddWithValue("idempleado", idempleado)
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

            'cmd2 = New SqlCommand("INSERT INTO Temp_Calculo(fecha,entrada,salida,hrstrab,puntualidad,detalle,clockin,clockout,horario) VALUES(@fecha," & entrada & "," & salida & "," & calc & "," & puntualidad & ",'" & Detalle & "','" & ChqIni & "','" & ChqFin & "','" & Horario & "')", dbc2)
            'cmd2.Parameters.AddWithValue("fecha", Fecha)
            'cmd2.ExecuteNonQuery()
            'cmd2.Dispose()

            d(m) = calc
            m = m + 1

            Fecha = DateAdd(DateInterval.Day, 1, Fecha).ToString("yyyy-MM-dd")

        End While
        dbc.Close() : dbc.Dispose()
        'Lmsg.Text = IniTol
        HTotalesT = acum
    End Sub
    Public Sub Temp_Horas()
        Dim dbc As New SqlConnection
        dbc.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc.Open()
        Dim dbc2 As New SqlConnection
        dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc2.Open()
        Dim cmd2 As New SqlCommand("", dbc2)
        '''''''''''''''''''''''''''''''''''''''''''
        Dim idempleado As Integer = 0
        Dim empleado As String = ""

        '''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''
        Dim dbc3 As New SqlConnection
        dbc3.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc3.Open()
        ''''''''''''''''''''''''''''''''''''''
        'LIMPIAR TABLA TEMPORAL
        cmd2 = New SqlCommand("DELETE FROM Temp_Horas", dbc2)
        cmd2.ExecuteNonQuery()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Iniciar un ciclo de comparacion por empleado de la sucursal
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Consulta de empleados por sucursal
        Dim cmd3 As New SqlCommand("SELECT idempleado,empleado FROM vm_EmpleadoSucursal  WHERE idsucursal =@idsucursal", dbc3)
        cmd3.Parameters.AddWithValue("idsucursal", wucSucursales.idSucursal)
        Dim rdr3 As SqlDataReader = cmd3.ExecuteReader
        'Inicio de ciclo
        While rdr3.Read



            idempleado = rdr3("idempleado").ToString
            empleado = rdr3("empleado").ToString


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
                cmd.Parameters.AddWithValue("idempleado", idempleado)
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
                cmd.Parameters.AddWithValue("idempleado", idempleado)
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
                    cmd.Parameters.AddWithValue("idempleado", idempleado)
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
                cmd.Parameters.AddWithValue("idempleado", idempleado)
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

                cmd2 = New SqlCommand("INSERT INTO Temp_Horas(fecha,entrada,salida,hrstrab,puntualidad,detalle,clockin,clockout,idempleado,empleado,horario) VALUES(@fecha," & entrada & "," & salida & "," & calc & "," & puntualidad & ",'" & Detalle & "','" & ChqIni & "','" & ChqFin & "',@idempleado,@empleado,'" & Horario & "')", dbc2)
                cmd2.Parameters.AddWithValue("fecha", Fecha)
                cmd2.Parameters.AddWithValue("idempleado", idempleado)
                cmd2.Parameters.AddWithValue("empleado", empleado)

                cmd2.ExecuteNonQuery()
                cmd2.Dispose()
                Fecha = DateAdd(DateInterval.Day, 1, Fecha).ToString("yyyy-MM-dd")
            End While
        End While
        dbc.Close() : dbc.Dispose()
        'Lmsg.Text = IniTol
        'TxHorasTrabajadas.Text = acum
    End Sub
    Public Sub HorasExtras()
        HorasExtrasT = 0
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

                cmd.Parameters.AddWithValue("idempleado", idempleado)
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
        HorasExtrasT = Acum
        Acum = 0
    End Sub
    Public Sub HorasExtrasTriples()
        HorasExtrasTriplesT = 0
        If HTotalesT > 10 Then
            HorasExtrasTriplesT = HorasExtrasT * 3
        Else
            HorasExtrasTriplesT = 0
        End If
    End Sub
    Public Sub DFestivosTrabajados()
        DFestivosTrabajadosT = 0
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date

        'Variables para operaciones
        Dim Acum As Integer = 0
        Dim acumH As Integer = 0

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
                Dim datos() As String = acceso.ConsultaAsistencia(idempleado, Format(CDate(F), "yyyy/MM/dd"), Format(CDate(FF), "yyyy/MM/dd"))
                If datos(0) <> 0 Then
                    'Acumular el dia
                    Acum = Acum + 1

                    'Consulta la hora del dia
                    Dim acceso2 As New ctiCalculo
                    Dim datos2() As String = acceso.ConsultaHrstrab(idempleado, Format(CDate(F), "yyyy/MM/dd"), Format(CDate(FF), "yyyy/MM/dd"))
                    Dim dat2 As Integer = datos2(0)
                    'Sumar horas
                    acumH = acumH + dat2
                Else
                    Acum = Acum
                End If


            End While
            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        End Using

        DFestivosTrabajadosT = acumH

        Acum = 0
        acumH = 0
    End Sub
    Public Sub DDescansadosTrabajados()
        DDescansadosTrabajadosT = 0
        'Datos de los campos de texto
        Dim FIn As Date
        Dim FFn As Date

        'Variable global
        Dim Fech As Date

        'Variables para operaciones
        Dim Acum As Integer = 0
        Dim acumH As Integer = 0
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

                cmd.Parameters.AddWithValue("idempleado", idempleado)
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
                        Dim datos() As String = acceso.ConsultaAsistencia(idempleado, F.ToString("yyyy-MM-dd"), FechaF.ToString("yyyy-MM-dd"))
                        Dim dat As Integer = datos(0)
                        If datos(0) > 0 Then
                            'Acumular el dia
                            Acum = Acum + 1
                            'Consulta la hora del dia
                            Dim acceso2 As New ctiCalculo
                            Dim datos2() As String = acceso.ConsultaHrstrab(idempleado, F.ToString("yyyy-MM-dd"), FechaF.ToString("yyyy-MM-dd"))
                            Dim dat2 As Integer = datos2(0)
                            acumH = acumH + dat2

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
        DDescansadosTrabajadosT = acumH

        Acum = 0
        acumH = 0
    End Sub
    Public Sub TotalHoras()
        TotalHorasT = 0
        TotalHorasT = HTotalesT + HorasExtrasT + HorasExtrasTriplesT + DFestivosTrabajadosT + DDescansadosTrabajadosT
    End Sub
    Public Sub ImporteNormal()
        ImporteNormalT = 0.0
        Dim dsP As New ctiCatalogos
        Dim datos() As String = dsP.datosSalarios2(idpuesto, wucSucursales.idSucursal)

        horasN = 0.0
        horasN = hora

        ImporteNormalT = HTotalesT * horasN
    End Sub
    Public Sub TiempoExtra()
        If (HTotalesT >= 96) Then
            TiempoExtraT = 0.0

            horasE = 0.0
            horasE = extra

            TiempoExtraT = HorasExtrasT * horasE
        Else
            TiempoExtraT = 0.0
        End If

    End Sub
    Public Sub TiempoExtraTiple()
        If (HTotalesT >= 96) Then
            TiempoExtraTipleT = 0.0

            horasET = 0.0
            horasET = extratiple

            TiempoExtraTipleT = HorasExtrasT * horasET
        Else
            TiempoExtraTipleT = 0.0
        End If
    End Sub
    Public Sub DiaFestivo()
        DiaFestivoT = 0.0
        diaF = 0.0
        diaF = diafes

        DiaFestivoT = DFestivosTrabajadosT * diaF
    End Sub
    Public Sub SeptimoDia()
        SeptimoDiaT = 0.0
        SeptimoDiaT = ImporteNormalT / 6
    End Sub
    Public Sub PrimaDominical()
        PrimaDominicalT = 0.0

        primaD = 0.0
        horasN = 0.0
        horasN = hora
        primaD = primadom

        PrimaDominicalT = (horasN * ((d(6) + d(13)) * (primaD / 100)))
    End Sub
    Public Sub DiaDescanso()
        DiaDescansoT = 0.0

        diaD = 0.0
        diaD = diades

        DiaDescansoT = DDescansadosTrabajadosT * diaD
    End Sub
    Public Sub ImporteTotal()
        ImporteTotalT = 0.0
        ImporteTotalT = ImporteNormalT + TiempoExtraT + TiempoExtraTipleT + DiaFestivoT + SeptimoDiaT + PrimaDominicalT + DiaDescansoT
    End Sub
End Class
