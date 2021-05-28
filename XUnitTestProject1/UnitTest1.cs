using System;
using Xunit;
using Bibliotek;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestHentLaaner()
        {
            // Arrange
            Laaner test = new Laaner(1, "Test");
            string expected = "Navn: Ikke Angivet \tLåner: 1 \tBibliotek: Test \tEmail: Ikke Angivet \nLånte bøger: \t\tUdlånsdato:\n";

            // Act
            string actual = test.HentLaaner();

            // Assert
            Assert.Equal(expected, actual);
        }

        public void TestCollection()
        {
            List<Laaner> collTest =  
        }
    }
}
