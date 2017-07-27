
Imports RHLogica

Partial Class _HPartidasJornadas
    Inherits System.Web.UI.Page
    Private _schuleData As Hashtable
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Login.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            btnEditar.Enabled = False
            wucSucursales.ddlAutoPostBack = True
            wucEmpleados2.ddlAutoPostBack = True

            btnActualizarr.Visible = False

            If GridView1.Visible = False Then
                GridView1.Visible = True
                btnGuardarNuevo.Enabled = False
                btnActualizarr.Visible = True
            ElseIf GridView1.Visible = True Then
                GridView1.Visible = False
                btnGuardarNuevo.Enabled = True

                btnActualizarr.Visible = False
            End If

        End If
        Calendar1.Caption = "Horario de empleado"
        Calendar1.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth
        Calendar1.TitleFormat = TitleFormat.MonthYear
        Calendar1.ShowGridLines = True
        Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Left
        Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top
        Calendar1.DayStyle.Height = New Unit(40)
        Calendar1.DayStyle.Width = New Unit(100)
        Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow

        Calendar1.TodayDayStyle.BackColor = System.Drawing.Color.LightGreen


        'Calendar1.SelectedDate = Today

        _schuleData = getSchedule()
    End Sub
    Function getSchedule() As Hashtable

        Dim cal As New ctiCalendario
        'Dim datos() As String = cal.datosJornada(1)

        Dim datos() As String = cal.datosCalendario
        Dim schedule As New Hashtable


        'schedule(datos(0)) = datos(1)

        schedule(FormatDateTime(datos(4), DateFormat.ShortDate)) = datos(1) & "<br />" & datos(2) & "-" & datos(3)

        Return schedule
    End Function
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim gvds As New ctiWUC
        wucEmpleados2.ddlDataSource(wucSucursales.idSucursal)

        gvds = Nothing
        wucEmpleados2.ddlAutoPostBack = True
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If

    End Sub

    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvPartida_Jornada(wucEmpleados2.idEmpleado)
        GridView1.Visible = False
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""


        End If
        btnEditar.Enabled = True
    End Sub
    Protected Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If GridView1.Visible = False Then
            GridView1.Visible = True
            btnGuardarNuevo.Enabled = False
            btnActualizarr.Visible = True
        ElseIf GridView1.Visible = True Then
            GridView1.Visible = False
            btnGuardarNuevo.Enabled = True

            btnActualizarr.Visible = False
        End If

    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        fecha.Text = ""
        wucJornadas.idJornada = 0
    End Sub
    Protected Sub FechaC_SelectionChanged(sender As Object, e As EventArgs) Handles FechaC.SelectionChanged
        fecha.Text = FechaC.SelectedDate.ToString("dd/MM/yyyy")
    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        Dim gp As New ctiCatalogos
        Dim r() As String = gp.agregarPartidaJornada(wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text)
        gp = Nothing
        Dim sgr As New clsCTI
        sgr = Nothing

    End Sub
    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender
        If (_schuleData(e.Day.Date.ToShortDateString)) <> Nothing Then
            Dim lit As New Literal
            lit.Text = "<br />"
            e.Cell.Controls.Add(lit)

            Dim lbl As New Label
            Dim str As String = _schuleData(e.Day.Date.ToShortDateString)
            lbl.Text += str
            lbl.Font.Size = New FontUnit(FontSize.XXSmall)
            lbl.ForeColor = Drawing.Color.Gray
            e.Cell.Controls.Add(lbl)
            'e.Cell.BackColor = Drawing.Color.Orange
            'e.Cell.CssClass = "calendar-active calendar-event"

        End If
    End Sub
End Class
