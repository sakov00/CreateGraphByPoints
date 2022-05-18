using CreateGraphByPoints.Interfaces;
using System;
using System.Collections.Generic;

namespace CreateGraphByPoints.Containers
{
    public static class ViewModelsContainer
    {
        private static readonly Dictionary<Type, IViewModel> instances = new Dictionary<Type, IViewModel>();

        public static void Register<T>() where T : IViewModel => instances[typeof(T)] = null;

        public static T CreateViewModel<T>() where T : IViewModel
        {
            if (instances.TryGetValue(typeof(T), out IViewModel viewModel))
            {
                viewModel = (T)Activator.CreateInstance(typeof(T));
                instances[typeof(T)] = viewModel;
            }
            return (T)viewModel;
        }
        public static T GetViewModel<T>() where T : IViewModel
        {
            if (instances.TryGetValue(typeof(T), out IViewModel viewModel))
            {
                viewModel = instances[typeof(T)];
            }
            return (T)viewModel;
        }
    }
}
