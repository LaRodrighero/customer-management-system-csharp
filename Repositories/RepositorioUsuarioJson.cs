using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SistemaClientes.Models;

namespace SistemaClientes.Repositories
{
    public class RepositorioUsuarioJson
    {
        private const string CAMINHO_ARQUIVO = "usuarios.json";
        private List<Usuario> usuarios = new List<Usuario>();

        public RepositorioUsuarioJson()
        {
            Carregar();
        }

        public void Adicionar(Usuario usuario)
        {
            usuarios.Add(usuario);
            Salvar();
        }

        public List<Usuario> Listar()
        {
            return new List<Usuario>(usuarios);
        }

        public Usuario? BuscarPorEmail(string email)
        {
            return usuarios.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        private void Salvar()
        {
            var json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CAMINHO_ARQUIVO, json);
        }

        private void Carregar()
        {
            if (File.Exists(CAMINHO_ARQUIVO))
            {
                var json = File.ReadAllText(CAMINHO_ARQUIVO);
                usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
            }
        }
    }
}
