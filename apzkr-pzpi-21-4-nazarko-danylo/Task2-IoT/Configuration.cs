public class Configuration
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Manufacturer
    {
        get
        {
            var companies = new string[] { "Samsung", "LG", "Siemens" };
            Random random = new Random();
            return companies[random.Next(companies.Length)];
        }
    }

    public string SerialNumber
    {
        get
        {
            return Guid.NewGuid().ToString().Split('-')[0].ToUpper();
        }
    }

    public string Host { get; set; } = "bifromq.cuqmbr.home";

    public int Port { get; set; } = 1883;
}
