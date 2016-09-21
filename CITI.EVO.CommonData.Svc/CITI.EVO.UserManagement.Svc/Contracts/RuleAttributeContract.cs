using System;
using System.Runtime.Serialization;

namespace CITI.EVO.UserManagement.Svc.Contracts
{
    [Serializable]
    [DataContract]
    public class RuleAttributeContract
    {
        public RuleAttributeContract()
        {
        }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public Guid RuleID { get; set; }

        [DataMember]
        public Guid AttributeSchemaNodeID { get; set; }

        [DataMember]
        public String Value { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime? DateChanged { get; set; }

        [DataMember]
        public DateTime? DateDeleted { get; set; }
    }
}