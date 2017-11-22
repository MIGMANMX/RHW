<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="CalculoHoras.aspx.vb" Inherits="CalculoHoras" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucIncidencias.ascx" tagname="wucIncidencias" tagprefix="uc3" %>

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
      
        .auto-style1 {
            width: 463px;
        }
        .auto-style2 {
            height: 1464px;
        }
      
        .auto-style5 {
            height: 70px;
            width: 199px;
        }
              
        </style>
    <div id="contenedor" class="auto-style2">
    <h3>Cálculo de Horas </h3>
    <div id="izquierdo">
        <table>
            <tr>
                <td class="auto-style22">Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
              <td class="auto-style23">Empleado:<asp:TextBox ID="grdSR" runat="server" Visible="false" Width="176px"></asp:TextBox>
                  <asp:TextBox ID="idEmpleadoTX" runat="server" Visible="False" Width="46px"></asp:TextBox>
                  <br />
                  <uc2:wucempleados2 ID="wucEmpleados2" runat="server" />  
                  <asp:TextBox ID="TxEmpleado" runat="server" Visible="False" Width="46px"></asp:TextBox>
            </tr>
    
            <tr>
                <td class="auto-style12">Fecha Inicio:<br />
                    <asp:TextBox ID="TxFechaInicio" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Enabled="False" /><asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                 </td>
                <td>Fecha Fin:<br />
                    <asp:TextBox ID="TxFechaFin" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" />

                    <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                

                    <asp:TextBox ID="TxFechaFin2" runat="server" MaxLength="40" Width="100px" CssClass="txtCaptura" Height="24px" Enabled="False" Visible="False" />

                                    

                    <td><asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Buscar"  ToolTip="Buscar Registros" Width="90px" /></td>               
                    </td>
            </tr>  
            <tr>
                <td class="auto-style25" ><asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
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
               <td class="auto-style25" ><asp:Calendar ID="FFinal" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="152px" TitleFormat="Month" Visible="False" >
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
               
            </tr></table>
        <table>
             <tr>
                <td>&nbsp;</td>
            </tr>
            </table>
        <table>
            <tr>
                <td>  
                </td>
                <td>
                  <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="fecha" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="537px" Height="238px" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging1" PageSize="16">
            <Columns>
                <asp:BoundField DataField="fecha" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
                <ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                </asp:BoundField>

               <%-- <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" />--%>
                <asp:BoundField DataField="fecha" HeaderText="Fecha y Hora" SortExpression="fecha" />
                <asp:BoundField DataField="entrada" HeaderText="Entrada" SortExpression="entrada" />
                <asp:BoundField DataField="salida" HeaderText="Salida" SortExpression="salida" />
                <asp:BoundField DataField="hrstrab" HeaderText="Horas" SortExpression="hrstrab" />
                <asp:BoundField DataField="detalle" HeaderText="Detalle" SortExpression="detalle" />

           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#FF9933" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center"  />
        </asp:GridView>
                
                
                
                
                
                </td>
            </tr>
        </table>
        <br />
    </div> <!-- listaDatos -->


    <div id="derecho" class="auto-style1">
        <table>                 
            <tr>
               
                <td class="auto-style19">Horas Totales:<br />
                    <asp:TextBox ID="TxHtotales" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                <td class="auto-style18">Horas Trabajadas:<br />
                    <asp:TextBox ID="TxHorasTrabajadas" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                 <td class="auto-style5">
                     
                     &nbsp;</td>
            </tr>
            <tr>
                <td>Dias Descansados:<br />
                    <br />
                    <asp:TextBox ID="TxDDescasados" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                <td>Dias Descansados<br />
                    &nbsp;Trabajados:<br />
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
            </tr>
            <tr>
                <td>Dias Festivos 
                    <br />
                    Trabajados:<br />
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
                <td>Horas Extras:<br />
                    <br />
                    <asp:TextBox ID="TxHorasExtras" runat="server" CssClass="txtCaptura" MaxLength="40" Width="110px" /></td>
            </tr>
            </table>

        <table>
            <tr>
                <td>  
                     
                    <br />
                     
                    <asp:Button ID="btnGenerar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Generar"  ToolTip="Generar" Width="101px" Visible="False" />               
                    
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td>  
                <br />
                <asp:Button ID="btnReporte" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Reporte"  ToolTip="Reporte" Width="108px" /></td>
            </tr>
        </table>
    </div>
        <rsweb:ReportViewer ID="repo" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1097px" Height="438px">
            <LocalReport ReportPath="ReportCalculoHoras.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="nomRHDataSetTableAdapters.Temp_CalculoTableAdapter" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}">
            <InsertParameters>
                <asp:Parameter Name="fecha" Type="DateTime" />
                <asp:Parameter Name="entrada" Type="String" />
                <asp:Parameter Name="salida" Type="String" />
                <asp:Parameter Name="hrstrab" Type="String" />
                <asp:Parameter Name="puntualidad" Type="String" />
                <asp:Parameter Name="detalle" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <%-- <rsweb:ReportViewer ID="reporte" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="554px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="890px">
        <LocalReport ReportPath="Report1.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
        </rsweb:ReportViewer>--%>        <%--<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDatahecIncidencia" TypeName="nomRHDataSetTableAdapters.vm_ChequeoIncidenciaTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="idEmpleadoTX" Name="idempleado" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="TxFechaInicio" Name="Fech1" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="TxFechaFin2" Name="Fech2" PropertyName="Text" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
   </div>
      
</asp:Content>
