namespace PersonManager.ConApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test PersonManager!");

            var personRepo = new Logic.Repositories.PersonalRepository();

            for (int i = 0; i < 10; i++)
            {
                var person = personRepo.Create();

                person.Firstname = $"Vorname{i + 1}";
                person.Lastname = $"Nachname{i + 1}";

                personRepo.Add(person);
            }
            personRepo.Save();
        }
    }
}