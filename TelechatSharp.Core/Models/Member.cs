namespace TelechatSharp.Core.Models
{
    public class Member
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            Member memberToCompare = (Member)obj;

            return string.Equals(Id, memberToCompare.Id) && string.Equals(Name, memberToCompare.Name);
        }

        public override int GetHashCode()
        {
            return (Id, Name).GetHashCode();
        }
    }
}
