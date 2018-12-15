using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GerenciamentoAniversarios.Models
{
    public class RepositorioDePessoas
    {
        //public Database1Entities db;

        //public RepositorioDePessoas()
        //{
        //    db = new Database1Entities();
        //}

      //  public RepositorioDePessoas(Database1Entities database)
       // {
       //     db = database;
       // }

        //private SqlConnection _con;

        //private void Connection()
        // {
        //   string constr = ConfigurationManager.ConnectionStrings["stringConexao"].ToString();
        //   _con = new SqlConnection(constr);
        // }

        //Adicionar uma pessoa

        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\ppedr\source\repos\Assessment_ASPNET_MVC\GerenciamentoAniversarios\App_Data\Database1.mdf';Integrated Security=True";
        private static int mesAtual = DateTime.Now.Month;
        private static int diaAtual = DateTime.Now.Day;
        private static int anoAtual = DateTime.Now.Year;



        public IEnumerable<Pessoa> GetAllPessoas()
        {

            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = "SELECT * FROM Pessoa";
                var selectCommand = new SqlCommand(commandText, connection);

                var pessoas = new List<Pessoa>();

                try
                {
                    connection.Open();

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var pessoa = new Pessoa();

                            pessoa.PessoaId = (int)reader["PessoaId"];
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.Aniversario = (DateTime)reader["Aniversario"];

                            pessoas.Add(pessoa);
                        }
                    }


                }
                finally
                {
                    connection.Close();
                }
                return pessoas;
            }
        }

        public void Cadastra(Pessoa pessoa)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Pessoa (PessoaId, Nome, Sobrenome, Aniversario) VALUES (@PessoaId, @Nome, @Sobrenome, @Aniversario)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PessoaId", pessoa.PessoaId);
                cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", pessoa.Sobrenome);
                cmd.Parameters.AddWithValue("@Aniversario", pessoa.Aniversario);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Atualiza(Pessoa pessoaobj)
        {

            int i;
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Pessoa SET PessoaId=@PessoaId, Nome=@Nome, Sobrenome=@Sobrenome, Aniversario=@Aniversario Where PessoaId=@PessoaId";

                //Ado .NET
                SqlCommand command = new SqlCommand(sql, conn);
                                
                command.Parameters.AddWithValue("@PessoaId", pessoaobj.PessoaId);
                command.Parameters.AddWithValue("@Nome", pessoaobj.Nome);
                command.Parameters.AddWithValue("@Sobrenome", pessoaobj.Sobrenome);
                command.Parameters.AddWithValue("@Aniversario", pessoaobj.Aniversario);

                try
                {
                    conn.Open();
                    i = command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Exclui(int id = 0)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE Pessoa Where PessoaId=@PessoaId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PessoaId", id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
              }
                finally
                {
                    conn.Close();
                }
            }
        }
        //DETALHES
        public Pessoa GetById(int id = 0)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "Select * FROM Pessoa WHERE PessoaId=@PessoaId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PessoaId", id);
                Pessoa p = null;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                p = new Pessoa();
                                //p.PessoaId = (int) reader["PessoaID"];
                                p.Nome = reader["Nome"].ToString();
                                p.Sobrenome = reader["Sobrenome"].ToString();
                                p.Aniversario = (DateTime) reader["Aniversario"];
                            }
                        }
                    }
                }

                finally
                {
                    conn.Close();
                }
                return p;
            }
        }

        public List<Pessoa> GetAniversariantesDoDia()
        {
            
            List<Pessoa> listaPessoas = GetAllPessoas().ToList();

            List<Pessoa> aniversariantes = listaPessoas.FindAll(p => p.Aniversario.Day == DateTime.Now.Day && p.Aniversario.Month == DateTime.Now.Month);
            
            return aniversariantes;
            }

        public List<Pessoa> GetProxAniversariantes()
        {

            List<Pessoa> listaPessoas = GetAllPessoas().ToList();
            //List<Pessoa> listaProxNivers = listaPessoas.OrderByDescending(p => p.Aniversario < dataDeHoje).ToList();
            //List<Pessoa> listaProxNivers = GetAllPessoas().OrderByDescending(p => p.Aniversario.Month < mesAtual).ThenByDescending(p => p.Aniversario.Day < diaAtual).ToList();
            //List<Pessoa> listaProxNivers = listaPessoas.Where(p=>)
            //List<Pessoa> datasAnoCorrente = listaPessoas.ForEach(p => p.Aniversario.Year == anoAtual);
            List<Pessoa> datasAnoCorrente = new List<Pessoa>();
            foreach (Pessoa p in listaPessoas)
            {
                //DateTime dt = new DateTime(p.Aniversario.Day, p.Aniversario.Month, anoAtual);
                //string data = p.Aniversario.Day + "/" + p.Aniversario.Month + "/" + anoAtual;
                //var d = DateTime.Parse(p.Aniversario.ToString("yyyy/MM/dd"));
                //string data = p.Aniversario.Day + "/" + p.Aniversario.Month + "/" + anoAtual;
                //p.Aniversario = DateTime.ParseExact(d, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));
                //p.Aniversario = Convert.ToInt32("2018");
                string data = p.Aniversario.Day + "/" + p.Aniversario.Month + "/" + anoAtual + " " + "00:00:00 AM";
                DateTime dt = DateTime.Parse(data, System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));
                p.Aniversario = dt;
                datasAnoCorrente.Add(p);
            }
            List<Pessoa> listaProxNivers = datasAnoCorrente.OrderByDescending(p => p.Aniversario).ToList();

            List<Pessoa> lista1 = listaProxNivers.FindAll(p => p.Aniversario.Month < mesAtual);
            List<Pessoa> lista2 = listaProxNivers.FindAll(p => p.Aniversario.Month > mesAtual);

            List<Pessoa> lista2Final = lista2.OrderBy(p => p.Aniversario).ToList();

            List<Pessoa> lista3 = new List<Pessoa>();

           // foreach(Pessoa p in lista1)
           // {
             //   lista3 = lista2Final.Add(p);
          //  }

            //foreach(Pessoa p in lista2)
            //{
            //  p.Aniversario = new DateTime(p.Aniversario.Day, p.Aniversario.Month, anoAtual+1);
            //}
            /**
            List<Pessoa> lista3 = new List<Pessoa>(); **/

            //for(int i = 0; i < lista1.Count; i++)
            //{
            //  foreach (Pessoa p in lista1)
            //  {
            //      lista3.Add(p);
            //  }
            //  }

            // for (int i = 0; i < lista2.Count; i++)
            //  {
            //      foreach (Pessoa p in lista2)
            //     {
            //         lista3.Add(p);
            //      }
            //  }


            return listaProxNivers;// listaFinal;

        }
        


        /**
public bool ExcluirPessoa(int id)
{
    Connection();

    int i;

    //Ado .NET
    using (SqlCommand command = new SqlCommand("ExcluirTimePorId", _con))
    {
        command.CommandType = CommandType.StoredProcedure; //informa que é procedure

        command.Parameters.AddWithValue("@PessoaId", id);              

        _con.Open();

        i = command.ExecuteNonQuery();
    }
    _con.Close();

    if (i >= 1) { return true; }
    else { return false; }
    
}

public bool AtualizarPessoa(Pessoa pessoaobj)
{
    Connection();

    int i;

    //Ado .NET
    using (SqlCommand command = new SqlCommand("AtualizarTime", _con))
    {
        command.CommandType = CommandType.StoredProcedure; //informa que é procedure

        command.Parameters.AddWithValue("@PessoaId", pessoaobj.PessoaId);
        command.Parameters.AddWithValue("@Nome", pessoaobj.Nome);
        command.Parameters.AddWithValue("@Sobrenome", pessoaobj.Sobrenome);
        command.Parameters.AddWithValue("@Aniversario", pessoaobj.Aniversario);

        _con.Open();

        i = command.ExecuteNonQuery();
    }
    _con.Close();

    return i >= 1;
}

/**
private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\ppedr\source\repos\Assessment_ASPNET_MVC\GerenciamentoAniversarios\App_Data\Database1.mdf';Integrated Security=True";

    public IEnumerable<Pessoa> GetAllPessoas()
{

    using (var connection = new SqlConnection(connectionString))
    {
        var commandText = "SELECT * FROM Pessoa";
        var selectCommand = new SqlCommand(commandText, connection);

        var pessoas = new List<Pessoa>();

        try
        {
            connection.Open();

            using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while(reader.Read())
                {
                    var pessoa = new Pessoa();

                    pessoa.PessoaId = (int)reader["PessoaId"];
                    pessoa.Nome = reader["Nome"].ToString();
                    pessoa.Sobrenome = reader["Sobrenome"].ToString();
                    pessoa.Aniversario = (DateTime)reader["Aniversario"];

                    pessoas.Add(pessoa);
                }
            }


        }
        finally
        {
            connection.Close();
        }
        return pessoas;
    }
}

public void Cadastra(Pessoa pessoa)
{          
    using (var conn = new SqlConnection(connectionString))
    {
        string sql = "INSERT INTO Pessoa (PessoaId, Nome, Sobrenome, Aniversario) VALUES (@PessoaId, @Nome, @Sobrenome, @Aniversario)";

        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PessoaId", pessoa.PessoaId);
        cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
        cmd.Parameters.AddWithValue("@Sobrenome", pessoa.Sobrenome);
        cmd.Parameters.AddWithValue("@Aniversario", pessoa.Aniversario);

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
        }
    }
}

**/

        // public List<Pessoa> GetAllPessoas()
        //{
        //   db.Database.
        // }

        // }
    }

}




