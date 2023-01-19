using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.VerimorOtp;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolves.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUser>().As<IUser>();

            builder.RegisterType<CarManager>().As<ICarService>();
            builder.RegisterType<EfCar>().As<ICar>();

            builder.RegisterType<TransactionsManager>().As<ITransactionsService>();
            builder.RegisterType<EfTransactions>().As<ITransactions>();

            builder.RegisterType<CityManager>().As<ICityService>();
            builder.RegisterType<EfCity>().As<ICity>();

            builder.RegisterType<PermitImageManager>().As<IPermitImageService>();
            builder.RegisterType<EfPermitImage>().As<IPermitImage>();

            builder.RegisterType<OtpSend>().As<IOtpSend>();
            builder.RegisterType<CheckOtpSend>().As<ICheckOtpSend>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaim>().As<IOperationClaim>();

            builder.RegisterType<RecipeManager>().As<IRecipeService>();
            builder.RegisterType<EfRecipe>().As<IRecipe>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<FileManager>().As<IFileService>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserCreditManager>().As<IUserCreditService>();
            builder.RegisterType<EfUserCreditDal>().As<IUserCreditDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector =new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
