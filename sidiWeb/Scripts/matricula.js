// Función para exportar tabla a PDF
function exportarTablaPDF() {
    const tabla = document.getElementById('tablaMatricula');

    if (!tabla || tabla.rows.length <= 1) {
        mostrarAlerta('No hay datos para exportar', 'warning');
        return;
    }

    // Obtener datos de la tabla
    const filas = [];
    const encabezados = [];

    // Obtener encabezados
    const thead = tabla.querySelector('thead');
    if (thead) {
        const ths = thead.querySelectorAll('th');
        ths.forEach(th => {
            encabezados.push(th.innerText.trim());
        });
    }

    // Obtener datos de las filas
    const tbody = tabla.querySelector('tbody');
    if (tbody) {
        const filasData = tbody.querySelectorAll('tr');
        filasData.forEach(fila => {
            const celdas = fila.querySelectorAll('td');
            const filaData = [];
            celdas.forEach(celda => {
                filaData.push(celda.innerText.trim());
            });
            if (filaData.length > 0) {
                filas.push(filaData);
            }
        });
    }

    if (filas.length === 0) {
        mostrarAlerta('No hay datos para exportar', 'warning');
        return;
    }

    // Configuración para PDF
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF({
        orientation: 'landscape',
        unit: 'mm',
        format: 'a4'
    });

    // Estilos para el PDF
    const estilos = {
        fontSize: 10,
        headerColor: [0, 123, 255], // Azul
        textColor: [50, 50, 50],
        bgColor: [240, 240, 240]
    };

    // Título del documento
    doc.setFontSize(16);
    doc.setTextColor(0, 123, 255);
    doc.text('Reporte de Matrícula Virtual', 148, 15, { align: 'center' });

    doc.setFontSize(10);
    doc.setTextColor(100, 100, 100);
    doc.text(`Fecha: ${new Date().toLocaleDateString()}`, 148, 22, { align: 'center' });

    // Preparar datos para la tabla
    const tableData = [encabezados, ...filas];

    // Configurar columnas
    const columnStyles = {};
    tableData[0].forEach((_, index) => {
        columnStyles[index] = { cellWidth: 'auto' };
    });

    // Generar tabla en PDF
    doc.autoTable({
        head: [encabezados],
        body: filas,
        startY: 30,
        theme: 'striped',
        headStyles: {
            fillColor: estilos.headerColor,
            textColor: [255, 255, 255],
            fontSize: 10,
            fontStyle: 'bold',
            halign: 'center'
        },
        bodyStyles: {
            fontSize: 9,
            textColor: estilos.textColor,
            halign: 'center'
        },
        alternateRowStyles: {
            fillColor: [248, 249, 250]
        },
        margin: { top: 30, left: 15, right: 15 },
        columnStyles: {
            0: { cellWidth: 40 },
            1: { cellWidth: 40 },
            2: { cellWidth: 40 },
            3: { cellWidth: 40 }
        }
    });

    // Guardar PDF
    doc.save(`reporte_matricula_${new Date().toISOString().slice(0, 10)}.pdf`);
}

// Función para mostrar alertas
function mostrarAlerta(mensaje, tipo = 'info') {
    const alertaDiv = document.createElement('div');
    alertaDiv.className = `alert alert-${tipo} alert-dismissible fade show position-fixed`;
    alertaDiv.style.top = '20px';
    alertaDiv.style.right = '20px';
    alertaDiv.style.zIndex = '9999';
    alertaDiv.style.minWidth = '300px';
    alertaDiv.style.animation = 'slideInRight 0.3s ease-out';
    alertaDiv.innerHTML = `
        <i class="fas ${tipo === 'success' ? 'fa-check-circle' : tipo === 'warning' ? 'fa-exclamation-triangle' : 'fa-info-circle'} me-2"></i>
        ${mensaje}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;

    document.body.appendChild(alertaDiv);

    setTimeout(() => {
        alertaDiv.style.animation = 'slideOutRight 0.3s ease-out';
        setTimeout(() => alertaDiv.remove(), 300);
    }, 3000);
}

// Estilos para las animaciones de alertas
const estiloAlertas = document.createElement('style');
estiloAlertas.textContent = `
    @keyframes slideInRight {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    @keyframes slideOutRight {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(100%);
            opacity: 0;
        }
    }
    
    .alert {
        box-shadow: 0 4px 12px rgba(0,0,0,0.15);
        border-left: 4px solid;
    }
    
    .alert-success {
        border-left-color: #28a745;
    }
    
    .alert-warning {
        border-left-color: #ffc107;
    }
    
    .alert-info {
        border-left-color: #17a2b8;
    }
`;

document.head.appendChild(estiloAlertas);

// Inicialización cuando el DOM está listo
document.addEventListener('DOMContentLoaded', function () {
    // Mejorar la tabla con tooltips si es necesario
    const filasTabla = document.querySelectorAll('#tablaMatricula tbody tr');
    filasTabla.forEach(fila => {
        fila.style.cursor = 'pointer';
        fila.addEventListener('click', function () {
            const celdas = this.querySelectorAll('td');
            const datos = Array.from(celdas).map(celda => celda.innerText);
            console.log('Fila seleccionada:', datos);
        });
    });
});