
function seleccionarCompra(cmpr) {
    // rw.title corresponde a la propiedad title del Span en el que está el Checkbox
    // en esta propiedad se puede poner el campo que se necesite (el id del registro o cualquier otro
    //alert(rw.title);  //cmpr.checked); rw.tagName o rw.nodeName para saber el tipo de DOM Object
    var rw = cmpr.parentNode;
    var ttl = document.getElementById('montop');
    var mnt = parseFloat(rw.title);
    var mt = parseFloat(ttl.value);
    if (cmpr.checked)
        if (isNaN(mt)) ttl.value = mnt;
        else ttl.value = mt + mnt;
    else
        if (isNaN(mt)) ttl.value = 0;
        else ttl.value = mt - mnt;
}
