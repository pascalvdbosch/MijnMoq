using System.Dynamic;
using ImpromptuInterface;
using Dynamitey;
using Xunit;

public interface IInlogSysteem
{
    public string IngelogdeGebruiker();
    public void Login(string gebruiker);
}

class InlogSysteem : IInlogSysteem
{
    public string IngelogdeGebruiker()
    {
        // Zogenaamd wordt hier de data opgehaald uit de database...
        // ...
        // ...
        // ...
        return "Bob";
    }
    public void Login(string gebruiker)
    {
        // Zogenaamd wordt hier data in de database gezet...
        // ...
        // ...
        // ...
    }
}

class ProfielPagina
{
    private IInlogSysteem inlogSysteem;
    public ProfielPagina(IInlogSysteem inlogSysteem)
    {
        this.inlogSysteem = inlogSysteem;
    }
    public string Tekst()
    {
        return "Dit is de profielpagina van " + inlogSysteem.IngelogdeGebruiker() + ". Klik hier om verder te gaan. ";
    }
}

class MijnMoq<T> where T : class
{
    private IDictionary<string, Object> obj = new ExpandoObject();
    public void Setup<S>(string Name, Func<S> f)
    {
        obj.Add(Name, Return<S>.Arguments(f));
    }
    public T Object
    {
        get
        {
            return obj.ActLike<T>();
        }
    }
}

namespace MyApp
{
    public class Program
    {
        static void Main(string[] args) { }

        [Fact]
        void TestProfielPagina()
        {
            // --- Arrange ---
            // Maak de mock aan
            MijnMoq<IInlogSysteem> mock = new MijnMoq<IInlogSysteem>();

            // Configureer de mock om iets te doen
            mock.Setup("IngelogdeGebruiker", () => "Bob");

            // Maak de mock aan
            IInlogSysteem obj = mock.Object;

            // Maak het te testen object aan
            ProfielPagina sut = new ProfielPagina(obj);

            // --- Act ---
            string resultaat = sut.Tekst();

            // --- Assert ---
            Assert.Contains("Bob", resultaat);
        }
    }
}