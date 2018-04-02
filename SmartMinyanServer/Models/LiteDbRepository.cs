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
            //To keep it simple add 180 to each value to prevent negatives and then compare
            latitude = latitude + 90;
            longitude = longitude + 180;
            return mMinyan.Find((n)=>((n.Latitude + 90.0) - latitude< degreesAroundToCheck || (n.Longitude +180.0) - longitude < degreesAroundToCheck));
        }
        public void DeleteMinyan(int minyanID)
        {
            mMinyan.Delete(new BsonValue(minyanID));
        }

        public void DeleteUser(int userID)
        {
            mUsers.Delete(new BsonValue(userID));
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

        public void AddComment(Comment comment, int minyanId)
        {
            mMinyan.FindById(new BsonValue(minyanId)).Comments.Add(comment);
        }

        public List<Comment> GetComments(int minyanId)
        {
            return mMinyan.FindById(new BsonValue(minyanId)).Comments;
        }

        //extremly ineffecient, hopefull will be able to iprove later
        public List<Comment> GetUsersComments(User user)
        {
            List<Comment> comments = new List<Comment>();
            foreach (Minyan minyan in mMinyan.FindAll()) {
                foreach (Comment comment in minyan.Comments) {
                    if (comment.UserName == user.UserName) {
                        comments.Add(comment);
                    }
                }
            }
            return comments;
        }
    }
}
