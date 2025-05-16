using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quanlykytucxa.Models;
using Quanlykytucxa.Models.Momo;

using RestSharp;

namespace Quanlykytucxa.Services.Momo
{
    public class Momoservice : IMomoservice
    {
        private readonly IOptions<MomoOptionModel> _options;
        public Momoservice(IOptions<MomoOptionModel> options)
        {
            _options = options;
        }

        public MomoExcuseResponeModel PaymentExecuteAsync(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;

            return new MomoExcuseResponeModel()
            {
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo
            };
        }
        public async Task<MomoCreatePaymentResponeModel> CreatePaymentMomo(OrderInforModel model)
        {
            model.OrderId = DateTime.UtcNow.Ticks.ToString();
            model.OrderInformation = "Khách hàng:" + model.FullName + "Nội dung" + model.OrderInformation;
            var rawData =
             $"partnerCode={_options.Value.PartnerCode}&" +
             $"accessKey={_options.Value.AccessKey}&" +
             $"requestId={model.OrderId}&" +
             $"amount={model.Amount}&" +
             $"orderId={model.OrderId}&" +
             $"orderInfo={model.OrderInformation}&" +
             $"returnUrl={_options.Value.ReturnUrl}&" +
             $"notifyUrl={_options.Value.NotifyUrl}&" +
             $"extraData=";
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
            var client = new RestClient(_options.Value.MoMoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = model.OrderId,
                amount = model.Amount.ToString(),
                orderInfo = model.OrderInformation,
                requestId = model.OrderId,
                extraData = "",
                signature = signature
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<MomoCreatePaymentResponeModel>(response.Content);
        }
        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
}