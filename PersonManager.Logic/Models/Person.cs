
namespace PersonManager.Logic.Models
{
    public sealed class Person : ModelObject, ICloneable
    {
        public Person()
        {
        }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public string FullName => $"{Lastname} {Firstname}";

        object ICloneable.Clone()
        {
            return new Person
            {
                Id = Id,
                Firstname = Firstname,
                Lastname = Lastname,
            };
        }
    }
}
