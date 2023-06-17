using TaskLibraryApp.Entities;
using TaskLibraryApp.BaseRepository;
using TaskLibraryApp.Models;
using Microsoft.Data.SqlClient;

namespace TaskLibraryApp.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private IConfiguration _configuration { get; set; }
        public UserRepository(LibraryDBContext dBContext, IConfiguration Configuration) : base(dBContext)
        {
            _configuration = Configuration;
        }
        public void CreateUser(User User)
        {
            Add(User);
        }

        public void DeleteUser(User User)
        {
            Delete(User);
        }

        public IEnumerable<User> GetAllUsers(bool isTrackingChanges)
        {
            return GetAll(isTrackingChanges);
        }

        public User GetById(int id, bool isTrackingChanges)
        {
            return GetWithCondition(c => c.Id == id, isTrackingChanges).FirstOrDefault();
        }

        public List<UserHistoryVM> GetUserHistory(string email)
        {
            var result = new List<UserHistoryVM>();
            string CS = _configuration.GetConnectionString("LibConnection");
            // Prepare the SQL query
            string sqlQuery = @"SELECT BookId, B.Title BookTitle, B.Author, B.ShortDescription Description, B.PhotoUrl  PhotoPath,BH.BookDate , BH.EndDate
                                FROM BookingHistory BH inner join Books B on BH.BookId = B.Id inner join Users U on U.Id = BH.UserId  WHERE U.Email = @UserMail";

            var SC = new SqlConnection(CS);
            SC.Open();
            // Create a SqlCommand object
            using (SqlCommand command = new SqlCommand(sqlQuery, SC))
            {
                // Add the parameter for the UserId
                command.Parameters.AddWithValue("@UserMail", email); // Replace userId with the actual value

                // Execute the query and retrieve the data
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Check if there are any rows returned
                    if (reader.HasRows)
                    {
                        // Read the data row by row
                        while (reader.Read())
                        {
                            var resultItem = new UserHistoryVM();
                            // Extract the values from the reader
                            resultItem.BookId = reader.GetInt32(0);
                            resultItem.BookTitle = reader.GetString(1);
                            resultItem.Author = reader.GetString(2);
                            resultItem.Description = reader.GetString(3);
                            resultItem.PhotoPath = reader.GetString(4);
                            resultItem.BookDate = reader.GetDateTime(5);
                            resultItem.EndDate = reader.GetDateTime(6);
                            result.Add(resultItem);
                        }
                        return result;
                    }
                    else
                        return null;
                }
            }
        }
        public void UpdateUser(User User)
        {
            Update(User);
        }
    }
}
