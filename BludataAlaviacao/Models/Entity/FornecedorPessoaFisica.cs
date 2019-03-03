using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BludataAlaviacao.Models.Entity
{
    [Table("tbl_fornecedor_pf")]
    public class FornecedorPessoaFisica
    {
        [Key]
        [Required]
        [Column("id_fornecedor")]
        public int IdFornecedor { get; set; }

        [Column("cpf_fornecedor_pf")]
        [StringLength(11, ErrorMessage = "CPF possui apenas 11 caracteres")]
        public string CpfFornecedorPessoaFisica { get; set; }

        [Column("dt_nasc_fornecedor_pf")]
        public DateTime DataNascimentoFornecedorPessoaFisica { get; set; }

        [Column("rg_fornecedor_pf")]
        [StringLength(20, ErrorMessage = "Quantidade de caracteres excedidos")]
        public string RgFornecedorPessoaFisica { get; set; }

        [NotMapped]
        public Fornecedor Fornecedor { get; set; }
    }
}