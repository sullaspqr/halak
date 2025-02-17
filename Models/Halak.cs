using System;
using System.Collections.Generic;

namespace halak.Models;

public partial class Halak
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Faj { get; set; } = null!;

    public decimal MeretCm { get; set; }

    public int? ToId { get; set; }

    public byte[] Kep { get; set; } = null!;

    public virtual ICollection<Fogasok> Fogasoks { get; set; } = new List<Fogasok>();

    public virtual Tavak? To { get; set; }
}
