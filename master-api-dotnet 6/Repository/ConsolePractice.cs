using master_api_dotnet_6.IRepository;
using System.Collections;

namespace master_api_dotnet_6.Repository
{
    public class ConsolePractice : IConsolePractice
    {
        public async Task<object> CollectionsInCSharp()
        {
            //Diffrence between array and Array List
            //1.array is strongly typed.(an array can store only specific type of elements)
            //2.array can contain fixed number of elements. here 10 elements can be stored
            //int[] array;
            //array = new int[10];
            //array[0] = 1;
            //array[1] = "Happy";

            //1.arraylist can store any type of elements
            //2.arraylist can store any number of elements
            ArrayList arrayList = new ArrayList();
            arrayList.Add(1);
            arrayList.Add("Happy");
            arrayList.Add(0.5);
            //return arrayList;

            //HashTable
            //Hashtable hashTable = new Hashtable();
            //hashTable.Add(1, "Akash");
            //hashTable.Add("TL", "Akash");
            //return hashTable;

            //List
            //List<string> list = new List<string>();
            //list.Add("ABC");
            //list.Add("Akash");
            //list.Add("TL");
            //return list;

            //Dictionary
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            dictionary.Add(1, "Md.");
            dictionary.Add(2, "Saidur");
            dictionary.Add(3, "Rahman");
            dictionary.Add(4, "Akash");

            foreach (KeyValuePair<int, string> kvp in dictionary)
            {
                Console.WriteLine($"{kvp.Key} {kvp.Value}");
            }
            return dictionary;
        }
    }


}
