<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RepIncidenciasT.aspx.vb" Inherits="RepIncidenciasT" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="left">
        <table class="auto-style3">
            <tr>
                <td class="auto-style1">Fecha Inicio:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Fecha Fin:<br />
<asp:TextBox ID="TFInicio" runat="server" Width="135px" Enabled="False"></asp:TextBox>   

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="Ffin" runat="server" Width="126px" Enabled="False"></asp:TextBox>

                </td>
                <td class="auto-style3">
                     <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Limpiar" Width="107px" />
                
                   </td><td class="auto-style2">
                     <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Generar" ToolTip="Generar" Width="133px" style="margin-left: 21" />
                
                </td>
                </tr>
            <tr>
                    <td class="auto-style4">

                <asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="148px" TitleFormat="Month" >
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
                 <td class="auto-style5">
                     &nbsp;</td>
               
                 
            </tr>
     
            <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Mens" runat="server" Width="214px"></asp:Label>
                    </td>
                 <td class="auto-style5">
                     &nbsp;</td>
               
                 
            </tr>
     
              </table>
    
      
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="437px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="884px" style="margin-bottom: 0px">
            <LocalReport ReportPath="RepoIncidenciaT.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataIncidencias" TypeName="nomRHDataSetTableAdapters.vm_IncidenciasTTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="TFInicio" Name="Fech1" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="Ffin" Name="Fech2" PropertyName="Text" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
        <br />
</div>
     </asp:Content>


