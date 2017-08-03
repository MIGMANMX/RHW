<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="RepHorario.aspx.vb" Inherits="_RHorario" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucsucursales" tagprefix="uc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 42px;
            .container{
    text-align:center
}
.left{
    float: left;
            width: 207px;
        }
        .auto-style2 {
            width: 207px;
        }
        .auto-style3 {
            width: 489px;
        }
        .auto-style4 {
            width: 141px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <div id="left">
        <table class="auto-style3">
            <tr>
                <td class="auto-style1">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />&nbsp; </td>
                <td class="auto-style4">
                     <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Generar" ToolTip="Generar" Width="108px" style="margin-left: 21" />
                   </td><td>
                     <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Limpiar" Enabled="false" Width="107px" />
                
                </td>
                </tr>
            <tr>
                    <td class="auto-style2">
                        Fecha Inicio:<br />
<asp:TextBox ID="TFInicio" runat="server" Width="110px"></asp:TextBox>   
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                <asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="154px" TitleFormat="Month" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
&nbsp;</td>
                 <td class="auto-style4">
                     Fecha Final:<br />
<asp:TextBox ID="TFFinal" runat="server" Width="110px"></asp:TextBox>     
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                <asp:Calendar ID="FIngreso0" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="154px" TitleFormat="Month" >
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
        <br />
</div>
   <div class="right">

       <table>
           <tr>
                <td>
                          &nbsp;</td>
                        
                     <td>
                         &nbsp;</td>
               
                <td>
                    &nbsp;</td>
               </tr>
       </table>
    </div> 
    
  
     </asp:Content>

