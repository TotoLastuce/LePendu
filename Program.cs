using System;
using System.Collections.Generic;

namespace LePendu
{
    class Program
    {
        static void Main(string[] args)
        {
            GameInstance jeu = new GameInstance();
            // InstancePartie jeu = new InstancePartie();
            jeu.Jouer();
        }
    }
}
