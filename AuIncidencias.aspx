<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="AuIncidencias.aspx.vb" Inherits="AuIncidencias" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados2.ascx" tagname="wucEmpleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucIncidencias.ascx" tagname="wucIncidencias" tagprefix="uc3" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="cti/wucJornadas.ascx" tagname="wucjornadas" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <%-- <% If IsNumeric(Session("idz_e")) Then
              Response.Write("<div id=confirmar style='position:fixed; left:200; top:300; background-color:White; border-style:solid; border-width:1px; border-color:Black;'>")
              Response.Write("<table>")
              Response.Write("<tr><td rowspan=7 width=5 /><td height=6 /><td rowspan=7 width=6 /></tr>")
              Response.Write("<tr><td class=c_titulo>Confirmación</td></tr>")
              Response.Write("<tr><td height=6 /></tr>")
              Response.Write("<tr><td class=c_texto>¿Confirma la eliminación :    <b><i>" & Session("dz_e") & "</i></b> ?</td></tr>")
              Response.Write("<tr><td height=6 /></tr>")
              Response.Write("<tr><td align=center><input type=submit name=btnSi value='   Sí   ' class='boton' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
              Response.Write("<input type=submit name=btnNo value='   No   ' class='boton' /></td></tr>")
              Response.Write("<tr><td height=6 /></tr></table></div>")
          End If%>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <%--<asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" /--%>
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
          .auto-style14 {
            margin-left: 0px;
            margin-right: 0px;
        }
          .auto-style15 {
        width: 148px;
    }
          </style>
      <div id="contenedor">
          <div id="izquierdo" >
              <table>
                  <tr>
                      <td>

                          Sucursal:<br />
                    <uc1:wucsucursales ID="wucSucursales" runat="server" />

                      </td>
                  </tr>
                  <tr>
                      <td>  
                          <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Limpiar"  ToolTip="Actualizar datos" Enabled="true" Width="108px" /> 
                      </td>
                      <td></td>
                      <td class="auto-style15">
                       
                          <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                       
                         <asp:Button ID="btnGuardarNuevo" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Autorizado" ToolTip="Autorizado" Width="108px" />                
                   
                      </td>
                      <td>&nbsp;&nbsp;</td>
                      <td>
                           <asp:Button ID="btnQuitar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="No Autorizado" ToolTip="No Autorizado" Width="122px" />                
                   
                      </td>
                  </tr>
                  <asp:Label ID="Lmsg" runat="server"></asp:Label>
                  </table>
               <table>
            <tr>
                <td>

                  <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="iddetalle_incidencia" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="1054px" Height="92px" AllowPaging="True" AllowSorting="True" PageSize="15" CssClass="auto-style14">
            <Columns>
                <asp:BoundField DataField="iddetalle_incidencia" ItemStyle-Width="1" ItemStyle-Font-Size="1" > 
                <ItemStyle Font-Size="1pt" Width="1px"></ItemStyle>
                </asp:BoundField>
               <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkRow" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
                <%--<asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png" /--%>
                <asp:BoundField DataField="iddetalle_incidencia" HeaderText="ID" SortExpression="iddetalle_incidencia" />
                <asp:BoundField DataField="incidencia" HeaderText="Incidencia" SortExpression="incidencia" />
                <asp:BoundField DataField="empleado" HeaderText="Empleado" SortExpression="empleado" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha"  HtmlEncode="False" DataFormatString = "{0:d}" />
                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" SortExpression="observaciones" />
                <asp:CheckBoxField DataField="verificado" HeaderText="Autorizado" />
               <%-- <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Imagenes/eliminar.png" />--%>
                

           </Columns>
            <HeaderStyle BackColor="#f39c12" ForeColor="#f8f8f8" />
            <RowStyle BackColor="#f3f3f3" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#fbfbfb" />
            <SelectedRowStyle BackColor="#fffcbf" />
            <FooterStyle BackColor="#FF9933" Font-Size="1" Height="1" />
            <PagerStyle BackColor="#3088b0" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
  
              </td>
            </tr>
         </table>
       </div>
    </div>
</asp:Content>
