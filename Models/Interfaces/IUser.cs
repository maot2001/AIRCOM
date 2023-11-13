namespace AIRCOM.Models.Interfaces
{
    public interface IUser
    {
        string? Email { get; set; }
        string? Pwd { get; set; }
        string? Type { get; set; }
    }
}
