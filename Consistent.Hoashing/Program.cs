using Consistent.Hoashing;
using System.Security.Cryptography;

namespace ConsistentHashingApp
{

    /// <summary>
    /// Implementation of consistent hashing
    /// </summary>
    public class ConsistentHashing
    {
        public SortedDictionary<UInt32, Node> circle = new SortedDictionary<UInt32, Node>();
        
        public UInt16 VirtialReplica { get; set; }
        
        public ConsistentHashing(List<string> nodes , int numberOfReplica) 
        {
            //create all nodes with replica count
            foreach (var item in nodes)
            {
                for (int replicaIndex = 0; replicaIndex < numberOfReplica; replicaIndex++)
                {
                    var nodeHash = GetHash($"{item}-{replicaIndex}");
                    circle.Add(nodeHash, new Node { Id = nodeHash, Name = $"Node-{replicaIndex}{nodeHash}" });
                }
            }
        }

        /// <summary>
        /// Function to return node in which the information suppose to store
        /// </summary>
        /// <param name="data"></param>
        public Node GetNode(string data)
        {
            if (string.IsNullOrEmpty(data)) throw new Exception("Data cannot be null");

            if (circle.Count == 0) throw new Exception("No node to save data");

            var hash = GetHash(data);

            foreach (var node in circle)
            {
                if (node.Key > hash)
                    return node.Value;
            }

            return circle.First().Value;
        }


        /// <summary>
        /// Function to generate hash of input string
        /// </summary>
        /// <param name="value">input value</param>
        /// <returns>UInt32 value of hash string</returns>
        public UInt32 GetHash(string value)
        {
            using (var sha1 = SHA1.Create())
            {
                return BitConverter.ToUInt32(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)) , 0) % 100;
            }

        }

    }

    /// <summary>
    /// program to identify node in which data will store using consistent hashing
    /// </summary>
    public class Program
    {
        public static void Main(String[] args)
        {
    
            //create node of consistent hashing
            ConsistentHashing consistentHashing = new ConsistentHashing(new List<string> { "Node-A", "Node-B" , "Node-C" }, 3);

            for (int i = 0; i < 100; i++)
            {
                string data = $"data:{i}:{DateTime.Now.ToLongTimeString()}";
                var node = consistentHashing.GetNode(data);
                
                Console.WriteLine($"Hash value:{consistentHashing.GetHash(data)}    Stores: {node.Name}");

            }

            Console.ReadLine();
        }
    }
}