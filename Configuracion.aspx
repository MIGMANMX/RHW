<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Configuracion.aspx.vb" Inherits="Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="Horario" runat="server" Text="Horario" CssClass="btn-warning" />
    <asp:Button ID="Datos" runat="server" Text="Datos" CssClass="btn-warning" />
    <asp:Button ID="Info" runat="server" Text="Información" CssClass="btn-warning" />
    <br/>
     <asp:Label ID="Lmsg" runat="server" CssClass="error"></asp:Label>
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
         #contenedor2{
            overflow:hidden
        }
        #izquierdo2{
            float:left;
        }
         #derecho2{
             float:right;
        }
         #contenedor3{
            overflow:hidden
        }
        #izquierdo3{
            float:left;
        }
         #derecho3{
             float:right;
        }
         .auto-style1 {
             width: 224px;
         }
     </style>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <h1>Configuracion de Horarios</h1>
           <div id="contenedor">
                 <div id="izquierdo">
                     <table>
                         <tr>
                             <td class="auto-style1">Dias permitidos para capturar horario:&nbsp;
                                 <br />
                                 <asp:CheckBox ID="CheckBox1" runat="server" Text="Lunes" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox2" runat="server" Text="Martes" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox3" runat="server" Text="Miercoles" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox4" runat="server" Text="Jueves" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox5" runat="server" Text="Viernes" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox6" runat="server" Text="Sabado" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox7" runat="server" Text="Domingo" />
                                 <br />
                             </td>
                             <td>
                                  
                           
                             </td>
                             <td>Hora limite para captura de horario:<br />
                             <%--    <asp:DropDownList ID="DropH" runat="server" Height="30px" Width="74px">
                                     <asp:ListItem Value="9">09</asp:ListItem>
                                     <asp:ListItem>10</asp:ListItem>
                                     <asp:ListItem>11</asp:ListItem>
                                     <asp:ListItem>12</asp:ListItem>
                                     <asp:ListItem>13</asp:ListItem>
                                 </asp:DropDownList>--%>
                                 <asp:CheckBox ID="CheckBox8" runat="server" Text="08:00" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox9" runat="server" Text="09:00" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox10" runat="server" Text="10:00" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox11" runat="server" Text="11:00" />
                                 <br />
                                 <asp:CheckBox ID="CheckBox12" runat="server" Text="12:00" />
                                 <br />
                                 Hora actual seleccionada:<br />
                                 <asp:TextBox ID="TextBox2" runat="server" Enabled="False"></asp:TextBox>
                                 <br />
                             </td>

                             <td>
                                 
                             </td>
                         </tr>
                     </table>
                     <asp:Label ID="Mens" runat="server" Width="259px"></asp:Label>
                     <br />
                     <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-danger btn-block btn-flat" Text="Guardar"  ToolTip="Guardar" Width="101px" />
                     <br />
                     <br />
                     <br />
                     <br />
                     <br />
                 </div>
           </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <h1>Configuracion Parametros</h1>
            <div id="contenedor2">
                 <div id="izquierdo2">
                     <table>
                         <tr>
                             <td>

                             </td>
                         </tr>
                     </table>
                 </div>
                <div id="derecho2">
                </div>
           </div>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <h1>Configuracion de datos</h1>
            <div id="contenedor3">
                 <div id="izquierdo3">
                     <table>
                         <tr>
                             <td>

                             </td>
                         </tr>
                     </table>
                 </div>
                <div id="derecho3">
                </div>
           </div>
        </asp:View>
    </asp:MultiView>

</asp:Content>

