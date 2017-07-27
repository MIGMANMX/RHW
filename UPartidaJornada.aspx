<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="UPartidaJornada.aspx.vb" Inherits="_UPartidaJornada" %>

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
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idempleado" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="677px" Height="71px">
            <Columns>
                <asp:BoundField DataField="idempleado" ItemStyle-Width="1" ItemStyle-Font-Size="1" /> 
                <asp:BoundField DataField="jornada" HeaderText="jornada" SortExpression="jornada" />
                  <asp:BoundField DataField="inicio" HeaderText="inicio" SortExpression="inicio" />
                <asp:BoundField DataField="fin" HeaderText="fin" SortExpression="fin" />
                  <asp:BoundField DataField="fecha" HeaderText="fecha" SortExpression="fecha" />
               
                
           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#3088b0" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
    </div> <!-- listaDatos --> 
</asp:Content>

