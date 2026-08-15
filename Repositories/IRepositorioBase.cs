using System;
using System.Collections.Generic;

namespace SistemaClientes.Repositories
{
    // Interface genérica para qualquer tipo de repositório
    public interface IRepositorioBase<T>
    {
        void Adicionar(T entidade);
        List<T> Listar();
        bool Remover(int id);
        T? BuscarPorId(int id);
    }
}
