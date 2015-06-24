using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.CodeDom.Compiler;

using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Scripting
{
    public class ScriptCompiler : IComponent
    {
        // Variables
        private readonly CodeDomProvider _provider;
        private readonly CompilerParameters _parameters;
        private readonly List<Precompiled> _precompiled;

        // Properties
        public int CompiledCount { get { return _precompiled.Count; } }

        // Constants
        public const string FILE_PATH = "Scripts";

        // Constructor
        public ScriptCompiler()
        {
            _provider = CodeDomProvider.CreateProvider("CSharp");
            _parameters = new CompilerParameters();
            _parameters.GenerateInMemory = true;
            _parameters.ReferencedAssemblies.Add("GameLibrary.dll");
            _parameters.ReferencedAssemblies.Add(@"C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86\Microsoft.Xna.Framework.dll");
            _parameters.ReferencedAssemblies.Add(@"C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86\Microsoft.Xna.Framework.Game.dll");

            _precompiled = new List<Precompiled>();
        }

        // Methods
        public Precompiled Compile(string path)
        {
            var prepare = Prepare(path);
            var builder = new StringBuilder();

            builder.AppendLine("using Faseway.GameLibrary;");
            builder.AppendLine("using Faseway.GameLibrary.Logging;");
            builder.AppendLine("using Faseway.GameLibrary.Scripting;");
            builder.AppendLine();
            builder.AppendLine("using Microsoft.Xna.Framework;");
            builder.AppendLine("using Microsoft.Xna.Framework.Audio;");
            builder.AppendLine("using Microsoft.Xna.Framework.Input;");
            builder.AppendLine("using Microsoft.Xna.Framework.Media;");
            builder.AppendLine();
            builder.AppendLine(prepare);
            
            var result = _provider.CompileAssemblyFromSource(_parameters, builder.ToString());
            foreach (CompilerError error in result.Errors)
            {
                Logger.Log("Compiler Error ({0}) : {1} on line {2} in {3}", error.ErrorNumber, error.ErrorText, error.Line, error.FileName);
            }

            if (!result.Errors.HasErrors)
            {
                foreach (Type type in result.CompiledAssembly.GetTypes())
                {
                    if (type.IsClass)
                    {
                        if (type.IsSubclassOf(typeof(Script)))
                        {
                            var assembly = result.CompiledAssembly.CreateInstance(type.FullName);
                            var compiled = new Precompiled(type.FullName, path, assembly);

                            AddCompiled(compiled);

                            return compiled;
                        }
                        else
                        {
                            Logger.Log("Missing subclass 'Script' at {0} ({1})", type.Name, type.BaseType);
                            return null;
                        }
                    }
                    else
                    {
                        Logger.Log("Invalid type {0} ({1})", type.Name, type.BaseType);
                        //throw new ScriptCompileException(fileName, -1, "0", "Invalid type");
                        return null;
                    }
                }
            }
            else
            {
                MsgBox.Show(MsgBoxIcon.Error, "ScriptCompiler::Compile", "{0} error(s) occured during compile process", result.Errors.Count);
            }

            Logger.Log("Empty script file ({0})", path);

            return null;
        }

        public string Prepare(string path)
        {
            if (File.Exists(path))
            {
                return new StreamReader(path).ReadToEnd();
            }
            else
            {
                throw new FileNotFoundException("File not found", path);
            }
        }

        public void AddCompiled(Precompiled compiled)
        {
            if (GetCompiled(compiled.Name) == null)
            {
                _precompiled.Add(compiled);
                Logger.Log("Compiled {0} ({1}) from {2}", compiled.Instance.GetType().BaseType.Name, compiled.Name, compiled.FileName);
            }
            else
            {
                Logger.Log("Compiled {0} ({1}) already added", compiled.Instance.GetType().BaseType.Name, compiled.Name);
            }
        }

        public Precompiled GetCompiled(string name)
        {
            return _precompiled.Find(pre => pre.Name == name);
        }
    }
}
