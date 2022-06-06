using Autofac;
using CreateGraphByPoints.ForWorkWithFiles;
using CreateGraphByPoints.ViewModels;

namespace CreateGraphByPoints.Containers
{
    public class AutofacConfig
    {
        public static IContainer GetContainer { get; set; }
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DrawFuncViewModel>().AsSelf();
            builder.RegisterType<WorkForExcel>().AsSelf();
            builder.RegisterType<WorkForXml>().AsSelf();

            builder.Register(x => new SaveFileViewModel(x.Resolve<DrawFuncViewModel>(), x.Resolve<WorkForExcel>(), x.Resolve<WorkForXml>()));
            builder.Register(x => new MainViewModel(x.Resolve<DrawFuncViewModel>(), x.Resolve<SaveFileViewModel>()));

            GetContainer = builder.Build();
        }
    }
}
