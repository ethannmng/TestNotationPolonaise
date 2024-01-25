/**
 * Application de test de la fonction 'Polonaise'
 * author : Ethan MENAGE
 * date : 25/01/2024
 */
using System;

namespace TestNotationPolonaise
{
    class Program
    {
        /// <summary>
        /// saisie d'une réponse d'un caractère parmi 2
        /// </summary>
        /// <param name="message">message à afficher</param>
        /// <param name="carac1">premier caractère possible</param>
        /// <param name="carac2">second caractère possible</param>
        /// <returns>caractère saisi</returns>
        static char saisie(string message, char carac1, char carac2)
        {
            char reponse;
            do
            {
                Console.WriteLine();
                Console.Write(message + " (" + carac1 + "/" + carac2 + ") ");
                reponse = Console.ReadKey().KeyChar;
            } while (reponse != carac1 && reponse != carac2);
            return reponse;
        }

        /// <summary>
        /// calcul à partir d'une notation polonaise
        /// </summary>
        /// <param name="formule">la formule qu'on souhaite calculer</param>
        /// <returns>le résultat du calcul</returns>
        static Double Polonaise(string formule)
        {
            try
            {
                // Transformation de la formule en vecteur
                string[] sep = formule.Split(' ');
                int formuleLength = sep.Length;

                // Boucle tant qu'il ne reste pas qu'une seule case
                while (formuleLength > 1)
                {
                    // Rercherche d'un signe à partir de la fin
                    int k = formuleLength - 1;
                    while (k > 0 && sep[k] != "+" && sep[k] != "-" && sep[k] != "*" && sep[k] != "/")
                    {
                        k--;
                    }

                    // Récupération des deux valeurs concernées par le calcul
                    float a = float.Parse(sep[k + 1]);
                    float b = float.Parse(sep[k + 2]);

                    // Calcul du résultat
                    float result = 0;
                    switch (sep[k])
                    {
                        case "+": result = a + b; break;
                        case "-": result = a - b; break;
                        case "*": result = a * b; break;
                        case "/":
                            if (b == 0)
                            {
                                return Double.NaN;
                            }
                            result = a / b; break;
                    }

                    // Stockage du résultat à la place du signe
                    sep[k] = result.ToString();

                    // Suppression des 2 cellules suivantes par décalage vers la gauche
                    for (int j = k + 1; j < formuleLength - 2; j++)
                    {
                        sep[j] = sep[j + 2];
                    }
                    // Les cases suivantes sont mises à blanc
                    for (int j = formuleLength - 2; j < formuleLength; j++)
                    {
                        sep[j] = " ";
                    }
                    formuleLength = formuleLength - 2;
                }

                return Double.Parse(sep[0]);
            } 
            catch
            {
                return Double.NaN;
            }
        }

        /// <summary>
        /// Saisie de formules en notation polonaise pour tester la fonction de calcul
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char reponse;
            // boucle sur la saisie de formules
            do
            {
                Console.WriteLine();
                Console.WriteLine("entrez une formule polonaise en séparant chaque partie par un espace = ");
                string laFormule = Console.ReadLine();
                // affichage du résultat
                Console.WriteLine("Résultat =  " + Polonaise(laFormule));
                reponse = saisie("Voulez-vous continuer ?", 'O', 'N');
            } while (reponse == 'O');
        }
    }
}
