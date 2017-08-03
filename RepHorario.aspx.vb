Imports System.Collections.Generic

Partial Class _RHorario
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'CalendarioM.Caption = "Seleccione un rango para mostrar"
        'CalendarioM.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        If Not IsPostBack Then
            FIngreso.Visible = False
            FIngreso0.Visible = False

        End If
        FIngreso.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
        FIngreso0.FirstDayOfWeek = WebControls.FirstDayOfWeek.Monday
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If FIngreso.Visible = True Then
            FIngreso.Visible = False
        ElseIf FIngreso.Visible = False Then
            FIngreso.Visible = True
        End If
    End Sub
    Protected Sub FIngreso_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso.SelectionChanged
        TFInicio.Text = FIngreso.SelectedDate.ToString("dd/MM/yyyy")
        FIngreso.Visible = False
    End Sub
    Protected Sub FIngreso0_SelectionChanged(sender As Object, e As EventArgs) Handles FIngreso0.SelectionChanged
        TFFinal.Text = FIngreso0.SelectedDate.ToString("dd/MM/yyyy")
        FIngreso0.Visible = False
    End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        If FIngreso0.Visible = True Then
            FIngreso0.Visible = False
        ElseIf FIngreso0.Visible = False Then
            FIngreso0.Visible = True
        End If
    End Sub
End Class
