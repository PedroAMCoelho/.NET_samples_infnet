using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aniversariantes.Classes
{
    public class RepositorioDePessoas : IRepoDePessoas
    {
        public static List<Pessoa> listaPessoas = new List<Pessoa>();
        private IEnumerable<Pessoa> lista_nova;
        private int escolha;
        
        public void cadastrarPessoa()
        {
            Console.Write("Digite o nome da pessoa que deseja adicionar: ");
            string inputNome = Console.ReadLine();
            Console.Write("Digite o sobrenome da pessoa que deseja adicionar: ");
            string inputSobrenome = Console.ReadLine();
            Console.Write("Digite a data de aniversário da pessoa no formato dd/MM/yyyy: ");
            string inputAniversario = Console.ReadLine();

            string format = "(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)";

            if (System.Text.RegularExpressions.Regex.IsMatch(inputAniversario, format))
            {
                //Pessoa p = new Pessoa(inputNome, inputSobrenome, DateTime.ParseExact(inputAniversario, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR")));
                Pessoa p = new Pessoa();
                p.nome = inputNome;
                p.sobrenome = inputSobrenome;
                p.aniversario = DateTime.ParseExact(inputAniversario, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));
                //p.id = listaPessoas.Count;
                listaPessoas.Add(p);
                Console.Write("O id de " + p.nome + " é " + listaPessoas.IndexOf(p));

                Console.Write("\n Dados Adicionados com sucesso! \n \n Clique em qualquer tecla para retornar ao menu");
                                
            }
            else
            {
                Console.Write("\n Formato de data invalido, tente novamente usando o formato dd/MM/yyyy");
            }

        }

        public void consultarPessoa()
        {

            Console.Write("Digite o nome, ou parte do nome, da pessoa que deseja pesquisar: ");
            string nomePesquisado = Console.ReadLine();
            Console.Write("Digite o sobrenome, ou parte do sobrenome, da pessoa que deseja pesquisar: ");
            string sobrenomePesquisado = Console.ReadLine();

            lista_nova = listaPessoas.Where(p => p.nome.Contains(nomePesquisado) || p.sobrenome.Contains(sobrenomePesquisado));
            //lista para agrupar todas as pessoas que contem parte ou totalidade do nome/sobrenome pesquisado

            if (lista_nova.Count() > 0)
            {
                Console.Write("Digite uma das opções abaixo para visualizar os dados de uma das pessoas encontradas: \n");

                foreach (Pessoa p in lista_nova)
                {
                    Console.WriteLine(lista_nova.ToList().IndexOf(p) + " - " + p.nome + " " + p.sobrenome);
                } //foreach para listar todos os elementos da busca

                escolha = Int32.Parse(Console.ReadLine());

                //Console.ReadKey();
                Console.Clear();

                Console.Write("Dados da pessoa: \n");
                Console.Write("Nome Completo: " + lista_nova.ToList().ElementAt(escolha).nomeCompleto() + "\n");
                Console.Write("Aniversário: " + lista_nova.ToList().ElementAt(escolha).aniversario + "\n");
                Console.Write("Faltam " + lista_nova.ToList().ElementAt(escolha).tempoProNiver() + " dias para esse aniversário");
                deletarEditarPessoa();
            }
            else
            {
                Console.WriteLine("\nNenhum contato foi localizado\n\nClique em qualquer tecla para voltar ao menu...");
            }
                        
        } // fechamento metodo consultarPessoa()

        public void deletarEditarPessoa()
        {

            Console.WriteLine("\n \n[ 1 ] Deletar Pessoa");
            Console.WriteLine("[ 2 ] Editar Pessoa");
            Console.WriteLine("[ 3 ] Voltar ao Menu");
            Console.WriteLine("-------------------------------------");

            int opcao = Int32.Parse(Console.ReadLine());

            //aqui será deletada/editada a pessoa que o usuário escolheu visualizar as infos no método consultarPessoa()
            Pessoa pessoa = lista_nova.ElementAt(escolha);

            if (opcao == 1)
            {
                
                // int indexEmListaPessoas = listaPessoas.IndexOf(pessoa);

                listaPessoas.Remove(pessoa);

                Console.Write("Você deletou " + pessoa.nomeCompleto() + " da sua lista de contatos. \n \nPressione qualquer tecla para retorar ao menu.");
            }
            else if (opcao == 2)
            {
                int index = listaPessoas.IndexOf(pessoa); // posicao na listaPessoa em que está aquele obj selecionado na lista nova
                Pessoa pListaPessoas = listaPessoas.ElementAt(index); // objeto da listaPessoas que corresponde ao obj selecionado pelo user na lista nova
                string nomeAnterior = pListaPessoas.nome;

                Console.WriteLine("Digite o novo nome de " + nomeAnterior + " ");
                string novoNome = Console.ReadLine();
                Console.Write("Digite o sobrenome de " + novoNome + " ");
                string novoSobrenome = Console.ReadLine();
                Console.Write("Digite a data de aniversário da pessoa no formato dd/MM/yyyy: ");
                string novoAniversario = Console.ReadLine();

                pListaPessoas.nome = novoNome;
                pListaPessoas.sobrenome = novoSobrenome;
                pListaPessoas.aniversario = DateTime.ParseExact(novoAniversario, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));

                Console.WriteLine("\nSeu antigo contato " + nomeAnterior + " foi editado conforme abaixo:\n");

                Console.Write("Nome Completo: " + listaPessoas.ElementAt(index).nomeCompleto() + "\n");
                Console.Write("Aniversário: " + listaPessoas.ElementAt(index).aniversario + "\n");
                Console.Write("Faltam " + listaPessoas.ElementAt(index).tempoProNiver() + " dias para esse aniversário");
                Console.Write("\n Clique em qualquer tecla para voltar ao menu...");

            }
            else
            {
                Console.Write("Vamos ao menu! Clique em qualquer tecla.");
            }

        } //fechamento do método deletarPessoa(), que permite a deleção após a busca

        public void gerenciarListaPessoas()
        {

            Console.WriteLine("[ 1 ] Desejo deletar um de meus contatos");
            Console.WriteLine("[ 2 ] Desejo editar um de meus contatos");
            Console.WriteLine("-------------------------------------");

            int opcao = Int32.Parse(Console.ReadLine());

            //Console.ReadKey();
            Console.Clear();

            if (opcao == 1)
            {
                
                if (listaPessoas.Count() > 0)
                {
                    Console.WriteLine("Selecione qual contato você deseja remover");

                    foreach (Pessoa p in listaPessoas)
                    {
                        Console.WriteLine(listaPessoas.IndexOf(p) + " - " + p.nome + " " + p.sobrenome);
                    } //foreach para listar todos os elementos da lista de pessoas

                    int pessoaEscolhida = Int32.Parse(Console.ReadLine());                  

                    Console.Write("Você deletou " + listaPessoas.ElementAt(pessoaEscolhida).nomeCompleto() + " da sua lista de contatos. \n \nPressione qualquer tecla para retorar ao menu.");

                    listaPessoas.RemoveAt(pessoaEscolhida);
                }
                else
                {
                    Console.WriteLine("Nenhum contato foi localizado");
                }

            }
            else if(opcao == 2)
            {
                if (listaPessoas.Count() > 0)
                {
                    Console.WriteLine("Selecione qual contato você deseja editar");

                    foreach (Pessoa p in listaPessoas)
                    {
                        Console.WriteLine(listaPessoas.IndexOf(p) + " - " + p.nome + " " + p.sobrenome);
                    } //foreach para listar todos os elementos da lista de pessoas

                    int indexPessoaEscolhida = Int32.Parse(Console.ReadLine());
                    Pessoa pEscolhida = listaPessoas.ElementAt(indexPessoaEscolhida);
                    string nomeAnterior = pEscolhida.nome;

                    Console.WriteLine("Digite o novo nome de " + nomeAnterior + " ");
                    string novoNome = Console.ReadLine();
                    Console.Write("Digite o sobrenome de " + novoNome + " ");
                    string novoSobrenome = Console.ReadLine();
                    Console.Write("Digite a data de aniversário da pessoa no formato dd/MM/yyyy: ");
                    string novoAniversario = Console.ReadLine();

                    pEscolhida.nome = novoNome;
                    pEscolhida.sobrenome = novoSobrenome;
                    pEscolhida.aniversario = DateTime.ParseExact(novoAniversario, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));

                    Console.WriteLine("Seu antigo contato " + nomeAnterior + " foi editado conforme abaixo:\n");

                    Console.Write("Nome Completo: " + listaPessoas.ElementAt(indexPessoaEscolhida).nomeCompleto() + "\n");
                    Console.Write("Aniversário: " + listaPessoas.ElementAt(indexPessoaEscolhida).aniversario + "\n");
                    Console.Write("Faltam " + listaPessoas.ElementAt(indexPessoaEscolhida).tempoProNiver() + " dias para esse aniversário");
                    Console.Write("\n\nDigite qualquer tecla para voltar ao menu...");
                }
                else
                {
                    Console.WriteLine("Nenhum contato foi localizado");
                    Console.Write("\n\nDigite qualquer tecla para voltar ao menu...");
                }
            }


        } // fechamento do método gerenciarListaPessoas() - edita ou deleta contato

        public void aniversariantesDoDia()
        {
            
            List<Pessoa> aniversariantes = listaPessoas.FindAll(p => p.aniversario.Day == DateTime.Now.Day && p.aniversario.Month == DateTime.Now.Month);

            if (listaPessoas.Count() > 0)
            {
                Console.WriteLine("\nDê os parabéns aos aniversariantes do dia !!!\n\nAniversariantes:\n");

                foreach (Pessoa p in aniversariantes)
                {
                    Console.WriteLine(p.nomeCompleto());
                } //foreach para listar todos os aniversariantes da lista aniversariantes

                Console.WriteLine("\n-------------------------------------");

            }
            else
            {
                Console.WriteLine();
            }
        }

        }
}
