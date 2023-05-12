using Book_API.Models;
using Book_API.Models.DTO;

namespace Book_API.Repository.IRepository
{
    public interface IUserRepository : IRepository<LocalUser>
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
         Task<LocalUser>UpdateAsync(LocalUser entity);
        

    }
}
