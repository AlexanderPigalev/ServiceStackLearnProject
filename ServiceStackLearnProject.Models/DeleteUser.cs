using ServiceStack;

namespace ServiceStackLearnProject.Models
{
    [Route("/deleteuser", "POST")]
    public class DeleteUser : IReturn<DeletedUserResponse>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }

    public class DeletedUserResponse
    {
        public string Response { get; set; }
    }
}