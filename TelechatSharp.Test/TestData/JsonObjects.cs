namespace TelechatSharp.Test.TestData
{
    public static class JsonObjects
    {
        public static IEnumerable<object[]> JsonArrays => new[]
        {
            new object[]
            {
                new object[] { new { type = "", text = "text entity" } },
                "text entity"
            },
            new object[]
            {
                new object[] { "plain text ", new { type = "", text = "text entity" } },
                "plain text text entity"
            },
            new object[]
            {
                new object[] { new { type = "", text = "text entity" }, " plain text" },
                "text entity plain text"
            },
            new object[]
            {
                new object[] { "plain text ", new { type = "", text = "text entity" }, " plain text" },
                "plain text text entity plain text"
            },
        };
    }
}
