
Imports RHLogica

Partial Class Particulares
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        Dim gvds As New ctiWUC

        If IsNothing(Session("usuario")) Then Response.Redirect("Default.aspx", True)
        If Not Page.IsPostBack Then
            Session("menu") = "C"
            wucSucursales.ddlAutoPostBack = True
            wucEmpleados2.ddlAutoPostBack = True
        End If
        FIngreso.Visible = False

        If wucEmpleados2.idEmpleado = 0 Then
            If datos(0) = 2 Then
                wucSucursales.idSucursal = datos(1)
                wucSucursales.Visible = False
                wucEmpleados2.ddlDataSource(datos(1))
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
        Dim gvds As New ctiCalendario
        GridView1.DataSource = gvds.gvParticulares(wucEmpleados2.idEmpleado)
        GridView1.Visible = True
        gvds = Nothing

        GridView1.DataBind()
        If IsNumeric(grdSR.Text) Then
            grdSR.Text = ""
        End If
        'btnActualizar.Enabled = True
        dropLTipo.Enabled = True
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FIngreso.Visible = True Then
            FIngreso.Visible = False
        ElseIf FIngreso.Visible = False Then
            FIngreso.Visible = True
        End If
    End Sub
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        fecha_ingreso.Text = FIngreso.SelectedDate.ToString("yyyy-MM-dd")
        FIngreso.Visible = False
    End Sub
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click

    End Sub
    Protected Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click

    End Sub
End Class
