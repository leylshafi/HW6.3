using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace HW6._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string current = System.IO.Path.GetFullPath(@"..\..\..\");
            List<string> list = new List<string>();
            string result = JsonConvert.SerializeObject(list);
            using (StreamWriter sw = new StreamWriter(Path.Combine(current, "names.json")))
            {
                sw.WriteLine(result);
            }

            Add("Leyla");
            Add("Adil");
            Add("Nigar");
            RemoveBook("Nigar");
            Add("Ruba");
            Console.WriteLine(Search(name => name == "Ad"));
            
        }

        public static List<string> ReadFromDb()
        {
            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "names.json");
            List<string> names;
            using (StreamReader sr = new StreamReader(path))
            {
                string result = sr.ReadToEnd();
                if (result == "") return new List<string>();
                try
                {
                    names = JsonConvert.DeserializeObject<List<string>>(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There is no names in db yet");
                    names = new List<string>();
                }
            }

            return names;
        }

        public static void WriteToDb(List<string> names)
        {
            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "names.json");
            string result = JsonConvert.SerializeObject(names);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(result);
            }
        }
        public static void Add(string name)
        {
            List<string> names = ReadFromDb();
            names.Add(name);
            WriteToDb(names);
        }

        public static bool Search(Predicate<string> predicate)
        {
            List<string> names = ReadFromDb();
            if (names.Any(predicate.Invoke))
                return true;
            else return false;
        }
        public static void RemoveBook(string name)
        {
            List<string> names = ReadFromDb();
            if (!names.Any(b => b==name))
                throw new Exception("There is no name like this");

            names.RemoveAll(b => b == name);
            WriteToDb(names);
        }
    }
}