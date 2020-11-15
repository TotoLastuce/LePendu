using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LePendu
{
    public class GameInstance
    {
        private int maxErreurs { get; set; }
        public List<char> Trouves { get; }
        public List<char> Manques { get; }
        public List<Mot> Mots { get; }
        internal List<SpecialChar> Speciaux { get; private set; }
        public Mot MotADeviner { get; }

        private const string V = "é";
        private Random random;
        private bool gagne { get; set; }
        private string lettresTrouvees;

        public GameInstance(int maxErreurs = 8)
        {
            random = new Random();
            this.maxErreurs = maxErreurs;

            Mots = new List<Mot>
            {
                new Mot("Informatique"),
                new Mot("Ordinateur"),
                new Mot("Variables"),
                new Mot("Conditions"),
                new Mot("Boucles"),
                new Mot("Méthodes"),
                new Mot("Dictionnaire"),
                new Mot("Héritage"),
                new Mot("Exception"),
                new Mot("Interface"),
            };

            Speciaux = new List<SpecialChar>
            {
                new SpecialChar("é"),
                new SpecialChar("è"),
                new SpecialChar("ç"),
                new SpecialChar("à"),
                new SpecialChar("ù"),
            };

            Trouves = new List<char>();
            Manques = new List<char>();

            MotADeviner = Mots[random.Next(0, Mots.Count)];

            Console.WriteLine("Le mot à deviner contient {0} lettres", MotADeviner.Taille);

            lettresTrouvees = MontrerMotADeviner();
        }

        public GameInstance(List<Mot> mots, int maxErreurs = 8)
        {
            random = new Random();
            this.maxErreurs = maxErreurs;

            Mots = mots;

            Trouves = new List<char>();
            Manques = new List<char>();

            MotADeviner = Mots[random.Next(0, Mots.Count)];

            Console.WriteLine("Le mot à deviner contient {0} lettres", MotADeviner.Taille);

            lettresTrouvees = MontrerMotADeviner();
        }
        public void Jouer()
        {
            while (!gagne)
            {
                Console.WriteLine("Proposez une lettre :");

                char lettre = char.ToUpper(Console.ReadKey(true).KeyChar);
                bool lettreVerif = char.IsLetter(lettre);
                int lettreIndex = MotADeviner.Position(lettre);
                Console.WriteLine();


                if (!lettreVerif) // || lettre.Equals.Speciaux[0] || lettre.Equals.Speciaux[1] || lettre.Equals.Speciaux[2] || lettre.Equals.Speciaux[3] || lettre.Equals.Speciaux[4] 
                {
                    Console.WriteLine("Vous devez taper une lettre uniquement pas de caractère spécial ni lettre avec accent");
                    continue;
                }
                else if (Trouves.Contains(lettre))
                {
                    Console.WriteLine("Vous avez déjà trouvé la lettre {0}", lettre);
                    continue;
                }
                else if (Manques.Contains(lettre))
                {
                    Console.WriteLine("Vous avez déjà essayé le {0} et c'était faux !", lettre);
                    continue;
                }

                if (lettreIndex != -1)
                {
                    Console.WriteLine("Bravo la lettre {0} est bien contenue dans le mot mystère", lettre);
                    Trouves.Add(lettre);
                }
                else
                {
                    Console.WriteLine("Dommage, la lettre {0} n'est pas contenue dans le mot mystère", lettre);
                    Manques.Add(lettre);
                }

                if (Manques.Count == 1)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("--------\n");
                }
                else if (Manques.Count == 2)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("--------\n       |\n");
                }
                else if (Manques.Count == 3)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("--------\n       |\n       O\n");
                }
                else if (Manques.Count == 4)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("--------\n       |\n       O\n       |\n");
                }
                else if (Manques.Count == 5)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("--------\n       |\n       O\n      /|\n");
                }
                else if (Manques.Count == 6)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("Attention plus que 3 erreurs possibles ...");
                    Console.WriteLine("--------\n       |\n       O\n      /|\\ \n");
                }
                else if (Manques.Count == 7)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("Attention plus que 2 erreurs possibles ...");
                    Console.WriteLine("--------\n       |\n       O\n      /|\\ \n      /\n");
                }
                else if (Manques.Count == 8)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("ATTENTION encore une erreur et c'est la fin !");
                    Console.WriteLine("--------\n       |\n       O\n      /|\\ \n      / \\\n");
                }

                lettresTrouvees = MontrerMotADeviner();

                if (lettresTrouvees.IndexOf('_') == -1)
                {
                    gagne = true;
                    Console.WriteLine("Fécilitations vous avez trouvé le mot mystère !");
                    Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);
                    Console.ReadKey();
                    break;
                }

                if (Manques.Count >= maxErreurs)
                {
                    Console.WriteLine("Dommage c'est perdu ...");
                    Console.WriteLine($"Le mot mystère était {MotADeviner.Texte}");
                    Console.Beep(300, 500); Thread.Sleep(50); Console.Beep(300, 500); Thread.Sleep(50); Console.Beep(300, 500); Thread.Sleep(50); Console.Beep(250, 500); Thread.Sleep(50); Console.Beep(350, 250); Console.Beep(300, 500); Thread.Sleep(50); Console.Beep(250, 500); Thread.Sleep(50); Console.Beep(350, 250); Console.Beep(300, 500); Thread.Sleep(50);
                    Console.ReadKey();
                    break;
                }
            }
        }

        private string MontrerMotADeviner()
        {
            string lettresTrouvees = "";

            for (int i = 0; i < MotADeviner.Taille; i++)
            {
                if (Trouves.Contains(MotADeviner.Texte[i]))
                {
                    lettresTrouvees += MotADeviner.Texte[i];
                }
                else
                {
                    lettresTrouvees += "_ ";
                }
            }

            Console.WriteLine(lettresTrouvees);
            Console.WriteLine();

            return lettresTrouvees;
        }
    }
}
