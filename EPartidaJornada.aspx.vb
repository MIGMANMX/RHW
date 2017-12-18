Imports System.Data
Imports System.Data.SqlClient
Imports RHLogica
Partial Class EPartidaJornada
    Inherits System.Web.UI.Page
    Public gvPos As Integer
    Dim idA As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'chk.Checked = False
        'chksalida.Checked = False

        Lmsg.Text = ""
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC
        FechaC.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday

        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
            wucEmpleados2.ddlAutoPostBack = True

            Session("idz_e") = ""

            If fecha.Text <> "" And wucJornadas.idJornada <> 0 Then
                btnActualizarr.Enabled = True
            Else
                btnActualizarr.Enabled = True
            End If
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
        idpartidas_jornadaT.Text = ""
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvPartida_Jornada2(wucEmpleados2.idEmpleado)
        GridView1.Visible = True
        gvds = Nothing

        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If

    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        fecha.Text = ""
        wucJornadas.idJornada = 0
        wucSucursales.idSucursal = 0
        wucEmpleados2.idEmpleado = 0
        chk.Checked = False
        chksalida.Checked = False
        Lmsg.Text = ""
        GridView1.Visible = False
    End Sub
    Protected Sub btnActualizarr_Click(sender As Object, e As EventArgs) Handles btnActualizarr.Click
        Dim ap As New ctiCatalogos
        'idA = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)
        Dim r As String = ap.actualizarPartidaJornada2(idpartidas_jornadaT.Text, wucEmpleados2.idEmpleado, wucJornadas.idJornada, fecha.Text, chk.Checked, chksalida.Checked)
        GridView1.DataSource = ap.gvPartida_Jornada2(wucEmpleados2.idEmpleado)
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
        btnActualizarr.Enabled = True
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If

            Dim dsP As New ctiCatalogos
            Dim sr As String

            sr = GridView1.Rows(Convert.ToString(e.CommandArgument)).Cells(5).Text

            Dim datos() As String = dsP.datosPartidaJornada2(wucEmpleados2.idEmpleado, Format(CDate(sr), "yyyy-MM-dd"))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                idpartidas_jornadaT.Text = datos(0)
                wucEmpleados2.idEmpleado = CInt(datos(1))
                wucJornadas.idJornada = datos(2).ToString
                fecha.Text = datos(3)
                chk.Checked = datos(4)
                chksalida.Checked = datos(5)
                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing

            End If

            btnActualizarr.Enabled = True
            Lmsg.Text = ""
        End If
    End Sub
    Protected Sub wucJornadas_jornadaSeleccionado(sender As Object, e As System.EventArgs) Handles wucJornadas.jornadaSeleccionado
        wucJornadas.ddlAutoPostBack = True
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        Dim ap As New ctiCatalogos
        'Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(2).Text)
        GridView1.DataSource = ap.gvPartida_Jornada2(wucEmpleados2.idEmpleado)
        ap = Nothing
        GridView1.DataBind()
    End Sub
End Class