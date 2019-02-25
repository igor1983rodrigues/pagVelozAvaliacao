using AcessoDados.BaseInterface;
using BludataAlaviacao.Models.Entity;
using System.Collections.Generic;

namespace BludataAlaviacao.Models.IDao
{
    public interface IEmpresaDao : IBaseDaoInterface<Empresa>
    {
        IEnumerable<Empresa> ObterListaEmpresa(object parametros, string strConexao);
    }
}
