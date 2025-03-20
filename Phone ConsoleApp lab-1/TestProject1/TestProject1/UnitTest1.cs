using ClassLibrary;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace TestPhone
{
    // Testy tworzenia telefonu
    [TestClass]
    public class TestTworzeniaTelefonu
    {
        // Test konstruktora - weryfikacja poprawnych danych
        [TestMethod]
        public void Test_Konstruktor_PrawidloweDane()
        {
            // Ustawienie danych (Arrange)
            string wlasciciel = "Dominik Filipkowski";
            string nrTelefonu = "535126393";

            // Tworzenie obiektu (Act)
            Phone telefon = new Phone(wlasciciel, nrTelefonu);

            // Sprawdzenie rezultatów (Assert)
            Assert.AreEqual(wlasciciel, telefon.Owner, "Nazwa właściciela nie jest prawidłowa");
            Assert.AreEqual(nrTelefonu, telefon.PhoneNumber, "Numer telefonu nie jest prawidłowy");
        }

        // Test, gdy właściciel jest pusty
        [TestMethod]
        public void Test_PustyWlasciciel()
        {
            string wlasciciel = "";
            string nrTelefonu = "535126393";

            try
            {
                Phone tel = new Phone(wlasciciel, nrTelefonu);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(
                    e.ToString().Contains("Owner name is empty or null!"),
                    "Błąd: nie pojawił się właściwy wyjątek dla pustego właściciela."
                );
            }
        }

        // Test, gdy numer telefonu jest pusty
        [TestMethod]
        public void Test_PustyNumerTelefonu()
        {
            string wlasciciel = "Dominik Filipkowski";
            string nrTelefonu = "";

            try
            {
                Phone tel = new Phone(wlasciciel, nrTelefonu);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(
                    e.ToString().Contains("Phone number is empty or null!"),
                    "Błąd: nie pojawił się właściwy wyjątek dla pustego numeru telefonu."
                );
            }
        }

        // Test, gdy numer telefonu zawiera litery
        [TestMethod]
        public void Test_NumerTelefonuZLiterami()
        {
            string wlasciciel = "Dominik Filipkowski";
            string nrTelefonu = "abcxyz123";

            try
            {
                Phone tel = new Phone(wlasciciel, nrTelefonu);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(
                    e.ToString().Contains("Invalid phone number!"),
                    "Błąd: nie pojawił się właściwy wyjątek dla numeru telefonu z literami."
                );
            }
        }

        // Test, gdy numer telefonu jest za długi
        [TestMethod]
        public void Test_NumerTelefonuZaDlugi()
        {
            string wlasciciel = "Dominik Filipkowski";
            string nrTelefonu = "1234567890"; // 10 znaków

            try
            {
                Phone tel = new Phone(wlasciciel, nrTelefonu);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(
                    e.ToString().Contains("Invalid phone number!"),
                    "Błąd: nie pojawił się właściwy wyjątek dla za długiego numeru telefonu."
                );
            }
        }
    }

    // Testy książki telefonicznej
    [TestClass]
    public class TestKsiazkiTelefonicznej
    {
        // Dodanie poprawnych danych
        [TestMethod]
        public void Test_DodaniaPoprawnychDanych()
        {
            Phone tel1 = new Phone("Dominik Filipkowski", "535126393");
            Phone tel2 = new Phone("Kowalski", "123456789");
            Phone tel3 = new Phone("Nowak", "987654321");

            tel1.AddContact(tel2.Owner, tel2.PhoneNumber);
            tel1.AddContact(tel3.Owner, tel3.PhoneNumber);

            int oczekiwanaLiczba = 2;

            Assert.AreEqual(
                oczekiwanaLiczba,
                tel1.Count,
                "Liczba dodanych kontaktów jest nieprawidłowa."
            );
        }

        // Dodanie kontaktów z tym samym imieniem
        [TestMethod]
        public void Test_DodaniaTychSamychImion()
        {
            Phone tel1 = new Phone("Dominik Filipkowski", "535126393");
            Phone tel2 = new Phone("Kowalski", "111111111");
            Phone tel3 = new Phone("Kowalski", "222222222");

            tel1.AddContact(tel2.Owner, tel2.PhoneNumber);
            tel1.AddContact(tel3.Owner, tel3.PhoneNumber);

            int oczekiwanaLiczba = 1;

            Assert.AreEqual(
                oczekiwanaLiczba,
                tel1.Count,
                "Nieprawidłowa liczba kontaktów przy dodawaniu osób o tym samym imieniu."
            );
        }

        // Dodanie kontaktów z tym samym numerem
        [TestMethod]
        public void Test_DodaniaTychSamychNumerow()
        {
            Phone tel1 = new Phone("Dominik Filipkowski", "535126393");
            Phone tel2 = new Phone("Kowalski", "234567891");
            Phone tel3 = new Phone("Nowak", "234567891");

            tel1.AddContact(tel2.Owner, tel2.PhoneNumber);
            tel1.AddContact(tel3.Owner, tel3.PhoneNumber);

            int oczekiwanaLiczba = 2;

            Assert.AreEqual(
                oczekiwanaLiczba,
                tel1.Count,
                "Nieprawidłowa liczba kontaktów przy dodawaniu tych samych numerów telefonu."
            );
        }

        // Sprawdzenie przekroczenia limitu książki telefonicznej
        [TestMethod]
        public void Test_LimitKsiazkiTelefonicznej()
        {
            Phone tel1 = new Phone("Dominik Filipkowski", "535126393");
            string nrKontaktu = "999888777";

            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    tel1.AddContact($"Osoba_{i}", nrKontaktu);
                }
                int oczekiwana
