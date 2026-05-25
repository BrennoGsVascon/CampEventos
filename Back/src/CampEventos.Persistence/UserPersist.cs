using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain.Identity;
using CampEventos.Persistence.Contextos;
using CampEventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace CampEventos.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly CampEventosContext _context;

        public UserPersist(CampEventosContext context) : base(context)
        {
            _context = context;
        }
        public async  Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
        
    }
}