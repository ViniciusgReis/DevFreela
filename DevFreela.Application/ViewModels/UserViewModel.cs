using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string fullName, bool active, string email)
        {
            FullName = fullName;
            Active = active;
            Email = email;
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
