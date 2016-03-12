namespace _03.Phonebook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var hashTable = new HashTable<string, string>();
            string command = Console.ReadLine();

            while (command != "search")
            {
                string[] nameNumber = command.Split('-');
                string name = nameNumber[0];
                string number = nameNumber[1];
                if (!hashTable.ContainsKey(name))
                {
                    hashTable.AddOrReplace(name, number);
                }
               
                command = Console.ReadLine();
            }

            var searchedNames = new List<string>();
            string searchedName = Console.ReadLine();
            while (!string.IsNullOrEmpty(searchedName))
            {
                searchedNames.Add(searchedName);
                searchedName = Console.ReadLine();
            }

            foreach (var phone in hashTable)
            {
                if (searchedNames.Any(x => x == phone.Key))
                {
                    Console.WriteLine("{0} -> {1}", phone.Key, phone.Value);
                }
                else
                {
                    Console.WriteLine("Contact {0} does not exist.", phone.Key);
                }
            }
        }
    }
}
