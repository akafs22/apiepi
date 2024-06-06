using Microsoft.AspNetCore.Identity;

namespace apiepi.Models;

public class ApplicationUser : IdentityUser
{
    public int Cpf { get; set; }
}
