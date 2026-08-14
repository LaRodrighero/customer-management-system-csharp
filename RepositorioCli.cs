using System;
using System.Collections.Generic;
using System.Linq;
using SistemaClientes.Models;
using SistemaClientes.Utils;

namespace SistemaClientes.Repositories
{
    public class RepositorioCli : IRepositorioCli
    {
        private List<Cliente> clientes = new List<Cliente>(); //Lista que armazena os clientes
        private int proximoId = 1; //Usado para atribuir os ID automaticamente

        public void Adicionar(Cliente cliente)
        {
            cliente.Id = proximoId++;
            clientes.Add(cliente);
        }

        public List<Cliente> Listar()
        {
            return clientes;
        }

        public List<Cliente> Buscar(string termo)
        {
            return clientes.FindAll(c => c.Nome.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 || c.CPF.Contains(termo));
        }

        public bool Editar(int id, string? novoNome = null, string? novoCpf = null, DateTime? novaDataNascimento = null)
        {
            var cliente = clientes.Find(c => c.Id == id);
            if (cliente == null)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                cliente.Nome = novoNome;
            }
            if (!string.IsNullOrWhiteSpace(novoCpf))
            {
                if (!Validador.CpfValido(novoCpf))
                {
                    Console.WriteLine("CPF inválido. Use o formato 000.000.000-00.");
                    return false;
                }
                if (cliente.CPF != novoCpf && ExisteCpf(novoCpf))
                {
                    Console.WriteLine("Este CPF já está cadastrado.");
                    return false;
                }

                cliente.CPF = novoCpf;
            }
            if (novaDataNascimento.HasValue)
            {
                if (Validador.DataFuturo(novaDataNascimento.Value))
                {
                    Console.WriteLine("A data de nascimento não pode estar no futuro.");
                    return false;
                }

                cliente.DataNascimento = novaDataNascimento.Value;
            }

            return true;

        }

        public bool Remover(int id)
        {
            var cliente = clientes.Find(c => c.Id == id);
            if (cliente == null)
            {
                return false;
            }
            return clientes.Remove(cliente);
        }

        public bool ExisteCpf(string cpf)
        {
            return clientes.Exists(c => c.CPF == cpf);
        }

        public Cliente? BuscarPorId(int id)
        {
            return clientes.Find(c => c.Id == id);
        }

        public List<Cliente> ListarOrdenadoNome()
        {
            return clientes.OrderBy(c => c.Nome).ToList();
        }

        public List<Cliente> ListarOrdenadoDataNasc()
        {
            return clientes.OrderBy(c => c.DataNascimento).ToList();
        }
    }
}