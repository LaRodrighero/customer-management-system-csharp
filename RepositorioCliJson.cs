using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SistemaClientes.Models;
using SistemaClientes.Utils;

namespace SistemaClientes.Repositories
{
    public class RepositorioCliJson : IRepositorioCli
    {
        private const string CAMINHO_ARQUIVO = "clientes.json";
        private List<Cliente> clientes = new List<Cliente>();
        private int proximoId = 1;

        public RepositorioCliJson()
        {
            Carregar();
        }

        public void Adicionar(Cliente cliente)
        {
            cliente.Id = proximoId++;
            clientes.Add(cliente);
            Salvar();
        }

        public List<Cliente> Listar()
        {
            return new List<Cliente>(clientes);
        }

        public List<Cliente> Buscar(string termo)
        {
            return clientes.FindAll(c =>
                c.Nome.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                c.CPF.Contains(termo));
        }

        public bool Editar(int id, string? novoNome = null, string? novoCpf = null, DateTime? novaDataNascimento = null)
        {
            var cliente = clientes.Find(c => c.Id == id);
            if (cliente == null) return false;

            if (!string.IsNullOrWhiteSpace(novoNome))
                cliente.Nome = novoNome;

            if (!string.IsNullOrWhiteSpace(novoCpf))
            {
                if (!Validador.CpfValido(novoCpf) || ExisteCpf(novoCpf))
                    return false;
                cliente.CPF = novoCpf;
            }

            if (novaDataNascimento.HasValue && !Validador.DataFuturo(novaDataNascimento.Value))
                cliente.DataNascimento = novaDataNascimento.Value;

            Salvar();
            return true;
        }

        public bool Remover(int id)
        {
            var cliente = clientes.Find(c => c.Id == id);
            if (cliente == null) return false;
            clientes.Remove(cliente);
            Salvar();
            return true;
        }

        public bool ExisteCpf(string cpf)
        {
            return clientes.Exists(c => c.CPF == cpf);
        }

        public Cliente? BuscarPorId(int id)
        {
            return clientes.Find(c => c.Id == id);
        }

        private void Salvar()
        {
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CAMINHO_ARQUIVO, json);
        }

        private void Carregar()
        {
            if (File.Exists(CAMINHO_ARQUIVO))
            {
                var json = File.ReadAllText(CAMINHO_ARQUIVO);
                clientes = JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
                proximoId = clientes.Count > 0 ? clientes[^1].Id + 1 : 1;
            }
        }
    }
}
