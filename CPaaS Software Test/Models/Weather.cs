namespace CPaaS_Software_Test;

public class Weather
{
    public string ResolvedAddress { get; set; }
    public string Temperature { get; set; }

    public Weather(string ResolvedAddress, string Temperature)
    {
        this.ResolvedAddress = ResolvedAddress;
        this.Temperature = Temperature;
    }

    public override string ToString()
    {
        return "Temperature in " + ResolvedAddress + " is " + Temperature + " degrees celsius";
    }
}