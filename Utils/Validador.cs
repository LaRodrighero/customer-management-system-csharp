using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace SistemaClientes.Utils
{
    public static class Validador
    {
        public static bool CpfValido(string cpf)
        {
            return Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        }

        public static bool ConvertData(string texto, out DateTime data)
        {
            return DateTime.TryParseExact(texto, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out data);
        }

        public static bool DataFuturo(DateTime data)
        {
            return data > DateTime.Today;
        }
    }
}