namespace RMSSystem.ViewModels
{
    public class StudentView
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public bool RemoveFromViewFlag { get; set; }

        ////added
        //public string FullName => FirstName + " " + LastName;
    }
}
