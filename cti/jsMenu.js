function colapsoMenu(menu, imagen, liMnu) {
    if (menu.style.display == 'none') {
        document.getElementById("liVentas").style.display = 'none';
        document.getElementById("liMnuOperaciones").style.display = 'none';
        document.getElementById("liMnuReportes").style.display = 'none';
        document.getElementById("liMnuCatalogos").style.display = 'none';
        liMnu.style.display = '';
        menu.style.display = '';
        imagen.className = 'ocultarMnu';
    }
    else {
        document.getElementById("liVentas").style.display = '';
        document.getElementById("liMnuOperaciones").style.display = '';
        document.getElementById("liMnuReportes").style.display = '';
        document.getElementById("liMnuCatalogos").style.display = '';
        menu.style.display = 'none';
        imagen.className = 'mostrarMnu';
    }
}