<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RepPrestamoEmpSuc.aspx.vb" Inherits="RepPrestamoEmpSuc" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucsucursales" tagprefix="uc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
   <div id="left">
        <table class="auto-style3">
            <tr>
                <td class="auto-style1" style="height: 59px" id="suc" runat="server">

                    <asp:TextBox ID="tSuc" runat="server" Visible="False"></asp:TextBox>

                    <br />
                    <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>

                    </td>
                <td class="auto-style3" style="height: 59px">

                     <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Generar" ToolTip="Generar" Width="100px" style="margin-left: 0" />
                
                   </td><td class="auto-style2" style="height: 59px">
                
                     <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Limpiar" Width="100px" />
                
                </td>
                </tr>
            <tr>
                    <td class="auto-style12" style="height: 69px">Fecha Inicio:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                 </td>
                <td style="height: 69px">Fecha Fin:<br />
                    <asp:TextBox ID="TxFechaFin" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" />

                    <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    <br />

                

                    <asp:TextBox ID="TxFechaFin2" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" Visible="False" />

                                    

                    <td style="height: 69px">

                        &nbsp;</td>               
                    </td>
            </tr>  
            <tr>
                <td class="auto-style25" style="height: 153px" ><asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
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
               <td class="auto-style25" style="height: 153px" ><asp:Calendar ID="FFinal" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>               

                   <asp:TextBox ID="TxId" runat="server" Visible="False"></asp:TextBox>

                </td>
               
                 
            </tr>
              </table>
    
      
        <rsweb:ReportViewer ID="Repo" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="461px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1005px">
            <LocalReport ReportPath="ReportPrestamoEmp.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="nomRHDataSetTableAdapters.vm_Prestamo_empleadoTableAdapter" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="tSuc" Name="sucursal" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="TxFechaInicio" Name="Fech1" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="TxFechaFin" Name="Fech2" PropertyName="Text" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
      
        <br />
</div>
</asp:Content>

