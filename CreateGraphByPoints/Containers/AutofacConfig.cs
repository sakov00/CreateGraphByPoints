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

            builder.RegisterType<InteractionWithFilesViewModel>();
            builder.RegisterType<InteractionOnCanvasViewModel>();
            builder.RegisterType<WorkWithJson>();
            builder.RegisterType<WorkWithXml>();
            builder.Register(x => new MainViewModel
            (
                x.Resolve<InteractionOnCanvasViewModel>(),
                x.Resolve<InteractionWithFilesViewModel>())
            );

            GetContainer = builder.Build();
        }
    }
}
