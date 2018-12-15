using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aniversariantes.Persistencia
{
    interface IPersistencia
    {
        void criarArquivoOuPopLista(); //reseta a lista, dps cria arq ou faz arq popular lista, se o txt ja existir
        void listaPopArquivo(); // reseta o arq, dps lista popula o arquivo
    }
}
