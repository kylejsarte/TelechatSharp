using TelechatSharp.Core.Models;

namespace TelechatSharp.Test.Models
{
    public class MemberTests
    {
        [Theory]
        [MemberData(nameof(MembersForEquals))]
        public void Equals_ReturnsExpectedBool(Member member, object obj, bool expected)
        {
            Assert.Equal(expected, member.Equals(obj));
        }

        [Theory]
        [MemberData(nameof(MembersForGetHashCode))]
        public void GetHashCode_Comparison(Member memberOne, Member memberTwo, bool expected)
        {
            var hashCodeOne = memberOne.GetHashCode();
            var hashCodeTwo = memberTwo.GetHashCode();

            Assert.Equal(expected, hashCodeOne == hashCodeTwo);
        }

        public static IEnumerable<object[]> MembersForEquals => new List<object[]>
        {
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new Member { Id = "1", Name = "Paul Atreides" }, true },
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new Member { Id = "2", Name = "Paul Atreides" }, false },
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new Member { Id = "1", Name = "Chani Kynes" }, false },
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new { Id = "1", Name = "Paul Atreides" }, false },
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, null, false },
        };

        public static IEnumerable<object[]> MembersForGetHashCode => new List<object[]>
        {
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new Member { Id = "1", Name = "Paul Atreides" }, true },
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new Member { Id = "2", Name = "Chani Kynes" }, false },
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new Member { Id = "1", Name = "Chani Kynes" }, false },
            new object[] { new Member { Id = "1", Name = "Paul Atreides" }, new Member { Id = "2", Name = "Paul Atreides" }, false }
        };
    }
}
