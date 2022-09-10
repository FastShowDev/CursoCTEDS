using AvaliacaoD3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoD3.Interfaces
{
    internal interface ILog
    {
        void RegisterAcess(Users user);
        Users LoginUser(string email, string password);
        Users SearchUser(string email, string password);
    }
}
