using System;
using System.Collections.Generic;
using System.Linq;
using SistemaClientes.Models;

namespace SistemaClientes.Services
{
    public class RepOrdenacao
    {
        private List<Cliente> clientes;
        public RepOrdenacao(List<Cliente> clientes)
        {
            this.clientes = clientes;
        }

        public List<Cliente> OrdenarPorNomeCres()
        {
            return clientes.OrderBy(c => c.Nome).ToList();
        }

        public List<Cliente> OrdenarPorNomeDecres()
        {
            return clientes.OrderByDescending(c => c.Nome).ToList();
        }

        public List<Cliente> OrdenarPorDataCres()
        {
            return clientes.OrderBy(c => c.DataNascimento).ToList();
        }

        public List<Cliente> OrdenarPorDataDecres()
        {
            return clientes.OrderByDescending(c => c.DataNascimento).ToList();
        }
    }
}