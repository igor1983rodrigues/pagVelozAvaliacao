using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BludataAlaviacao.Models.Entity
{
    [Table("tbl_empresa", Schema = "public")]
    public class Empresa
    {
        [Key]
        [Column("id_empresa")]
        public int IdEmpresa { get; set; }

        [Required]
        [Column("nome_empresa")]
        public string NomeEmpresa { get; set; }

        [Required]
        [Column("cnpj_empresa")]
        public string CnpjEmpresa { get; set; }

        [Required]
        [Column("uf_empresa")]
        [StringLength(2, ErrorMessage = "Quantidade excedida")]
        public string UfEmpresa { get; set; }
    }
}