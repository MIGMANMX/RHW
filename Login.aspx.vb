Imports RHLogica

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session("idusuario") = "" : Session("usuario") = "" : Session("nivel") = ""
        Session("idsucursal") = "" : Session("sucursal") = ""
        Session("menu") = ""
    End Sub

    Protected Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        If Request.Form("usuario") <> "" And Request.Form("clave") <> "" Then
            Dim acceso As New ctiAdmin
            Dim ingreso As String = ""
            ingreso = acceso.ingresar(Request.Form("usuario"), Request.Form("clave"))
            If CInt(ingreso.Split(",")(0)) > 0 Then
                Session("idusuario") = ingreso.Split(",")(0)
                Session("usuario") = ingreso.Split(",")(1)
                Session("nivel") = ingreso.Split(",")(2)
                Session("idsucursal") = ingreso.Split(",")(3)
                Session("sucursal") = ingreso.Split(",")(4)
                Response.Redirect("Default.aspx")
            Else
                eValidar.Text = "No existe este usuario o la clave es incorrecta."
            End If
        Else
            eValidar.Text = "Es necesario capturar Usuario y Contraseña."
        End If
    End Sub
End Class
