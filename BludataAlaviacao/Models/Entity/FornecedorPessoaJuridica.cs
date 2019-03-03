using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BludataAlaviacao.Models.Entity
{
    [Table("tbl_fornecedor_pj")]
    public class FornecedorPessoaJuridica
    {
        [Key]
        [Required]
        [Column("id_fornecedor")]
        public int IdFornecedor { get; set; }

        [Column("cnpj_fornecedor_pj")]
        [StringLength(14, ErrorMessage = "CNPJ possui apenas 14 caracteres")]
        public string CnpjFornecedorPessoaJuridica { get; set; }

        [NotMapped]
        public Fornecedor Fornecedor { get; set; }
    }
}