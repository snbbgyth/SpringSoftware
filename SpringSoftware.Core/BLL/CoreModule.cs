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

            builder.RegisterType<FluentNHibernateDAL>()
                .As<IFluentNHibernate>()
                .SingleInstance();

            builder.RegisterType<OtherLogInfoDAL>()
    .As<IOtherLogInfo>();

            builder.Register(c => c.Resolve<IFluentNHibernate>().GetSession()).As<ISession>().InstancePerLifetimeScope();
        }
    }
}
