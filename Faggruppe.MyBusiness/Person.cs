namespace Faggruppe.MyBusiness
{
    public class Person
    {
        private readonly AdresseService adresseService;

        public Person()
        {
            this.adresseService = new AdresseService();
        }

        public long Id { get; set; }

        public Adresse HentAdresse()
        {
            return adresseService.HentAdresse(Id);
        }
    }

    public class AdresseService
    {
        public Adresse HentAdresse(long personId)
        {
            return new Adresse();
        }
    }

    public class Adresse
    {
        public string Gate { get; set; }
        public string Postnummer { get; set; }
    }
}
