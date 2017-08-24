
Imports RHLogica

Partial Class Principal
    Inherits System.Web.UI.MasterPage

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Session.Clear()
        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session("idusuario")) Then
            Response.Redirect("Default.aspx", True)
        End If
        direc.Visible = False
        Dim acceso As New ctiCatalogos
        Dim datos() As String = acceso.datosUsuarioV(Session("idusuario"))
        If datos(0) >= 3 Then
            Response.Redirect("Personal.aspx", True)
        End If
        If datos(0) = 2 Then
            direc.Visible = True
            catalogo.Visible = False
            catalogo2.Visible = False
            catalogo1.Visible = False
            nomina.Visible = False
            acc.Visible = False
            jor.Visible = False
            rep.Visible = True
            che.Visible = False
            repo.Visible = False
            incide.Visible = False
            regInidencia.Visible = True
        End If
    End Sub
End Class

