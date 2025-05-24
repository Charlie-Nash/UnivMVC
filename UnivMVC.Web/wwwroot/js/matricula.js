document.addEventListener("DOMContentLoaded", function () {
    ActualizarSeleccion();
});

function ActualizarSeleccion() {
    const lstRbtHorario = $(".css-horario-rbt");

    let totCursos = 0;
    let totCreditos = 0;

    lstRbtHorario.each(function (indice, rbt) {
        if (rbt.checked) {
            let cursoCod = rbt.name.replace("Horarios", "").replace("[", "").replace("]", "");

            totCursos += 1;
            totCreditos += parseInt($("#spnCursoCreditos_" + cursoCod).text(), 10);

            $("#" + rbt.id).attr("seleccionado", "1");
        }
        else {
            $("#" + rbt.id).attr("seleccionado", "0");
        }
    });

    $("#spnTotCursos").text(totCursos);
    $("#spnTotCreditos").text(totCreditos);
}

function rtbHorario_Click(controlID) {
    const seleccionado = $("#" + controlID).attr("seleccionado");

    if (seleccionado == "1") {
        $("#" + controlID).prop("checked", false);
        $("#" + controlID).attr("seleccionado", "0");
    }

    ActualizarSeleccion();

    $(".error-msg").hide();
}