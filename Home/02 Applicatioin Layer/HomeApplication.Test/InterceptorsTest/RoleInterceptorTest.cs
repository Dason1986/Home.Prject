using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using HomeApplication.Interceptors;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Test.InterceptorsTest
{
    [TestFixture(Category = "樣例"), Category("ICO注入")]
    public class RoleInterceptorTest
    {
        [Intercept(typeof(RoleInterceptor))]
        public interface IHasI
        {
            int GetI();
        }
        public interface IPublicInterface
        {
            string PublicMethod();
        }
        public class C : IHasI, IPublicInterface
        {
            public C(int i)
            {
                I = i;
            }

            public C()
            {
                I = 10;
            }

            public int I { get; private set; }
            public int GetI()
            {
                return I;
            }

            public string PublicMethod()
            {
                throw new NotImplementedException();
            }
        }


        [Test, Category("ICO注入")]
        public void DetectsNonInterfaceServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<C>().EnableInterfaceInterceptors();
            builder.RegisterType<RoleInterceptor>();
            var c = builder.Build();
            var dx = Assert.Throws<DependencyResolutionException>(() => c.Resolve<C>());
            Assert.IsInstanceOf<InvalidOperationException>(dx.InnerException);
        }

        [Test, Category("ICO注入")]
        public void FindsInterceptionAttributeOnReflectionComponent()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<C>().As<IHasI>().EnableInterfaceInterceptors();
            builder.RegisterType<RoleInterceptor>();
            var cpt = builder.Build().Resolve<IHasI>();

            Assert.AreEqual(10, cpt.GetI()); // proxied
        }

        [Test, Category("ICO注入")]
        public void FindsInterceptionAttributeOnExpressionComponent()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.Register(c => new C()).As<IHasI>().EnableInterfaceInterceptors();
            builder.RegisterType<RoleInterceptor>();
            var cpt = builder.Build().Resolve<IHasI>();

            Assert.AreEqual(10, cpt.GetI()); // proxied
        }

        [Test, Category("ICO注入")]
        public void InterceptsReflectionBasedComponent()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<C>().EnableClassInterceptors();
            builder.RegisterType<RoleInterceptor>();
            var container = builder.Build();
            var i = 10;
            var c = container.Resolve<C>(TypedParameter.From(i));
            var got = c.GetI();
            Assert.AreEqual(i, got);
        }
        [Test(), Category("ICO注入")]
        public void DoesNotInterceptInternalInterfaces()
        {
            // DynamicProxy2 only supports visible interfaces so internal won't work.
            var builder = new ContainerBuilder();
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<RoleInterceptor>();
            builder
                .RegisterType<C>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(RoleInterceptor))
                .As<IPublicInterface>();
            var container = builder.Build();
            var dre = Assert.Throws<NotImplementedException>(() =>
            {

                var obj = container.Resolve<IPublicInterface>();
                obj.PublicMethod();
            });
            Assert.IsInstanceOf<NotImplementedException>(dre);


            //   Assert.IsInstanceOf<InvalidOperationException>(dre.InnerException, "The inner exception should explain about public interfaces being required.");
        }

        [Test,Category("ICO注入")]
        public void InterceptsWhenUsingExtendedPropertyAndType()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<C>()
                .As<IHasI>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(RoleInterceptor));
            builder.RegisterType<RoleInterceptor>();
            var container = builder.Build();
            var cs = container.Resolve<IHasI>();
            Assert.AreEqual(10, cs.GetI());
        }
    }
}
