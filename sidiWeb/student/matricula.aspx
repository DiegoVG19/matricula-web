<%@ Page Title="Matrícula Virtual" Language="C#" MasterPageFile="~/SiteStudent.Master"
AutoEventWireup="true" CodeBehind="matricula.aspx.cs"
Inherits="sidiWeb.student.matricula" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
<link href="<%= ResolveUrl("~/Resources/matricula.css?v=10") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="matricula-container">

    <!-- HEADER -->
    <div class="matricula-header">
        <h2>
            <i class="fas fa-graduation-cap"></i>
            Matrícula Virtual
        </h2>

        <div class="student-info">
            Estudiante:
            <strong>
                <asp:Label ID="lblNombreAlumno" runat="server" />
            </strong>
        </div>
    </div>

    <!-- MENSAJE -->
    <asp:Label
        ID="lblMensajeGeneral"
        runat="server"
        CssClass="alert alert-info mensaje-general"
        Visible="false">
    </asp:Label>

    <div class="matricula-dashboard">

        <!-- IZQUIERDA -->
        <div class="panel-idiomas">

            <asp:PlaceHolder ID="phIdiomas" runat="server"></asp:PlaceHolder>

            <asp:Panel
                ID="pnlSinGrupos"
                runat="server"
                Visible="false"
                CssClass="sin-grupos">

                <div class="alert alert-warning">
                    <h5>No hay grupos disponibles</h5>
                    <p>Actualmente no existen grupos abiertos para su matrícula.</p>
                </div>

            </asp:Panel>

        </div>

        <!-- DERECHA -->
<div class="panel-formulario">

    <!-- PRECIOS -->
    <div class="panel-pagos panel-tributos-pro">
        <div class="tributos-layout">

            <div class="tributos-info">
                <h4>Tributos</h4>
                <p>
                    Recuerda usar los pagos correspondientes para tu inscripción.
                </p>
            </div>

            <div class="tributos-precios">
                <div class="tributo-card">
                    <div class="tributo-top">CAJA CENTRAL</div>
                    <div class="tributo-label">Faustiniano</div>
                    <div class="tributo-monto" id="precioFaustiniano">S/.--- Soles</div>
                </div>

                <div class="tributo-card">
                    <div class="tributo-top">CAJA CENTRAL</div>
                    <div class="tributo-label">Particular</div>
                    <div class="tributo-monto" id="precioParticular">S/.--- Soles</div>
                </div>
            </div>

        </div>

        <div class="tributos-extra">
            <div class="tributos-alternativo">
                <strong>PAGO ALTERNATIVO</strong>
                También puedes pagar mediante el Banco de la Nación o Págalo.pe
            </div>

            <a class="tributos-guia-link" href="https://www.youtube.com/watch?v=xNtpjgBk8es" target="_blank" rel="noopener noreferrer">
                Guía para realizar el pago virtual
            </a>
        </div>
    </div>

    <!-- HORARIO -->
    <div class="panel-horario">
        <h4>Seleccionar horario</h4>

        <select id="ddlHorarios" class="form-control">
            <option value="">-- Seleccione --</option>
        </select>

        <asp:HiddenField ID="hfGrupoSeleccionado" runat="server" />
    </div>

    <!-- ESTADO -->

    <div class="panel-estado-matricula">

        <h4>Estado de matrícula</h4>

        <div class="estado-item">
            <span>Tipo de alumno</span>

            <strong id="lblTipoAlumno">
                Consultando...
            </strong>
        </div>

        <div class="estado-item">
            <span>Tributo matrícula</span>

            <strong id="lblTributoMatricula">
                ---
            </strong>
        </div>

        <div class="estado-item">
            <span>Reincorporación</span>

            <strong id="lblEstadoReincorporacion">
                No requerida
            </strong>
        </div>

    </div>
    

     <!-- PAGOS -->

    <div class="panel-pagos-verificados">

        <h4>Pagos verificados</h4>

        <table class="tabla-pagos">

            <thead>

                <tr>
                    <th>Tributo</th>
                    <th>Pago</th>
                    <th>Estado</th>
                </tr>

            </thead>

            <tbody id="tblPagos">

                <tr>

                    <td colspan="3" class="sin-pagos">
                        Seleccione un horario para consultar los pagos.
                    </td>

                </tr>

            </tbody>

        </table>

        <div
            id="mensajePagoPendiente"
            class="mensaje-pago"
            style="display:none;">

        </div>

    </div>

    

    <!-- DOCUMENTOS -->
   

    <!-- VOUCHER REINCORPORACIÓN -->
    
    

    <!-- BOTÓN -->
    <div class="panel-confirmar">
        <asp:Button
            ID="btnConfirmar"
            runat="server"
            CssClass="btn-confirmar"
            Text="Confirmar Matrícula"
            OnClick="btnConfirmar_Click"
            UseSubmitBehavior="false" />
    </div>



</div>

    </div>

</div>

    <!-- PANEL DE RESULTADO -->
<asp:Panel ID="pnlResultadoSolicitud" runat="server" CssClass="resultado-overlay" Style="display:none;">
    <div id="resultadoBox" class="resultado-box resultado-exito">
        <div class="resultado-icono" id="resultadoIcono">✔</div>
        <div class="resultado-titulo" id="resultadoTitulo">Solicitud enviada correctamente</div>
        <div class="resultado-texto" id="resultadoTexto">
            Tu solicitud fue registrada y será revisada por el área encargada.
        </div>
    </div>
</asp:Panel>

<asp:HiddenField ID="hfResultadoTipo" runat="server" />
<asp:HiddenField ID="hfResultadoMensaje" runat="server" />

<!-- ============================= -->
<!-- JS FUNCIONAL -->
<!-- ============================= -->

<script>
    let animandoMatricula = false;
    let estadoAlumnoActual = null;

    function limpiarPrecios() {
        const precioFaustiniano = document.getElementById("precioFaustiniano");
        const precioParticular = document.getElementById("precioParticular");

        if (precioFaustiniano instanceof HTMLElement) {
            precioFaustiniano.textContent = "S/.--- Soles";
        }

        if (precioParticular instanceof HTMLElement) {
            precioParticular.textContent = "S/.--- Soles";
        }
    }

    function cargarPreciosPorGrupo(idGrupo) {

        const precioFaustiniano = document.getElementById("precioFaustiniano");
        const precioParticular = document.getElementById("precioParticular");

        if (!(precioFaustiniano instanceof HTMLElement)) return;
        if (!(precioParticular instanceof HTMLElement)) return;


        precioFaustiniano.textContent = "S/. ... Soles";
        precioParticular.textContent = "S/. ... Soles";


        console.log("Consultando precios grupo:", idGrupo);


        fetch("matricula.aspx/ObtenerPreciosPorGrupo", {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            body: JSON.stringify({
                idGrupo: parseInt(idGrupo, 10)
            })
        })
            .then(response => {

                console.log("Status precios:", response.status);

                if (!response.ok) {
                    throw new Error(
                        "No se pudo obtener la respuesta del servidor."
                    );
                }

                return response.json();
            })
            .then(data => {

                console.log("Respuesta precios:", data);


                const precios = data.d || {};


                const montoFaustiniano =
                    Number(precios.faustiniano || 0).toFixed(0);


                const montoParticular =
                    Number(precios.particular || 0).toFixed(0);



                precioFaustiniano.textContent =
                    `S/.${montoFaustiniano} Soles`;


                precioParticular.textContent =
                    `S/.${montoParticular} Soles`;



                const lblTributo =
                    document.getElementById("lblTributoMatricula");


                if (window.tipoAlumnoActual === "Faustiniano") {

                    lblTributo.textContent =
                        precios.tributoFaustiniano;

                }
                else if (window.tipoAlumnoActual === "Particular") {

                    lblTributo.textContent =
                        precios.tributoParticular;

                }
                else {

                    lblTributo.textContent = "---";

                }

            })
            .catch(error => {

                console.error(
                    "Error al obtener precios:",
                    error
                );

                limpiarPrecios();

            });
    }

    async function consultarEstadoAlumno(idGrupo) {

        if (estadoAlumnoActual !== null) {

            console.log("Alumno ya consultado:", estadoAlumnoActual);

            return estadoAlumnoActual;

        }

        try {

            console.log("=================================");
            console.log("Consultando estado del alumno...");
            console.log("ID Grupo enviado:", idGrupo);


            const response = await fetch("Matricula.aspx/ObtenerEstadoAlumno", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify({
                    idGrupo: parseInt(idGrupo, 10)
                })
            });


            console.log("Respuesta HTTP recibida");
            console.log("Status:", response.status);
            console.log("OK:", response.ok);


            const texto = await response.text();

            console.log("Respuesta cruda del servidor:");
            console.log(texto);


            // Convertimos nuevamente a JSON
            const resultado = JSON.parse(texto);


            console.log("JSON parseado:");
            console.log(resultado);


            const data = resultado.d;


            console.log("Contenido de d:");
            console.log(data);


            if (!data.exito) {

                console.error("Error desde WebMethod:", data.mensaje);


                document.getElementById("lblTipoAlumno").textContent =
                    "No disponible";

                document.getElementById("lblTributoMatricula").textContent =
                    "---";


                return null;

            }


            const alumno = data.datos;
            estadoAlumnoActual = alumno;


            console.log("Datos del alumno:");
            console.log(alumno);


            console.log("Tipo alumno recibido:");
            console.log(alumno.tipoAlumno);


            document.getElementById("lblTipoAlumno").textContent =
                alumno.tipoAlumno;


            window.tipoAlumnoActual = alumno.tipoAlumno;


            console.log("Tipo alumno guardado en variable global:");
            console.log(window.tipoAlumnoActual);


            console.log("Consulta finalizada correctamente");
            console.log("=================================");


            return alumno;


        }
        catch (error) {

            console.error("ERROR consultando alumno:");
            console.error(error);


            document.getElementById("lblTipoAlumno").textContent =
                "Error de consulta";


            return null;

        }
    }

    function limpiarEstadoMatricula() {

        document.getElementById("lblTipoAlumno").textContent = "Consultando...";
        document.getElementById("lblTributoMatricula").textContent = "---";
        document.getElementById("lblEstadoReincorporacion").textContent = "---";

        document.getElementById("tblPagos").innerHTML = `
        <tr>
            <td colspan="3" class="sin-pagos">
                Seleccione un horario para consultar los pagos.
            </td>
        </tr>
    `;

        document.getElementById("mensajePagoPendiente").style.display = "none";
        document.getElementById("mensajePagoPendiente").innerHTML = "";

        document.getElementById("<%= btnConfirmar.ClientID %>").disabled = true;

    }

    function abrirFormulario(btn) {
        if (animandoMatricula) return;

        const card = btn.closest(".idioma-card");
        if (!(card instanceof HTMLElement)) return;

        const combo = card.querySelector(".combo-horarios");
        const panel = document.querySelector(".panel-formulario");
        const dashboard = document.querySelector(".matricula-dashboard");

        /** @type {HTMLSelectElement|null} */
        const comboForm = /** @type {HTMLSelectElement|null} */ (document.getElementById("ddlHorarios"));

        /** @type {HTMLInputElement|null} */
        const hiddenGrupo = /** @type {HTMLInputElement|null} */ (document.getElementById("<%= hfGrupoSeleccionado.ClientID %>"));

        if (!(combo instanceof HTMLSelectElement)) return;
        if (!(panel instanceof HTMLElement)) return;
        if (!(dashboard instanceof HTMLElement)) return;
        if (!(comboForm instanceof HTMLSelectElement)) return;
        if (!(hiddenGrupo instanceof HTMLInputElement)) return;

        animandoMatricula = true;

        panel.classList.remove("visible");
        dashboard.classList.add("modo-matricula");

        document.querySelectorAll(".idioma-card").forEach(c => {
            if (!(c instanceof HTMLElement)) return;

            c.classList.remove("activa", "oculto");

            const btnExistente = c.querySelector(".btn-volver-card");
            if (btnExistente instanceof HTMLElement) {
                btnExistente.remove();
            }

            if (c !== card) {
                c.classList.add("oculto");
            } else {
                c.classList.add("activa");

                const btnVolver = document.createElement("button");
                btnVolver.type = "button";
                btnVolver.innerText = "← Volver";
                btnVolver.className = "btn-volver btn-volver-card";
                btnVolver.onclick = volverSeleccion;
                c.appendChild(btnVolver);
            }
        });

        comboForm.innerHTML = combo.innerHTML;

        let valorInicial = "";
        for (let i = 0; i < comboForm.options.length; i++) {
            const opt = comboForm.options[i];
            if (opt.value && opt.value.trim() !== "") {
                comboForm.selectedIndex = i;
                valorInicial = opt.value;
                break;
            }
        }

        hiddenGrupo.value = valorInicial;

        if (valorInicial) {

            consultarEstadoAlumno()
                .then(() => {

                    cargarPreciosPorGrupo(valorInicial);

                });

        }
        else {

            limpiarPrecios();
        }

        comboForm.onchange = function () {
            hiddenGrupo.value = comboForm.value || "";

            if (comboForm.value) {
                cargarPreciosPorGrupo(comboForm.value);
            } else {
                limpiarPrecios();
                limpiarEstadoMatricula();
            }
        };

        setTimeout(() => {
            panel.classList.add("visible");
            animandoMatricula = false;
        }, 120);

        if (window.innerWidth <= 900) {
            setTimeout(() => {
                panel.scrollIntoView({
                    behavior: "smooth",
                    block: "start"
                });
            }, 320);
        }
    }

    function volverSeleccion() {
        if (animandoMatricula) return;

        const dashboard = document.querySelector(".matricula-dashboard");
        const panel = document.querySelector(".panel-formulario");

        /** @type {HTMLSelectElement|null} */
        const comboForm = /** @type {HTMLSelectElement|null} */ (document.getElementById("ddlHorarios"));

        /** @type {HTMLInputElement|null} */
        const hiddenGrupo = /** @type {HTMLInputElement|null} */ (document.getElementById("<%= hfGrupoSeleccionado.ClientID %>"));

        const campoFicha = document.getElementById("campoFicha");

        const campoVoucherReincorporacion = document.getElementById("campoVoucherReincorporacion");

       

        if (!(dashboard instanceof HTMLElement)) return;
        if (!(panel instanceof HTMLElement)) return;

        animandoMatricula = true;

        panel.classList.remove("visible");

        if (comboForm instanceof HTMLSelectElement) {
            comboForm.innerHTML = "<option value=''>-- Seleccione --</option>";
            comboForm.onchange = null;
        }

        if (hiddenGrupo instanceof HTMLInputElement) {
            hiddenGrupo.value = "";
        }

        limpiarPrecios();
        limpiarEstadoMatricula();

        setTimeout(() => {
            dashboard.classList.remove("modo-matricula");

            document.querySelectorAll(".idioma-card").forEach(c => {
                if (!(c instanceof HTMLElement)) return;

                c.classList.remove("activa", "oculto");

                const btn = c.querySelector(".btn-volver-card");
                if (btn instanceof HTMLElement) {
                    btn.remove();
                }
            });

            animandoMatricula = false;
        }, 220);
    }

    document.addEventListener("keydown", function (e) {
        if (e.key === "Escape") {
            const panel = document.querySelector(".panel-formulario");
            if (panel instanceof HTMLElement && panel.classList.contains("visible")) {
                volverSeleccion();
            }
        }
    });

    function actualizarEstadoMatricula(datos) {

        document.getElementById("lblTipoAlumno").textContent = datos.tipoAlumno;

        document.getElementById("lblTributoMatricula").textContent = datos.tributo;

        document.getElementById("lblEstadoReincorporacion").textContent =
            datos.reincorporacion
                ? "Requerida"
                : "No requerida";

    }


    function mostrarPanelResultadoDesdeServidor() {
        const overlay = document.getElementById("<%= pnlResultadoSolicitud.ClientID %>");
        const tipo = document.getElementById("<%= hfResultadoTipo.ClientID %>");
        const mensaje = document.getElementById("<%= hfResultadoMensaje.ClientID %>");
        const resultadoBox = document.getElementById("resultadoBox");
        const resultadoIcono = document.getElementById("resultadoIcono");
        const resultadoTitulo = document.getElementById("resultadoTitulo");
        const resultadoTexto = document.getElementById("resultadoTexto");

        if (!(overlay instanceof HTMLElement)) return;
        if (!(tipo instanceof HTMLInputElement)) return;
        if (!(mensaje instanceof HTMLInputElement)) return;
        if (!(resultadoBox instanceof HTMLElement)) return;
        if (!(resultadoIcono instanceof HTMLElement)) return;
        if (!(resultadoTitulo instanceof HTMLElement)) return;
        if (!(resultadoTexto instanceof HTMLElement)) return;

        if (!tipo.value) return;

        overlay.classList.add("activo");

        if (tipo.value === "exito") {
            resultadoBox.classList.remove("resultado-error");
            resultadoBox.classList.add("resultado-exito");
            resultadoIcono.textContent = "✔";
            resultadoTitulo.textContent = "Solicitud enviada correctamente";
        } else {
            resultadoBox.classList.remove("resultado-exito");
            resultadoBox.classList.add("resultado-error");
            resultadoIcono.textContent = "✖";
            resultadoTitulo.textContent = "Error al enviar la solicitud";
        }

        resultadoTexto.textContent = mensaje.value || "";

        setTimeout(() => {
            overlay.classList.remove("activo");
            overlay.style.display = "none";

            if (tipo.value === "exito") {
                volverSeleccion();
            }

            tipo.value = "";
            mensaje.value = "";
        }, 2600);
    }

    window.addEventListener("load", function () {
        mostrarPanelResultadoDesdeServidor();
    });

    //SIMULACION DE DATOS//

    function cargarPagosVerificados(pagos) {

        let html = "";

        pagos.forEach(function (p) {

            html += `
        <tr>

            <td>${p.tributo}</td>

            <td>S/${p.monto}</td>

            <td>

                <span class="badge-estado badge-ok">

                    ✔ Verificado

                </span>

            </td>

        </tr>`;
        });

        document.getElementById("tblPagos").innerHTML = html;

    }

    function mostrarPagoPendiente(texto) {

        const div = document.getElementById("mensajePagoPendiente");

        div.style.display = "block";

        div.innerHTML = texto;

    }

    function habilitarConfirmacion(habilitar) {

        document.getElementById("<%= btnConfirmar.ClientID %>").disabled = !habilitar;

    }

    function cargarDatosPrueba() {

        actualizarEstadoMatricula({

            tipoAlumno: "Faustiniano",

            tributo: "2073",

            reincorporacion: false

        });

        cargarPagosVerificados([

            {

                tributo: "2073",

                monto: "120.00"

            }

        ]);

        habilitarConfirmacion(true);

    }

</script>

</asp:Content>