using MongoDB.Driver;
using MongoDBUsers.Models;

namespace MongoDbDemo.Repositories
{
    public class LoginUsersContext
    {
        IMongoDatabase WheelFactory;
        IMongoClient WheelFactoryClient;
        public LoginUsersContext()
        {
            WheelFactoryClient = new MongoClient("mongodb://localhost:27017");
            WheelFactory = WheelFactoryClient.GetDatabase("WheelFactory");
        }
        public IMongoCollection<LoginUsers> Users => WheelFactory.GetCollection<LoginUsers>("users");
        public bool AddUser(LoginUsers user)
        {
            Users.InsertOne(user);
            return true;
        }
        public List<LoginUsers> GetUsers()
        {
            return Users.Find(u => true).ToList();
        }
        public bool UpdateUser(int id, LoginUsers user)
        {
            var filter = Builders<LoginUsers>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<LoginUsers>.Update.Set(u => u.password, user.password);

            var result = Users.UpdateOne(filter, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public bool DeleteUser(int id)
        {
            var result = Users.DeleteOne(u => u.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        public bool Validate(LoginUsers user)
        {
            var result = Users.Find(u => u.userid == user.userid && u.password == user.password);
            if (result != null)
                return result.CountDocuments() > 0;
            return false;
        }
    }
}

