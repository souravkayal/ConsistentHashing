namespace Consistent.Hoashing
{
    /// <summary>
    /// Mock Implementation of node of a distributed system
    /// </summary>
    public class Node
    {
        public UInt32 Id { get; set; }
        public string Name { get; set; }

        //Mimicing disc in node of distributed system
        public List<string> Data { get; set; }
    }
}
