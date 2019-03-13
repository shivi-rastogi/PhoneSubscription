using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Data
{
    public class DbAccess : IDbAccess
    {
        PhoneSubscriptionDBEntities _ctx;
        public DbAccess(PhoneSubscriptionDBEntities ctx)
        {
            _ctx = ctx;
        }

        #region User table operations

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await Task.FromResult(_ctx.Users.ToList<User>());
            }

            catch (Exception ex)
            {
                UnDoDbContext();
                throw (ex);
            }
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                return await Task.FromResult(_ctx.Users.Where(u => u.Id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw (ex);
            }
        }

        public async Task<bool> Insert(User usr)
        {
            try
            {
                _ctx.Users.Add(usr);
                return await _ctx.SaveChangesAsync() >= 0;
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw ex;
            }

        }

        public async Task<bool> Update(User usr)
        {
            try
            {
                User existingUser = _ctx.Users.Where(u => u.Id == usr.Id).FirstOrDefault();
                existingUser.FirstName = usr.FirstName;
                existingUser.LastName = usr.LastName;
                existingUser.Email = usr.Email;
                return await _ctx.SaveChangesAsync() >= 0;
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw (ex);
            }

        }
        public async Task<bool> DeleteUser(int Id)
        {
            try
            {
                var usersubscription = _ctx.UserSubscriptions.Where(u => u.UserId == Id).ToList();
                if (usersubscription != null)
                {
                    _ctx.UserSubscriptions.RemoveRange(usersubscription);
                    //return true;
                }
                var entity = _ctx.Users.Where(u => u.Id == Id).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Users.Remove(entity);
                    return await _ctx.SaveChangesAsync() >= 0;

                }
            }
            catch (Exception e)
            {
                UnDoDbContext();
                throw (e);
            }

            return false;

        }
        #endregion

        #region Subscription table operations

        public async Task<List<Subscription>> GetSubscriptions()
        {
            try
            {
                return await Task.FromResult(_ctx.Subscriptions.ToList<Subscription>());
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw (ex);
            }
        }

        public async Task<Subscription> GetSubscription(string id)
        {
            try
            {
                return await Task.FromResult(_ctx.Subscriptions.Where(s => s.SubscriptionId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw (ex);
            }
        }

        public async Task<bool> Insert(Subscription sub)
        {
            try
            {
                _ctx.Subscriptions.Add(sub);
                return await _ctx.SaveChangesAsync() >= 0;
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw ex;
            }

        }

        public async Task<bool> Update(Subscription sub)
        {
            try
            {
                Subscription existingVal = _ctx.Subscriptions.Where(x => x.SubscriptionId == sub.SubscriptionId).FirstOrDefault();

                existingVal.Name = sub.Name;
                existingVal.Price = sub.Price;
                existingVal.VatAmount = sub.VatAmount;
                existingVal.CallMinutes = sub.CallMinutes;


                return await _ctx.SaveChangesAsync() >= 0;

            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw (ex);
            }

        }
        public async Task<bool> DeleteSubscription(string Id)
        {
            try
            {
                var usersubscription = _ctx.UserSubscriptions.Where(s => s.SubscriptionId == Id).ToList();
                if (usersubscription != null)
                {
                    _ctx.UserSubscriptions.RemoveRange(usersubscription);
                    //return true;
                }
                var entity = _ctx.Subscriptions.Where(s => s.SubscriptionId == Id).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Subscriptions.Remove(entity);
                    return await _ctx.SaveChangesAsync() >= 0;

                }
            }
            catch (Exception e)
            {
                UnDoDbContext();
                throw (e);
            }

            return false;

        }


        #endregion

        #region UserSubscription table operations
        public async Task<List<UserSubscription>> GetUserSubscriptions(int UserId)
        {
            try
            {
                return await Task.FromResult(_ctx.UserSubscriptions.Include(s => s.Subscription).Where(x => x.UserId == UserId).ToList());
            }

            catch (Exception ex)
            {
                UnDoDbContext();
                throw (ex);
            }
        }

        public async Task<bool> Insert(UserSubscription usrSub)
        {
            try
            {
                _ctx.UserSubscriptions.Add(usrSub);
                return await _ctx.SaveChangesAsync() >= 0;
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw ex;
            }

        }

        public async Task<bool> DeleteUserSubscription(string subId, int UserId)
        {
            try
            {
                var UserSubToDelete = _ctx.UserSubscriptions.Where(us => us.SubscriptionId == subId && us.UserId == UserId);
                foreach (var usrSub in UserSubToDelete)
                {
                    _ctx.UserSubscriptions.Remove(usrSub);

                }
                return await _ctx.SaveChangesAsync() >= 0;
            }
            catch (Exception ex)
            {
                UnDoDbContext();
                throw ex;
            }

        }
        #endregion

        void UnDoDbContext()
        {
            foreach (DbEntityEntry entry in _ctx.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }
    }
}
