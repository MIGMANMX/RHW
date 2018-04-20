<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="ConFHor.aspx.vb" Inherits="ConFHor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table __designer:mapid="4">
    <tr __designer:mapid="5">
        <td class="auto-style1" __designer:mapid="6">Dias permitidos para capturar horario:&nbsp;
                                 <br __designer:mapid="7" />
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Lunes" />
            <br __designer:mapid="9" />
            <asp:CheckBox ID="CheckBox2" runat="server" Text="Martes" />
            <br __designer:mapid="b" />
            <asp:CheckBox ID="CheckBox3" runat="server" Text="Miercoles" />
            <br __designer:mapid="d" />
            <asp:CheckBox ID="CheckBox4" runat="server" Text="Jueves" />
            <br __designer:mapid="f" />
            <asp:CheckBox ID="CheckBox5" runat="server" Text="Viernes" />
            <br __designer:mapid="11" />
            <asp:CheckBox ID="CheckBox6" runat="server" Text="Sabado" />
            <br __designer:mapid="13" />
            <asp:CheckBox ID="CheckBox7" runat="server" Text="Domingo" />
            <br />
            <br />
            <br __designer:mapid="15" /></td>
        <td __designer:mapid="16"></td>
        <td __designer:mapid="17">Hora limite para captura de horario:<br __designer:mapid="24" />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br __designer:mapid="26" /></td>
        <td __designer:mapid="27"></td>
    </tr>
</table>
<asp:Label ID="Mens" runat="server" Width="259px"></asp:Label>
<br __designer:mapid="29" />
<asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Guardar"  ToolTip="Guardar" Width="101px" />
</asp:Content>

