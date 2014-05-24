using System;
using System.Numerics;

namespace DirtyPGP
{
	public static class RASHelper
	{
		private static Random random = new Random();

		public static bool IsPrime(long number)
		{
			long i;
			for (i = 2; i <= number - 1; i++)
			{
				if (number % i == 0)
				{
					return false;
				}
			}
			if (i == number)
			{
				return true;
			}
			return false;
		}

		public static long GetPrime()
		{
			long start = random.Next(10000) * 2 + 1001;

			while (!IsPrime(start))
			{
				start -= 2;
			}

			return start;
		}

		public static long GetMultInv(long _e, long _m)
		{
			BigInteger d = new BigInteger(1);

			BigInteger e = new BigInteger(_e);
			BigInteger m = new BigInteger(_m);

			while ((e * d) % m != 1)
			{
				d++;
			}

			return (long)d;
		}

		public static long GGT(long a, long b)
		{
			long tmp = 0;
			while (b != 0)
			{
				tmp = a % b;
				a = b;
				b = tmp;
			}
			return a;
		}

		public static long LongRandom(long min, long max)
		{
			byte[] buf = new byte[8];
			random.NextBytes(buf);
			long longRand = BitConverter.ToInt64(buf, 0);

			return (Math.Abs(longRand % (max - min)) + min);
		}

		public static long GetRandomCoprime(long m)
		{
			long v = LongRandom(2, m);

			while (GGT(v, m) != 1)
			{
				v++;
				if (v == m)
					v = 2;
			}

			return v;
		}
	}
}
