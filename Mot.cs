using System;
using System.Collections.Generic;
using System.Text;

namespace LePendu
{
    public class Mot
    {
        public string Texte { get; set; }
        public int Taille { get; }

        public Mot(string texte)
        {
            Texte = texte.ToUpper();
            Taille = texte.Length;
        }

        public int Position(char lettre)
        {
            return Texte.IndexOf(lettre);
        }
    }
}
