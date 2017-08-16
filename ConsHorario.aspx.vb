Imports System.Collections
Imports System.Data.SqlClient
Imports System.Data
Imports RHLogica

Partial Class _ConsHorario
    Inherits System.Web.UI.Page
    Private _schuleData As Hashtable

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC

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
        Calendar1.DayStyle.Height = New Unit(65)
        Calendar1.DayStyle.Width = New Unit(90)
        Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow

        Calendar1.TodayDayStyle.BackColor = System.Drawing.Color.LightGreen


        'Calendar1.SelectedDate = Today
        _schuleData = loadSchedule()

        If wucEmpleados2.idEmpleado = 0 Then
            If datos(0) = 2 Then
                wucSucursales.idSucursal = datos(1)
                wucSucursales.Visible = False
                suc.Visible = False
                suc1.Visible = False
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

    End Sub
    Function loadSchedule() As Hashtable
        Dim schedule As New Hashtable
        schedule("17/07/2017") = "" & "<br />" & "" & "<br />" & ""
        Return schedule
    End Function
    Function getSchedule() As Hashtable
        Dim schedule As New Hashtable
        Using con As New SqlConnection
            con.ConnectionString = ConfigurationManager.ConnectionStrings("StarTconnStrRH").ToString
            Dim strSQL As String = "SELECT * FROM MostrarCalendario where idEmpleado ='" & wucEmpleados2.idEmpleado & "'"
            Dim cmd As New SqlCommand(strSQL, con)

            'buscar todos los eventos de la base de datos

            'Dim cmd As New SqlCommand(strSQL, con)
            Dim ds As SqlClient.SqlDataReader

            con.Open()
            ds = cmd.ExecuteReader()
            While ds.Read
                Dim cad As String = ds(1)
                schedule(FormatDateTime(ds(4), DateFormat.ShortDate)) = cad
            End While

            ds = Nothing
            cmd = Nothing
            con.Close()
        End Using

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
            lbl.Font.Bold = True
            lbl.Font.Size = New FontUnit(FontSize.Smaller)
            lbl.ForeColor = Drawing.Color.Black
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
        _schuleData = getSchedule()
        Dim gvds As New ctiCatalogos
        'GridView1.DataSource = gvds.gvPartida_Jornada(wucEmpleados2.idEmpleado)
        'gvds = Nothing
        'GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""


        End If

    End Sub
End Class