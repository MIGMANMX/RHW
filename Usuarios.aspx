<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Usuarios.aspx.vb" Inherits="_Usuarios" %>

<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucEmpleados.ascx" tagname="wucEmpleados" tagprefix="uc2" %>
<%@ Register src="cti/wucSuc.ascx" tagname="wucSuc" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <style type="text/css">
        .auto-style1 {
            width: 653px;
        }
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
               width: 375px;
           }
    </style>
    <div id="contenedor">
         <%  If IsNumeric(Session("idz_e")) Then
                 Response.Write("<div id=confirmar style='position:fixed; left:200; top:300; background-color:White; border-style:solid; border-width:1px; border-color:Black;'>")
                 Response.Write("<table>")
                 Response.Write("<tr><td rowspan=7 width=5 /><td height=6 /><td rowspan=7 width=6 /></tr>")
                 Response.Write("<tr><td class=c_titulo>Confirmación</td></tr>")
                 Response.Write("<tr><td height=6 /></tr>")
                 Response.Write("<tr><td class=c_texto>¿Confirma la eliminación del usuario <b><i>" & Session("dz_e") & "</i></b> ?</td></tr>")
                 Response.Write("<tr><td height=6 /></tr>")
                 Response.Write("<tr><td align=center><input type=submit name=btnSi value='   Sí   ' class='boton' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
                 Response.Write("<input type=submit name=btnNo value='   No   ' class='boton' /></td></tr>")
                 Response.Write("<tr><td height=6 /></tr></table></div>")
             End If%>

    <h3>Usuarios</h3>
    <div id="izquierdo" class="auto-style2">
        <table>
            <tr>
                <td>Sucursal:</td>
                <td><uc1:wucSucursales ID="wucSucursales" runat="server" /></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames ="idusuario" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="351px">
            <Columns>
                <asp:BoundField DataField="idusuario" ItemStyle-Width="1" ItemStyle-Font-Size="1" />
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagenes/editar.png"></asp:ButtonField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                <asp:BoundField DataField="usuario" HeaderText="Usuario" SortExpression="usuario" />
                <asp:BoundField DataField="nivel" HeaderText="Nivel" SortExpression="nivel" />
               <%-- <asp:BoundField DataField="sucursal" HeaderText="Sucursal" SortExpression="sucursal" />--%>
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
    <div id="right">
        <table>
            <tr>
                <td colspan="5">
                    <h4>
                        Editar registro del usuario
                       
                    </h4>
                </td>
                </tr>
            <tr>
                <td>
                     <span class="agregarElemento"><asp:Button ID="btnGuardarNuevo" runat="server"  CssClass="btn btn-success btn-block btn-flat" Text="Agregar" ToolTip="Agregar" Width="90px"/></span>
                </td>
                 <td>
                     <span><asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Enabled="false" Width="90px" /></span>
                </td>
            </tr>
            <tr><td colspan="2"><asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label></td></tr>
            <tr>
                <td>Nombre:</td>
                <td class="auto-style1">
                    <asp:TextBox ID="nombre" runat="server" CssClass="txtCaptura" MaxLength="80" Width="200px" /></td>
            </tr>
            <tr>
                <td>Usuario:</td>
                <td class="auto-style1"><asp:TextBox ID="usuario" runat="server" CssClass="txtCaptura" MaxLength="20" Width="200px" /></td>
            </tr>
            <tr>
                <td>Clave:</td>
                <td class="auto-style1"><asp:TextBox ID="clave" runat="server" CssClass="txtCaptura" MaxLength="15" Width="150px" /></td>
            </tr>
            <tr>
                <td>Nivel:</td>
                <td class="auto-style1"><asp:TextBox ID="nivel" runat="server" CssClass="txtCapturaR" MaxLength="1" Width="20px" />
                    <br />1. Administrador;&nbsp;&nbsp;&nbsp;2. Gerente sucursal;&nbsp;&nbsp;&nbsp;3. Supervisor
                </td>
            </tr>
            <tr>
                <td>Sucursal:</td>
                <td class="auto-style1">
                    <uc3:wucsuc ID="wucSuc" runat="server" />

                </td>
            </tr>
        </table>
    </div> <!-- registroDatos -->
    </div>
   
</asp:Content>

