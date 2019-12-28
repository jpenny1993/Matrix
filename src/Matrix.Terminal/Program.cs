using System;
using System.Threading;

namespace Matrix.Terminal
{
    class Program
    {
		private const int CharacterRange = 30;
		private const char DashCharacter = '-';
		private const char FirstPrintableCharacter = '!';
		private const char WhiteSpaceCharacter = ' ';

		private static char GetChar(int seed) => (char)(FirstPrintableCharacter + (seed % CharacterRange));

		static void Main(string[] args)
        {
			Console.Title = "Terminal";
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Green;

			int rowSize = Console.WindowWidth; // 120 by default

			// prevent scrollbar by setting the buffersize to the window size
			Console.SetBufferSize(rowSize, Console.WindowHeight);

			var rng = new Random();
			var row = new char[rowSize];

			// indices of whitespace
			int j = 7;
			int k = 2;
			int l = 5;
			int m = 1;

			int i; // index of current character
			int s; // index of special character
			while (true)
			{
				// Generate the location of the special character
				s = rng.Next(0, rowSize);

				// output a random row of characters
				for (i = 0; i < rowSize; ++i)
				{
					// colour special characters white
					var isSpecial = i == s;
					if (isSpecial) {
						Console.ForegroundColor = ConsoleColor.White;
					}

					// generate text for when cell is not whitespace
					if (row[i] != WhiteSpaceCharacter)
					{
						row[i] = GetChar(j + i * i);
					}

					// write the character
					Console.Write(row[i]);

					if (isSpecial)
					{
						Console.ForegroundColor = ConsoleColor.Green;
					}
				}

				// update indices of whitespace
				j += 31;
				k += 17;
				l += 47;
				m += 67;

				// affect the next row by adding and removing whitespace
				row[j % rowSize] = DashCharacter;
				row[k % rowSize] = WhiteSpaceCharacter;
				row[l % rowSize] = DashCharacter;
				row[m % rowSize] = WhiteSpaceCharacter;

				// add some delay
				Thread.Sleep(25);
			}
		}
    }
}
