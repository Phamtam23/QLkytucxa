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
        private readonly string _accessKey;
        private readonly string _secretKey;
  
        public Momoservice(IOptions<MomoOptionModel> options)
        {
            _accessKey = "F8BBA842ECF85";
            _secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
         
            _options = options;
        }
        public bool IsValidSignature(MomoInformodel data)
        {
            // Tạo raw signature theo thứ tự tài liệu MoMo
            var rawHash = $"accessKey={_accessKey}&amount={data.amount}&extraData={data.extraData}&message={data.message}" +
                          $"&orderId={data.orderId}&orderInfo={data.orderInfo}&orderType={data.orderType}&partnerCode={data.partnerCode}" +
                          $"&payType={data.payType}&requestId={data.requestId}&responseTime={data.responseTime}&resultCode={data.errorCode}&transId={data.transId}";

            var computedSignature = HmacSHA256(rawHash, _secretKey);
            return computedSignature == data.signature;
        }
        private string HmacSHA256(string message, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public MomoExcuseResponeModel PaymentExecuteAsync(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            var errorCode = collection.First(s => s.Key == "errorCode").Value;
           
           

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
             $"extraData={model.ExtraData}";
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
                extraData = model.ExtraData,
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