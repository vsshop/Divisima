namespace WebApplication48.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public RegistrationModel Account { get; set; }
        public List<ProductModel> Cart { get; set; }

        public UserModel()
        {
            Id = Guid.NewGuid();
            Cart = new List<ProductModel>();
        }
        public UserModel(RegistrationModel account) : this() 
        {
            Account = account;
        }
    }
}
