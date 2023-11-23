using System;
using System.Collections.Generic;

namespace AppWeb_Astro.Models;

public partial class MisionesEspaciale
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Astronauta> Astronauta { get; set; } = new List<Astronauta>();
}
