namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ITurnRepository Turn { get; }
        IStudentRepository Student { get; }
        IMachineRepository Machine { get; }
        IAdminRepository Admin { get; }
    }
}
