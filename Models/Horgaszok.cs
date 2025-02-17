using System;
using System.Collections.Generic;

namespace halak.Models;

public partial class Horgaszok
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public int? Eletkor { get; set; }

    public virtual ICollection<Fogasok> Fogasoks { get; set; } = new List<Fogasok>();
}
