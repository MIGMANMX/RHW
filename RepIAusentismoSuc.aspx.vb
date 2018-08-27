Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica
Partial Class _RepIAusentismoSuc
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Dim IDempleado As Integer = 0
    Dim IA As Double = 0.0
    Dim F As Integer = 0
    Dim HT As Integer = 0

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

    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                ''''Funcion para generar datos
                CalculoHoras()
                IndiceSucursal()


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

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                Repo.LocalReport.SetParameters(p)

                Repo.ServerReport.Refresh()
            Else
                Mens.Text = "Error: Falta Capturar Fecha"
            End If
        Else
            Mens.Text = "Error: Falta Capturar Empleado"
        End If
    End Sub
    Public Sub CalculoHoras()
        Dim dbc As New SqlConnection
        dbc.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc.Open()
        Dim dbc2 As New SqlConnection
        dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc2.Open()
        Dim rdr2 As SqlDataReader
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
        cmd2 = New SqlCommand("DELETE FROM Temp_CalculoSucursal", dbc2)
        cmd2.ExecuteNonQuery()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Iniciar un ciclo de comparacion por empleado de la sucursal
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Consulta de empleados por sucursal
        Dim cmd3 As New SqlCommand("SELECT idempleado,empleado FROM vm_EmpleadoSucursal  WHERE idsucursal ='" & wucSucursales.idSucursal & "' ", dbc3)
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

            Dim entrada, salida, entradanom, salidanom, calc, calcnom, puntualidad, acum, acumnom, hextCerrador, ininom, finnom, hrsextras As Integer


            While Fecha <= FechaFinal
                ChqIni = "" : ChqFin = "" : calc = 0 : calcnom = 0 : entrada = 0 : salida = 0 : Detalle = "" : Horario = "" : puntualidad = 0 : hextCerrador = 0 : IniJ = "" : FinJ = "" : hrsextras = 0 : entradanom = 0 : salidanom = 0

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

                    'Revisar si tiene horas extras autorizadas
                    IniJ = rdr("inicio").ToString.Substring(0, 2)
                    ininom = CInt(IniJ)
                    FinJ = rdr("fin").ToString.Substring(0, 2)
                    finnom = CInt(FinJ)
                    cmd2 = New SqlCommand("SELECT * FROM vm_Particulares WHERE fecha=@Fecha AND idempleado=@idempleado AND verificado = 1", dbc2)
                    cmd2.Parameters.AddWithValue("Fecha", Format(CDate(Fecha), "yyyy-MM-dd"))
                    cmd2.Parameters.AddWithValue("idempleado", idempleado)
                    rdr2 = cmd2.ExecuteReader
                    If rdr2.Read Then
                        entradanom = entrada
                        hrsextras = CInt(rdr2("cantidad"))
                        salidanom = FinJ + hrsextras
                    Else
                        entradanom = entrada
                        If (salida - entrada) > 8 Then
                            salidanom = FinJ
                        Else
                            salidanom = salida
                        End If
                        'If entrada <> salida Then

                        '    If entrada >= IniJ Then entradanom = IniJ Else entradanom = entrada
                        '    If CBool(rdr("ausente")) Then
                        '        salidanom = salida
                        '    Else
                        '        If salida >= FinJ Then salidanom = FinJ Else salidanom = salida
                        '    End If
                        'Else
                        '    entradanom = 0 : salidanom = 0
                        'End If
                    End If
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
                'calcnom = salidanom
                'Si su hora de entrada es igual a su hora de salida (solo checó una vez) poner Calc en 0
                If ChqIni = ChqFin Then calc = 0

                'Si su hora de entrada están en la misma hora, porner Calc en 0
                If ChqIni <> "" Then
                    If CDate(ChqIni).Hour = CDate(ChqFin).Hour Then calc = 0 : calcnom = 0
                End If
                rdr.Close()

                'Cuando no está bien su horario, el cálculo de nómina sale en 0
                'If calcnom = 0 And calc > 0 Then
                '    calcnom = calc
                'End If

                acum = acum + calc
                acumnom = acumnom + calcnom

                cmd2 = New SqlCommand("INSERT INTO Temp_CalculoSucursal(fecha,entrada,salida,hrstrab,hrstrabnom,puntualidad,detalle,clockin,clockout,idempleado,empleado,horario) VALUES(@fecha," & entrada & "," & salida & "," & calc & "," & calcnom & "," & puntualidad & ",'" & Detalle & "','" & ChqIni & "','" & ChqFin & "',@idempleado,@empleado,'" & Horario & "')", dbc2)
                cmd2.Parameters.AddWithValue("fecha", Fecha)
                cmd2.Parameters.AddWithValue("idempleado", idempleado)
                cmd2.Parameters.AddWithValue("empleado", empleado)

                cmd2.ExecuteNonQuery()
                cmd2.Dispose()
                Fecha = DateAdd(DateInterval.Day, 1, Fecha).ToString("yyyy-MM-dd")
            End While
            'acum = 0
            'acumnom = 0
        End While
        dbc.Close() : dbc.Dispose()
        'Lmsg.Text = IniTol
        'TxHorasTrabajadas.Text = acum
    End Sub
    Public Sub IndiceSucursal()
        'Limpiar Tabla Temporal
        Dim dbc2 As New SqlConnection
        dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc2.Open()
        Dim rdr2 As SqlDataReader
        Dim cmd2 As New SqlCommand("", dbc2)
        cmd2 = New SqlCommand("DELETE FROM Temp_Ausentismo", dbc2)
        cmd2.ExecuteNonQuery()
        rdr2 = Nothing : cmd2.Dispose() : dbc2.Close() : dbc2.Dispose()

        'Conexion y busqueda de registros
        Using dbC As New SqlConnection
            dbC.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
            dbC.Open()
            Dim cmd As New SqlCommand("Select idempleado From Temp_CalculoSucursal ", dbC)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            Dim dsP As String()
            'Lectura de registros
            While rdr.Read
                ReDim dsP(1)
                'Obtener valores
                dsP(0) = rdr("idempleado").ToString
                IDempleado = dsP(0)


                'Obtener Faltas del empleado
                Dim dsPF As New ctiCatalogos
                Dim datosF() As String = dsPF.datosfaltSuc(IDempleado)
                dsP = Nothing
                If datosF(0).StartsWith("Error") Then
                    Mens.CssClass = "error"
                    Mens.Text = datosF(0)
                Else
                    Ft.Text = datosF(0)
                    F = datosF(0)
                End If


                'Obtener Horas por trabajar
                Dim dsPH As New ctiCatalogos
                Dim datosH() As String = dsPH.datoshrsSuc(IDempleado)
                dsPH = Nothing
                If datosH(0).StartsWith("Error") Then
                    Mens.CssClass = "error"
                    Mens.Text = datosH(0)
                Else
                    H.Text = datosH(0)
                    HT = datosH(0)
                End If

                'Calculo de Indice de Ausentismo
                IA = ((F / HT) * 100)


                'Insertar datos en tabla temporal
                Dim dbc3 As New SqlConnection
                dbc3.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
                dbc3.Open()
                Dim rdr3 As SqlDataReader
                Dim cmd3 As New SqlCommand("", dbc3)
                cmd3 = New SqlCommand("INSERT INTO Temp_Ausentismo(idempleado,Faltas,Horas,IA) VALUES(" & IDempleado & "," & F & "," & HT & ",'0')", dbc3)

                'cmd3.Parameters.AddWithValue("idempleado", IDempleado)
                'cmd3.Parameters.AddWithValue("IA", IA)
                'cmd3.Parameters.AddWithValue("Horas", HT)
                'cmd3.Parameters.AddWithValue("Faltas", F)

                cmd3.ExecuteNonQuery()
                cmd3.Dispose()
                dbc3.Close() : dbc3.Dispose()

                ' IAt.Text = IA


            End While
            rdr.Close() : rdr = Nothing : cmd.Dispose() : dbC.Close() : dbC.Dispose()
        End Using


    End Sub

End Class
