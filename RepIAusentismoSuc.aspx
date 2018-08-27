<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RepIAusentismoSuc.aspx.vb" Inherits="_RepIAusentismoSuc" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="listaDatos">
        <table class="auto-style3">
            <tr>
                <td class="auto-style1" style="width: 234px; height: 68px" id="suc" runat="server">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />
                    <br />
                    <asp:TextBox ID="tSuc" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td style="height: 68px; width: 219px;">
                                    
                     
                
            <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Generar" ToolTip="Generar" Width="120px"  />
                
                     
                
                     
                </td>
                <td class="auto-style4" style="width: 234px">
                    <asp:TextBox ID="Ft" runat="server" Width="46px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="H" runat="server" Width="46px" Visible="False"></asp:TextBox>
                        <br />
                    <asp:TextBox ID="IAt" runat="server" Width="46px" Visible="False"></asp:TextBox>
                        <asp:Label ID="Mens" runat="server" Width="259px"></asp:Label>
                        <asp:TextBox ID="grdSR" runat="server" Visible="false" Width="176px" Height="23px"></asp:TextBox>
                    </td>
                </tr>
            <tr>
                    <td style="width: 234px; height: 45px;">
                       
                        Fecha Inicio:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    </td>
                 <td style="width: 219px; height: 45px;">
                    
                     Fecha Fin:<br />
                    <asp:TextBox ID="TxFechaFin" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" />

                    <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    <asp:TextBox ID="TxFechaFin2" runat="server" MaxLength="40" Width="69px" CssClass="txtCaptura" Height="24px" Enabled="False" Visible="False" />

                                    

                    </td>
               
            </tr>
            <tr>
                <td style="width: 234px">
                    <asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
                </td>
                <td style="width: 219px">
                    <asp:Calendar ID="FFinal" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>               

                </td>
                
            </tr>
           
  
        </table>
         <rsweb:ReportViewer ID="Repo" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="911px">
             <LocalReport ReportPath="ReportIndiceAusentismoSuc.rdlc">
                 <DataSources>
                     <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                 </DataSources>
             </LocalReport>
         </rsweb:ReportViewer>
         <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="nomRHDataSetTableAdapters.vm_IndiceAusentismoTableAdapter"></asp:ObjectDataSource>
         <br />
     </div>
</asp:Content>

