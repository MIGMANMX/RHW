
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TFInicio.Text = FIngreso.SelectedDate.ToString("dd/MM/yyyy")

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Protected Sub FIngreso1_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso1.SelectionChanged
        TFFinal0.Text = FIngreso1.SelectedDate.ToString("dd/MM/yyyy")

    End Sub
End Class
