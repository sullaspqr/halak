using halak.Models;

namespace halak.DTOs
{
    public class HorgaszDTO
    {
        public string Nev { get; set; } = null!;

        public int? Eletkor { get; set; }

        public List<FogasDTO> Fogasok { get; set; }
    }
}
