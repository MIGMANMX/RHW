
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports RHLogica

Partial Class CalculosHSucursal
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Mens.Text = "" : gvPos = 0
        Session("idz_e") = ""
        'Repo.ServerReport.Refresh()
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                ''''Funcion para generar datos
                CalculoHrsTrab()
                ''''Generar grid
                Dim ec As New ctiCalculo
                GridView1.DataSource = ec.gvCalculoSucursal()
                ec = Nothing
                GridView1.DataBind()

            Else
                Mens.Text = "Error: Falta Capturar Fecha"
            End If
        Else
            Mens.Text = "Error: Falta Capturar Empleado"
        End If
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
        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")

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

                Dim p As New ReportParameter("Fecha1", dt1)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("Fecha2", dtf)
                Repo.LocalReport.SetParameters(p)

                p = New ReportParameter("sucursal", wucSucursales.sucursal)
                Repo.LocalReport.SetParameters(p)

                Repo.ServerReport.Refresh()
            Else
                Mens.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Mens.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub
    Protected Sub GridView1_PageIndexChanging1(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Dim ec As New ctiCalculo
        'Dim FechaFinal As Date
        'Dim FechaFinal2 As Date
        'FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
        'FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")
        GridView1.DataSource = ec.gvCalculoSucursal()
        ec = Nothing
        GridView1.DataBind()
    End Sub
    Public Sub CalculoHrsTrab()
        Dim FIn, FFn, Checada As Date
        Dim HEntrada, HSalida, ChqEnt, ChqSal As String
        Dim FHEntrada, FHPuntual, FinTolerancia, FHSalida, FHSalida2, FHSalida3, HoraEnt, Salida, IniTolerancia, IniDia, SigDia, FFnCierre As String
        Dim calc, ent, sal, acum, entrada As Integer
        Dim cerrador As Boolean
        Dim puntualidad, hrstarde As Integer
        Dim incidencia, idjornada, detalle As String
        Dim revisar, clockin, clockout As String

        '''''''''''''''''''''''''''''''''''''''''''
        Dim idempleado As Integer = 0
        Dim empleado As String = ""

        '''''''''''''''''''''''''''''''''''''''''''
        FIn = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
        FFn = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

        Dim dbc As New SqlConnection
        dbc.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc.Open()
        Dim dbc2 As New SqlConnection
        dbc2.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc2.Open()
        Dim cmd2 As New SqlCommand("", dbc2)

        ''''''''''''''''''''''''''''''''''''
        Dim dbc3 As New SqlConnection
        dbc3.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
        dbc3.Open()
        ''''''''''''''''''''''''''''''''''''''
        'Dim rdr2 As SqlDataReader

        'LIMPIAR TABLA TEMPORAL
        cmd2 = New SqlCommand("DELETE FROM Temp_CalculoSucursal", dbc2)
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




            Dim cmd As New SqlCommand("SELECT * FROM vm_Jornada WHERE fecha >=@FIn AND fecha <=@FFn  AND idempleado=@idempleado Order BY fecha ASC", dbc)
            cmd.Parameters.AddWithValue("idempleado", idempleado)
            cmd.Parameters.AddWithValue("FIn", Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("FFn", Format(CDate(TxFechaFin.Text), "yyyy-MM-dd"))
        Dim rdr As SqlDataReader = cmd.ExecuteReader
        While rdr.Read
            puntualidad = 0
            calc = 0
            'Filtar Jornadas
            idjornada = rdr("idjornada").ToString
            'Guardar si jornada es de cierre o no
            cerrador = rdr("cierre").ToString
            detalle = ""
            'Calculos de Puntualidad, tolerancia y retardo
            HEntrada = Left(rdr("inicio").ToString, 2) - 1
            HoraEnt = Left(rdr("inicio").ToString, 2)
            FHEntrada = Left(rdr("fecha").ToString, 11) + HEntrada + ":45:00"
            FHPuntual = Left(rdr("fecha").ToString, 11) + HoraEnt + ":00:59"
            IniTolerancia = Left(rdr("fecha").ToString, 11) + HoraEnt + ":01:00"
            FinTolerancia = Left(rdr("fecha").ToString, 11) + HoraEnt + ":05:59"

            'Cálculos de salida
            Salida = Left(rdr("fin").ToString, 2) - 1
            FHSalida = Left(rdr("fecha").ToString, 11) + Salida + ":45:00"
            FHSalida2 = Left(rdr("fecha").ToString, 11) + Left(rdr("fin").ToString, 8)
            FHSalida3 = Left(rdr("fecha").ToString, 11) + "23:59:59"

            'Cálculos de salida CERRADOR
            SigDia = DateAdd(DateInterval.Day, 1, rdr("fecha"))

            FFnCierre = SigDia + " " + Left(rdr("fin").ToString, 8)

            IniDia = SigDia + " 05:59:00"
            sal = 0 : ent = 0

            'If idjornada <> "21" And idjornada <> "22" And idjornada <> "24" And idjornada <> "36" And idjornada <> "9" Then

            '********************** C E R R A D O R *************************************************************************

            If cerrador Then
                entrada = 0
                'REVISAR SI LLEGO PUNTUAL
                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                    cmd2.Parameters.AddWithValue("idempleado", idempleado)
                    cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada), "yyyy-MM-dd HH:mm:ss"))
                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHPuntual), "yyyy-MM-dd HH:mm:ss"))
                Dim rdr2 As SqlDataReader = cmd2.ExecuteReader

                If rdr2.Read Then
                    entrada = 1
                    puntualidad = 1
                    detalle = "PUNTUALIDAD"
                    'Obtener la hora del campo chec
                    Checada = rdr2("chec").ToString
                    clockin = CStr(Checada)
                    ChqEnt = rdr2("chec").ToString.Substring(0, 2)
                    'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                    'ChqEnt = CStr(Checada).Substring(11, 2)
                    'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                    If ChqEnt.Contains(":") Then
                        'Eliminar ":" cuando son horas menores a 10
                        ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
                    Else
                        ent = CInt(ChqEnt)
                    End If
                    If ent = HEntrada Then
                        ent = ent + 1
                    End If
                    'Lmsg.Text = "Puntual"
                Else
                    'REVISAR SI LLEGO EN TOLERANCIA
                    rdr2.Close()
                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                        cmd2.Parameters.AddWithValue("idempleado", idempleado)
                        cmd2.Parameters.AddWithValue("FHin", Format(CDate(IniTolerancia), "yyyy-MM-dd HH:mm:ss"))
                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
                    rdr2 = cmd2.ExecuteReader
                    If rdr2.Read Then
                        entrada = 1
                        puntualidad = 0
                        detalle = "ASISTENCIA"
                        'Obtener la hora del campo chec
                        Checada = rdr2("chec").ToString
                        clockin = CStr(Checada)
                        ChqEnt = rdr2("chec").ToString.Substring(0, 2)
                        'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                        'ChqEnt = CStr(Checada).Substring(11, 2)
                        'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                        If ChqEnt.Contains(":") Then
                            'Eliminar ":" cuando son horas menores a 10
                            ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
                        Else
                            ent = CInt(ChqEnt)
                        End If
                        'Lmsg.Text = ent
                    Else
                        'TRAER LA PRIMER CHECADA SI LLEGO TARDE
                        rdr2.Close()
                        puntualidad = 0
                        'detalle = "ASISTENCIA"
                        cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                            cmd2.Parameters.AddWithValue("idempleado", idempleado)
                            cmd2.Parameters.AddWithValue("FHin", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
                        cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida), "yyyy-MM-dd HH:mm:ss"))
                        rdr2 = cmd2.ExecuteReader
                        If rdr2.Read Then
                            entrada = 1
                            puntualidad = 0
                            detalle = "ASISTENCIA"
                            'Obtener la hora del campo chec3
                            Checada = rdr2("chec").ToString
                            clockin = CStr(Checada)
                            ChqEnt = rdr2("chec").ToString.Substring(0, 2)

                            'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                            'ChqEnt = CStr(Checada).Substring(11, 2)
                            'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                            'revisar = CStr(Checada)
                            If ChqEnt.Contains(":") Then
                                'Eliminar ":" cuando son horas menores a 10
                                ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
                            Else
                                ent = CInt(ChqEnt)
                            End If

                            'Descontarle las horas que llega tarde
                            If CInt(HoraEnt) = ent Then
                                ent = ent + 1
                            Else
                                hrstarde = CInt(ChqEnt) - CInt(HoraEnt)
                                ent = ent + hrstarde
                            End If
                            'Lmsg.Text = "Tarde"
                        End If
                    End If
                End If
                rdr2.Close()

                'REVISAR SALIDA.
                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHIn AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                    cmd2.Parameters.AddWithValue("idempleado", idempleado)
                    cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FFnCierre), "yyyy-MM-dd HH:mm:ss"))
                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(IniDia), "yyyy-MM-dd HH:mm:ss"))
                rdr2 = cmd2.ExecuteReader
                If rdr2.Read Then
                    'Agregarle una hora a su hora de salida.
                    'ChqSal = rdr2("chec").ToString.Substring(11, 2)

                    'Obtener la hora del campo chec3
                    Checada = rdr2("chec").ToString
                    clockout = CStr(Checada)
                    ChqSal = rdr2("chec").ToString.Substring(0, 2)
                    revisar = rdr2("chec").ToString
                    If ChqSal.Contains(":") Then
                        sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
                    Else
                        sal = CInt(ChqSal)
                    End If
                Else
                    rdr2.Close()
                    'REVISAR SALIDA. Traer última checada después de su hora de salida.
                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                        cmd2.Parameters.AddWithValue("idempleado", idempleado)
                        cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHSalida2), "yyyy-MM-dd HH:mm:ss"))
                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
                    rdr2 = cmd2.ExecuteReader
                    If rdr2.Read Then
                        'ChqSal = rdr2("chec").ToString.Substring(11, 2)

                        'Obtener la hora del campo chec3
                        Checada = rdr2("chec").ToString
                        clockout = CStr(Checada)
                        ChqSal = rdr2("chec").ToString.Substring(0, 2)

                        If ChqSal.Contains(":") Then
                            sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
                        Else
                            sal = CInt(ChqSal)
                        End If
                    End If
                End If
                rdr2.Close()

                If sal = 0 Or ent = 0 Then
                    calc = 0
                Else
                    ' Si es cerrador se le agrega una hora
                    calc = (24 - ent) + sal + 1
                End If

                'acum = acum + calc

                '********************** N O R M A L *************************************************************************
            Else
                entrada = 0
                'REVISAR SI LLEGO PUNTUAL
                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                    cmd2.Parameters.AddWithValue("idempleado", idempleado)
                    cmd2.Parameters.AddWithValue("FHin", Format(CDate(FHEntrada), "yyyy-MM-dd HH:mm:ss"))
                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHPuntual), "yyyy-MM-dd HH:mm:ss"))
                Dim rdr2 As SqlDataReader = cmd2.ExecuteReader

                If rdr2.Read And entrada = 0 Then
                    entrada = 1
                    puntualidad = 1
                    detalle = "PUNTUALIDAD"
                    'Obtener la hora del campo chec
                    Checada = rdr2("chec").ToString
                    clockin = CStr(Checada)
                    ChqEnt = rdr2("chec").ToString.Substring(0, 2)
                    'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                    'ChqEnt = CStr(Checada).Substring(11, 2)
                    'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                    If ChqEnt.Contains(":") Then
                        'Eliminar ":" cuando son horas menores a 10
                        ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
                    Else
                        ent = CInt(ChqEnt)
                    End If
                    If ent = HEntrada Then
                        ent = ent + 1
                    End If
                    'Lmsg.Text = "Puntual"
                ElseIf entrada = 0 Then

                    'REVISAR SI LLEGO EN TOLERANCIA
                    rdr2.Close()
                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                        cmd2.Parameters.AddWithValue("idempleado", idempleado)
                        cmd2.Parameters.AddWithValue("FHin", Format(CDate(IniTolerancia), "yyyy-MM-dd HH:mm:ss"))
                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
                    rdr2 = cmd2.ExecuteReader
                    If rdr2.Read Then
                        entrada = 1
                        puntualidad = 0
                        detalle = "ASISTENCIA"
                        'Obtener la hora del campo chec
                        Checada = rdr2("chec").ToString
                        clockin = CStr(Checada)
                        ChqEnt = rdr2("chec").ToString.Substring(0, 2)
                        'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                        'ChqEnt = CStr(Checada).Substring(11, 2)
                        'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                        If ChqEnt.Contains(":") Then
                            'Eliminar ":" cuando son horas menores a 10
                            ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
                        Else
                            ent = CInt(ChqEnt)
                        End If
                        'Lmsg.Text = ent
                    ElseIf entrada = 0 Then

                        'SACAR LA PRIMER CHECADA SI LLEGO TARDE
                        rdr2.Close()
                        cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec ASC", dbc2)
                            cmd2.Parameters.AddWithValue("idempleado", idempleado)
                            cmd2.Parameters.AddWithValue("FHin", Format(CDate(FinTolerancia), "yyyy-MM-dd HH:mm:ss"))
                        cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida), "yyyy-MM-dd HH:mm:ss"))
                        rdr2 = cmd2.ExecuteReader
                        If rdr2.Read Then
                            entrada = 1
                            puntualidad = 0
                            detalle = "ASISTENCIA"
                            'Obtener la hora del campo chec3
                            Checada = rdr2("chec").ToString
                            clockin = CStr(Checada)
                            ChqEnt = rdr2("chec").ToString.Substring(0, 2)

                            'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                            'ChqEnt = CStr(Checada).Substring(11, 2)
                            'Cuando son horas menores a 10 no toma en cuenta el 0 antes del entero, por lo que se trae ":" 
                            revisar = CStr(Checada)
                            If ChqEnt.Contains(":") Then
                                'Eliminar ":" cuando son horas menores a 10
                                ent = CInt(Mid(ChqEnt, 1, Len(ChqEnt) - 1))
                            Else
                                ent = CInt(ChqEnt)
                            End If

                            'Descontarle las horas que llega tarde
                            If CInt(HoraEnt) = ent Then
                                ent = ent + 1
                            Else
                                hrstarde = CInt(ChqEnt) - CInt(HoraEnt)
                                ent = ent + hrstarde
                            End If


                            'Lmsg.Text = "Tarde"
                        End If
                    End If
                End If
                rdr2.Close()

                'REVISAR SALIDA. Traer checada de 15 min antes de su salida, hasta su hora de salida.
                cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec DESC", dbc2)
                    cmd2.Parameters.AddWithValue("idempleado", idempleado)
                    cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHSalida), "yyyy-MM-dd HH:mm:ss"))
                cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida2), "yyyy-MM-dd HH:mm:ss"))
                rdr2 = cmd2.ExecuteReader
                If rdr2.Read Then
                    'Agregarle una hora a su hora de salida.
                    Checada = rdr2("chec").ToString
                    clockout = CStr(Checada)
                    ChqSal = rdr2("chec").ToString.Substring(0, 2)
                    'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                    'ChqEnt = CStr(Checada).Substring(11, 2)
                    If ChqSal.Contains(":") Then
                        sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
                    Else
                        sal = CInt(ChqSal)
                    End If
                    sal = sal + 1
                Else
                    rdr2.Close()
                    'REVISAR SALIDA. Traer última checada después de su hora de salida.
                    cmd2 = New SqlCommand("SELECT TOP(1) CONVERT(VARCHAR(8), chec, 108) AS chec FROM vm_ChequeoIncidencia WHERE chec BETWEEN @FHin AND @FHFn AND idempleado=@idempleado ORDER by chec DESC", dbc2)
                        cmd2.Parameters.AddWithValue("idempleado", idempleado)
                        cmd2.Parameters.AddWithValue("FHIn", Format(CDate(FHSalida2), "yyyy-MM-dd HH:mm:ss"))
                    cmd2.Parameters.AddWithValue("FHFn", Format(CDate(FHSalida3), "yyyy-MM-dd HH:mm:ss"))
                    rdr2 = cmd2.ExecuteReader
                    If rdr2.Read Then
                        Checada = rdr2("chec").ToString
                        clockout = CStr(Checada)
                        ChqSal = rdr2("chec").ToString.Substring(0, 2)
                        'Checada = Format(CDate(rdr2("chec").ToString), "yyyy-MM-dd HH:mm:ss")
                        'ChqEnt = CStr(Checada).Substring(11, 2)
                        If ChqSal.Contains(":") Then
                            sal = CInt(Mid(ChqSal, 1, Len(ChqSal) - 1))
                        Else
                            sal = CInt(ChqSal)
                        End If
                    End If
                End If
                rdr2.Close()

                If sal = 0 Or ent = 0 Then
                    calc = 0
                Else
                    calc = sal - ent
                End If

            End If



            acum = acum + calc

            'Else
            If detalle = "" Then
                detalle = rdr("jornada").ToString
            End If

                'End If


                cmd2 = New SqlCommand("INSERT INTO Temp_CalculoSucursal(fecha,entrada,salida,hrstrab,puntualidad,detalle,clockin,clockout,idempleado,empleado) VALUES(@fecha," & CStr(ent) & "," & CStr(sal) & "," & CStr(calc) & "," & CStr(puntualidad) & ",'" & detalle & "', '" & clockin & "','" & clockout & "',@idempleado,@empleado)", dbc2)
                Dim Fech As Date
            Fech = rdr("fecha").ToString
                cmd2.Parameters.AddWithValue("idempleado", idempleado)
                cmd2.Parameters.AddWithValue("empleado", empleado)
                cmd2.Parameters.AddWithValue("fecha", Fech.ToString("yyyy-MM-dd"))
                cmd2.ExecuteNonQuery()
            cmd2.Dispose()

        End While

            rdr.Close()
        End While
        'Lmsg.Text = revisar
        'TxHorasTrabajadas.Text = acum
    End Sub
End Class
