using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Scripting;

namespace Faseway.GameLibrary.Scripting
{
    public class CommandScript : Script
    {
        // Properties
        public string Icon { get; set; }
        public string Cursor { get; set; }
        public CommandRestriction Restriction { get; set; }

        // Constructor
        public CommandScript()
        {
            Restriction = CommandRestriction.RESTRICT_NONE;
        }

        // Methods
        public bool CheckTarget(object caller, object target)
        {
            return Seed.Components.GetAndRequire<ScriptCompiler>().GetCompiled(Name).Execute<bool>("CheckTarget", caller, target);
        }

        public void PushActions(object caller, object target)
        {
            Seed.Components.GetAndRequire<ScriptCompiler>().GetCompiled(Name).Execute<object>("PushActions", caller, target);
        }
    }
}
