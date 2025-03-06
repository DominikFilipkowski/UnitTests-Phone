using ClassLibrary;
using System.ComponentModel.DataAnnotations;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Konstruktor_Dane_Poprawne_Ok()
        {
            // AAA

            // Arrange
            var wlasciciel = "Dominik";
            var numerTelefonu = "123456789";
            // ACT
            var phone = new Phone(wlasciciel, numerTelefonu);
            // Assert
            Assert.AreEqual(wlasciciel, phone.Owner);
            Assert.AreEqual(numerTelefonu, phone.PhoneNumber);
        }
    }
}