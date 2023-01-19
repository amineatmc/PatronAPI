using Business.Abstract;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RecipeManager : IRecipeService
    {     
        private readonly IRecipe _recipe;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RecipeManager(IRecipe recipe)
        {           
            _recipe = recipe;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public IResult Add(Recipe recipe)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        if(recipe!=null)
                        {
                            try
                            {
                                _recipe.Add(recipe);
                                return new SuccessResult("Tarife Ekleme Başarılı", 200);
                            }
                            catch (Exception ex)
                            {

                                return new ErrorResult($"{ex.Message}", 400);
                            }
                        }
                    }
                    else
                    {
                       
                        return new ErrorResult("Lütfen Doğru Bir Şifre Giriniz", 401);
                    }
                }
            }
            return new ErrorResult("Api ye Erişiminiz Bulunmuyor", 401);

        }

        public IDataResult<List<Recipe>> GetAllList()
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        return new SuccessDataResult<List<Recipe>>(_recipe.GetRecipeList(),"Tarife Listelemesi Başarılı",200);
                    }
                    else
                    {
                        
                        return new ErrorDataResult<List<Recipe>>(null,"Lütfen Doğru Bir Şifre Giriniz", 401);
                    }
                }
            }
            return new ErrorDataResult<List<Recipe>>(null,"Apiye Erişiminiz Bulunmuyor",402);
        }

        public IResult Update(Recipe recipe)
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
                            _recipe.Update(recipe);
                            return new SuccessResult("Güncelleme İşlemi Başarılı", 200);
                        }
                        catch (Exception ex)
                        {
                            return new ErrorResult($"{ex.Message}", 404);
                        }
                       
                    }
                    else
                    {

                        return new ErrorDataResult<List<Recipe>>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);
                    }
                }
            }
            return new ErrorDataResult<List<Recipe>>(null, "Apiye Erişiminiz Bulunmuyor", 402);
        }
    }
}
