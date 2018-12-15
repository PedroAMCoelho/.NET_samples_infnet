using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GerenciamentoAniversarios.Models;

namespace GerenciamentoAniversarios.Controllers
{
    public class PessoaController : Controller
    {
        private RepositorioDePessoas _repositorio = new RepositorioDePessoas();
        int maiorId;

        // GET: Pessoa
        public ActionResult ObterPessoas()
        {
            //_repositorio = new RepositorioDePessoas();
            _repositorio.GetAllPessoas();
            ModelState.Clear(); //limpa todos os models

            return View(_repositorio.GetAllPessoas());
        }

        [HttpGet]
        public ActionResult IncluirPessoa()
        {
            return View(); //cria na View os campos para input dos dados da Pessoa
        }

        [HttpPost]
        public ActionResult IncluirPessoa(Pessoa pessoaobj)
        {
                        
            try
            {//esse primeiro if verifica se todos os 
             //dados - nome, sobrenome, aniversario - do Modelo Pessoa foram passados
                if (ModelState.IsValid) 
                {
                   // _repositorio = new RepositorioDePessoas();
                    //List<Pessoa> listaDePessoas = _repositorio.GetAllPessoas().ToList();
                    //ModelState.Clear();
                    //List<Pessoa> todasPessoasMenosACorrente = _repositorio.GetAllPessoas().Where(p => p.PessoaId != id).ToList();

                    pessoaobj.PessoaId = maiorIdDaLista() + 1;
                    _repositorio.Cadastra(pessoaobj);
                    ViewBag.Mensagem = "Pessoa cadastrada com sucesso!";
                    
                }
                return View();
                //return RedirectToAction("ObterPessoas");
            }
            catch (Exception)
            {
                return View("ObterPessoas");
            }
        }
        
        [HttpGet]
        public ActionResult EditarPessoa(int id)
        {

            //_repositorio = new RepositorioDePessoas();

            return View(_repositorio.GetAllPessoas().ToList().Find(p => p.PessoaId == id)); //cria na View os campos para input dos dados da Pessoa
        }

        [HttpPost]
        public ActionResult EditarPessoa(int id, Pessoa pessoaobj)
        {
            try
            {
                    //_repositorio = new RepositorioDePessoas();
                    _repositorio.Atualiza(pessoaobj);

                return RedirectToAction("ObterPessoas");                   
            }
            catch (Exception)
            {
                return View("ObterPessoas");
            }
        }
        
        //é um http post tbm
        public ActionResult ExcluirPessoa(int id)
        {
            //_repositorio = new RepositorioDePessoas();
            
            _repositorio.Exclui(id);            

            return RedirectToAction("ObterPessoas");
        }

        // GET: Busca
        public ActionResult BuscarPessoa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarPessoa(Pessoa pessoa)
        {
            //_repositorio = new RepositorioDePessoas();
            List<Pessoa> listaDePessoas = _repositorio.GetAllPessoas().ToList();

            if (pessoa.Nome == null || pessoa.Sobrenome == null)
            {
                ViewBag.ErrorMessage = "Por favor, preencha todos os campos!";
            }
            else
            {
                IEnumerable<Pessoa> lista_nova = listaDePessoas.Where(p => p.Nome.Contains(pessoa.Nome) || p.Sobrenome.Contains(pessoa.Sobrenome));
                //lista para agrupar todas as pessoas que contem parte ou totalidade do nome/sobrenome pesquisado

                ViewBag.PessoasEncontradas = lista_nova.ToList();
            }

            return View("BuscarPessoa");
        }

        public int maiorIdDaLista()
        {
            List<Pessoa> listaDePessoas = _repositorio.GetAllPessoas().ToList();
            List<int> ids = new List<int>();

            if (listaDePessoas.Count != 0)
            {
                
                foreach (Pessoa p in listaDePessoas)
                {                    
                    ids.Add(p.PessoaId);
                    maiorId = ids.Max();
                }
            }
            else
            {
                maiorId = 0;
            }

            return maiorId;
        }

    }
}