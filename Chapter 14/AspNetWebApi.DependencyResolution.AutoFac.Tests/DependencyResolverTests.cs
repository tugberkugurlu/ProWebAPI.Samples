using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using NUnit.Framework;

namespace AspNetWebApi.DependencyResolution.AutoFac.Tests
{
    [TestFixture]
    public class DependencyResolverTests
    {

        [Test]
        public void TestResolve()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<Dummmy>()
                .As<IDummy>();

            var autoFacDependencyResolver = new AutoFacDependencyResolver(builder.Build());
            var service = autoFacDependencyResolver.GetService(typeof (IDummy));
            Assert.AreEqual(typeof(Dummmy), service.GetType());
        }

        [Test]
        public void TestResolveAll()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<Dummmy>()
                .As<IDummy>();
            builder
                .RegisterType<Dummmy2>()
                .As<IDummy>();


            var autoFacDependencyResolver = new AutoFacDependencyResolver(builder.Build());
            var services = (IEnumerable<IDummy>) autoFacDependencyResolver.GetService(typeof(IEnumerable<IDummy>));
            Assert.IsTrue(services.Any(x => x.GetType() == typeof(Dummmy)));
            Assert.IsTrue(services.Any(x => x.GetType() == typeof(Dummmy2)));
        }



    }


    public interface IDummy
    {
        
    }

    public class Dummmy : IDummy
    {
        
    }

    public class Dummmy2 : IDummy
    {

    }


}
