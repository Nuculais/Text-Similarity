using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Similarity
{
    class Minhash
    {           
            public delegate int Hash(int index);
            public Hash[] Funcs;

        public Minhash(int ub, int HashFuncNums)
        {
            this.HashFuncNums = HashFuncNums;

            int q = Ubits(ub);
            SkapaHashen(q);
        }

        private int HashFuncNums;

        public int NumHash
        {
            get { return HashFuncNums; }
        }

        public void SkapaHashen(int q)
        { 
            Funcs = new Hash[HashFuncNums];

            Random ran = new Random(10);
            for(int i=0; i<HashFuncNums;i++)
            {
                uint ett = 0;
                while (ett % 1 == 1 || ett <= 0)
                {
                    ett = (uint)ran.Next();
                }
                    uint två = 0;
                    int max = 1 << q;                
                    while(två <=0)
                    {
                        två = (uint)ran.Next(max);
                    }
                Funcs[i] = x => QHash(x, ett, två, q);
            }
        } 

        public int Ubits(int storlek)
        {
            int bitarna = (int)Math.Truncate(Math.Log(storlek, 2.0)) + 1;
            return bitarna;
        }

        //Universal hash
        public static uint QHash(int x, uint ett, uint två, int q)
        {
            uint UniversalHash = (ett * (uint)x + två) >> (32 - q);
            return UniversalHash;
        }

       public List<int> AllaHashen(List<uint> IDs)
        {
            uint[] minhashen = new uint[HashFuncNums];
            for(int a = 0; a < HashFuncNums;a++)
            {
                minhashen[a] = int.MaxValue;
            }
            foreach(int i in IDs)
            {
                for(int b=0;b<HashFuncNums;b++)
                {
                    uint hash = Funcs[b](i);
                    minhashen[b] = Math.Min(minhashen[b], hash);
                }               
            }
                return minhashen.ToList();
        }

        //Comparing MinHash signatures
        public double CompareSets(int HashFuncNums, List<int> s1, List<int> s2)
        {
            int identical = 0;
            int kort = HashFuncNums;
            for(int i=0;i<kort;i++)
            {
                if(s1[i] == s2[i])
                {
                    identical++;
                }
            }
            return (1.0 * identical / HashFuncNums);
        }
}


}


