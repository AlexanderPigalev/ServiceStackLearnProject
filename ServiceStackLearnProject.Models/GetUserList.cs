using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;

namespace ServiceStackLearnProject.Models
{
    [Route("/getusers")]
    public class GetUserList : IReturn<GetUserListResponse>
    {

    }
    [DataContract]
    public class GetUserListResponse
    {
        [DataMember]
        public IEnumerable<CreateUser> Users { get; set; }
    }
}