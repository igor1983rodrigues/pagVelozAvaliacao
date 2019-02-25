using BludataAlaviacao.Models.Entity;
using BludataAlaviacao.Models.IDao;
using BludataAlaviacao.Properties;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BludataAlaviacao.Areas.WebApi
{
    [RoutePrefix("api/empresa")]
    public class EmpresaApiController : ApiController
    {
        private IEmpresaDao iEmpresaDao;
        private string mensagem;

        public EmpresaApiController(IEmpresaDao iEmpresaDao)
        {
            this.iEmpresaDao = iEmpresaDao;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> ListarAsync()
        {
            try
            {
                var res = await Task.Run(() => iEmpresaDao.ObterListaEmpresa(new { }, Resources.Conexao));

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
                Empresa model = parametros.ToObject<Empresa>();
                int res = await Task.Run(() => iEmpresaDao.Inserir(model, out mensagem, Resources.Conexao));

                if (!string.IsNullOrEmpty(mensagem)) {
                    throw new System.Exception(mensagem);
                }

                return Ok(new {
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
                Empresa model = parametros.ToObject<Empresa>();
                await Task.Run(() => iEmpresaDao.Alterar(model, out mensagem, Resources.Conexao));

                if (!string.IsNullOrEmpty(mensagem))
                {
                    throw new System.Exception(mensagem);
                }

                return Ok(new
                {
                    Id = model.IdEmpresa,
                    Message = "Registro alterado com sucesso!"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> ExcluirAsync(int id)
        {
            try
            {
                mensagem = null;
                await Task.Run(() => iEmpresaDao.Excluir(id, out mensagem, Resources.Conexao));

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