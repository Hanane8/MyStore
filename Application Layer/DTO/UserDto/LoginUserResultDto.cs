using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.UserDto
{
    public class LoginUserResultDto
    {
        public string? Token { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
    }
}
