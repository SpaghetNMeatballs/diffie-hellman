using System;

namespace diffie_hellman
{

    class Program
    {

        class Alice
        {
            public long K;
            private long a, g, p, A;

            public Alice(long a, long g, long p)
            {
                this.a = a;
                this.g = g;
                this.p = p;
                this.A = calc(this.g, this.a) % this.p;
            }
            public long[] returnPacket()
            {
                return new long[] { this.g, this.p, this.A };
            }

            public void calcFinal(long B)
            {
                this.K = calc(B, this.a) % this.p;
            }
        }

        class Bob
        {
            public long K;
            private long b;
            public Bob(long b)
            {
                this.b = b;
            }

            public long calcB(long[] param)
            {
                long g = param[0], p = param[1], A = param[2];
                long B = calc(g, this.b) % p;
                this.K = calc(A, this.b) % p;
                return B;
            }
        }
        static long calc(long a, long b)
        {
            long output = 1;
            for (int i = 0; i < b; i++)
            {
                output *= a;
            }
            return output;
        }

        static void Main(string[] args)
        {
            long a = 6, g = 5, p = 23, b = 15;
            Alice test_Alice = new Alice(a, g, p);
            Console.WriteLine(String.Format("Created first user with a = {0}, g = {1} and p = {2}", a, g, p));
            Bob test_Bob = new Bob(b);
            Console.WriteLine(String.Format("Created second user with b = {0}", b));
            long[] param = test_Alice.returnPacket();
            Console.WriteLine(String.Format("Recieved package from first user with g = {0}, p = {1} and A = {2}", param[0], param[1], param[2]));
            long B = test_Bob.calcB(param);
            test_Alice.calcFinal(B);
            Console.WriteLine(String.Format("Alice's final key is {0}, while Bob's is {1}. The keys are {2}", test_Alice.K, test_Bob.K, test_Alice.K == test_Bob.K ? "equal" : "different"));
        }
    }
}
