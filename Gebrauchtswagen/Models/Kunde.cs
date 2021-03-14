using System;
using System.Collections.Generic;

#nullable disable

namespace Gebrauchtswagen.Models
{
    public partial class Kunde
    {
        public Kunde()
        {
            Rechnungs = new HashSet<Rechnung>();
        }

        public int KundeId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Ort { get; set; }
        public string Plz { get; set; }
        public string Strasse { get; set; }
        public string Hn { get; set; }

        public virtual ICollection<Rechnung> Rechnungs { get; set; }
    }
}
