using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Shared.DataTransferObjets
{
    public record TokenDto(string AccessToken, string RefreshToken);
}
