using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.DataServices;
using BusinessLayer.Interfaces;
using DatabaseLayer;
using DatabaseLayer.Entities;

namespace BusinessLayer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _ctx;

        public UsersRepository(AppDbContext context)
        {
            _ctx = context;
        }

        public IEnumerable<User> GetAll() => _ctx.Users;
        public User GetById(string id) => _ctx.Users.FirstOrDefault(u => u.Id == id);
        public bool IsExist(string id) => _ctx.Users.Any(u => u.Id == id);
        public void SaveChanges() => _ctx.SaveChanges();

        public User GetByEmail(string email) =>
            email == null ? null : _ctx.Users.FirstOrDefault(u => u.Email == email);


        public void Create(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _ctx.Users.Add(entity);
            SaveChanges();
        }

        public void Delete(string id)
        {
            _ctx.Users.Remove(new User {Id = id});
            SaveChanges();
        }

        public string GenerateUniqueAddress() => DataUtil.GenerateUniqueAddress(this, 8);

        public User GetByUniqueAddress(string address) => _ctx.Users.FirstOrDefault(a => a.ProfileAddress == address);

        public IEnumerable<User> GetLatest(ushort count) => throw new NotImplementedException();
    }
}