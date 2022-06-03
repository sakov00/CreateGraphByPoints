using Autofac;
using CreateGraphByPoints.ForWorkWithFiles;
using CreateGraphByPoints.Interfaces;
using CreateGraphByPoints.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateGraphByPoints.Containers
{
    public class AutofacConfig
    {
        public static IContainer GetContainer { get; set; }
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DrawFuncViewModel>().AsSelf();
            builder.RegisterType<SaveFileViewModel>().AsSelf();
            builder.RegisterType<WorkForExcel>().As<IForWorkWithFiles>();
            builder.RegisterType<WorkForXml>().As<IForWorkWithFiles>();
            builder.Register(x => new MainViewModel(x.Resolve<DrawFuncViewModel>(), x.Resolve<SaveFileViewModel>()));
            GetContainer = builder.Build();
            GetContainer.Resolve<DrawFuncViewModel>();
            GetContainer.Resolve<SaveFileViewModel>();
            GetContainer.Resolve<MainViewModel>();

        }
    }
}
