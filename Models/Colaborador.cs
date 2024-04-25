using System;
using System.Collections.Generic;

namespace apiepi.Models;

public partial class Colaborador
{
    public int CodigoId { get; set; }

    public decimal Cpf { get; set; }

    public string Ctps { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
