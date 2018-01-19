<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Particulares.aspx.vb" Inherits="Particulares" %>

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
        .auto-style2 {
            height: 434px;
            width: 929px;
        }
        .auto-style3 {
            width: 386px;
        }
        .auto-style5 {
            width: 213px;
        }
        .auto-style6 {
            width: 153px;
        }
        .auto-style7 {
            width: 87px;
        }
  </style>
    <div id="contenedor" class="auto-style2">
    <% If IsNumeric(Session("idz_e")) Then
            Response.Write("<div id=confirmar style='position:fixed; left:200; top:300; background-color:White; border-style:solid; border-width:1px; border-color:Black;'>")
            Response.Write("<table>")
            Response.Write("<tr><td rowspan=7 width=5 /><td height=6 /><td rowspan=7 width=6 /></tr>")
            Response.Write("<tr><td class=c_titulo>Confirmación</td></tr>")
            Response.Write("<tr><td height=6 /></tr>")
            Response.Write("<tr><td class=c_texto>¿Confirma la eliminación del empleado <b><i>" & Session("dz_e") & "</i></b> ?</td></tr>")
            Response.Write("<tr><td height=6 /></tr>")
            Response.Write("<tr><td align=center><input type=submit name=btnSi value='   Sí   ' class='boton' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
            Response.Write("<input type=submit name=btnNo value='   No   ' class='boton' /></td></tr>")
            Response.Write("<tr><td height=6 /></tr></table></div>")
        End If%>

    <h3>Casos Particulares</h3>
    <div id="izquierdo">
        <table>
            <tr>
                <td>Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" /><br />
                </td>
               <td>Empleado:<br />
                   <uc2:wucempleados2 ID="wucEmpleados2" runat="server" />
               </td>
               
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idparticulares" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="491px">
            <Columns>
                <asp:BoundField DataField="idparticulares" ItemStyle-Width="1" ItemStyle-Font-Size="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png"></asp:ButtonField>
                <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="tipo" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
               <asp:BoundField DataField="observaciones" HeaderText="Observaciones" SortExpression="observaciones" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" SortExpression="cantidad" />
                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png"></asp:ButtonField> 
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
    <div id="derecho">
        <table class="auto-style3">
            <tr>
                <td class="auto-style7">
                    <h4 class="auto-style5">
                        Registro</h4>  </td>
                    <td class="auto-style6">
                        &nbsp;</td>
               
            </tr>
            <tr>
                <td class="auto-style7">Tipo:<br />
                   <asp:DropDownList ID="dropLTipo" runat="server" AutoPostBack="True" Height="24px" Width="175px" Enabled="False">
                       <asp:ListItem Value="0">Seleccionar....</asp:ListItem>
                       <asp:ListItem Value="HExtras">Horas Extras</asp:ListItem>
                       <asp:ListItem Value="Justificadas">Faltas Justificadas</asp:ListItem>
                       <asp:ListItem Value="Injustificadas">Faltas Injustificadas</asp:ListItem>
                   </asp:DropDownList>
               </td>
                <td class="auto-style6"><asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label></td>

            </tr>
            <tr>
                <td class="auto-style7">Fecha:<br />
                    <asp:TextBox ID="fecha_ingreso" runat="server" CssClass="txtCaptura" MaxLength="40" Width="135px" />

                    
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/img/favicon.ico" Width="19px" />

                    </td>
                <td class="auto-style6">Cantidad:<br />
                    <asp:TextBox ID="cantidad" runat="server" CssClass="txtCaptura" MaxLength="40" Width="134px" />

                    </td>        
            </tr>
            <tr>
                <td class="auto-style7">
                      <asp:Calendar ID="FIngreso" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="76px" Width="161px" TitleFormat="Month" >
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
                <td class="auto-style6">

                </td>
            </tr>
             <tr>
                <td class="auto-style7">Observaciones:<br />
                    <asp:TextBox ID="observaciones" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" Height="57px" TextMode="MultiLine" /></td>
                <td class="auto-style6">
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Enabled="false" Width="108px" />
                    <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="108px" />
                 </td>    
                
            </tr>
                 <tr>
                      <td class="auto-style7">
                          &nbsp;</td>    
                 </tr>        
        </table>
        
    </div> <!-- registroDatos -->
</div>
</asp:Content>