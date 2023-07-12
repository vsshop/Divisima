using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using WebApplication48.Models;

namespace WebApplication48.Services
{
	public class AuthorizationService
	{
		IConfiguration _configuration;
        EntityDatabase _database;

        public AuthorizationService(IConfiguration configuration, EntityDatabase database)
		{
            _configuration = configuration;
            _database = database;
        }
		public async Task<BaseResponse<RegistrationModel>> SignIn(AuthorizationModel model)
		{
			BaseResponse<RegistrationModel> responce = new BaseResponse<RegistrationModel>();
			try
			{
				using (EntityDatabase db = _database)
				{
					var userFind = await db.User.Include(u => u.Account).FirstOrDefaultAsync(u => u.Account.Login == model.Login);

					if (userFind == null)
						throw new Exception("Не існує такого користувача");

					if (userFind.Account.Password != PasswordHash(model.Password))
						throw new Exception("невірний пароль");

					responce.Data = userFind.Account;
				}
			}
			catch (Exception ex)
			{
				responce.Message = ex.Message;
				responce.Status = StatusCode.NotFound;
			}

			return responce;
		}
		public async Task<BaseResponse<RegistrationModel>> SignUp(RegistrationModel model)
		{
			BaseResponse<RegistrationModel> responce = new BaseResponse<RegistrationModel>();
			try
			{
				using (EntityDatabase db = _database)
				{
					var findLogin = await db.User.Include(u => u.Account).FirstOrDefaultAsync(u => u.Account.Login == model.Login);

					if (findLogin != null)
						throw new Exception("Користувач з таким логіном існує");

					var findEmail = await db.User.Include(u => u.Account).FirstOrDefaultAsync(u => u.Account.Email == model.Email);

					if (findEmail != null)
						throw new Exception("Користувач з такою поштою вже існує");

					model.Password = PasswordHash(model.Password);
                    await db.User.AddAsync(new UserModel(model));
					await db.SaveChangesAsync();

					responce.Data = model;
				}
			}
			catch (Exception ex)
			{
				responce.Message = ex.Message;
				responce.Status = StatusCode.NotFound;
			}

			return responce;
		}
		
		public async Task<BaseResponse<RegistrationModel>> SignUpGoogle(GooglePerson person)
		{
			BaseResponse<RegistrationModel> responce = new BaseResponse<RegistrationModel>();
			try
			{
				using (EntityDatabase db = _database)
				{
					if (!person.EmailVerified)
						throw new Exception("Користувач не авторизуввався");

					var findUser = await db.User.Include(u => u.Account).FirstOrDefaultAsync(u => u.Account.Email == person.Email);

					if (findUser == null)
						throw new Exception("Користувач з такою поштою відсутній");

					responce.Data = findUser.Account;
				}
			}
			catch (Exception ex)
			{
				responce.Message = ex.Message;
				responce.Status = StatusCode.NotFound;
			}

			return responce;
		}

        string PasswordHash(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256 hesher = SHA256.Create();
			byte[] hesherBytes = hesher.ComputeHash(bytes);
			return Encoding.UTF8.GetString(hesherBytes);
        }
    }
}
