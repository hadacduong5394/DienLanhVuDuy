using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Models.DAO
{
    public class UserDAO
    {
        OnlineShopDbContext context = null;
        public UserDAO()
        {
            context = new OnlineShopDbContext();
        }

        public List<User> getAllUser(string searchString)
        {
            List<User> lstUser = null;
            if (!string.IsNullOrEmpty(searchString))
            {
                lstUser = context.Users.Where(n => n.UserName.Contains(searchString) || n.FullName.Contains(searchString)).OrderByDescending(n => n.CreateDate).ToList();
                return lstUser;
            }
            lstUser = context.Users.ToList();
            return lstUser;
        }
        public bool checkUserName(string userName)
        {
            var user = context.Users.Count(n => n.UserName.Equals(userName));
            if (user > 0)
            {
                return false;
            }
            return true;
        }
        public bool checkUserEmail(string email)
        {
            var user = context.Users.Count(n => n.Email.Equals(email));
            if (user > 0)
            {
                return false;
            }
            return true;
        }

        public bool checkEmailToUpdate(long id, string email)
        {
            var res = context.Users.Where(n => n.ID != id && n.Email == email).Count();
            return res > 0 ? true : false;
        }
        //add a user
        public bool insertUser(User entity)
        {
            try
            {
                context.Users.Add(entity);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool update(User entity)
        {
            try
            {
                var user = context.Users.Find(entity.ID);
                user.Avartar = entity.Avartar;
                user.PrivateID = entity.PrivateID;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = entity.ModifiedDate;
                user.FullName = entity.FullName;
                user.Phone = entity.Phone;
                user.Address = entity.Address;
                user.Status = entity.Status;
                user.Email = entity.Email;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User getByName(string userName)
        {
            return context.Users.Where(n => n.UserName == userName).SingleOrDefault();
        }
        public User getByID(long UserID)
        {
            return context.Users.Where(n => n.ID == UserID).SingleOrDefault();
        }
        public bool loginAdmin(string account, string passWord)
        {
            IQueryable<User> query = context.Users.Where(n => n.UserName == account && n.PassWord == passWord && n.Status == true);
            var res = query.Where(n => n.PrivateID == 1 || n.PrivateID == 3).Count();
            if (res > 0)
                return true;
            return false;
        }

        public bool loginUser(string account, string passWord)
        {
            var res = context.Users.Count(n => n.UserName == account && n.PassWord == passWord && n.Status == true && n.PrivateID == 2);
            if (res > 0)
                return true;
            return false;
        }

        public bool Delete(User entity, out string errorMess)
        {
            try
            {
                var countOrderCustumer = context.Orders.Where(n => n.CustomerID == entity.ID).Count();
                if(countOrderCustumer > 0)
                {
                    errorMess = "Khách hàng này đang có kiện hàng mua tại cửa hàng, không thể xóa!";
                    return false;
                }

                if(entity.PrivateID == 1)
                {
                    errorMess = "Đây là ADMIN, không thể xóa!";
                    return false;
                }
                context.Users.Remove(entity);
                context.SaveChanges();
                errorMess = "Xóa người dùng thành công!";
                return true;
            }
            catch
            {
                errorMess = "Có lỗi xảy ra, hãy thử lại!";
                return false;
            }
        }

        public bool? ChangeStatus(long userID)
        {
            var user = context.Users.Find(userID);
            user.Status = !user.Status;
            context.SaveChanges();
            return user.Status;
        }

        public string getPrivilege(long userID)
        {
            var res = (from a in context.Users
                       join b in context.Privates on a.PrivateID equals b.ID
                       where a.ID == userID
                       select b).FirstOrDefault();
            return res.Content;

        }

        public bool changePassword(long id, string newPassword)
        {
            try
            {
                var user = context.Users.Find(id);
                user.PassWord = newPassword;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}