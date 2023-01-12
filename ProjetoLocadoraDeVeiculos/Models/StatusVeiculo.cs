﻿using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLocadoraDeVeiculos.Models
{
    public class StatusVeiculo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [DisplayName("Data Cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }

        [DisplayName("Data Alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();

    }
}