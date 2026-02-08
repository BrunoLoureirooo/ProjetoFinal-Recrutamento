using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Server.Entities.Exceptions.BadRequest
{
    public sealed class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException()
        : base("Collection count mismatch comparing to ids.")
        {
        }
    }
}
