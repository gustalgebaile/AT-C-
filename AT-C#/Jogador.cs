using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT
{
    public class Jogador
    {
        public DateTime DataDeNasc { get; set; }
        public string Nome { get; set; }
        public string UltSobrenome { get; set; }
        public string Time { get; set; }
        public bool Convocado { get; set; }
        public double Gols { get; set; }
        public ushort NumCamisa { get; set; }
        public Guid Id { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public int CalcularIdade()
        {
            DateTime dataAtual = DateTime.Now;
            int idade = dataAtual.Year - DataDeNasc.Year;

            if (DataDeNasc > dataAtual.AddYears(-idade))
                idade--;

            return idade;
        }
    }

}
