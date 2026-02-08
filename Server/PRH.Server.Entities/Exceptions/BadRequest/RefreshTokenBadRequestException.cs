using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Entities.Exceptions.BadRequest
{
    public sealed class RefreshTokenBadRequestException : BadRequestException
    {
        public RefreshTokenBadRequestException()
        : base("Invalid client request. The tokenDto has some invalid values.")
        {
        }
    }
}
