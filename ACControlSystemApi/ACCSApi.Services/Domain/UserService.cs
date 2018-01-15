using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passHashService;

        public UserService(IUserRepository userRepository, IPasswordHashingService passHashService)
        {
            _userRepository = userRepository;
            _passHashService = passHashService;
        }

        public int AddUser(IUserRegister userData)
        {
            if (userData == null)
                throw new ArgumentNullException();
            if (_userRepository.Find(x => x.EmailAddress.Equals(userData.AuthenticationData.EmailAddress)).Any())
                throw new ItemAlreadyExistsException("User with given email address is already registered!");

            try
            {
                new System.Net.Mail.MailAddress(userData.AuthenticationData.EmailAddress);
            }
            catch(Exception)
            {
                throw new ArgumentException("Invalid email address!");
            }

            IUser user = new User()
            {
                Id = 0,
                EmailAddress = userData.AuthenticationData.EmailAddress,
                PasswordHash = _passHashService.CreateHash(userData.AuthenticationData.Password),
                RegistrationTimestamp = DateTime.Now
            };

            _userRepository.Add(user);
            var userId = _userRepository.Find(x => x.EmailAddress.Equals(user.EmailAddress)).Single().Id;

            return userId;
        }

        public IEnumerable<IUserPublic> GetAllUsers()
        {
            return _userRepository.GetAll().Select(user => user.PublicData);
        }

        public IUser FindUser(string email)
        {
            var user = _userRepository.Find(x => x.EmailAddress.Equals(email)).SingleOrDefault();
            return user;
        }

        public IUser GetUser(int id)
        {
            var user = _userRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();

            return user ?? throw new ItemNotFoundException();
        }

        public void RemoveUser(int id)
        {
            var user = _userRepository.Find(x => x.Id.Equals(id)).SingleOrDefault();

            if (user == null)
                throw new ItemNotFoundException();

            _userRepository.Delete(id);
        }

        public void UpdateUserAuthData(IUserRegister userData)
        {
            if (userData == null)
                throw new ArgumentNullException("UserRegister object is null!");

            var user = _userRepository.Find(x => x.EmailAddress.Equals(userData.AuthenticationData.EmailAddress)).SingleOrDefault();

            if (user == null)
                throw new ItemNotFoundException();

            user.EmailAddress = userData.AuthenticationData.EmailAddress;
            user.PasswordHash = _passHashService.CreateHash(userData.AuthenticationData.Password);

            _userRepository.Update(user);
        }

        public void UpdateUserPublicData(IUserPublic userData)
        {
            throw new NotImplementedException();
        }
    }
}
