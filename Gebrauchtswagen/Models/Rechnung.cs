using System;
using System.Collections.Generic;

#nullable disable

namespace Gebrauchtswagen.Models
{
    public partial class Rechnung
    {
        public int RechnungId { get; set; }
        public int Rechnungsnummer { get; set; }
        public DateTime Datum { get; set; }
        public int KundeId { get; set; }
        public int VerkaeuferId { get; set; }
        public int RechnungfahrzeugId { get; set; }

        public virtual Kunde Kunde { get; set; }
        public virtual Rechnungfahrzeug Rechnungfahrzeug { get; set; }
        public virtual Verkaeufer Verkaeufer { get; set; }
    }
}
