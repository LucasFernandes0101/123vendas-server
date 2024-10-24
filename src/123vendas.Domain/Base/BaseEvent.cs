namespace _123vendas.Domain.Base;

public class BaseEvent
{
    public BaseEvent(string domain)
    {
        Domain = domain;
    }

    public string Domain { get; set; }
}