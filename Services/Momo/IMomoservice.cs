using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Quanlykytucxa.Models;
using Quanlykytucxa.Models.Momo;
using RestSharp;
namespace Quanlykytucxa.Services.Momo
{
    public interface IMomoservice
    {
        Task<MomoCreatePaymentResponeModel> CreatePaymentMomo(OrderInforModel model);
       MomoExcuseResponeModel PaymentExecuteAsync(IQueryCollection collection);
        public bool IsValidSignature(MomoInformodel data);
    }
}
