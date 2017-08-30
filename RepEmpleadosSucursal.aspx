<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RepEmpleadosSucursal.aspx.vb" Inherits="_RepEmpleadosSucursal" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="listaDatos">
        <table class="auto-style3">
            <tr>
                <td class="auto-style1">Sucursal:<asp:TextBox ID="tSuc" runat="server" Visible="False"></asp:TextBox>
                    <br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />&nbsp; </td>
                <td class="auto-style3">
                     <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Limpiar" Width="107px" />
                
                   </td><td class="auto-style2">
                     <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Generar" ToolTip="Generar" Width="120px"  />
                
                </td>
                </tr>
   
     
            <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Mens" runat="server" Width="259px"></asp:Label>
                    </td>
                 <td class="auto-style5">
                     <br />
                     <br />
                    </td>
            </tr>   
  
        </table>
         <br />
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="911px">
             <LocalReport ReportPath="ReportEmpleados.rdlc">
                 <DataSources>
                     <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                 </DataSources>
             </LocalReport>
         </rsweb:ReportViewer>
         <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataEmpleados" TypeName="nomRHDataSetTableAdapters.vm_ReporteEmpleadoSucursalTableAdapter">
             <SelectParameters>
                 <asp:ControlParameter ControlID="tSuc" Name="sucursal" PropertyName="Text" Type="String" />
             </SelectParameters>
         </asp:ObjectDataSource>
     </div>
</asp:Content>

