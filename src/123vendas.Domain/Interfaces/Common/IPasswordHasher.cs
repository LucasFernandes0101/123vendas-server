namespace _123vendas.Domain.Interfaces.Common;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string hash);
}