using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aniversariantes.Classes
{
    interface IRepoDePessoas
    {

        void cadastrarPessoa(); // cadastro de nome, sobrenome e data de aniversario de uma pessoa
        void consultarPessoa(); // consulta de pessoas pelas palavras chave nome e sobrenome
        void gerenciarListaPessoas(); // editar e deletar objeto pessoa e suas informações (nome, sobrenome e aniversario)
        void deletarEditarPessoa(); // editar/deletar na própria busca
        void aniversariantesDoDia(); // busca e retorna todos os aniversariantes do dia
    }
}
