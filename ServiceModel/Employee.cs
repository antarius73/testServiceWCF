using System.Runtime.Serialization;


namespace ServiceModel
{
    [DataContract]
    public class Employee 
    {
        [DataMember]
        public int BusinessEntityID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}
