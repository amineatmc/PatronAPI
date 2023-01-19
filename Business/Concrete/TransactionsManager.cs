using Business.Abstract;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class TransactionsManager : ITransactionsService
    {
        private readonly ITransactions _transactions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionsManager(ITransactions transactions)
        {
            _transactions = transactions;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public IResult Add(TransactionsDto transactions)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        try
                        {
                            var dataAdd = new TransactionsTbl()
                            {
                                TableName = transactions.TableName,
                                DataDescription = transactions.DataDescription,
                                UserLoginIp = transactions.UserLoginIp,
                                DataID = transactions.DataID,
                                CreatedDate = DateTime.Now
                            };
                            _transactions.Add(dataAdd);
                            return new SuccessResult("İşlem Ekleme Başarılı", 200);
                        }
                        catch (Exception ex)
                        {
                            return new ErrorResult($"{ex}", 402);                          
                        }
                       
                    }
                    else
                    {
                        return new ErrorResult("Api ye Erişiminiz Bulunmuyor", 401);
                    }
                }
            }
            return new ErrorResult("Lütfen Doğru Bir Şifre Giriniz", 401);
        }
    }
}
