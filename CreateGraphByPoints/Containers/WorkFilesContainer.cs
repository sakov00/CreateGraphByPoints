using CreateGraphByPoints.Interfaces;
using System;
using System.Collections.Generic;

namespace CreateGraphByPoints.Containers
{
    public static class WorkFilesContainer
    {
        private static readonly Dictionary<Type, IForWorkWithFiles> instances = new Dictionary<Type, IForWorkWithFiles>();

        public static void Register<T>() where T : IForWorkWithFiles => instances[typeof(T)] = null;

        public static void CreateForWorkWithFile<T>() where T : IForWorkWithFiles
        {
            if (instances.TryGetValue(typeof(T), out IForWorkWithFiles typeOfFile))
            {
                if (typeOfFile == null)
                {
                    instances[typeof(T)] = (T)Activator.CreateInstance(typeof(T));
                }
            }
        }

        public static T GetForWorkWithFile<T>() where T : IForWorkWithFiles
        {
            if (instances.TryGetValue(typeof(T), out IForWorkWithFiles typeOfFile))
            {
                typeOfFile = instances[typeof(T)];
            }
            return (T)typeOfFile;
        }

        public static void RemoveForWorkWithFile<T>() where T : IForWorkWithFiles
        {
            if (instances.TryGetValue(typeof(T), out IForWorkWithFiles typeOfFile))
            {
                instances.Remove(typeof(T));
            }
        }
    }
}
