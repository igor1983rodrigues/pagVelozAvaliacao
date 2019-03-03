using System.Collections.Generic;
using AcessoDados.BaseRepository;
using BludataAlaviacao.Models.Entity;
using BludataAlaviacao.Models.IDao;
using BludataAlaviacao.Models.Resources;

namespace BludataAlaviacao.Models.Dao
{
    public class EmpresaDao : BaseDaoRepository<Empresa>, IEmpresaDao
    {
    }
}