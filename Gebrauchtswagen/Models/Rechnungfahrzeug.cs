using System;
using System.Collections.Generic;

#nullable disable

namespace Gebrauchtswagen.Models
{
    public partial class Rechnungfahrzeug
    {
        public Rechnungfahrzeug()
        {
            Rechnungs = new HashSet<Rechnung>();
        }

        public int RechnungfahrzeugId { get; set; }
        public int Menge { get; set; }
        public int FahrzeugId { get; set; }
        public decimal PreisBeiRechnungserstellung { get; set; }

        public virtual Fahrzeug Fahrzeug { get; set; }
        public virtual ICollection<Rechnung> Rechnungs { get; set; }
    }
}
