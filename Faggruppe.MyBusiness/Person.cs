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

        public Adresse hentAdresse()
        {
            return adresseService.HentAdresse(Id);
        }
    }

    public class AdresseService : IAdresseService
    {
        public Adresse HentAdresse(long personId)
        {
            return new Adresse();
        }
    }

    public interface IAdresseService
    {
        Adresse HentAdresse(long personId);
    }

    public class Adresse
    {
        public string Gate { get; set; }
        public string Postnummer { get; set; }
    }
}
