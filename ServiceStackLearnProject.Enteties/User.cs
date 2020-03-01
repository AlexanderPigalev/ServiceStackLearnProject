using ServiceStack.DataAnnotations;

namespace ServiceStackLearnProject.Enteties
{
    [Alias("Users")]
    public class User
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string PhotoPath { get; set; }
    }
}