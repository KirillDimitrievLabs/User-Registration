using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UserRegistration.Components.PluginSystem
{
    public class PluginLoader
    {
        public static List<IDestination> Plugins { get; set; }

        public void LoadPlugins(Dictionary<object, object>[] configDict)
        {
            Plugins = new List<IDestination>();

            if (Directory.Exists("Plugins"))
            {
                string[] files = Directory.GetFiles("Plugins");
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(IDestination);

            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                .ToArray();

            foreach (Dictionary<object, object> config in configDict)
            {
                foreach (Type type in types)
                {
                    if (config["ConnectionName"].ToString() == type.Name)
                    {
                        Plugins.Add((IDestination)Activator.CreateInstance(type, new object[] { config }));
                    }
                }
            }
        }
    }
}
