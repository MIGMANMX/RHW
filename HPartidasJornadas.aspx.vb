Imports System.Data
Imports System.Data.SqlClient

Imports RHLogica

Partial Class _HPartidasJornadas
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Private _schuleData As Hashtable
    Public bandera As Boolean
    Public IDP As Integer
    Dim hora As Integer
    Dim h As Boolean = False
    Dim dia As String
    Dim ho As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dsP As New ctiConfiguracion
        Dim datos1() As String = dsP.datosHorario()
        dsP = Nothing
        dia = datos1(0)
        ho = datos1(1)

        THORA.Text = DateTime.Now.ToString("hh")
        hora = Convert.ToInt32(THORA.Text)
        h = False
        btnEliminar.Visible = False

        Lmsg.Text = ""
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC
        FechaC.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday

        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If (Session("nivel")) = 1 Then
            btnEliminar.Visible = True

        End If
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            btnEditar.Enabled = False
            wucSucursales.ddlAutoPostBack = True
            wucEmpleados2.ddlAutoPostBack = True

            If Request("btnSi") <> "" Then
                Dim ec As New ctiCatalogos
                Dim err As String = ec.eliminarPartidas_Jornada(CInt(Session("idz_e")))
                GridView1.DataSource = ec.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                ec = Nothing
                GridView1.DataBind()
                If err.StartsWith("Error") Then
                    Lmsg.CssClass = "error"
                    grdSR.Text = ""

                Else
                    Lmsg.CssClass = "correcto"
                    grdSR.Text = ""



                End If
                Lmsg.Text = err
            End If
            Session("idz_e") = ""


            btnActualizarr.Visible = True

            If GridView1.Visible = False Then
                GridView1.Visible = True
                btnGuardarNuevo.Enabled = False

            ElseIf GridView1.Visible = True Then
                GridView1.Visible = False
                btnGuardarNuevo.Enabled = True
            End If


            If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                btnActualizarr.Enabled = True
            Else
                btnActualizarr.Enabled = True
            End If
        End If
        Calendar1.Caption = "Horario de empleado"
        Calendar1.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth
        Calendar1.TitleFormat = TitleFormat.MonthYear
        Calendar1.ShowGridLines = True
        Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Left
        Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top
        Calendar1.DayStyle.Height = New Unit(55)
        Calendar1.DayStyle.Width = New Unit(140)
        Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow

        Calendar1.TodayDayStyle.BackColor = System.Drawing.Color.LightGreen
        'idpartidas_jornadaT.Text = ""

        'Calendar1.SelectedDate = Today

        _schuleData = loadSchedule()

        If wucEmpleados2.idEmpleado = 0 Then
            If datos(0) = 2 Then
                wucSucursales.idSucursal = datos(1)
                wucSucursales.Visible = False
                suc.Visible = False
                wucEmpleados2.ddlDataSource(datos(1))
                _schuleData = getSchedule()
                gvds = Nothing
                wucEmpleados2.ddlAutoPostBack = True
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""
                End If
            End If
        Else
            wucEmpleados2.ddlAutoPostBack = True
            _schuleData = getSchedule()
        End If
        'bandera = True



    End Sub
    Function loadSchedule() As Hashtable
        Dim schedule As New Hashtable
        schedule("17/07/2017") = "" & "<br />" & "" & "<br />" & ""
        Return schedule
    End Function
    Function getSchedule() As Hashtable
        Dim schedule As New Hashtable

        Dim strSQL As String = "SELECT * FROM MostrarCalendario where idEmpleado = '" & wucEmpleados2.idEmpleado & "'"
        'buscar todos los eventos de la base de datos
        Using con As New SqlConnection
            con.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
            Dim cmd As New SqlCommand(strSQL, con)
            Dim ds As SqlClient.SqlDataReader

            con.Open()
            ds = cmd.ExecuteReader()
            While ds.Read
                schedule(FormatDateTime(ds(4), DateFormat.ShortDate)) = ds(1)
            End While
            ds = Nothing
            cmd = Nothing
            con.Close()
        End Using

        Return schedule
    End Function
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim gvds As New ctiWUC
        Dim acceso As New ctiCatalogos
        wucEmpleados2.ddlDataSource(wucSucursales.idSucursal)
        gvds = Nothing
        wucEmpleados2.ddlAutoPostBack = True
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
        FechaC.SelectedDates.Clear()
    End Sub
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado
        fecha.Text = ""
        wucJornadas.idJornada = 0
        FechaC.SelectedDates.Clear()
        _schuleData = getSchedule()
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvPartida_Jornada(wucEmpleados2.idEmpleado)
        GridView1.Visible = False
        gvds = Nothing

        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""


        End If
        'wucEmpleados2.ddlAutoPostBack = True
        btnEditar.Enabled = True

    End Sub
    Protected Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If GridView1.Visible = False Then
            GridView1.Visible = True
            btnGuardarNuevo.Enabled = False

        ElseIf GridView1.Visible = True Then
            GridView1.Visible = False
            btnGuardarNuevo.Enabled = True
        End If

    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        fecha.Text = ""
        wucJornadas.idJornada = 0
        idpartidas_jornadaT.Text = ""
        TIDPJ.Text = ""
        wucSucursales.idSucursal = 0
        wucEmpleados2.idEmpleado = 0
        _schuleData = loadSchedule()
        Lmsg.Text = ""
        GridView1.Visible = False
        FechaC.SelectedDates.Clear()
    End Sub
    Protected Sub FechaC_SelectionChanged(sender As Object, e As EventArgs) Handles FechaC.SelectionChanged
        'FechaC.SelectedDates.Clear()
        'wucJornadas.idJornada = 0
        fecha.Text = FechaC.SelectedDate.ToString("dd/MM/yyyy")
        TIDPJ.Text = ""
        Dim sr As String
        sr = fecha.Text

        Dim dts As New ctiCatalogos
        Dim da() As String = dts.datosPJ(wucEmpleados2.idEmpleado, Format(CDate(sr), "yyyy-MM-dd"))
        dts = Nothing
        If da(0).StartsWith("Error") Then
            Lmsg.CssClass = "error"
            Lmsg.Text = da(0)
        Else
            TIDPJ.Text = da(0)
            IDP = da(0)

            idpartidas_jornadaT.Text = da(0)
            wucJornadas.idJornada = da(1)
        End If
        'If wucJornadas.idJornada = 0 Then
        '    'btnGuardarNuevo.Enabled = True
        'End If
        If TIDPJ.Text <> "" Then
            bandera = False
            btnActualizarr.Enabled = True
            btnGuardarNuevo.Enabled = False
        Else
            bandera = True
            btnActualizarr.Enabled = True
            btnGuardarNuevo.Enabled = True
        End If
        Lmsg.Text = ""
    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        Dim fech As Date
        fech = Now
        DiaS.Text = fech.DayOfWeek.ToString()

        If (Session("nivel")) = 1 Or (Session("nivel")) = 7 Or (Session("nivel")) = 8 Then
            If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                Dim gp As New ctiCatalogos
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""

                End If
                Dim gc As New ctiCatalogos
                Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                gc = Nothing
                GridView1.DataBind()
                If r(0).StartsWith("Error") Then
                    Lmsg.CssClass = "error"
                Else
                    Lmsg.CssClass = "correcto"
                    Dim sgr As New clsCTI
                    sgr = Nothing
                    FechaC.SelectedDates.Clear()
                End If
                Lmsg.Text = r(0)
                _schuleData = getSchedule()
            Else
                Lmsg.Text = "Error: Seleccione una fecha o una Jornada"
            End If
        Else
            If DiaS.Text = "Monday" Or DiaS.Text = "Tuesday" Or DiaS.Text = "Wednesday" Or DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
                If DiaS.Text = "Monday" And hora < 10 Then
                    'If DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
                    '    If DiaS.Text = "Wednesday" And hora < 10 Then
                    If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                        Dim gp As New ctiCatalogos
                        If IsNumeric(grdSR.Text) Then
                            grdSR.Text = ""

                        End If
                        Dim gc As New ctiCatalogos
                        Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                        GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                        gc = Nothing
                        GridView1.DataBind()
                        If r(0).StartsWith("Error") Then
                            Lmsg.CssClass = "error"
                        Else
                            Lmsg.CssClass = "correcto"
                            Dim sgr As New clsCTI
                            sgr = Nothing

                        End If
                        Lmsg.Text = r(0)
                        _schuleData = getSchedule()
                    Else
                        Lmsg.Text = "Error: Seleccione una fecha o una Jornada"
                    End If
                    h = False
                Else
                    Lmsg.Text = "Termino el tiempo de Captura"
                    h = True
                End If
                If h = False Then
                    '  Lmsg.Text = "No esta permitido capturar"

                End If
            Else
                If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                    Dim gp As New ctiCatalogos
                    If IsNumeric(grdSR.Text) Then
                        grdSR.Text = ""

                    End If
                    Dim gc As New ctiCatalogos
                    Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                    GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                    gc = Nothing
                    GridView1.DataBind()
                    If r(0).StartsWith("Error") Then
                        Lmsg.CssClass = "error"
                    Else
                        Lmsg.CssClass = "correcto"
                        Dim sgr As New clsCTI

                        sgr = Nothing

                    End If
                    Lmsg.Text = r(0)
                    _schuleData = getSchedule()
                Else
                    Lmsg.Text = "Error: Seleccione una fecha o una Jornada"
                End If
            End If
        End If
    End Sub
    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender
        If (_schuleData(e.Day.Date.ToShortDateString)) <> Nothing Then
            Dim lit As New Literal
            lit.Text = "<br/>"
            e.Cell.Controls.Add(lit)

            Dim lbl As New Label
            Dim str As String = _schuleData(e.Day.Date.ToShortDateString)
            lbl.Text += str
            lbl.Font.Size = New FontUnit(FontSize.Smaller)
            lbl.ForeColor = Drawing.Color.Black
            e.Cell.Controls.Add(lbl)
            'e.Cell.BackColor = Drawing.Color.Orange
            'e.Cell.CssClass = "calendar-active calendar-event"

        End If
    End Sub
    Protected Sub btnActualizarr_Click(sender As Object, e As EventArgs) Handles btnActualizarr.Click
        Dim fech As Date
        fech = Now
        DiaS.Text = fech.DayOfWeek.ToString()

        If (Session("nivel")) = 1 Or (Session("nivel")) = 7 Or (Session("nivel")) = 8 Then
            Dim ap As New ctiCatalogos
            If bandera = True Then

                Dim idA As Integer = CInt(idpartidas_jornadaT.Text)

                Dim r As String = ap.actualizarPartidaJornada(idA, wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                GridView1.DataSource = ap.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                ap = Nothing
                GridView1.DataBind()
                If r.StartsWith("Error") Then
                    Lmsg.CssClass = "error"
                Else
                    Lmsg.CssClass = "correcto"

                End If

                Dim gvp As New clsCTI
                gvp = Nothing
                Lmsg.Text = r
                _schuleData = getSchedule()
                FechaC.SelectedDates.Clear()
            ElseIf bandera = False Then

                Dim idA As Integer = CInt(TIDPJ.Text)

                Dim r As String = ap.actualizarPartidaJornada(idA, wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                GridView1.DataSource = ap.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                ap = Nothing
                GridView1.DataBind()
                If r.StartsWith("Error") Then
                    Lmsg.CssClass = "error"
                Else
                    Lmsg.CssClass = "correcto"
                    FechaC.SelectedDates.Clear()
                End If

                Dim gvp As New clsCTI

                gvp = Nothing
                Lmsg.Text = r
                _schuleData = getSchedule()
                bandera = True
            End If
            btnActualizarr.Enabled = True
        Else
            'If DiaS.Text = "Monday" Or DiaS.Text = "Tuesday" Or DiaS.Text = "Wednesday" Or DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
            '    If DiaS.Text = "Monday" And hora < 10 Then
            'If DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
            '    If DiaS.Text = "Wednesday" And hora < 10 Then
            If DiaS.Text = "Monday" Or DiaS.Text = "Tuesday" Or DiaS.Text = "Wednesday" Or DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
                If DiaS.Text = "Monday" And hora < 10 Then
                    Dim ap As New ctiCatalogos
                    If bandera = True Then

                        Dim idA As Integer = CInt(idpartidas_jornadaT.Text)

                        Dim r As String = ap.actualizarPartidaJornada(idA, wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                        GridView1.DataSource = ap.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                        ap = Nothing
                        GridView1.DataBind()
                        If r.StartsWith("Error") Then
                            Lmsg.CssClass = "error"
                        Else
                            Lmsg.CssClass = "correcto"
                            FechaC.SelectedDates.Clear()
                        End If

                        Dim gvp As New clsCTI

                        gvp = Nothing
                        Lmsg.Text = r
                        _schuleData = getSchedule()

                    ElseIf bandera = False Then

                        Dim idA As Integer = CInt(TIDPJ.Text)

                        Dim r As String = ap.actualizarPartidaJornada(idA, wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                        GridView1.DataSource = ap.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                        ap = Nothing
                        GridView1.DataBind()
                        If r.StartsWith("Error") Then
                            Lmsg.CssClass = "error"
                        Else
                            Lmsg.CssClass = "correcto"

                        End If

                        Dim gvp As New clsCTI
                        gvp = Nothing
                        Lmsg.Text = r
                        _schuleData = getSchedule()
                        bandera = True
                    End If
                    btnActualizarr.Enabled = True
                    h = False
                Else
                    Lmsg.Text = "Termino el tiempo de Captura"
                    h = True
                End If
                If h = False Then
                    Lmsg.Text = "No esta permitido capturar"
                End If

            Else
                Dim ap As New ctiCatalogos
                If bandera = True Then

                    Dim idA As Integer = CInt(idpartidas_jornadaT.Text)

                    Dim r As String = ap.actualizarPartidaJornada(idA, wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                    GridView1.DataSource = ap.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                    ap = Nothing
                    GridView1.DataBind()
                    If r.StartsWith("Error") Then
                        Lmsg.CssClass = "error"
                    Else
                        Lmsg.CssClass = "correcto"
                        FechaC.SelectedDates.Clear()
                    End If

                    Dim gvp As New clsCTI
                    gvp = Nothing
                    Lmsg.Text = r
                    _schuleData = getSchedule()

                ElseIf bandera = False Then

                    Dim idA As Integer = CInt(TIDPJ.Text)

                    Dim r As String = ap.actualizarPartidaJornada(idA, wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                    GridView1.DataSource = ap.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                    ap = Nothing
                    GridView1.DataBind()
                    If r.StartsWith("Error") Then
                        Lmsg.CssClass = "error"
                    Else
                        Lmsg.CssClass = "correcto"
                        FechaC.SelectedDates.Clear()
                    End If

                    Dim gvp As New clsCTI
                    gvp = Nothing
                    Lmsg.Text = r
                    _schuleData = getSchedule()
                    bandera = True
                End If
                btnActualizarr.Enabled = True
            End If
        End If
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If

            Dim dsP As New ctiCatalogos
            Dim sr As String

            sr = GridView1.Rows(Convert.ToString(e.CommandArgument)).Cells(6).Text

            Dim datos() As String = dsP.datosPartidaJornada(wucEmpleados2.idEmpleado, Format(CDate(sr), "yyyy-MM-dd"))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                idpartidas_jornadaT.Text = datos(0)
                wucEmpleados2.idEmpleado = CInt(datos(1))
                wucJornadas.idJornada = datos(2)
                fecha.Text = datos(3).ToString
                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing

            End If
            bandera = True
            btnActualizarr.Enabled = True
            Lmsg.Text = ""
        End If
    End Sub
    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        Dim ap As New ctiCatalogos
        Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(2).Text)
        GridView1.DataSource = ap.gvPartida_Jornada(wucEmpleados2.idEmpleado)
        ap = Nothing
        GridView1.DataBind()
    End Sub
    Protected Sub wucJornadas_jornadaSeleccionado(sender As Object, e As System.EventArgs) Handles wucJornadas.jornadaSeleccionado
        wucJornadas.ddlAutoPostBack = True
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
    End Sub
    Protected Sub btnFechaSemana_Click(sender As Object, e As EventArgs) Handles btnFechaSemana.Click
        Dim fech As Date
        fech = Now
        DiaS.Text = fech.DayOfWeek.ToString()

        If (Session("nivel")) = 1 Or (Session("nivel")) = 7 Or (Session("nivel")) = 8 Then
            Dim cont As Integer
            cont = 0

            If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                If cont = 0 Then
                    Dim gp As New ctiCatalogos
                    If IsNumeric(grdSR.Text) Then
                        grdSR.Text = ""

                    End If
                    Dim gc As New ctiCatalogos
                    Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                    GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                    gc = Nothing
                    GridView1.DataBind()
                    If r(0).StartsWith("Error") Then
                        Lmsg.CssClass = "error"
                    Else
                        Lmsg.CssClass = "correcto"
                        Dim sgr As New clsCTI
                        sgr = Nothing
                        cont = cont + 1
                        FechaC.SelectedDates.Clear()
                    End If
                    Lmsg.Text = r(0)
                    _schuleData = getSchedule()

                End If

                While cont > 0 And cont < 7

                    Dim gp As New ctiCatalogos
                    If IsNumeric(grdSR.Text) Then
                        grdSR.Text = ""

                    End If
                    Dim gc As New ctiCatalogos
                    Dim fe As New Date
                    fe = Convert.ToDateTime(fecha.Text)

                    Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, DateAdd(DateInterval.Day, cont, fe))
                    GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                    gc = Nothing
                    GridView1.DataBind()
                    If r(0).StartsWith("Error") Then
                        Lmsg.CssClass = "error"
                    Else
                        Lmsg.CssClass = "correcto"
                        Dim sgr As New clsCTI
                        sgr = Nothing
                        FechaC.SelectedDates.Clear()
                    End If
                    Lmsg.Text = r(0)
                    _schuleData = getSchedule()
                    cont = cont + 1
                End While

            Else
                Lmsg.Text = "Error: Seleccione una Fecha o una Jornada"

            End If

        Else
            'If DiaS.Text = "Monday" Or DiaS.Text = "Tuesday" Or DiaS.Text = "Wednesday" Or DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
            '    If DiaS.Text = "Wednesday" And hora < 10 Then
            'If DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
            '    If DiaS.Text = "Wednesday" And hora < 10 Then
            If DiaS.Text = "Monday" Or DiaS.Text = "Tuesday" Or DiaS.Text = "Wednesday" Or DiaS.Text = "Thursday" Or DiaS.Text = "Friday" Then
                If DiaS.Text = "Monday" And hora < 10 Then
                    Dim cont As Integer
                    cont = 0

                    If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                        If cont = 0 Then
                            Dim gp As New ctiCatalogos
                            If IsNumeric(grdSR.Text) Then
                                grdSR.Text = ""

                            End If
                            Dim gc As New ctiCatalogos
                            Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                            GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                            gc = Nothing
                            GridView1.DataBind()
                            If r(0).StartsWith("Error") Then
                                Lmsg.CssClass = "error"
                            Else
                                Lmsg.CssClass = "correcto"
                                Dim sgr As New clsCTI
                                sgr = Nothing
                                cont = cont + 1
                                FechaC.SelectedDates.Clear()
                            End If
                            Lmsg.Text = r(0)
                            _schuleData = getSchedule()

                        End If

                        While cont > 0 And cont < 7

                            Dim gp As New ctiCatalogos
                            If IsNumeric(grdSR.Text) Then
                                grdSR.Text = ""

                            End If
                            Dim gc As New ctiCatalogos
                            Dim fe As New Date
                            fe = Convert.ToDateTime(fecha.Text)

                            Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, DateAdd(DateInterval.Day, cont, fe))
                            GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                            gc = Nothing
                            GridView1.DataBind()
                            If r(0).StartsWith("Error") Then
                                Lmsg.CssClass = "error"
                            Else
                                Lmsg.CssClass = "correcto"
                                Dim sgr As New clsCTI
                                sgr = Nothing
                                FechaC.SelectedDates.Clear()
                            End If
                            Lmsg.Text = r(0)
                            _schuleData = getSchedule()
                            cont = cont + 1
                        End While

                    Else
                        Lmsg.Text = "Error: Seleccione una Fecha o una Jornada"

                    End If
                    h = False
                Else
                    Lmsg.Text = "Termino el tiempo de Captura"
                    h = True
                End If
                If h = False Then
                    Lmsg.Text = "No esta permitido capturar"

                End If
            Else
                Dim cont As Integer
                cont = 0

                If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                    If cont = 0 Then
                        Dim gp As New ctiCatalogos
                        If IsNumeric(grdSR.Text) Then
                            grdSR.Text = ""

                        End If
                        Dim gc As New ctiCatalogos
                        Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
                        GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                        gc = Nothing
                        GridView1.DataBind()
                        If r(0).StartsWith("Error") Then
                            Lmsg.CssClass = "error"
                        Else
                            Lmsg.CssClass = "correcto"
                            Dim sgr As New clsCTI
                            sgr = Nothing
                            cont = cont + 1
                            FechaC.SelectedDates.Clear()
                        End If
                        Lmsg.Text = r(0)
                        _schuleData = getSchedule()

                    End If

                    While cont > 0 And cont < 7

                        Dim gp As New ctiCatalogos
                        If IsNumeric(grdSR.Text) Then
                            grdSR.Text = ""

                        End If
                        Dim gc As New ctiCatalogos
                        Dim fe As New Date
                        fe = Convert.ToDateTime(fecha.Text)

                        Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, DateAdd(DateInterval.Day, cont, fe))
                        GridView1.DataSource = gc.gvPartida_Jornada(wucEmpleados2.idEmpleado)
                        gc = Nothing
                        GridView1.DataBind()
                        If r(0).StartsWith("Error") Then
                            Lmsg.CssClass = "error"
                        Else
                            Lmsg.CssClass = "correcto"
                            Dim sgr As New clsCTI
                            sgr = Nothing
                            FechaC.SelectedDates.Clear()
                        End If
                        Lmsg.Text = r(0)
                        _schuleData = getSchedule()
                        cont = cont + 1
                    End While

                Else
                    Lmsg.Text = "Error: Seleccione una Fecha o una Jornada"

                End If

            End If
        End If


    End Sub
    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If idpartidas_jornadaT.Text = "" Or idpartidas_jornadaT.Text = " " Then
            Lmsg.Text = "Error : Selecciona primero una fecha a eliminar"
        Else
            Dim ec As New ctiCatalogos
            Dim err As String = ec.eliminarPartidas_Jornada(idpartidas_jornadaT.Text)
            GridView1.DataSource = ec.gvPartida_Jornada(wucEmpleados2.idEmpleado)
            ec = Nothing
            GridView1.DataBind()
            If err.StartsWith("Error") Then
                Lmsg.CssClass = "error"
                grdSR.Text = ""

            Else
                Lmsg.CssClass = "correcto"
                grdSR.Text = ""



            End If
            Lmsg.Text = err
            _schuleData = getSchedule()
            fecha.Text = ""
            wucJornadas.idJornada = 0
            idpartidas_jornadaT.Text = ""
            TIDPJ.Text = ""
        End If

    End Sub
End Class