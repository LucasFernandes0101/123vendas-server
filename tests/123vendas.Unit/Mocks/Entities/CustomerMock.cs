using _123vendas.Domain.Entities;
using Bogus;

namespace _123vendas.Tests.Mocks.Entities;

public class CustomerMock : Faker<Customer>
{
    public CustomerMock()
    {
        RuleFor(c => c.Id, f => f.Random.Short())
        .RuleFor(c => c.Name, f => f.Name.FullName())
        .RuleFor(c => c.Document, f => f.Random.Short(12, 13).ToString())
        .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Address, f => f.Address.FullAddress())
        .RuleFor(c => c.IsActive, f => f.Random.Bool())
        .RuleFor(c => c.CreatedAt, f => f.Date.Past(1))
        .RuleFor(c => c.UpdatedAt, f => f.Date.Recent(0));
    }
}