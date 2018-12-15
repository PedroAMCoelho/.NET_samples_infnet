using System;
using Aniversariantes.Classes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aniversariantes.Persistencia
{
    public class PersistenciaEmArquivo : IPersistencia
    {            
        private const string DATABASE = "Database.txt";
        //string[] array = File.ReadAllLines(DATABASE);

        //vai ser usado na inicializacao do programa, pra popular a lista ou criar um arquivo txt
        public void criarArquivoOuPopLista()
        {
            //RepositorioDePessoas.listaPessoas.Count < File.ReadAllLines(DATABASE).Length/3

            if (File.Exists(DATABASE))
            {                
                //string[] array = File.ReadAllLines(DATABASE);
                //Console.WriteLine("o txt tem linhas/3 = " + File.ReadAllLines(DATABASE).Length / 3);
                //Console.WriteLine("A lista tem elementos = " + RepositorioDePessoas.listaPessoas.Count);

                // Abre streamreader e lê
                using (StreamReader sr = new StreamReader(DATABASE))
                {
                    String linha;
                    //é necessário apagar a lista antes de passar os dados do txt, senão duplica.
                    RepositorioDePessoas.listaPessoas.Clear();
                    //while percorre o txt e popular a lista
                    while ((linha = sr.ReadLine()) != null)
                    {
                        Pessoa p = new Pessoa();
                        if (linha != "")
                        {
                            p.nome = linha;
                        }
                        if ((linha = sr.ReadLine()) != null)
                        {
                            p.sobrenome = linha;
                        }
                        if ((linha = sr.ReadLine()) != null)
                        {
                            p.aniversario = DateTime.Parse(linha);
                        }
                        if (linha != null)
                        {
                            RepositorioDePessoas.listaPessoas.Add(p);
                        }
                    }
                    sr.Close();
                }      
                
                //Verificar resultado - percorrer lista e exibir os valores recuparados pelo listaPessoas
               // foreach (var item in RepositorioDePessoas.listaPessoas)
                    //{
                     //   Console.WriteLine(@"Nome: {0}; Sobrenome: {1}; Aniversário: {2};", item.nome, item.sobrenome, item.aniversario);
                     //   Console.WriteLine(@"----------------------------------------------------------");
                    //}

                }

            else
            {
                StreamWriter writer = new StreamWriter("Database.txt");
                writer.Close();
            }

        } // fechamento de criarArquivoOuPopLista()

        //método que vai apagar o txt existente e usar a lista p/ popular o txt após ações de cadastro, edit ou delete
        public void listaPopArquivo()
        {
            //primeiro, apagar/resetar o conteudo do txt
            FileStream fileStream = File.Open(DATABASE, FileMode.Open);
            fileStream.SetLength(0);
            fileStream.Close();

            //fazer abaixo o codigo que usa a lista e popula um txt. Acho que pra dar certo tem q fz cada propriedade em uma linha

            StreamWriter writer = new StreamWriter("Database.txt");

            for (int i = 0; i < RepositorioDePessoas.listaPessoas.Count; i++)
            {
                writer.WriteLine(RepositorioDePessoas.listaPessoas.ElementAt(i).nome); //tava assim - listaPessoas[i].nome
                writer.WriteLine(RepositorioDePessoas.listaPessoas.ElementAt(i).sobrenome);
                writer.WriteLine(RepositorioDePessoas.listaPessoas.ElementAt(i).aniversario.ToString("dd/MM/yyyy"));                                               
            }
            writer.Close();
        }
        
        //public void lerArq()
        //{
            //try
           // {
                // Cria uma instância de StreamReader para ler de um arquivo.
                // ps: o using fecha o StreamReader.
               // using (StreamReader sr = new StreamReader(DATABASE))
               // {
                 //   string line;
                    // Lê e exibe as linhas de um arquivo txt até o final do arquivo                     
                  //  while ((line = sr.ReadLine()) != null)
                    //{
                      //  Console.WriteLine(line);
                    //}
                //}
            //}
            //catch (Exception e)
            //{
                
               // Console.WriteLine("The file could not be read:");
              //  Console.WriteLine(e.Message);
           // }
        //}

    }
}
