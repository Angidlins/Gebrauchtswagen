using System;
using System.Collections.Generic;

#nullable disable

namespace Gebrauchtswagen.Models
{
    public partial class Verkaeufer
    {
        public Verkaeufer()
        {
            Rechnungs = new HashSet<Rechnung>();
        }

        public int VerkaeuferId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public virtual ICollection<Rechnung> Rechnungs { get; set; }
    }
}
