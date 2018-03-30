using System;
using System.Collections.Generic;

namespace SmartMinyanServer.Models
{
    public interface IRepository
    { 
        //methods for users
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        IEnumerable<User> getUsers();
        User GetUser(String userName, String emailAddress);

        //methods for minyanim
        void AddMinyan(Minyan minyan);
        void DeleteMinyan(Minyan minyan);
        void UpdateMinyan(Minyan minyan);
        IEnumerable<Minyan> getNearbyMinyan(double latitude, double longitude, double degreesArountToCheck);
        IEnumerable<Minyan> getMinyanim();
    }
}
