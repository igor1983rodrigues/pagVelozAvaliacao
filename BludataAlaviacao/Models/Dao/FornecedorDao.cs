using AcessoDados.BaseRepository;
using BludataAlaviacao.Models.Entity;
using BludataAlaviacao.Models.IDao;
using BludataAlaviacao.Models.Resources;
using Dapper;
using System;
using System.Collections.Generic;

namespace BludataAlaviacao.Models.Dao
{
    public class FornecedorDao : BaseDaoRepository<Fornecedor>, IFornecedorDao
    {
        public override int Inserir(Fornecedor model, out string mensagem, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                System.Data.Common.DbTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    mensagem = null;
                    int id = conn.Insert(model) ?? 0;
                    if (id == 0) throw new Exception("Erro ao inserir os dados. Entre em contato com o administrador.");

                    model.IdFornecedor = id;

                    if (model.FornecedorPessoaFisica != null)
                    {
                        model.FornecedorPessoaFisica.IdFornecedor = id;
                        id = conn.Insert(model.FornecedorPessoaFisica) ?? 0;
                        if (id == 0) throw new Exception("Erro ao inserir os dados. Entre em contato com o administrador.");
                    }
                    else if (model.FornecedorPessoaJuridica != null)
                    {
                        model.FornecedorPessoaJuridica.IdFornecedor = id;
                        id = conn.Insert(model.FornecedorPessoaJuridica) ?? 0;
                        if (id == 0) throw new Exception("Erro ao inserir os dados. Entre em contato com o administrador.");
                    }

                    if (model.Telefones != null)
                        foreach (Telefone telefone in model.Telefones)
                        {
                            telefone.IdFornecedor = id;
                            conn.Insert(telefone);
                        }

                    transaction.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    mensagem = ex.Message;
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<Fornecedor> ObterFornecedorList(object @params, string strConexao)
        {
            return ExecuteQuery<Fornecedor, Empresa, Fornecedor>(FornecedorQuery.GetAll, @params, strConexao, (fornecedor, empresa) =>
            {
                fornecedor.Empresa = empresa;
                return fornecedor;
            }, new string[] { "IdEmpresa" }, System.Data.CommandType.Text);
        }
    }
}