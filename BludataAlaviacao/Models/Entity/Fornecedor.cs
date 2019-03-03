using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BludataAlaviacao.Models.Entity
{
    [Table("tbl_fornecedor", Schema = "public")]
    public class Fornecedor
    {
        [Key]
        [Column("id_fornecedor")]
        public int IdFornecedor { get; set; }

        [Column("id_empresa")]
        public int IdEmpresa { get; set; }

        [Column("nome_fornecedor")]
        [StringLength(128, ErrorMessage = "Quantidade de caracteres excedidas")]
        public string NomeFornecedor { get; set; }

        [Column("dt_cad_fornecedor")]
        public DateTime DataCadastroFornecedor { get; set; }

        [NotMapped]
        public Empresa Empresa { get; set; }

        [NotMapped]
        public FornecedorPessoaFisica FornecedorPessoaFisica { get; set; }

        [NotMapped]
        public FornecedorPessoaJuridica FornecedorPessoaJuridica { get; set; }

        [NotMapped]
        public IEnumerable<Telefone> Telefones { get; set; }

        public Fornecedor()
        {
            DataCadastroFornecedor = DateTime.Now;
        }
    }
}