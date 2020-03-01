using ServiceStack;

namespace ServiceStackLearnProject.Models
{
    [Route("/addnewuser", "POST")]
    public class CreateUser : IReturn<CreatedUserResponse>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }

    public class CreatedUserResponse
    {
        public string Response { get; set; }
    }
}