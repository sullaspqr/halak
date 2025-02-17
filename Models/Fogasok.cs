using System;
using System.Collections.Generic;

namespace halak.Models;

public partial class Fogasok
{
    public int Id { get; set; }

    public int? HalId { get; set; }

    public int? HorgaszId { get; set; }

    public DateOnly Datum { get; set; }

    public virtual Halak? Hal { get; set; }

    public virtual Horgaszok? Horgasz { get; set; }
}
