using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Scripting
{
    public class Precompiled
    {
        // Properties
        public string Name { get; private set; }
        public string FileName { get; private set; }
        public object Instance { get; private set; }

        // Constructor
        public Precompiled(string name, string fileName, object instance)
        {
            Name = name;
            FileName = fileName;
            Instance = instance;
        }

        // Methods
        public T ConvertTo<T>() where T : Script
        {
            return (T)Instance;
        }

        public void Execute(string method, params object[] parameters)
        {
            Execute<object>(method, parameters);
        }

        public T Execute<T>(string method, params object[] parameters)
        {
            var reflection = Instance.GetType().GetMethod(method);
            var invoke = reflection.Invoke(Instance, parameters);
            
            return (T)Convert.ChangeType(invoke, typeof(T));
        }
    }
}
