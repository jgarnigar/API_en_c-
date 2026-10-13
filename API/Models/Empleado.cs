using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int IdPuesto { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    [JsonIgnore]
    public virtual Puesto? IdPuestoNavigation { get; set; } = null!;
}
