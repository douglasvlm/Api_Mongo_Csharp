using System;

namespace Api.Models
{
    public class InfectadoDto
    {
        public uint Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}