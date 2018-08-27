<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div class="row">
            <div class="col-lg-4 col-xs-6">
                <div class="info-box bg-blue">
                    <span class="info-box-icon"><i class="fa fa-user"></i></span>
                         <div class="info-box-content">
                            <span class="info-box-text">Empleados</span>
                             <span class="info-box-number"><% =Empleados %></span>                       
                           </div>
                </div>
                </div>
        <div class="col-lg-4 col-xs-6">
                <div class="info-box bg-red">
                    <span class="info-box-icon"><i class="fa fa-university"></i></span>
                         <div class="info-box-content">
                            <span class="info-box-text">Sucursales</span>
                             <span class="info-box-number"><% =Sucursales %></span>       
                           </div>
                </div>
                </div>
        <div class="col-lg-4 col-xs-6">
                <div class="info-box bg-yellow">
                    <span class="info-box-icon"><i class="fa fa-building"></i></span>
                         <div class="info-box-content">
                            <span class="info-box-text">Empresas</span>
                             <span class="info-box-number"><% =Empresas %></span>                     
                           </div>
                </div>
                </div>
        </div>
         
     <img src="img/Logo-SSL-(Horizontal).png" alt="HTML5 Icon" width="850" height="300">

</asp:Content>

