using System;
using System.Collections.Generic;
using System.Text;

namespace LePendu
{
    class SpecialChar
    {
        public string Texte { get; set; }

        public SpecialChar(string texte)
        {
            Texte = texte.ToUpper();
        }
    }
}
