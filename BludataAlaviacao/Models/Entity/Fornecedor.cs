using System;
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

        [Column("dt_cad_fornecedor")]
        public DateTime DataCadastroFornecedor { get; set; }

        [NotMapped]
        public Empresa Empresa { get; set; }
    }
}