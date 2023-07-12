using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using WebApplication48.Models;

namespace WebApplication48.Services
{
    public class PaymentService
    {
        const string PRIMARY = "";
        const string FOREIGN = "";
        readonly LiqPayClient _client;


        public PaymentService()
        {
            _client = new LiqPayClient(PRIMARY, FOREIGN);
            _client.IsCnbSandbox = true;
        }


        LiqPayRequest CreatePayment(UserModel user)
        {
            LiqPayRequest request = new LiqPayRequest()
            {
                ResultUrl = "",
                ServerUrl = "",
                OrderId = Guid.NewGuid().ToString(),
                ActionPayment = LiqPayRequestActionPayment.Pay,
                Action = LiqPayRequestAction.InvoiceSend,
                Email = user.Account.Email,
                Amount = user.Cart.Sum(p => p.Price),
                Currency = "UAH",
            };

            var query = from product in user.Cart
                        select new LiqPayRequestGoods
                        {
                            Amount = product.Price,
                            Count = 1,
                            Unit = "од",
                            Name = product.Title
                        };

            request.Goods = query.ToList();
            return request;
        }

        public async Task<string> Payment(UserModel user)
        {
            var product = CreatePayment(user);
            var responce = await _client.RequestAsync("request", product);
            return responce.Href;
        }
    }
}
