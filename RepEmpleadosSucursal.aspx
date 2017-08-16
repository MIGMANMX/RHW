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
                     <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Generar" ToolTip="Generar" Width="120px" style="margin-left: 21" />
                
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
         <rsweb:ReportViewer ID="RepoEmSuc" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="526px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="921px">
             <LocalReport ReportPath="ReportEmpSuc.rdlc">
                 <DataSources>
                     <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                 </DataSources>
             </LocalReport>
         </rsweb:ReportViewer>
         <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataEmSuc" TypeName="nomRHDataSetTableAdapters.RepEmSucTableAdapter">
             <SelectParameters>
                 <asp:ControlParameter ControlID="tSuc" Name="Suc" PropertyName="Text" Type="Int32" />
             </SelectParameters>
         </asp:ObjectDataSource>
     </div>
</asp:Content>

