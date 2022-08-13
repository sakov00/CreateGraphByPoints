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

            builder.RegisterType<InteractionOnCanvasViewModel>().AsSelf();
            builder.RegisterType<WorkWithExcel>().AsSelf();
            builder.RegisterType<WorkWithXml>().AsSelf();

            builder.Register(x => new InteractionWithFilesViewModel
            (
                x.Resolve<InteractionOnCanvasViewModel>(),
                x.Resolve<WorkWithExcel>(),
                x.Resolve<WorkWithXml>())
            );

            builder.Register(x => new MainViewModel
            (
                x.Resolve<InteractionOnCanvasViewModel>(),
                x.Resolve<InteractionWithFilesViewModel>())
            );

            GetContainer = builder.Build();
        }
    }
}
