using System;
using System.Collections.Generic;

#nullable disable

namespace Gebrauchtswagen.Models
{
    public partial class Fahrzeug
    {
        public Fahrzeug()
        {
            Rechnungfahrzeugs = new HashSet<Rechnungfahrzeug>();
        }

        public int FahrzeugId { get; set; }
        public decimal Preis { get; set; }
        public string Zustand { get; set; }
        public string Bezeichnung { get; set; }

        public virtual ICollection<Rechnungfahrzeug> Rechnungfahrzeugs { get; set; }
    }
}
