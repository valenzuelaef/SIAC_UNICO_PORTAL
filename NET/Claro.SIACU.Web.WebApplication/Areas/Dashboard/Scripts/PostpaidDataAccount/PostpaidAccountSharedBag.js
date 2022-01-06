$(document).ready(function () {
    var vEstado = $('#validateListSharedBag').text();
  
    if (vEstado.trim() === 'False') {
        showAlert("El Cliente no cuenta con Bolsas Compartidas", "Mensaje Bolsa Compartida", function () { window.close(); });
    } 
});