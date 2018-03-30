using System;
using System.Collections.Generic;

namespace SmartMinyanServer.Models
{
    public interface IRepository
    { 
        //methods for users
        void AddUser(User user);
        void DeleteUser(int userID);
        void UpdateUser(User user);
        IEnumerable<User> getUsers();
        User GetUser(String userName, String emailAddress);

        //methods for minyanim
        void AddMinyan(Minyan minyan);
        void DeleteMinyan(int MinyanID);
        void UpdateMinyan(Minyan minyan);
        IEnumerable<Minyan> getNearbyMinyan(double latitude, double longitude, double degreesArountToCheck);
        IEnumerable<Minyan> getMinyanim();

        //methods for comments
        void AddComment(Comment comment, int minyanId);
        List<Comment> GetComments(int minyanId);
        List<Comment> GetUsersComments(User user);
    }
}
