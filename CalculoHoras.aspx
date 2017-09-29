<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="CalculoHoras.aspx.vb" Inherits="CalculoHoras" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        #contenedor{
            overflow:hidden
        }
        #izquierdo{
            float:left;
        }

         #derecho{
             float:right;
        }
        .auto-style4 {
            width: 174px;
            height: 56px;
        }
        .auto-style5 {
            height: 56px;
        }
        .auto-style9 {
        width: 953px;
        height: 702px;
        margin-bottom: 0px;
    }
    .auto-style10 {
        width: 473px;
    }
        .auto-style12 {
            width: 466px;
            height: 56px;
        }
        .auto-style14 {
            width: 394px;
        }
        .auto-style16 {
            width: 395px;
        }
        .auto-style17 {
            width: 376px;
        }
        .auto-style18 {
            width: 258px;
            height: 56px;
        }
        .auto-style19 {
            height: 56px;
            width: 141px;
        }
        .auto-style20 {
            width: 141px;
        }
        .auto-style22 {
            width: 466px;
        }
        .auto-style23 {
            width: 174px
        }
        </style>
    <div id="contenedor" class="auto-style9">
    <h3>Calculo de Horas </h3>
    <div id="izquierdo" class="auto-style10">
        <table class="auto-style16">
            <tr>
                <td class="auto-style22">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
              <td class="auto-style23">Empleado:<asp:TextBox ID="grdSR" runat="server" Visible="false" Width="176px"></asp:TextBox>
                  <br />
                  <uc2:wucempleados2 ID="wucEmpleados2" runat="server" />  
            </tr>
    
            <tr>
                <td class="auto-style12">Fecha Inicio:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                 </td>
                <td class="auto-style4">Fecha Fin:<br />
                    <asp:TextBox ID="TxFechaFin" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" />

                    <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    <td><asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Buscar"  ToolTip="Buscar Registros" Width="90px" /></td>               
                    </td>
            </tr>  
            <tr>
                <td ><asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
                    <DayHeaderStyle BackColor="White" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#999999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#FFCC66" ForeColor="#336666" />
                    <TitleStyle BackColor="#FF9900" BorderColor="#FFCC66" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>               
                    <asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label>

                </td>
               <td ><asp:Calendar ID="FFinal" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
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
        <table>
            <tr>
                <td>
                  <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="chec" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="455px" Height="92px" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging1" PageSize="15">
            <Columns>
                <asp:BoundField DataField="chec" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
                <ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />
                <asp:BoundField DataField="chec" HeaderText="Fecha y Hora" SortExpression="chec" />
                <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="tipo" />
           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#FF9933" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
                </td>
            </tr>
        </table>
    </div> <!-- listaDatos -->


    <div id="derecho" class="auto-style14">
        <table class="auto-style17">                 
            <tr>
               
                <td class="auto-style19">Horas Totales:<br />
                    <asp:TextBox ID="TxHtotales" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                <td class="auto-style18">Horas Trabajadas:<br />
                    <asp:TextBox ID="TxHorasTrabajadas" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                 <td class="auto-style5">
                     
                    <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Generar"  ToolTip="Generar" Width="100px" />               
                    
                 </td>
            </tr>
            <tr>
                <td class="auto-style20">Dias Descansados:<br />
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                <td class="auto-style18">Dias Descansados<br />
                    &nbsp;Trabajados:<asp:TextBox ID="TextBox2" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
            </tr>
            <tr>
                <td class="auto-style20">Dias Festivos Trabajados:<br />
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                <td class="auto-style18">Horas Extras:<br />
                    <br />
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
            </tr>
            </table>

        <table>
            <tr>
                <td>  <asp:Button ID="btnReporte" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Reporte"  ToolTip="Reporte" Width="100px" /></td>
            </tr>
        </table>

         <table>
            <tr>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="367px"></rsweb:ReportViewer>
            </tr>
        </table>
    </div>
        
    
   </div>
</asp:Content>

