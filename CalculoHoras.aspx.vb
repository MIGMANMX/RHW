
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
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

        reporte.ServerReport.Refresh()


    End Sub
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado

        idEmpleadoTX.Text = wucEmpleados2.idEmpleado.ToString

        Dim dsP As New ctiCatalogos
        Dim sr As String()

        sr = dsP.datosEmpleado(wucEmpleados2.idEmpleado)

        '  TxEmpleado.Text = sr(0)
        ' TxEmpleado.Text = wucEmpleados
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


                Dim FechaFinal As Date
                Dim FechaFinal2 As Date
                FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
                FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")


                Dim ec As New ctiCalculo
                GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
                ' GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(FechaFinal), "yyyy-MM-dd"))

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
        GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(TxFechaFin.Text), "yyyy-dd-MM"), 3)
        ec = Nothing
        GridView1.DataBind()
    End Sub
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        HTotales()
        HTrabajadas()
        HorasExtras()
        'DDescansados()
        'DDescansadosTrabajados()
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
        ' Dim Acum As TimeSpan = TimeSpan.Zero
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
                Dim cmd As New SqlCommand("Select inicio,fin From vm_Jornada where idempleado=@idempleado AND fecha BETWEEN '" & Fech.ToString("yyyy-MM-dd") & "'  AND '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' Order BY fecha", dbC)
                'Dim cmd As New SqlCommand("Select * From Chequeo where chec>=@chec AND chec <= '" & DateAdd(DateInterval.Day, 1, Fech).ToString("yyyy-MM-dd") & "' AND idempleado=@idempleado Order BY chec ", dbC)

                cmd.Parameters.AddWithValue("idempleado", wucEmpleados2.idEmpleado)
                cmd.Parameters.AddWithValue("fecha", Fech)
                Dim rdr As SqlDataReader = cmd.ExecuteReader
                Dim dsP As String()
                If rdr.Read Then
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
                    'Limpiar variables
                    HI = 0
                    HF = 0
                    If tsDiferencia < 1 Then
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
    Public Sub HTrabajadas()
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
    Public Sub HorasExtras()
        Dim A, B As Integer
        A = Convert.ToInt32(TxHorasTrabajadas.Text)
        B = Convert.ToInt32(TxHtotales.Text)
        TxHorasExtras.Text = A - B
    End Sub

    'Protected Sub GVCalculoH_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GVCalculoH.RowCancelingEdit
    '    'Dim FechaFinal As Date
    '    'Dim FechaFinal2 As Date
    '    'FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
    '    'FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")


    '    'Dim ec As New ctiCalculo
    '    'GVCalculoH.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
    '    '' GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(FechaFinal), "yyyy-MM-dd"))

    '    'ec = Nothing
    '    'GVCalculoH.DataBind()

    '    'GVCalculoH.EditIndex = -1

    'End Sub
    'Protected Sub GVCalculoH_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GVCalculoH.RowEditing
    'Dim FechaFinal As Date
    'Dim FechaFinal2 As Date
    'FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
    'FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")


    'Dim ec As New ctiCalculo
    'GVCalculoH.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
    '' GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(FechaFinal), "yyyy-MM-dd"))

    'ec = Nothing
    'GVCalculoH.DataBind()



    'GVCalculoH.EditIndex = e.NewEditIndex

    ' End Sub
    'Protected Sub GVCalculoH_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GVCalculoH.RowDeleting
    '    'Dim FechaFinal As Date
    '    'Dim FechaFinal2 As Date
    '    'FechaFinal = Convert.ToDateTime(TxFechaFin.Text)
    '    'FechaFinal2 = DateAdd(DateInterval.Day, 1, FechaFinal).ToString("yyyy-MM-dd")


    '    'Dim ec As New ctiCalculo
    '    'GVCalculoH.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-dd-MM"), Format(CDate(FechaFinal2), "yyyy-dd-MM"), 3)
    '    '' GridView1.DataSource = ec.gvChequeo(wucEmpleados2.idEmpleado, Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd"), Format(CDate(FechaFinal), "yyyy-MM-dd"))

    '    'ec = Nothing
    '    'GVCalculoH.DataBind()
    '    'GVCalculoH.EditIndex = -1

    'End Sub
    Protected Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click


        'Creación de un objeto CultureInfo con referencia cultural de México
        Dim c As CultureInfo = New CultureInfo("es-MX")

        reporte.ServerReport.Refresh()
        If wucSucursales.idSucursal <> 0 Then
            If TxFechaInicio.Text <> "" And TxFechaFin.Text <> "" Then
                Dim dt1 As DateTime
                dt1 = Format(CDate(TxFechaInicio.Text), "yyyy-MM-dd")
                Dim dt2 As DateTime
                dt2 = Format(CDate(TxFechaFin.Text), "yyyy-MM-dd")

                Dim Htotales As Integer = 0
                Dim HTrabajadas As Integer = 0

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

                Dim p As New ReportParameter("Fech1", dt1)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("Fech2", dt2)
                reporte.LocalReport.SetParameters(p)

                p = New ReportParameter("idempleado", idEmpleadoTX.Text)
                reporte.LocalReport.SetParameters(p)

                'p = New ReportParameter("HTotales", Htotales)
                'reporte.LocalReport.SetParameters(p)

                'p = New ReportParameter("HTrabajadas", HTrabajadas)
                'reporte.LocalReport.SetParameters(p)

                reporte.ServerReport.Refresh()
            Else
                    Lmsg.Text = "Error: Debes capturar una fecha"
            End If
        Else
            Lmsg.Text = "Error: Debes seleccionar una sucursal"
        End If
    End Sub


    'Protected Sub GVCalculoH_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GVCalculoH.RowUpdating

    '    Dim cad1 As String = GVCalculoH.Rows(1).Cells(0).ToString()
    '    Dim cad2 As String = GVCalculoH.Rows(1).Cells(3).ToString()


    '    Dim ec As New ctiCalculo
    '    GVCalculoH.DataSource = ec.gvAsigIncidencias(3,
    '                                                  wucEmpleados2.idEmpleado, GVCalculoH.Rows(1).Cells(0).ToString,
    '                                                 GVCalculoH.Rows(1).Cells(3).ToString)
    '    'CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))

    '    ec = Nothing
    '    GVCalculoH.DataBind()
    'End Sub
    'Protected Sub GVCalculoH_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GVCalculoH.RowCommand
    '    'If e.CommandName = "Update" Then
    '    '    If IsNumeric(grdSR.Text) Then
    '    '        GVCalculoH.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
    '    '        grdSR.Text = ""
    '    '    End If


    '    '    Dim sr As String

    '    '    'sr = GVCalculoH.Rows(1).Cells(0).Text
    '    '    'TxDDescasados.Text = sr

    '    '    TxDDescasados.Text = GVCalculoH.SelectedRow.Cells(0).Text

    '    'End If
    'End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If



            Dim dsP As New ctiCatalogos
            Dim sr As String

            sr = GridView1.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text

            TxDDescasados.Text = sr
        End If
    End Sub
End Class
