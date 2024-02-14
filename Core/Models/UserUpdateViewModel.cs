namespace Core.Models
{
    public class UserUpdateViewModel
    {
        public string WriterUserName { get; set; }

        public string WriterNameSurname { get; set; }

        public string WriterAbout { get; set; }

        public string WriterMail { get; set; }

        public virtual string WriterPassword { get; set; }
    }
}
