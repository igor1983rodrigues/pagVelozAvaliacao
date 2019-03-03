using AcessoDados.BaseInterface;
using BludataAlaviacao.Models.Entity;
using System.Collections.Generic;

namespace BludataAlaviacao.Models.IDao
{
    public interface IFornecedorDao : IBaseDaoInterface<Fornecedor>
    {
        IEnumerable<Fornecedor> ObterFornecedorList(object @params, string strConexao);
    }
}