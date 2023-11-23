using System;
using System.Collections.Generic;

namespace AppWeb_Astro.Models;

public partial class Astronauta
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Nacionalidad { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public int? Edad { get; set; }

    public bool? Activo { get; set; }

    public string? ImagenUrl { get; set; }

    public string? Redes { get; set; }

    public virtual ICollection<MisionesEspaciale> MisionEspacials { get; set; } = new List<MisionesEspaciale>();
}
