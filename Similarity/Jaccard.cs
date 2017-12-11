using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Similarity
{
    class Jaccard
    {
        public double JaccardSim(List<int> s1, List<int> s2)
        {
            int intersects = s1.Intersect(s2).Count();
            int unions = s1.Union(s2).Count();

            double HowSimilar = 1.0 * intersects / unions;
            return HowSimilar;
        }
    }
}
