// Mostrar alerta (Invocado desde C#)
function mostrarAlerta(mensaje, tipo = 'info') {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${tipo} alert-dismissible fade show`;
    alertDiv.role = 'alert';
    alertDiv.innerHTML = `
        <i class="fas ${tipo === 'success' ? 'fa-check-circle' : tipo === 'danger' ? 'fa-times-circle' : 'fa-info-circle'} me-2"></i>
        ${mensaje}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    const container = document.getElementById('divMensajes');
    if (container) {
        container.innerHTML = '';
        container.appendChild(alertDiv);
    }

    setTimeout(() => {
        if (alertDiv) alertDiv.remove();
    }, 5000);
}

// Funciones visuales de carga
function mostrarLoading() {
    let loading = document.querySelector('.loading-overlay');
    if (!loading) {
        loading = document.createElement('div');
        loading.className = 'loading-overlay';
        loading.innerHTML = `
            <div class="loading-content">
                <div class="loading-spinner"></div>
                <p class="mb-0">Procesando...</p>
            </div>
        `;
        document.body.appendChild(loading);
    }
    loading.style.display = 'flex';
}

function ocultarLoading() {
    const loading = document.querySelector('.loading-overlay');
    if (loading) loading.style.display = 'none';
}

// Mostrar detalle de costos (Invocado desde C#)
function mostrarDetalleCostos(idioma, montoMensual, meses, requiereReinscripcion, montoPagado) {
    const detalleDiv = document.getElementById('detalleCostosHTML');

    let total = montoMensual * meses;
    let html = `
        <p><strong>${idioma}</strong> - S/ ${montoMensual.toFixed(2)} × ${meses} meses</p>
        <p class="text-muted ms-3">= S/ ${(montoMensual * meses).toFixed(2)}</p>
    `;

    if (requiereReinscripcion) {
        total += 20;
        html += `
            <p><strong>Costo de Reinscripción:</strong> S/ 20.00</p>
            <p class="text-warning ms-3"><i class="fas fa-exclamation-triangle"></i> Pago por reinscripción aplicado</p>
        `;
    }

    html += `<hr class="my-2">`;
    html += `<p class="fw-bold">Total a pagar: S/ ${total.toFixed(2)}</p>`;
    html += `<p class="text-success">Monto pagado: S/ ${montoPagado.toFixed(2)}</p>`;

    if (Math.abs(montoPagado - total) < 0.01) {
        html += `<div class="alert alert-success mt-2 mb-0 py-2">
                    <i class="fas fa-check-circle"></i> Pago completo - Puedes proceder con la matrícula
                 </div>`;
    } else if (montoPagado > total) {
        html += `<div class="alert alert-warning mt-2 mb-0 py-2">
                    <i class="fas fa-exclamation-triangle"></i> Has pagado en exceso. Se generará un saldo a favor.
                 </div>`;
    } else {
        html += `<div class="alert alert-danger mt-2 mb-0 py-2">
                    <i class="fas fa-times-circle"></i> Pago insuficiente. Faltan S/ ${(total - montoPagado).toFixed(2)}
                 </div>`;
    }

    if (detalleDiv) detalleDiv.innerHTML = html;

    // Habilitar o deshabilitar botón ASP.NET basado en el pago
    const btnConfirmar = document.querySelector('[id$="btnConfirmarMatricula"]');
    if (btnConfirmar) {
        if (montoPagado < total) {
            btnConfirmar.disabled = true;
            btnConfirmar.classList.add('disabled');
        } else {
            btnConfirmar.disabled = false;
            btnConfirmar.classList.remove('disabled');
        }
    }

    const divDetalle = document.getElementById('divDetalleCostos');
    if (divDetalle) divDetalle.style.display = 'block';
}

// Inicialización de la página
document.addEventListener('DOMContentLoaded', function () {
    const fechaElement = document.querySelector('.text-muted:contains("Fecha:")');
    // La fecha ya se inyecta desde C#, este bloque puede quedar por si hay elementos que lo necesiten, 
    // pero hemos eliminado el listener roto de la tabla.
});

// Exportar funciones para uso global y permitir que el servidor las invoque
window.mostrarAlerta = mostrarAlerta;
window.mostrarLoading = mostrarLoading;
window.ocultarLoading = ocultarLoading;
window.mostrarDetalleCostos = mostrarDetalleCostos;