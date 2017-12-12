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
     </style>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <h1>Configuracion de Horarios</h1>
           <div id="contenedor">
                 <div id="izquierdo">
                     <table>
                         <tr>
                             <td>Dia limite para capturar horario:&nbsp;
                                 <br />
                                 <asp:DropDownList ID="DropDownList1" runat="server" Height="30px" Width="121px">
                                     <asp:ListItem Value="Monday">Lunes</asp:ListItem>
                                     <asp:ListItem Value="Tuesday">Martes</asp:ListItem>
                                     <asp:ListItem Value="Wednesday">Miercoles</asp:ListItem>
                                     <asp:ListItem Value="Thursday">Jueves</asp:ListItem>
                                     <asp:ListItem Value="Friday">Viernes</asp:ListItem>
                                     <asp:ListItem Value="Saturday">Sabado</asp:ListItem>
                                     <asp:ListItem Value="Sunday">Domingo</asp:ListItem>
                                 </asp:DropDownList>
                                 <br />
                                 <asp:TextBox ID="txtdia" runat="server"></asp:TextBox>
                                 <br />
                             </td>
                             <td>
                                  
                                 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                  
                             </td>
                             <td>Hora limite para captura de horario:<br />
                             <%--    <asp:DropDownList ID="DropH" runat="server" Height="30px" Width="74px">
                                     <asp:ListItem Value="9">09</asp:ListItem>
                                     <asp:ListItem>10</asp:ListItem>
                                     <asp:ListItem>11</asp:ListItem>
                                     <asp:ListItem>12</asp:ListItem>
                                     <asp:ListItem>13</asp:ListItem>
                                 </asp:DropDownList>--%>
                                 <asp:TextBox ID="txthora" runat="server"></asp:TextBox>
                             </td>

                             <td>
                                 
                             </td>
                         </tr>
                     </table>
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

