namespace StudentAPI.Repository.Interface
{
    public interface ITokenService
    {
        string GenerateToken(string userName, string role);
    }
}
