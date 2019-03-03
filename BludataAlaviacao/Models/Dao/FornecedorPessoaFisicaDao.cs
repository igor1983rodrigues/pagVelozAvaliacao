using AcessoDados.BaseRepository;
using BludataAlaviacao.Models.Entity;
using BludataAlaviacao.Models.IDao;
using Dapper;
using System;

namespace BludataAlaviacao.Models.Dao
{
    public class FornecedorPessoaFisicaDao : BaseDaoRepository<FornecedorPessoaFisica>, IFornecedorPessoaFisicaDao
    {
    }
}