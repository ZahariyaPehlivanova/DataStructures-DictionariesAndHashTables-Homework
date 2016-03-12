namespace _02.CountSymobls
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();
            var hashTable = new HashTable<char, int>();

            foreach (var ch in input)
            {
                if (!hashTable.ContainsKey(ch))
                {
                    hashTable.Add(ch, 0);
                }

                hashTable[ch]++;
            }

            var ordered = hashTable.OrderBy(x => x.Key);
            foreach (var word in ordered)
            {
                Console.WriteLine("{0}: {1} time/s", word.Key, word.Value);
            }
        }
    }
}
