using CreateGraphByPoints.Interfaces;
using System;
using System.Collections.Generic;

namespace CreateGraphByPoints.Containers
{
    public static class WorkFilesContainer
    {
        private static readonly Dictionary<Type, IForWorkWithFiles> instances = new Dictionary<Type, IForWorkWithFiles>();

        public static void Register<T>() where T : IForWorkWithFiles => instances[typeof(T)] = null;

        public static T CreateForWorkWithFile<T>() where T : IForWorkWithFiles
        {
            if (instances.TryGetValue(typeof(T), out IForWorkWithFiles typeOfFile))
            {
                typeOfFile = (T)Activator.CreateInstance(typeof(T));
                instances[typeof(T)] = typeOfFile;
            }
            return (T)typeOfFile;
        }
        public static T GetForWorkWithFile<T>() where T : IForWorkWithFiles
        {
            if (instances.TryGetValue(typeof(T), out IForWorkWithFiles typeOfFile))
            {
                typeOfFile = instances[typeof(T)];
            }
            return (T)typeOfFile;
        }
    }
}
