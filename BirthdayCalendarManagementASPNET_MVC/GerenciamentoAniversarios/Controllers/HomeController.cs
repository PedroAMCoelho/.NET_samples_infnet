using System;
using System.Collections.Generic;
using System.Linq;
using GerenciamentoAniversarios.Models;
using System.Web;
using System.Web.Mvc;

namespace GerenciamentoAniversarios.Controllers
{
    public class HomeController : Controller
    {
        private RepositorioDePessoas _repositorio;
        List<Pessoa> lista1;
        List<Pessoa> lista2;

        public ActionResult Index()
        {
            _repositorio = new RepositorioDePessoas();
            lista1 = _repositorio.GetProxAniversariantes().FindAll(p => p.Aniversario.Month < DateTime.Now.Month);
            lista2 = _repositorio.GetProxAniversariantes().FindAll(p => p.Aniversario.Month >= DateTime.Now.Month);


            //aniversariantes do dia
            if (_repositorio.GetAniversariantesDoDia().Count > 0)
            {
                ViewBag.PessoasNiverHoje = _repositorio.GetAniversariantesDoDia();
            }
            else
            {
                ViewBag.SemNiver = "Não há aniversariantes hoje";                
            }
            //proximos aniversariantes do ano
            if (_repositorio.GetAllPessoas().ToList().Count > 0)
            {
              /**  try
                {
                    foreach (Pessoa p in lista2)
                    {
                        if (DateTime.Now.Month == p.Aniversario.Month && DateTime.Now.Day > p.Aniversario.Day)
                        {
                            lista1.Add(p);
                            lista2.Remove(p); // lista com os aniversariantes do ano, inclusive do mês
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                } **/

                List<Pessoa> lista2Final = lista2.OrderBy(p => p.Aniversario).ToList();

                ViewBag.ProxNivers = lista2Final;               
            }
            else
            {
                ViewBag.SemContatos = "Vamos lá, adicione um contato!";
            }
            //proximos aniversariantes (ano que vem)
            if (_repositorio.GetAllPessoas().ToList().Count > 0)
            {
                //lista1 = _repositorio.GetProxAniversariantes().FindAll(p => p.Aniversario.Month <= DateTime.Now.Month);
                //List<int> i = new List<int>();



                //foreach (Pessoa p in lista1)
                // {
                //  if (DateTime.Now.Month == p.Aniversario.Month && DateTime.Now.Day < p.Aniversario.Day)
                //  {
                //      int indexASerDeletado = lista1.IndexOf(p);
                //       i.Add(indexASerDeletado);
                //   }
                //  }
                // foreach (int x in i)
                // {
                //      lista1.RemoveAt(x);
                //  }

                List<Pessoa> datasAnoQueVem = new List<Pessoa>();
                foreach (Pessoa p in lista1)
                {                    
                    string data = p.Aniversario.Day + "/" + p.Aniversario.Month + "/" + "2019" + " " + "00:00:00 AM";
                    DateTime dt = DateTime.Parse(data, System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));
                    p.Aniversario = dt;
                    datasAnoQueVem.Add(p);
                }

                datasAnoQueVem = lista1.OrderBy(p => p.Aniversario).ToList();

                
                    ViewBag.ProxNiversAnoQueVem = datasAnoQueVem;              

            }
            else
            {
                ViewBag.SemContatos = "Vamos lá, adicione um contato!";
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}