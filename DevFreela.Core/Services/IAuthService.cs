using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Services
{
    public  interface IAuthService
    {
        string GenerateJwtToken(string email, string role); //Gerar Chave segura Token
        string ComputeSha256Has(string password); //Guardar uma senha Segura

    }
}
