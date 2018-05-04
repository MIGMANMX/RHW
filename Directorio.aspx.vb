Imports RHLogica

Partial Class _Directorio
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

        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC
        If wucEmpleados2.idEmpleado = 0 Then
            If datos(0) = 2 Then
                wucSucursales.idSucursal = datos(1)
                wucSucursales.Visible = False
                suc.Visible = False
                wucEmpleados2.ddlDataSource(datos(1))
                idsucursal.Text = datos(1)
                gvds = Nothing
                wucEmpleados2.ddlAutoPostBack = True
                If IsNumeric(grdSR.Text) Then
                    grdSR.Text = ""
                End If
            End If
        Else
            wucEmpleados2.ddlAutoPostBack = True

        End If

    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If idempleado.Text <> "" And empleado.Text <> "" And idsucursal.Text <> "" And calle.Text <> "" And numero.Text <> "" And colonia.Text <> "" And cp.Text <> "" And telefono.Text <> "" And correo.Text <> "" And claveTX.Text <> "" Then
            Dim ap As New ctiCatalogos

            Dim r As String = ap.actualizarDirectorio(Convert.ToInt32(idempleado.Text), empleado.Text, idsucursal.Text, calle.Text, numero.Text, colonia.Text, cp.Text, telefono.Text, correo.Text, claveTX.Text, nombreTxt.Text, telefonoTxt.Text)

            ap = Nothing

            If r.StartsWith("Error") Then
                Lmsg.CssClass = "error"
            Else
                Lmsg.CssClass = "correcto"
            End If
            Lmsg.Text = r
        Else
            Lmsg.Text = "Error: Falta Capturar algun dato"
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
        idsucursal.Text = wucSucursales.idSucursal
    End Sub
    Protected Sub wucEmpleados_empleadoSeleccionada(sender As Object, e As System.EventArgs) Handles wucEmpleados2.empleadoSeleccionado
        Dim dsP As New ctiCatalogos
        Dim datos() As String = dsP.datosEmpleado(wucEmpleados2.idEmpleado)
        dsP = Nothing
        If datos(0).StartsWith("Error") Then
            Lmsg.CssClass = "error"
            Lmsg.Text = datos(0)
        Else
            empleado.Text = datos(0)

            calle.Text = datos(8)
            numero.Text = datos(9)
            colonia.Text = datos(10)
            cp.Text = datos(11)
            telefono.Text = datos(12)
            correo.Text = datos(13)
            idempleado.Text = datos(15)
            claveTX.Text = datos(16)

            nombreTxt.Text = datos(20)
            telefonoTxt.Text = datos(21)
            nombreTxt.Text = datos(22)
        End If
    End Sub

End Class
