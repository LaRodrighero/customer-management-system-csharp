using System;
using System.Collections.Generic;

namespace SistemaClientes.Models
{
    public class Usuario
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}
