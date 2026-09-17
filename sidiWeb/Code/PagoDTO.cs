using System;

namespace sidiWeb.Code.Api
{
    public class PagoDTO
    {
        public int identificador { get; set; }

        public string transaccionId { get; set; }

        public string fecha { get; set; }

        public decimal monto { get; set; }

        public string estado { get; set; }

        public ConceptoDTO concepto { get; set; }

        public OficinaDTO oficina { get; set; }
    }
}