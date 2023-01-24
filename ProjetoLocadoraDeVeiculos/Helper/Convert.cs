using System.Globalization;

namespace ProjetoLocadoraDeVeiculos.Helper
{
    public class Convert
    {
        public static decimal ConvertStringDecimal(string desconto)
        {
            var money = decimal.Parse(desconto);

            return money;
        }

        public static string RemoverCaracteresCpf(string cpf)
        {
            var result = cpf.Replace(".", "").Replace("-", "");

            return result;
        }

        public static string RemoverCaracteresPlaca(string placa)
        {
            var result = placa.Replace("-", "").ToUpper();

            return result;
        }
    }
}
