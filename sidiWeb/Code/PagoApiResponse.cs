using System.Collections.Generic;

namespace sidiWeb.Code.Api
{
    public class PagoApiResponse
    {
        public int totalPages { get; set; }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int totalItems { get; set; }

        public List<PagoDTO> data { get; set; }
    }
}