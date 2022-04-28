namespace DataBaseIntegration
{
    public class User 
    {
            public string Name { get; private set; }
            public string LastName { get; private set; }
            public string Email { get; private set; }
            public int IsCompleted { get; private set; }

            public User (string name, string lastName, string email, int isCompleted)
            {
                this.Name = name;
                this.LastName = lastName;
                this.Email = email;
                this.IsCompleted = isCompleted;
            }
            
            #region  GetMethods

            public string GetName() => Name;
            public string GetLastName() => LastName;
            public string GetEmail() => Email;
            public int GetIsCompleted() => IsCompleted;

            #endregion

            #region  SetMethods

            public string SetName(string userName) => Name = userName;
            public string SetLastName(string userLastName) => LastName = userLastName;
            public string SetEmail(string userEmail) => Email = userEmail;
            public int SetIsCompleted(int value) => IsCompleted = value;

            #endregion
    }
}
