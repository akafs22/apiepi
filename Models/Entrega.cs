using System;
using System.Collections.Generic;

namespace apiepi.Models;

public partial class Entrega
{
    public int EntregaId { get; set; }

    public int? ColaboradorId { get; set; }

    public int? EpiId { get; set; }

    public DateOnly DtEntrega { get; set; }

    public DateOnly DtValidade { get; set; }

    public virtual Colaborador? Colaborador { get; set; }

    public virtual Epi? Epi { get; set; }
}
