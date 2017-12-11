using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Similarity
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> docs = Whichdocs(Corpus);
            Similarities(docs[0], docs[1]);            
        }

        //Same quote in 6 different languages. I thought it would make for an interesting comparison.
        //English, Swedish, Icelandic, Danish, German, Dutch, in that order.
       static string[] Corpus = { "Races of fishes begin to die out one by one. The corals. The whales. I swim with a bale of sea turtles one day and fewer the next day and then one, and then nothing. The ocean begins to go acid like tea. One day, I sit on a rock and watch as the entire sky goes white. Then dark. Then cold.",
            "Arter av fisk börjar dö ut en efter en. Korallerna. Valarna. Jag simmar med en bal sköldpaddor en dag och färre nästa dag och sedan en, och sen ingenting. Havet börjar bli surt som te. En dag sitter jag på en sten och tittar när hela himlen blir vit. Sen mörk. Sen kall.",
            "Kynþáttur fiskanna byrjar að deyja eitt af öðru. Kórallar. Hvalarnir. Ég syng með báli sjávar skjaldbökur einum degi og færri daginn eftir og síðan einn, og þá ekkert. Hafið byrjar að fara sýru eins og te. Einn daginn sit ég á rokk og horfa á þegar allt himinninn fer hvítur. Þá dökk. Þá kalt.",
            "Arter af fisk begynder at dø ud en efter en. Korallerne. Hvalerne. Jeg svømmer med en bald havskildpadder en dag og færre den næste dag og derefter en, og derefter ingenting. Havet begynder at gå sur som te. En dag sidder jeg på en sten og ser, da hele himlen bliver hvid. Så mørk. Så koldt.",
            "Die Rassen der Fische beginnen eins nach dem anderen auszusterben. Die Korallen. Die Wale. Ich schwimme mit einem Ballen Meeresschildkröten einen Tag und weniger am nächsten Tag und dann einen und dann nichts. Der Ozean beginnt wie Tee zu säuren. Eines Tages sitze ich auf einem Felsen und sehe zu, wie der ganze Himmel weiß wird. Dann dunkel. Dann kalt.",
            "De rassen van vissen beginnen een voor een uit te sterven. De koralen. De walvissen. Ik zwem één dag met een baal zeeschildpadden en de volgende dag minder en dan één en dan niets. De oceaan begint zuur als thee te worden. Op een dag zit ik op een rots en kijk toe terwijl de hele lucht wit wordt. Dan donker. Dan koud."};

        public static List<string> Whichdocs(string[] corpus)
        {
            List<string> Resultat = new List<string>();

            Console.WriteLine("Welcome. Choose the first document to be compared.");
            int whichone = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Now choose the second document.");
            int whichtwo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Thank you. Now they will be shingled and similarities will be calculated.");
            Console.WriteLine("Shingle size k=3");
            try
            {
                if ((Enumerable.Range(1, 6).Contains(whichone)) && (Enumerable.Range(1, 6).Contains(whichtwo)))
                {
                    Resultat.Add(corpus[whichone-1]);
                    Resultat.Add(corpus[whichtwo-1]);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Needs to be two numbers.");
            }
            return Resultat;
            
        }

        public static void Similarities(string doc1, string doc2)
        {
            Shingling Shi = new Shingling();
            Minhash Min = new Minhash(10, 100);
            Jaccard Jac = new Jaccard();

            //Shingle both documents, hash the shingles
            List<string> shinglat1 = Shi.Tokenize(doc1);
            List<string> shinglat2 = Shi.Tokenize(doc2);
            List<int> hashshinglat1 = Shi.ShingleHash(shinglat1);
            List<int> hashshinglat2 = Shi.ShingleHash(shinglat2);

            //MinHash the shingled documents
            List<int> minhashshinglat1 = Min.AllaHashen(hashshinglat1);
            List<int> minhashshinglat2 = Min.AllaHashen(hashshinglat2);

            //Compute MinHash signature similarity
            double SigSim = Min.CompareSets(100, minhashshinglat1, minhashshinglat2);

            //Compute Jaccard similarity
            double JacSim = Jac.JaccardSim(hashshinglat1, hashshinglat2);

            Console.WriteLine("MinHash signature similarity is: " + SigSim);
            Console.WriteLine("Jaccard similarity is: " + JacSim);

            double threshold = 0.85;
            if(JacSim >= threshold)
            {
                Console.WriteLine("The two documents are similar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("The two documents are not very alike.");
                Console.ReadKey();
            }
        }

    }
}
