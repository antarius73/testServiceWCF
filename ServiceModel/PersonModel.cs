using System;
using System.Runtime.Serialization;

[assembly: CLSCompliant(true)]
namespace ServiceModel
{
    [DataContract]
    public class PersonModel
    {
        [DataMember]
        public int BusinessEntityId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime LastDateModif { get; set; }
    }
}
