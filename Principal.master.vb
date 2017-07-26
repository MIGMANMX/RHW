
Partial Class Principal
    Inherits System.Web.UI.MasterPage

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Session.Clear()
        Response.Redirect("Login.aspx")
    End Sub
End Class

