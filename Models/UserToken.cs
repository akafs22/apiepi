
using NuGet.Protocol.Plugins;

namespace apiepi.Models;

public class UserToken
{
    public string? Token { get; set; }
    public DateTime Expiration { get; set;}
    public IList<string> Roles { get; set; }
    public int IdCol{get; set;}

}
