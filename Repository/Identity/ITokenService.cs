using Standard.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Identity
{
    public interface ITokenService
    {
        string CreteToken(AppUser user);
    }
}
