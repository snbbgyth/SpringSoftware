using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NHibernate;
using SpringSoftware.Core.DAL;
using SpringSoftware.Core.IDAL;

namespace SpringSoftware.Core.BLL
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<FluentNHibernateDal>().As<IFluentNHibernate>().SingleInstance();
            builder.Register(c => c.Resolve<IFluentNHibernate>().GetSession()).As<ISession>().InstancePerLifetimeScope();
            builder.RegisterType<OtherLogInfoDal>().As<IOtherLogInfo>();
            builder.RegisterType<NewsDal>().As<INewsDal>();
            builder.RegisterType<NewsTypeDal>().As<INewsTypeDal>();
        }
    }
}
