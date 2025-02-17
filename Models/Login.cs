using System;
using System.Collections.Generic;

namespace halak.Models;

public partial class Login
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Password { get; set; } = null!;
}
