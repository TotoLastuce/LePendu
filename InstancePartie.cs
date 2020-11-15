using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LePendu
{
    public class InstancePartie
    {
        private int maxErreurs { get; set; }
        public List<char> Trouves { get; }
        public List<char> Manques { get; }
        public List<Mot> Mots { get; }
        internal List<SpecialChar> Speciaux { get; set; }
        public Mot MotADeviner { get; }
        
        private Random random;
        private bool gagne { get; set; }
        private string lettresTrouvees;
        
        public InstancePartie(int maxErreurs = 8)
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
        
        public InstancePartie(List<Mot> mots, int maxErreurs = 10)
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

                char lettreSaisie = char.ToUpper(Console.ReadKey(true).KeyChar);
                bool lettreVerif = char.IsLetter(lettreSaisie);
                const ConsoleKey special1 = ConsoleKey.Oem5; // µ
                const ConsoleKey special2 = ConsoleKey.D2; // é
                const ConsoleKey special3 = ConsoleKey.D7; // è
                const ConsoleKey special4 = ConsoleKey.D9; // ç
                const ConsoleKey special5 = ConsoleKey.D0; // à
                const ConsoleKey special6 = ConsoleKey.Oem3; // ù
                int lettreIndex = MotADeviner.Position(lettreSaisie);
                Console.WriteLine();
                try
                {
                    if (!lettreVerif) // || lettreSaisie.Equals.Speciaux[0] ... 
                    {
                        throw new RuleException("Seul les lettres sont acceptées");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (Trouves.Contains(lettreSaisie))
                {
                    Console.WriteLine("Vous avez déjà trouvé la lettre {0}", lettreSaisie);
                    continue;
                }
                else if (Manques.Contains(lettreSaisie))
                {
                    Console.WriteLine("Vous avez déjà essayé le {0} et c'était faux !", lettreSaisie);
                    continue;
                }
                
                if (lettreIndex != -1)
                {
                    Console.WriteLine("Bravo la lettre {0} est bien contenue dans le mot mystère", lettreSaisie);
                    Trouves.Add(lettreSaisie);
                }
                else
                {
                    Console.WriteLine("Dommage, la lettre {0} n'est pas contenue dans le mot mystère", lettreSaisie);
                    Manques.Add(lettreSaisie);
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
                    Console.WriteLine("Attention plus que 3 erreurs possibles ...");
                    Console.WriteLine("--------\n       |\n       O\n      /|\n");
                }
                else if (Manques.Count == 6)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("Attention plus que 2 erreurs possibles ...");
                    Console.WriteLine("--------\n       |\n       O\n      /|\\ \n");
                }
                else if (Manques.Count == 7)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
                    Console.WriteLine("ATTENTION encore une erreur et c'est la fin !");
                    Console.WriteLine("--------\n       |\n       O\n      /|\\ \n      /\n");
                }
                else if (Manques.Count == 8)
                {
                    Console.WriteLine($"Erreurs : ({Manques.Count}) : {string.Join(", ", Manques)}");
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



