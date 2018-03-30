using System;
using System.Collections.Generic;
using LiteDB;

namespace SmartMinyanServer.Models
{
    
    public class LiteDbRepository : IRepository
    {
        
        LiteDatabase mLiteDatabase;
        LiteCollection<User> mUsers;
        LiteCollection<Minyan> mMinyan;
        public LiteDbRepository(){
            mLiteDatabase = new LiteDatabase("smartminyan.db");
            mUsers = mLiteDatabase.GetCollection<User>("users");
            mMinyan = mLiteDatabase.GetCollection<Minyan>("minyanim");
        }

        public void AddMinyan(Minyan minyan)
        {
            mMinyan.Insert(minyan);
        }

        public void AddUser(User user)
        {
           mUsers.Insert(user);
        }
        public IEnumerable<Minyan> getNearbyMinyan(double latitude,double longitude,double degreesAroundToCheck){
            return mMinyan.Find((n)=>(n.Latitude - latitude < degreesAroundToCheck || n.Longitude - longitude < degreesAroundToCheck));
        }
        public void DeleteMinyan(Minyan minyan)
        {
            mMinyan.Delete(new LiteDB.BsonValue(minyan.Id));
        }

        public void DeleteUser(User user)
        {
            mUsers.Delete(new LiteDB.BsonValue(user.Id));
        }

        public IEnumerable<Minyan> getMinyanim()
        {
           return mMinyan.FindAll();

        }

        public User GetUser(string userName, string emailAddress)
        {
            return mUsers.FindOne((n) => n.UserName == userName && n.EmailAddress ==emailAddress);
        }

        public IEnumerable<User> getUsers()
        {
            return mUsers.FindAll();
        }

        public void UpdateMinyan(Minyan minyan)
        {
            mMinyan.Update(minyan);
        }

        public void UpdateUser(User user)
        {
            mUsers.Update(user);
        }
    }
}
