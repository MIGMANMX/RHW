Imports RHLogica

Partial Class _Default
    Inherits System.Web.UI.Page
    Private _schuleData As Hashtable
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Login.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
            wucJornadas.ddlAutoPostBack = True
        End If
        'Lmsg.Text = "" : gvPos = 0

        Calendar1.Caption = "Horario de empleado"
        Calendar1.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth
        Calendar1.TitleFormat = TitleFormat.MonthYear
        Calendar1.ShowGridLines = True
        Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Left
        Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top
        Calendar1.DayStyle.Height = New Unit(75)
        Calendar1.DayStyle.Width = New Unit(90)
        Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow

        Calendar1.TodayDayStyle.BackColor = System.Drawing.Color.LightGreen


        'Calendar1.SelectedDate = Today

        _schuleData = getSchedule()




        If Not IsPostBack Then
            FechaC.Visible = False
        End If

    End Sub
    Function getSchedule() As Hashtable

        Dim cal As New ctiCalendario
        'Dim datos() As String = cal.datosJornada(1)
        Dim datos() As String = cal.datosCalendario
        Dim schedule As New Hashtable

        'schedule(datos(0)) = datos(1)
        
        schedule(FormatDateTime(datos(0), DateFormat.ShortDate)) = datos(1) & "<br />" & datos(2) & "<br />" & datos(3)
        
        Return schedule
    End Function

    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender

        If (_schuleData(e.Day.Date.ToShortDateString)) <> Nothing Then
            Dim lit As New Literal
            lit.Text = "<br />"
            e.Cell.Controls.Add(lit)

            Dim lbl As New Label
            Dim str As String = _schuleData(e.Day.Date.ToShortDateString)
            lbl.Text += str
            lbl.Font.Size = New FontUnit(FontSize.Smaller)
            lbl.ForeColor = Drawing.Color.Gray
            e.Cell.Controls.Add(lbl)
            'e.Cell.BackColor = Drawing.Color.Orange
            'e.Cell.CssClass = "calendar-active calendar-event"

        End If

    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvEmpleados(wucSucursales.idSucursal, chkActivo.Checked)
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""

            empleado.Text = ""
        End If

    End Sub

    Protected Sub FechaC_SelectionChanged(sender As Object, e As EventArgs) Handles FechaC.SelectionChanged
        fecha.Text = FechaC.SelectedDate.ToString("dd/MM/yyyy")
        FechaC.Visible = False
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FechaC.Visible = True Then
            FechaC.Visible = False
        ElseIf FechaC.Visible = False Then
            FechaC.Visible = True
        End If
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        fecha.Text = ""

    End Sub

    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        'Calendar1.ddlAutoPostBack = True
        Dim gp As New ctiCatalogos
        Dim r() As String = gp.agregarPartidaJornada(Convert.ToInt32(idempleado.Text), Convert.ToInt32(idjornada.Text), fecha.Text)
        gp = Nothing
        Dim sgr As New clsCTI
        sgr = Nothing
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Editar" Then

            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosEmpleado(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))

         
            dsP = Nothing
            If datos(0).StartsWith("Error") Then

            Else
                empleado.Text = datos(0)
                wucSucursales.idSucursal = CInt(datos(1))
                idempleado.Text = datos(15)



                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI

                gvp = Nothing

            End If
        End If
    End Sub

    Protected Sub wucJornadas_jornadaSeleccionada(sender As Object, e As System.EventArgs) Handles wucJornadas.jornadaSeleccionado
           Dim dsJ As New ctiCalendario
        Dim datos2() As String = dsJ.datosJornada(wucJornadas.idJornada)
        idjornada.Text = datos2(0)
        Dim gvp As New clsCTI

        gvp = Nothing
    End Sub
End Class
