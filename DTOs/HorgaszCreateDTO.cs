namespace halak.DTOs
{
    public class HorgaszCreateDTO
    {
        public string Nev { get; set; } = null!;
        public int Eletkor { get; set; }
        public List<FogasDTO> Fogasok { get; set; }
    }
}
