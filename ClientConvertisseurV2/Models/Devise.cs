﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace ClientConvertisseurV2.Models
{
    public class Devise
    {
        public int Id { get; set; }
        public string NomDevise { get; set; }
        public double Taux { get; set; }
        public Devise()
        {
        }

        public Devise(int id, string nomDevise, double taux)
        {
            Id = id;
            NomDevise = nomDevise;
            Taux = taux;
        }

        public override bool Equals(object obj)
        {
            return obj is Devise devise &&
                   Id == devise.Id &&
                   NomDevise == devise.NomDevise &&
                   Taux == devise.Taux;
        }

        public override string ToString()
        {
            return this.NomDevise;
        }
    }
}
