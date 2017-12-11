using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Similarity
{
    class Shingling
    {
        private const int size = 3;
        private const int overlap = 1;

        public List<string> Tokenize(string doc, int shiSize = size, int shiOverlap = overlap)
        {
            var Resultat = new List<string>();
          
                int loops = doc.Length - shiSize;
                for (int i = 0; i < loops; i = i + shiSize - shiOverlap) Resultat.Add(doc.Substring(i, shiSize));
           
            return Resultat;
        }


        public List<int> ShingleHash(List<string> shingles)
        {
            List<int> Hashed = new List<int>();
            
            for(int j=0;j<shingles.Count();j++)
            {
                Hashed.Add(j);
                j++;
            }

            Hashed.Sort();

            return Hashed;
        }

    }
}
