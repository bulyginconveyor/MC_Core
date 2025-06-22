using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace core_service.services.Jwt;

public class JwtHelper
{
    public static Guid UserId(string headers)
    {
        string token = headers.Split(' ')[1];

        var helper = new JwtSecurityTokenHandler();
        var jwt = helper.ReadToken(token);

        var claim = (jwt as JwtSecurityToken).Claims.FirstOrDefault(c => c.Type == "uid").Value;

        return Guid.Parse(claim);
    }
}
