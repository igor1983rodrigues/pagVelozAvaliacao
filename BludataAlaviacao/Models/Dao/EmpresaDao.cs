using System.Collections.Generic;
using AcessoDados.BaseRepository;
using BludataAlaviacao.Models.Entity;
using BludataAlaviacao.Models.IDao;
using BludataAlaviacao.Models.Resources;

namespace BludataAlaviacao.Models.Dao
{
    public class EmpresaDao : BaseDaoRepository<Empresa>, IEmpresaDao
    {
        public IEnumerable<Empresa> ObterListaEmpresa(object parametros, string strConexao)
        {
            return ExecuteQuery(EmpresaQuery.GetAll, parametros, strConexao, System.Data.CommandType.Text);
        }
    }
}