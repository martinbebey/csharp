using System;
using BinarySearchTree;

namespace SpecializationLog
{
    public class BinaryTree
    {

        public static void Main()
        {
            string name, command;
            Console.Write("Press [I]nsert new specialization names and [D]one when you're done: ");
            command = Console.ReadLine().ToUpper();
            while (command != "I" && command != "D")
            {
                Console.Write("Invalid input ! Press [I]nsert new specialization names and [D]one when you're done: ");
                command = Console.ReadLine().ToUpper();
            }
           

            switch (command)
            {
                case "I":
                    Specialization spec = new Specialization();
                    Console.Write("Enter the name of the specialization: ");
                    name = Console.ReadLine().ToLower();
                    spec.Insert(name);

                    break;

                case "D":


                    break;                    
            }

        }

    }

    public class Specialization
    {
        private BinarySearchTree<string> spec;
        public void Insert(string name)
        {
            if (!spec.Contains(name))
            {
                spec.Add(name);
            }

            else
                throw new Exception("A specialization called " + name + " already exists");
        }
    }
}

