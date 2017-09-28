using ACControlSystemApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACControlSystemApi.Model.Interfaces;
using ACControlSystemApi.Repositories.Interfaces;
using ACControlSystemApi.Services.Exceptions;

namespace ACControlSystemApi.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IUser AddUser(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (_userRepository.Find(x => x.EmailAddress.Equals(user.EmailAddress)).Any())
                throw new ItemAlreadyExistsException();

            try
            {
                new System.Net.Mail.MailAddress(user.EmailAddress);
            }
            catch(Exception)
            {
                throw new ArgumentException("Invalid email address!");
            }

            user.Id = 0;
            user.RegistrationTimestamp = DateTime.Now;

            _userRepository.Add(user);

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

        public void UpdateUser(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException("User object is null!");

            var oldUser = _userRepository.Find(x => x.Id.Equals(user.Id)).SingleOrDefault();

            if (oldUser == null)
                throw new ItemNotFoundException();

            _userRepository.Update(user);
        }
    }
}
