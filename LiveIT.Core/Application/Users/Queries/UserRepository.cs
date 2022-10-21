//using ARI.Common;
//using Infrastructure.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ARI.Users.Queries
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly ARIContext _context;
//        public UserRepository(ARIContext context)
//        {
//            _context = context;
//        }
//        public User GetAdministrator(int Id)
//        {
//            return _context.Users.FirstOrDefault(k => k.UserId == Id && k.RoleId == (int)RoleType.Administrator);
//        }
//    }
//}
