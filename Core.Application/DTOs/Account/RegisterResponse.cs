using Core.Application.DTOs.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Account
{
    public class RegisterResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<ToastRequestDTO> Toasts { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
