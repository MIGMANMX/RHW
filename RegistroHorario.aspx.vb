Imports System.Collections
Imports System.Data.SqlClient
Imports System.Data
Imports RHLogica

Partial Class _RegistroHorario
    Inherits System.Web.UI.Page
    Private _schuleData As Hashtable

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Login.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
            wucEmpleados2.ddlAutoPostBack = True
        End If



        Calendar1.Caption = "Horario de empleado"
        Calendar1.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth
        Calendar1.TitleFormat = TitleFormat.MonthYear
        Calendar1.ShowGridLines = True
        Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Left
        Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top
        Calendar1.DayStyle.Height = New Unit(95)
        Calendar1.DayStyle.Width = New Unit(120)
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
        schedule("17/07/2017") = "Tiempo Completo" & "<br />" & "8:00:00" & "<br />" & "16:00:00"
        schedule("18/07/2017") = "Tiempo Completo" & "<br />" & "8:00:00" & "<br />" & "16:00:00"
        schedule("19/07/2017") = "Tiempo Completo" & "<br />" & "8:00:00" & "<br />" & "16:00:00"
        'schedule(FormatDateTime(datos(0), DateFormat.ShortDate)) = datos(1) & "<br />" & datos(2) & "<br />" & datos(3)
        schedule("21/07/2017") = "Tiempo Completo" & "<br />" & "8:00:00" & "<br />" & "16:00:00"
        schedule("22/07/2017") = "Tiempo Completo" & "<br />" & "8:00:00" & "<br />" & "16:00:00"
        schedule("23/07/2017") = "Descanso" & "<br />" & "" & "<br />" & ""
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

        'If e.Day.IsToday Then
        '    e.Cell.BackColor = Drawing.Color.CadetBlue
        '    e.Cell.ForeColor = Drawing.Color.Black
        'End If
    End Sub
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
        'GridView1.DataSource = gvds.gvPartida_Jornada(wucEmpleados2.idEmpleado)
        'gvds = Nothing
        'GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""


        End If

    End Sub
End Class




