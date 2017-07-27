
Imports RHLogica

Partial Class _HPartidasJornadas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Login.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
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
        GridView1.DataSource = gvds.gvPartida_Jornada(wucEmpleados2.idEmpleado)
        GridView1.Visible = False
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""


        End If
        btnEditar.Enabled = False
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
End Class
