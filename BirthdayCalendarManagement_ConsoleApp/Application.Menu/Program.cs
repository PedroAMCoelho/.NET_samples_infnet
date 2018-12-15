using Aniversariantes.Classes;
using Aniversariantes.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Menu
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            int opcao;

            RepositorioDePessoas repo = new RepositorioDePessoas();
            PersistenciaEmArquivo persist = new PersistenciaEmArquivo();

            do
            {
                persist.criarArquivoOuPopLista();
                //persist.resetDaLista();
                //persist.deletarArq();
                repo.aniversariantesDoDia();

                Console.WriteLine("[ 1 ] Cadastrar Pessoa");
                Console.WriteLine("[ 2 ] Consultar Pessoa");
                Console.WriteLine("[ 3 ] Gerenciar (Editar ou Remover) Pessoas");
                //Console.WriteLine("[ 4 ] Extra: Veja seus contatos salvos em arquivo .txt");
                Console.WriteLine("[ 0 ] Sair do Programa");
                Console.WriteLine("-------------------------------------");
                Console.Write("Digite uma opção: ");
                opcao = Int32.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        repo.cadastrarPessoa();
                        persist.listaPopArquivo();
                        break;
                    case 2:
                        repo.consultarPessoa();
                        persist.listaPopArquivo();
                        break;
                    case 3:
                        repo.gerenciarListaPessoas();
                        persist.listaPopArquivo();
                        break;
                    //case 4:
                       // persist.lerArq();
                        //break;
                    default:
                        sairDoPrograma();
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
            while (opcao != 0);

             void sairDoPrograma()
            {
                Console.WriteLine();
                Console.WriteLine("Você saiu do programa! Clique em qualquer tecla para sair...");
            }   
        }
    }
}