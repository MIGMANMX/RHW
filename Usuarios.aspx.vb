Imports RHLogica

Partial Class _Usuarios
    Inherits System.Web.UI.Page
    Public gvPos As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
        End If
        Lmsg.Text = "" : gvPos = 0
        If Request.Form("btnSi") <> "" Then
            Dim ep As New ctiCatalogos
            Dim err As String = ep.eliminarUsuario(CInt(Session("idz_e").ToString))
            GridView1.DataSource = ep.gvUsuarios(wucSucursales.idSucursal)
            ep = Nothing
            GridView1.DataBind()
            If err.StartsWith("Error") Then
                Lmsg.CssClass = "error"
                If IsNumeric(grdSR.Text) AndAlso CInt(grdSR.Text) <= GridView1.Rows.Count Then
                    GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
                End If
            Else
                Lmsg.CssClass = "correcto"
                grdSR.Text = ""
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
            End If
            Lmsg.Text = err
        End If
        Session("idz_e") = ""
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Eliminar" Then
            Session("idz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text
            Session("dz_e") = GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(2).Text
        ElseIf e.CommandName = "Editar" Then
            If IsNumeric(grdSR.Text) Then
                GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Normal
                grdSR.Text = ""
            End If
            Dim dsP As New ctiCatalogos
            Dim datos() As String = dsP.datosUsuario(CInt(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).Cells(0).Text))
            dsP = Nothing
            If datos(0).StartsWith("Error") Then
                Lmsg.CssClass = "error"
                Lmsg.Text = datos(0)
            Else
                nombre.Text = datos(0)
                usuario.Text = datos(1)
                clave.Text = datos(2)
                nivel.Text = datos(3)
                wucSuc.idSucursal = CInt(datos(4))
                grdSR.Text = e.CommandArgument.ToString
                GridView1.Rows(Convert.ToInt32(e.CommandArgument)).RowState = DataControlRowState.Selected
                Dim gvp As New clsCTI
                gvPos = gvp.gridViewScrollPos(CInt(e.CommandArgument))
                gvp = Nothing
                btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = True
            End If
        End If
    End Sub

    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
        End If
        If nivel.Text = "" Then nivel.Text = "0"
        If Not IsNumeric(nivel.Text) Then nivel.Text = "0"
        Dim gp As New ctiCatalogos
        Dim r() As String = gp.agregarUsuario(nombre.Text, usuario.Text, clave.Text, CInt(nivel.Text), wucSuc.idSucursal)
        GridView1.DataSource = gp.gvUsuarios(wucSucursales.idSucursal)
        gp = Nothing
        GridView1.DataBind()
        If r(0).StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
            GridView1.DataBind()
            Dim sgr As New clsCTI
            grdSR.Text = sgr.seleccionarGridRow(GridView1, CInt(r(1))).ToString
            gvPos = sgr.gridViewScrollPos(CInt(grdSR.Text))
            sgr = Nothing
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = True
        End If
        Lmsg.Text = r(0)
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If nivel.Text = "" Then nivel.Text = "0"
        If Not IsNumeric(nivel.Text) Then nivel.Text = "0"
        Dim ap As New ctiCatalogos
        Dim idA As Integer = CInt(GridView1.Rows(Convert.ToInt32(grdSR.Text)).Cells(0).Text)
        Dim r As String = ap.actualizarUsuario(idA, nombre.Text, usuario.Text, clave.Text, CInt(nivel.Text), wucSuc.idSucursal)
        GridView1.DataSource = ap.gvUsuarios(wucSucursales.idSucursal)
        ap = Nothing
        GridView1.DataBind()
        If r.StartsWith("Error") Then
            Lmsg.CssClass = "error"
        Else
            Lmsg.CssClass = "correcto"
        End If
        Dim gvp As New clsCTI
        grdSR.Text = gvp.seleccionarGridRow(GridView1, idA)
        If IsNumeric(grdSR.Text) AndAlso CInt(grdSR.Text) > 0 Then
            GridView1.Rows(Convert.ToInt32(grdSR.Text)).RowState = DataControlRowState.Selected
            gvPos = gvp.gridViewScrollPos(CInt(grdSR.Text))
        Else
            nombre.Text = "" : usuario.Text = "" : clave.Text = "" : nivel.Text = ""
            wucSuc.idSucursal = 0
        End If
        gvp = Nothing
        Lmsg.Text = r
    End Sub
    Protected Sub wucSucursales_sucursalSeleccionada(sender As Object, e As System.EventArgs) Handles wucSucursales.sucursalSeleccionada
        Dim gvds As New ctiCatalogos
        GridView1.DataSource = gvds.gvUsuarios(wucSucursales.idSucursal)
        gvds = Nothing
        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
            btnActualizar.CssClass = "btn btn-info btn-block btn-flat" : btnActualizar.Enabled = False
            nombre.Text = "" : usuario.Text = "" : clave.Text = "" : nivel.Text = ""
            wucSucursales.idSucursal = 0
        End If
    End Sub
End Class
