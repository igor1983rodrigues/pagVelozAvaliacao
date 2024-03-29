﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BludataAlaviacao.Models.Entity
{
    [Table("tbl_telefone")]
    public class Telefone
    {
        [Key]
        [Column("id_telefone")]
        public int IdTelefone { get; set; }

        [Column("id_fornecedor")]
        public int IdFornecedor { get; set; }

        [Column("numero_telefone")]
        [StringLength(11, ErrorMessage = "Quantidade de caracteres excedida.")]
        public string NumeroTelefone { get; set; }

        [NotMapped]
        public Fornecedor Fornecedor { get; set; }
    }
}