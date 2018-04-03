using System;
using System.Collections.Generic;

namespace SmartMinyanServer.Models
{
    public interface IRepository
    { 
        //When there is a bool as return type that indicates successfull insertino or not
        //methods for users
        bool AddUser(User user);
        void DeleteUser(int userID);
        void UpdateUser(User user);
        IEnumerable<User> getUsers();

        //methods for minyanim
        bool AddMinyan(Minyan minyan);
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
