using BludataAlaviacao.Models.Entity;
using BludataAlaviacao.Models.IDao;
using BludataAlaviacao.Properties;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BludataAlaviacao.Areas.WebApi
{
    [RoutePrefix("api/fornecedor")]
    public class FornecedorApiController : ApiController
    {
        private IFornecedorDao iFornecedorDao;
        private IFornecedorPessoaFisicaDao iFornecedorPessoaFisicaDao;
        private IFornecedorPessoaJuridicaDao iFornecedorPessoaJuridicaDao;
        private ITelefoneDao iTelefoneDao;
        private string mensagem;

        public FornecedorApiController(
            IFornecedorDao iFornecedorDao,
            IFornecedorPessoaFisicaDao iFornecedorPessoaFisicaDao,
            IFornecedorPessoaJuridicaDao iFornecedorPessoaJuridicaDao,
            ITelefoneDao iTelefoneDao)
        {
            this.iFornecedorDao = iFornecedorDao;
            this.iFornecedorPessoaFisicaDao = iFornecedorPessoaFisicaDao;
            this.iFornecedorPessoaJuridicaDao = iFornecedorPessoaJuridicaDao;
            this.iTelefoneDao = iTelefoneDao;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> ListarAsync()
        {
            try
            {
                var res = await Task.Run(() => iFornecedorDao.ObterFornecedorList(new { }, Resources.Conexao));

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var res = await Task.Run(() => iFornecedorDao.ObterPorChave(id, Resources.Conexao));

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> InserirAsync([FromBody]JObject parametros)
        {
            try
            {
                mensagem = null;
                Fornecedor model = parametros.ToObject<Fornecedor>();

                int res = await Task.Run(() => iFornecedorDao.Inserir(model, out mensagem, Resources.Conexao));
                if (!string.IsNullOrEmpty(mensagem))
                {
                    throw new System.Exception(mensagem);
                }

                return Ok(new
                {
                    Id = res,
                    Message = "Registro salvo com sucesso!"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> AlterarAsync(int id, [FromBody]JObject parametros)
        {
            try
            {
                mensagem = null;
                Fornecedor model = parametros.ToObject<Fornecedor>();
                await Task.Run(() => iFornecedorDao.Alterar(model, out mensagem, Resources.Conexao));

                if (!string.IsNullOrEmpty(mensagem))
                {
                    throw new System.Exception(mensagem);
                }

                return Ok(new
                {
                    Id = model.IdFornecedor,
                    Message = "Registro alterado com sucesso!"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> ExcluirAsync(int id)
        {
            try
            {
                mensagem = null;

                await Task.Run(() => iFornecedorDao.Excluir(id, out mensagem, Resources.Conexao));

                if (!string.IsNullOrEmpty(mensagem))
                {
                    throw new System.Exception(mensagem);
                }

                return Ok(new
                {
                    Message = "Registro excluído com sucesso!"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}