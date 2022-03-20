using System;

namespace diffie_hellman
{

    class Program
    {

        class Alice
        {
            public int K;
            private int a, g, p, A;

            public Alice(int a, int g, int p)
            {
                this.a = a;
                this.g = g;
                this.p = p;
                this.A = calc(this.g, this.a, this.p);
            }
            public int[] returnPacket()
            {
                return new int[] { this.g, this.p, this.A };
            }

            public void calcFinal(int B)
            {
                this.K = calc(B, this.a, this.p);
            }
        }

        class Bob
        {
            public int K;
            private int b;
            public Bob(int b)
            {
                this.b = b;
            }

            public int calcB(int[] param)
            {
                int g = param[0], p = param[1], A = param[2];
                int B = calc(g, this.b, p);
                this.K = calc(A, this.b, p);
                return B;
            }
        }
        public static int calc(int inp, int exponent, int modulus) {
            int d = 1, t = inp;
            string bin = Convert.ToString(exponent, 2);
            for (int i = bin.Length-1; i >= 0; i--)
            {
                if (bin[i] == '1')
                {
                    d = d * t % modulus;
                }
                t = t * t % modulus;
            }
            return d;
        }

        static void Main(string[] args)
        {
            int a = 6, g = 5, p = 23, b = 15;
            Alice test_Alice = new Alice(a, g, p);
            Console.WriteLine(String.Format("Created first user with a = {0}, g = {1} and p = {2}", a, g, p));
            Bob test_Bob = new Bob(b);
            Console.WriteLine(String.Format("Created second user with b = {0}", b));
            int[] param = test_Alice.returnPacket();
            Console.WriteLine(String.Format("Recieved package from first user with g = {0}, p = {1} and A = {2}", param[0], param[1], param[2]));
            int B = test_Bob.calcB(param);
            test_Alice.calcFinal(B);
            Console.WriteLine(String.Format("Alice's final key is {0}, while Bob's is {1}. The keys are {2}", test_Alice.K, test_Bob.K, test_Alice.K == test_Bob.K ? "equal" : "different"));
        }
    }
}
