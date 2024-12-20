namespace BitmailServer.Models
{
    public class EmailDto
    {
        /// <summary>
        /// The email address of the user sending the email
        /// </summary>
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        /// <summary>
        /// The email address of the receiver
        /// </summary>
        public List<EmailAndName> ToEmail { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
