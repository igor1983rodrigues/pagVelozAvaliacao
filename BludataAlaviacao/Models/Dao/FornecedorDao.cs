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
        public override Fornecedor ObterPorChave(object parametros, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();
                    Fornecedor res = conn.Get<Fornecedor>(parametros);

                    res.Empresa = conn.Get<Empresa>(res.IdEmpresa);
                    res.Telefones = conn.GetList<Telefone>(new { IdFornecedor = parametros });
                    res.FornecedorPessoaFisica = conn.Get<FornecedorPessoaFisica>(parametros);
                    res.FornecedorPessoaJuridica = conn.Get<FornecedorPessoaJuridica>(parametros);

                    return res;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

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
            return ExecuteQuery<Fornecedor, Empresa, FornecedorPessoaJuridica,FornecedorPessoaFisica, Fornecedor>(FornecedorQuery.GetAll, @params, strConexao, (fornecedor, empresa, fornecedorPessoaJuridica, fornecedorPessoaFisica) =>
            {
                fornecedor.Empresa = empresa;
                fornecedor.FornecedorPessoaFisica = fornecedorPessoaFisica;
                fornecedor.FornecedorPessoaJuridica = fornecedorPessoaJuridica;
                return fornecedor;
            }, new string[] { "IdEmpresa", "IdFornecedor", "IdFornecedor" }, System.Data.CommandType.Text);
        }

        public override void Alterar(Fornecedor model, out string mensagem, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                System.Data.Common.DbTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    int id = 0;
                    if (model.FornecedorPessoaFisica != null)
                    {
                        id = conn.Update(model.FornecedorPessoaFisica);
                        if (id == 0) throw new Exception("Erro ao atualizar os dados. Entre em contato com o administrador.");
                    }

                    if (model.FornecedorPessoaJuridica != null)
                    {
                        id = conn.Update(model.FornecedorPessoaJuridica);
                        if (id == 0) throw new Exception("Erro ao atualizar os dados. Entre em contato com o administrador.");
                    }

                    if (conn.RecordCount<Telefone>(new { IdFornecedor = model.IdFornecedor}) > 0)
                    {
                        conn.DeleteList<Telefone>(new { IdFornecedor = model.IdFornecedor });
                    }

                    if (model.Telefones != null)
                        foreach (Telefone item in model.Telefones)
                        {
                            item.IdTelefone = 0;
                            id = conn.Insert(item) ?? 0;
                            if (id == 0) throw new Exception("Erro ao atualizar os dados. Entre em contato com o administrador.");
                        }

                    id = conn.Update(model);
                    if (id == 0) throw new Exception("Erro ao atualizar os dados. Entre em contato com o administrador.");
                    mensagem = null;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public override void Excluir(object obj, out string mensagem, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                System.Data.Common.DbTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    if (conn.RecordCount<Telefone>(new { IdFornecedor = obj }) > 0)
                    {
                        conn.DeleteList<Telefone>(new { IdFornecedor = obj });
                    }

                    if (conn.RecordCount<FornecedorPessoaFisica>(new { IdFornecedor = obj }) > 0)
                    {
                        conn.Delete<FornecedorPessoaFisica>(obj);
                    }

                    if (conn.RecordCount<FornecedorPessoaFisica>(new { IdFornecedor = obj }) > 0)
                    {
                        conn.Delete<FornecedorPessoaJuridica>(obj);
                    }

                    int id = conn.Delete<Fornecedor>(obj);
                    if (id == 0) throw new Exception("Erro ao excluir os dados. Entre em contato com o administrador.");
                    mensagem = null;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}