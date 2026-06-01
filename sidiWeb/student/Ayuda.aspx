<%@ Page Title="Centro de Ayuda" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="Ayuda.aspx.cs" Inherits="sidiWeb.student.silabo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- BARRA DE BÚSQUEDA PRINCIPAL -->
    <div class="card p-4 mb-4 border-0 shadow-sm text-center" style="background: linear-gradient(135deg, #003366 0%, #002244 100%); border-radius: 15px;">
        <h2 class="text-white fw-bold mb-2"><i class="fa-solid fa-circle-question me-2"></i>¿En qué podemos ayudarte hoy?</h2>
        <p class="text-white-50 small mb-4">Busca información sobre matrículas, ubicación, becas, trámites y calificaciones del Instituto de Idiomas</p>
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="input-group input-group-lg shadow-sm" style="border-radius: 30px; overflow: hidden;">
                    <span class="input-group-text bg-white border-0 text-muted ps-4"><i class="fa-solid fa-magnifying-glass"></i></span>
                    <input text="text" id="txtBuscarAyuda" class="form-control border-0 ps-2" placeholder="Escribe tu duda aquí... (ej. Ubicación, Becas, Constancia, Faltas)" onkeyup="filtrarAyuda()" style="font-size: 1rem; height: 50px;" />
                </div>
            </div>
        </div>
    </div>

    <div class="row g-4">
        <!-- SECCIÓN DE PREGUNTAS Y REGLAMENTOS -->
        <div class="col-lg-8">
            <div class="card profile-info p-4 h-100 border-0 shadow-sm" style="border-radius: 15px;">
                <h5 class="fw-bold mb-4 text-dark border-bottom pb-2">
                    <i class="fa-solid fa-book-bookmark text-primary me-2"></i>Guía Informativa Institucional
                </h5>
                
                <div class="accordion" id="accordionAyuda">
                    
                    <!-- CATEGORÍA: ACCESO -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaAcceso">
                                <i class="fa-solid fa-key me-2 text-primary"></i> Credenciales de Acceso al Aula Virtual
                            </button>
                        </h2>
                        <div id="ayudaAcceso" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted line-height-lg">
                                Tu número de <strong>DNI</strong> funciona como tu usuario y tu contraseña inicial para el ingreso al aula virtual. Te recomendamos cambiarla tras tu primer inicio de sesión por motivos de seguridad.
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA: REQUISITOS DE MATRÍCULA -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaMatricula">
                                <i class="fa-solid fa-file-signature me-2 text-primary"></i> Requisitos Obligatorios de Matrícula por Niveles
                            </button>
                        </h2>
                        <div id="ayudaMatricula" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted">
                                <p class="fw-bold text-dark">Estructura Común (Todos los niveles):</p>
                                <ul class="small mb-3">
                                    <li>Llenar la ficha de matrícula correspondiente.</li>
                                    <li>Adjuntar el recibo original por derecho de matrícula.</li>
                                    <li>Adjuntar el recibo original por derecho de carnet por nivel.</li>
                                    <li>01 foto tamaño carnet con fondo blanco y ropa oscura.</li>
                                    <li>Copia de ficha de matrícula del semestre académico vigente si cuentas con vínculo laboral UNJFSC (Docentes, administrativos nombrados, CAS o hijos de ellos).</li>
                                </ul>
                                <p class="fw-bold text-dark mb-1">Nivel Intermedio:</p>
                                <ul class="small mb-3">
                                    <li>Todos los requisitos comunes + Copia de constancia de notas de nivel básico o certificado de Idioma de Nivel Básico.</li>
                                </ul>
                                <p class="fw-bold text-dark mb-1">Nivel Avanzado:</p>
                                <ul class="small mb-0">
                                    <li>Todos los requisitos comunes + Copia de constancia de notas de nivel intermedio.</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA: DURACIÓN DE IDIOMAS -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaDuracion">
                                <i class="fa-solid fa-clock me-2 text-primary"></i> Duración de Estudios y Modalidades
                            </button>
                        </h2>
                        <div id="ayudaDuracion" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted">
                                <p class="small">El tiempo total requerido varía según el idioma elegido:</p>
                                <div class="table-responsive">
                                    <table class="table table-sm table-bordered text-center small">
                                        <thead class="table-light text-dark">
                                            <tr>
                                                <th>Idioma</th>
                                                <th>Regular</th>
                                                <th>Intensivo</th>
                                                <th>Súper Intensivo</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="fw-bold text-start">Inglés</td>
                                                <td>36 meses</td>
                                                <td>18 meses</td>
                                                <td>12 meses</td>
                                            </tr>
                                            <tr>
                                                <td class="fw-bold text-start">Francés / Quechua</td>
                                                <td>18 meses</td>
                                                <td>9 meses</td>
                                                <td>4 meses y 2 semanas</td>
                                            </tr>
                                            <tr>
                                                <td class="fw-bold text-start">Portugués</td>
                                                <td>15 meses</td>
                                                <td>8 meses</td>
                                                <td>4 meses</td>
                                            </tr>
                                            <tr>
                                                <td class="fw-bold text-start">Italiano</td>
                                                <td>13 meses</td>
                                                <td>7 meses</td>
                                                <td>3 meses y 2 semanas</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA NUEVA: EXAMEN DE UBICACIÓN (image_d4253f.jpg e image_d4251d.png) -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaUbicacion">
                                <i class="fa-solid fa-map-pin me-2 text-primary"></i> Examen de Ubicación: Reglas y Requisitos
                            </button>
                        </h2>
                        <div id="ayudaUbicacion" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <p>Orientado a quienes ya dominan el idioma pero no cursaron estudios escolarizados o dejaron de estudiar.</p>
                                <ul class="ps-3 mb-3">
                                    <li class="mb-2"><strong>Evaluación integral:</strong> Mide habilidades de <em>Listening, Speaking, Reading, Writing</em> y <em>Grammar in use</em>.</li>
                                    <li class="mb-2"><strong>Asistencia obligatoria:</strong> Si faltas al examen, pierdes el derecho de la tasa pagada sin devolución. El Instituto solo reembolsa si el examen se cancela por razones administrativas internas.</li>
                                    <li class="mb-2"><strong>Puntajes y Vigencia:</strong> Para clasificar al ciclo IV requieres una nota mínima equivalente a 40 puntos. Si obtienes 100 puntos perfectos, se te otorgará la certificación directa del nivel. Una vez rendido, tienes un <strong>plazo máximo de 4 meses</strong> para incorporarte al ciclo asignado.</li>
                                    <li class="mb-2"><strong>Disconformidad:</strong> Si no estás de acuerdo con el resultado, puedes retirarte firmando un documento para ser reubicado en un ciclo menor.</li>
                                </ul>
                                <p class="fw-bold text-dark mb-1">Requisitos para Niveles Superiores al Básico:</p>
                                <ul class="ps-3 mb-0">
                                    <li>Formulario Único de Trámite (FUT).</li>
                                    <li>Recibo de pago por derecho de Trámite.</li>
                                    <li>Recibo de pago por derecho de Examen de Ubicación (según TUPA).</li>
                                    <li>Diploma y/o Certificado de estudios del nivel anterior.</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA NUEVA: BECAS Y DESCUENTOS (image_d4251d.png e image_d424fb.png) -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaBecas">
                                <i class="fa-solid fa-tags me-2 text-primary"></i> Sistema de Becas y Descuentos en Pensiones
                            </button>
                        </h2>
                        <div id="ayudaBecas" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <p>El Instituto otorga beneficios acumulables/exclusivos (solo se puede optar por uno) según las siguientes modalidades:</p>
                                <ul class="ps-3 mb-3">
                                    <li class="mb-2"><strong>Por Rendimiento Académico:</strong> 
                                        <ul>
                                            <li><strong>Beca Integra:</strong> Al mejor alumno con nota promedio mayor o igual a 19.50 durante los dos últimos ciclos consecutivos.</li>
                                            <li><strong>Media Beca:</strong> Al segundo puesto con nota de 19.00 a más durante los dos últimos ciclos consecutivos (se solicita adjuntando constancia de notas).</li>
                                        </ul>
                                    </li>
                                    <li class="mb-2"><strong>Comunidad Universitaria (Tasa de Matrícula Externa):</strong>
                                        <ul>
                                            <li><strong>Administrativos nombrados, CAS e hijos:</strong> 50% de descuento (presentar constancia de trabajo de Recursos Humanos UNJFSC).</li>
                                            <li><strong>Docentes e hijos:</strong> 15% de descuento (presentar constancia de trabajo de Recursos Humanos UNJFSC).</li>
                                            <li><strong>Estudiantes de Pregrado Regular:</strong> 15% de descuento general o 30% si obtuviste el primer puesto en el ciclo vigente (presentar ficha de matrícula o resolución).</li>
                                        </ul>
                                    </li>
                                    <li class="mb-2"><strong>Educación Básica Regular (Colegios):</strong> Beca íntegra al 1er puesto y Media Beca al 2do puesto en concursos locales o regionales de inglés (tramitar con Diploma de Mérito).</li>
                                </ul>
                                <div class="alert alert-warning p-2 mb-0" style="font-size: 0.8rem;">
                                    <i class="fa-solid fa-triangle-exclamation me-1"></i><strong>Causales de pérdida del beneficio:</strong> Matrícula extemporánea, obtener una nota desaprobatoria en el ciclo, o registrar inasistencias continuas o parciales sin justificación.
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA: ASISTENCIA Y FALTAS -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaAsistencia">
                                <i class="fa-solid fa-user-check me-2 text-primary"></i> Control de Asistencias, Tardanzas y Justificaciones
                            </button>
                        </h2>
                        <div id="ayudaAsistencia" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <ul class="ps-3 mb-0">
                                    <li class="mb-2"><strong>Asistencia Mínima Obligatoria:</strong> Requieres un mínimo del <strong>70% de asistencia</strong> en el ciclo para aprobar. Quedarás desaprobado automáticamente con nota 00 si superas el límite de faltas.</li>
                                    <li class="mb-2"><strong>Tolerancia de Ingreso:</strong> Se establece una tolerancia máxima de <strong>10 minutos</strong>. Pasado este tiempo se registrará como inasistencia.</li>
                                    <li class="mb-2"><strong>Justificaciones:</strong> Dispones de un plazo máximo de <strong>48 horas posteriores</strong> a la falta para presentar tu sustento (Enfermedad acreditada con certificado médico o emergencia familiar directa).</li>
                                    <li><strong>Sesiones semanales:</strong> Alumnos con una sola sesión semanal de 6 horas únicamente podrán registrar un máximo de <strong>1 inasistencia al mes</strong>.</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA: CAMBIOS DE HORARIO -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaHorarios">
                                <i class="fa-solid fa-calendar-days me-2 text-primary"></i> Solicitud de Cambio de Horarios y Cierre de Grupos
                            </button>
                        </h2>
                        <div id="ayudaHorarios" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <p>Las modificaciones de horario procederán bajo las siguientes directrices:</p>
                                <ul class="ps-3">
                                    <li class="mb-2">Se solicitan únicamente una vez concluido el ciclo de estudios y antes de iniciar el siguiente periodo académico.</li>
                                    <li class="mb-2"><strong>Cierre de grupos:</strong> Si un grupo no alcanza el mínimo de 10 estudiantes matriculados, se procederá a su cierre y deberás coordinar tu reubicación en otro horario disponible.</li>
                                    <li><strong>Casos aprobados sin costo:</strong> Por cierre de grupo, exceso de alumnos o cruce justificado con asignaturas de pregrado o centro laboral (sustentado mediante Trámite Documentario).</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA: EVALUACIONES Y NOTAS -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaNotas">
                                <i class="fa-solid fa-graduation-cap me-2 text-primary"></i> Sistema de Calificaciones y Nota Mínima
                            </button>
                        </h2>
                        <div id="ayudaNotas" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <p>La evaluación es permanente e incluye pruebas virtuales/presenciales, prácticas orales y simulacros:</p>
                                <ul class="ps-3 mb-3">
                                    <li class="mb-2"><strong>Nota Mínima Aprobatoria:</strong> El sistema califica bajo la escala centesimal (0-100), donde el mínimo aprobatorio son <strong>70 puntos</strong>.</li>
                                    <li>Al finalizar, el promedio se transforma a la escala vigesimal (0-20) tradicional de la Faustino.</li>
                                </ul>
                                <div class="row text-center fw-bold text-dark">
                                    <div class="col-4 border-end">100 pts &rarr; 20</div>
                                    <div class="col-4 border-end">85 pts &rarr; 17</div>
                                    <div class="col-4">70 pts &rarr; 14</div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA NUEVA: RECONOCIMIENTO DE OTRAS INSTITUCIONES (image_d3d2fe.png) -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaValidacion">
                                <i class="fa-solid fa-building-columns me-2 text-primary"></i> Validación de Certificados de Otros Centros de Idiomas
                            </button>
                        </h2>
                        <div id="ayudaValidacion" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <p>Para convalidar certificados o diplomas externos, estos pasarán por un proceso de autenticación y visto bueno. El interesado deberá rendir un <strong>examen de suficiencia</strong> que garantice sus competencias.</p>
                                <p class="fw-bold text-dark mb-1">Requisitos para la aprobación (Mínimo 16 en escala vigesimal o 75%-100%):</p>
                                <ul class="ps-3">
                                    <li>Formulario Único de Trámite (FUT).</li>
                                    <li>Recibo de pago por derecho de Trámite.</li>
                                    <li>Recibo de pago por derecho de Examen de Acreditación (Ver TUPA).</li>
                                    <li>Copia del Documento de Identidad (DNI).</li>
                                    <li>Copia legalizada del diploma o certificado del nivel que deseas acreditar (Básico, Intermedio o Avanzado).</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA NUEVA: EMISIÓN DE CONSTANCIAS Y CERTIFICADOS (image_d3c743.png e image_d3c723.png) -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaDocumentos">
                                <i class="fa-solid fa-id-card-clip me-2 text-primary"></i> Emisión de Constancias (Matrícula, Estudios, Notas y Certificados)
                            </button>
                        </h2>
                        <div id="ayudaDocumentos" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <p>Todos los trámites se inician mediante la Unidad de Trámites Documentarios de la UNJFSC dirigidos al Director(a) del Instituto de Idiomas. Las fotos requeridas deben ser tamaño pasaporte, con ropa formal, fondo blanco, sin anteojos, en papel normal brillante.</p>
                                
                                <div class="mb-3 p-2 bg-light rounded">
                                    <strong>1. Constancia de Matrícula o de Estudios:</strong><br />
                                    <span class="text-muted">Requisitos: FUT + Recibo por derecho de Trámite + Recibo por derecho de Constancia correspondientes + 02 fotos pasaporte. Entrega en 3 días hábiles tras la recepción en el Instituto.</span>
                                </div>
                                
                                <div class="mb-3 p-2 bg-light rounded">
                                    <strong>2. Constancia de Notas:</strong><br />
                                    <span class="text-muted">Requisitos: FUT + Recibo por derecho de Trámite + Recibo por derecho de Constancia de Notas + 02 fotos pasaporte. Entrega en 5 días hábiles (Firma la Jefatura Académica y Registros Académicos).</span>
                                </div>

                                <div class="p-2 bg-light rounded">
                                    <strong>3. Certificado de Nivel (Básico, Intermedio o Avanzado):</strong><br />
                                    <span class="text-muted">Requisitos: FUT + Recibo por derecho de Trámite + Recibo por derecho de Certificado de Nivel + Copia de las constancias de notas correspondientes + 02 fotos pasaporte. Entrega en 8 días hábiles (Lleva firma de la Dirección y el Rector).</span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- CATEGORÍA: LICENCIAS Y ABANDONOS -->
                    <div class="accordion-item border-0 shadow-sm mb-3 item-ayuda">
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed fw-bold text-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#ayudaLicencias">
                                <i class="fa-solid fa-user-slash me-2 text-primary"></i> Abandono de Estudios y Reincorporación
                            </button>
                        </h2>
                        <div id="ayudaLicencias" class="accordion-collapse collapse" data-bs-parent="#accordionAyuda">
                            <div class="accordion-body text-muted small">
                                <ul class="ps-3 mb-0">
                                    <li class="mb-2">Si dejas de estudiar por más de <strong>4 meses consecutivos</strong> perderás la continuidad directa y no podrás matricularte de manera regular.</li>
                                    <li>A partir del quinto mes deberás realizar obligatoriamente una solicitud de <strong>examen de ubicación</strong> abonando la tasa correspondiente a través de la plataforma <em>facilita.gob.pe</em>.</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
                
                <!-- MENSAJE DE NO RESULTADOS (OCULTO POR DEFECTO) -->
                <div id="divNoResultados" class="text-center py-4 d-none">
                    <i class="fa-solid fa-folder-open text-muted h1 mb-2"></i>
                    <p class="text-muted mb-0">No se encontraron temas relacionados con tu búsqueda.</p>
                </div>
            </div>
        </div>

        <!-- BLOQUE DERECHO: SOPORTE Y CANALES DIRECTOS -->
        <div class="col-lg-4">
            <div class="card p-4 border-0 shadow-sm" style="border-radius: 15px;">
                <h5 class="fw-bold mb-2 text-dark"><i class="fa-solid fa-headset me-2 text-primary"></i>Asistencia en Línea</h5>
                <p class="small text-muted mb-4">¿No encontraste lo que buscabas o necesitas iniciar un trámite formal? Comunícate directamente con nuestros canales de atención:</p>
                
                <div class="d-flex align-items-center gap-3 mb-3 p-2 bg-light rounded" style="transition: 0.3s;">
                    <div class="bg-success text-white rounded-circle d-flex align-items-center justify-content-center flex-shrink-0" style="width: 40px; height: 40px;">
                        <i class="fa-brands fa-whatsapp"></i>
                    </div>
                    <div>
                        <span class="d-block fw-bold text-dark small">WhatsApp Matrículas</span>
                        <a href="https://wa.me/51987654321" target="_blank" class="text-decoration-none small text-success fw-semibold">+51 953 813 572</a>
                    </div>
                </div>

                <div class="d-flex align-items-center gap-3 p-2 bg-light rounded" style="transition: 0.3s;">
                    <div class="bg-primary text-white rounded-circle d-flex align-items-center justify-content-center flex-shrink-0" style="width: 40px; height: 40px;">
                        <i class="fa-solid fa-envelope"></i>
                    </div>
                    <div>
                        <span class="d-block fw-bold text-dark small">Mesa de Ayuda</span>
                        <span class="text-muted small" style="font-size: 0.85rem;">idiomas@unjfsc.edu.pe</span>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- SCRIPT DE FILTRADO EN TIEMPO REAL -->
    <script>
        function filtrarAyuda() {
            var input = document.getElementById('txtBuscarAyuda');
            var filter = input.value.toLowerCase().normalize ? input.value.toLowerCase().normalize("NFD").replace(/[\u0300-\u036f]/g, "") : input.value.toLowerCase();
            var items = document.getElementsByClassName('item-ayuda');
            var noResultados = document.getElementById('divNoResultados');
            var encontrados = 0;

            for (var i = 0; i < items.length; i++) {
                var textoItem = items[i].textContent || items[i].innerText;
                var textoNormalizado = textoItem.toLowerCase().normalize ? textoItem.toLowerCase().normalize("NFD").replace(/[\u0300-\u036f]/g, "") : textoItem.toLowerCase();

                if (textoNormalizado.indexOf(filter) > -1) {
                    items[i].style.display = "";
                    encontrados++;

                    if (filter.length > 1) {
                        var collapseElement = items[i].querySelector('.accordion-collapse');
                        if (collapseElement && !collapseElement.classList.contains('show')) {
                            var btn = items[i].querySelector('.accordion-button');
                            btn.classList.remove('collapsed');
                            collapseElement.classList.add('show');
                        }
                    }
                } else {
                    items[i].style.display = "none";
                    if (filter.length === 0) {
                        var collapseElement = items[i].querySelector('.accordion-collapse');
                        if (collapseElement && collapseElement.classList.contains('show')) {
                            var btn = items[i].querySelector('.accordion-button');
                            btn.classList.add('collapsed');
                            collapseElement.classList.remove('show');
                        }
                    }
                }
            }

            if (encontrados === 0) {
                noResultados.classList.remove('d-none');
            } else {
                noResultados.classList.add('d-none');
            }
        }
    </script>

</asp:Content>