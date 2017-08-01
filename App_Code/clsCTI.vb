Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System

Public Class clsCTI

    Public Shared multiE As Boolean = True
    Public Shared contpaqF As Boolean = True

    Public Function sumarColumnaGrid(ByVal grd As GridView, ByVal col As Integer) As Double
        Dim sCG As Double = 0
        Dim gR As GridViewRow
        For Each gR In grd.Rows
            If IsNumeric(gR.Cells(col).Text) Then sCG += CDbl(gR.Cells(col).Text)
        Next
        Return sCG
    End Function

    Public Function seleccionarGridRow(ByVal grd As GridView, ByVal val As Integer) As Integer
        Dim ik As Integer = 0, sr As Integer = 0
        While ik < grd.Rows.Count
            If CInt(grd.Rows(Convert.ToInt32(ik)).Cells(0).Text) = val Then
                grd.Rows(Convert.ToInt32(ik)).RowState = DataControlRowState.Selected
                sr = ik
                ik = grd.Rows.Count
            End If
            ik += 1
        End While
        Return sr
    End Function
    Public Function seleccionarGridRow2(ByVal grd As GridView, ByVal val As Integer) As Integer
        Dim ik As Integer = 0, sr As Integer = 0
        While ik < grd.Rows.Count
            If CInt(grd.Rows(Convert.ToInt32(ik)).Cells(2).Text) = val Then
                grd.Rows(Convert.ToInt32(ik)).RowState = DataControlRowState.Selected
                sr = ik
                ik = grd.Rows.Count
            End If
            ik += 1
        End While
        Return sr
    End Function
    Public Function gridViewScrollPos(rw As Integer) As Integer
        Return rw * 24
    End Function

End Class