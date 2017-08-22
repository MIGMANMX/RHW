<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Directorio.aspx.vb" Inherits="_Directorio" %>
<%@ Register src="cti/wucSucursales.ascx" tagname="wucSucursales" tagprefix="uc1" %>
<%@ Register src="cti/wucempleados2.ascx" tagname="wucempleados2" tagprefix="uc2" %>
<%@ Register src="cti/wucSuc.ascx" tagname="wucSuc" tagprefix="uc3" %>
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
          width: 598px;
      }
      .auto-style2 {
          width: 281px;
      }
      .auto-style5 {
          width: 265px;
      }
  </style>
    <div id="contenedor">
    <div id="izquierdo">
        <table class="auto-style5">
            <tr>
                <td class="auto-style7" id="suc" runat="server">Sucursal:<uc1:wucsucursales ID="wucSucursales" runat="server" /></td>
                <td class="auto-style3">Empleado:<uc2:wucempleados2 ID="wucEmpleados2" runat="server" />
                    <asp:TextBox ID="idpartidas_jornadaT" runat="server" Visible="False" Width="37px"></asp:TextBox>
                    <asp:TextBox ID="TIDPJ" runat="server" Visible="False" Width="37px"></asp:TextBox>
                    <asp:TextBox ID="idempleado" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="idsucursal" runat="server" Visible="False"></asp:TextBox>
                </td>       
            </tr>           
        </table>
        <table class="auto-style1">
            <tr>
                <td colspan="2">
                    <h4>
                        Editar directorio del empleado
                      </h4>  </td>
                    <td>
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-info btn-block btn-flat" Text="Actualizar"  ToolTip="Actualizar datos" Width="90px" />
                </td>
               
                <td>
                    &nbsp;</td>
                
            </tr>
            <tr><td colspan="2"><asp:label ID="Lmsg" runat="server" CssClass="error"></asp:label></td></tr>
            <tr>
                <td class="auto-style5">Empleado:<br />
                    <asp:TextBox ID="empleado" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" Enabled="False" /></td>
                <td class="auto-style2">Clave:<br />
                    <asp:TextBox ID="claveTX" runat="server" CssClass="txtCaptura" MaxLength="40" Width="155px" style="margin-left: 0" Height="24px" /></td>        
            </tr>
             <tr>
                <td class="auto-style5">Calle:<br />
                    <asp:TextBox ID="calle" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style2">Numero:&nbsp; Colonia:<br />
                    <asp:TextBox ID="numero" runat="server" CssClass="txtCaptura" MaxLength="40" Width="53px" />&nbsp;<asp:TextBox ID="colonia" runat="server" CssClass="txtCaptura" MaxLength="40" Width="107px" /></td>        
            </tr>
             <tr>
                <td class="auto-style5">CP:<br />
                    <asp:TextBox ID="cp" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style2">Telefono:<br />
                    <asp:TextBox ID="telefono" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>        
            </tr>
            <tr>
                <td class="auto-style5">Correo:<br />
                    <asp:TextBox ID="correo" runat="server" CssClass="txtCaptura" MaxLength="40" Width="168px" /></td>
                <td class="auto-style2" id="baj" runat="server">&nbsp;</td>        
            </tr>
           
        </table>

    </div>
         <asp:TextBox ID="grdSR" runat="server" Visible="false"></asp:TextBox>
</div>
</asp:Content>

