using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApplication48.Models;

namespace WebApplication48.Services
{
    public class UserService
    {
		EntityDatabase _database;
        private readonly PaymentService _paymentService;
        public UserService(EntityDatabase database, PaymentService paymentService)
		{
            _database = database;
            _paymentService = paymentService;
        }

        public async Task<BaseResponse<List<ProductModel>>> AppendProductToCart(string? login, ProductModel model)
        {
            BaseResponse<List<ProductModel>> result = new BaseResponse<List<ProductModel>>();

			try
            {
				using(EntityDatabase db = _database)
				{
                    var user = await db.User.Include(u => u.Cart).FirstOrDefaultAsync(x => x.Account.Login == login);
                    if (user == null)
                    {
                        throw new Exception("Відсутній користувач");
                    }


                    var product = db.Products.Add(new ProductModel()
                    {
                        Id = Guid.NewGuid(),
                        Image = model.Image,
                        Price = model.Price,
                        Title = model.Title
                    });

                    user.Cart.Add(product.Entity);
                    await db.SaveChangesAsync();

                    result.Data = user.Cart;
                }
			}
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = StatusCode.NotFound;
            }

            return result;
        }


		public async Task<BaseResponse<List<ProductModel>>> GetCart(string? login)
		{
			BaseResponse<List<ProductModel>> result = new BaseResponse<List<ProductModel>>();
			try
			{
                using (EntityDatabase db = _database)
                {
                    var user = await db.User.Include(u => u.Cart).FirstOrDefaultAsync(x => x.Account.Login == login);
                    if (user == null)
                    {
                        throw new Exception("Відсутній користувач");
                    }

                    result.Data = user.Cart;
                }
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Status = StatusCode.NotFound;
			}

			return result;
		}

		public async Task<BaseResponse<string>> CreatePayment(string? login)
		{
			BaseResponse<string> result = new BaseResponse<string>();
			try
			{
                using (EntityDatabase db = _database)
                {
                    var user = db.User
                        .Include(u => u.Account)
                        .Include(u => u.Cart)
                        .FirstOrDefault(u => u.Account.Login == login);
                    if (user == null)
                    {
                        throw new Exception("Відсутній користувач");
                    }
                    result.Data = await _paymentService.Payment(user);
                }
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Status = StatusCode.NotFound;
			}

			return result;
		}
	}
}
