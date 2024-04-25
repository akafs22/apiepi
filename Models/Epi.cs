using System;
using System.Collections.Generic;

namespace apiepi.Models;

public partial class Epi
{
    public int EpiId { get; set; }

    public string Nome { get; set; } = null!;

    public string Observacao { get; set; } = null!;

    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
