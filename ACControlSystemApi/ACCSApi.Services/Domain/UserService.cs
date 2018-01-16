using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddUser(AuthData userData)
        {
            if (userData == null)
                throw new ArgumentNullException();
            if (_userRepository.Find(x => x.EmailAddress.Equals(userData.EmailAddress)).Any())
                throw new ItemAlreadyExistsException("User with given email address is already registered!");

            try
            {
                new System.Net.Mail.MailAddress(userData.EmailAddress);
            }
            catch(Exception)
            {
                throw new ArgumentException("Invalid email address!");
            }

            IUser user = new User()
            {
                Id = 0,
                EmailAddress = userData.EmailAddress,
                PasswordHash = userData.PasswordHash,
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

        public void UpdateUserAuthData(AuthData userData)
        {
            if (userData == null)
                throw new ArgumentNullException("UserRegister object is null!");

            var user = _userRepository.Find(x => x.EmailAddress.Equals(userData.EmailAddress)).SingleOrDefault();

            if (user == null)
                throw new ItemNotFoundException();

            user.EmailAddress = userData.EmailAddress;
            user.PasswordHash = userData.PasswordHash;

            _userRepository.Update(user);
        }

        public void UpdateUserPublicData(IUserPublic userData)
        {
            throw new NotImplementedException();
        }
    }
}
