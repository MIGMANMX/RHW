<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RegistroHorario.aspx.vb" Inherits="_RegistroHorario" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="listaDatos">
        <table>
            <tr>
                <td>Sucursal:</td>
                <td><uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
         
                <td>Empleado:</td>
                <td><uc2:wucempleados2 ID="wucEmpleados2" runat="server" /></td>
            </tr>
        </table>
     </div>
    <asp:Calendar ID="Calendar1" runat="server">
        <DayHeaderStyle BackColor="#FFCC66" />
        <TitleStyle BackColor="#FF9900" Font-Bold="True" ForeColor="White" />
    </asp:Calendar>

        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
    
</asp:Content>

