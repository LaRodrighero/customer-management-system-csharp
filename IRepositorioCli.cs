using System;
using System.Collections.Generic;
using SistemaClientes.Models;

namespace SistemaClientes.Repositories
{
    // herdo operações genéricas e mantenho só as específicas de Cliente
    public interface IRepositorioCli : IRepositorioBase<Cliente>
    {
        List<Cliente> Buscar(string termo);
        bool Editar(int id, string? novoNome = null, string? novoCpf = null, DateTime? novaDataNascimento = null);
        bool ExisteCpf(string cpf);
    }
}
