using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aniversariantes.Classes
{
    public class Pessoa
    {
        //public int id;
        public string nome;
        public string sobrenome;
        public DateTime aniversario;
        
        /**
        public Pessoa(string nome, string sobrenome, DateTime aniversario)
        {
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.aniversario = aniversario;
        } **/

        public string nomeCompleto()
        {
            return nome + " " + sobrenome;
        }

        public string tempoProNiver()
        {
            
            DateTime anivAnoAtual = new DateTime(DateTime.Now.Year, aniversario.Month, aniversario.Day);
            DateTime anivProxAno = anivAnoAtual.AddYears(1);
            DateTime hoje = DateTime.Now;
            TimeSpan tempoRestante;

            if(hoje<anivAnoAtual)
            {
                tempoRestante = anivAnoAtual.Subtract(hoje);
            } else
            {
                tempoRestante = anivProxAno.Subtract(hoje);
            }

            int dias = tempoRestante.Days + 1;

            return dias.ToString();
        }

    }
}
