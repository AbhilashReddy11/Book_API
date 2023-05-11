using AutoMapper;
using Book_API.Data;
using Book_API.Models;
using Book_API.Models.DTO;
using Book_API.Repository.IRepository;



namespace Book_API.Repository
{
    public class UserRepository : Repository<UserDTO>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

      

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower()
            && u.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return null;
            }
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO();
            return loginResponseDTO;


        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            LocalUser user = new()
            {
                UserName = registrationRequestDTO.UserName,
                Password = registrationRequestDTO.Password,
                Email = registrationRequestDTO.Email,
                Role = registrationRequestDTO.Role
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
        public async Task<LocalUser> Edit(LocalUser entity)
        {
            _db.LocalUsers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

    

