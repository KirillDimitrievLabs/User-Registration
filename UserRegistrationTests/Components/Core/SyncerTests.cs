using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration.Components.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Components.PluginSystem;
using UserRegistration;
using YamlDotNet.Serialization;
using System.IO;
using Microsoft.Extensions.Logging;

namespace UserRegistration.Components.Core.Tests
{
    [TestClass()]
    public class SyncerTests
    {
        [TestMethod()]
        public async Task UpdateUserTest()
        {
            PluginLoader pluginLoader = new PluginLoader();
            Dictionary<object, object>[] ConfigDictionary = TestHelpers.ReadConfig("Config.yaml");
            pluginLoader.LoadPlugins(ConfigDictionary);
            IDestination plugin = PluginLoader.Plugins[0];
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            UserSourceModel userSourceModel = new UserSourceModel
            {
                Avatar = "asdasd",
                Disabled = false,
                Email = "Test2@gmail.comm",
                FullName = "TestFakeUser",
                ServiceAction = "save",
                OrgStructure = new OrgStructureModel
                {
                    Company = "Sellout",
                    Office = "Cheb",
                    Team = "MDTC"
                }
            };
        }

        [TestMethod()]
        public async Task DeleteUserTest_UserDoesntExsits()
        {
            PluginLoader pluginLoader = new PluginLoader();
            Dictionary<object, object>[] ConfigDictionary = TestHelpers.ReadConfig("Config.yaml");
            pluginLoader.LoadPlugins(ConfigDictionary);
            IDestination plugin = PluginLoader.Plugins[0];
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            UserSourceModel userSourceModel = new UserSourceModel
            {
                Avatar = "asdasd",
                Disabled = false,
                Email = "Test2@gmail.comm",
                FullName = "TestFakeUser",
                ServiceAction = "save",
                OrgStructure = new OrgStructureModel
                {
                    Company = "Sellout",
                    Office = "Cheb",
                    Team = "MDTC"
                }
            };
        }

        [TestMethod()]
        public async Task CreateUserTest_ExsistingUser()
        {
            PluginLoader pluginLoader = new PluginLoader();
            Dictionary<object, object>[] ConfigDictionary = TestHelpers.ReadConfig("Config.yaml");
            pluginLoader.LoadPlugins(ConfigDictionary);
            IDestination plugin = PluginLoader.Plugins[0];
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            UserSourceModel userSourceModel = new UserSourceModel
            {
                Avatar = "asdasd",
                Disabled = false,
                Email = "Test2@gmail.comm",
                FullName = "Test User",
                ServiceAction = "save",
                OrgStructure = new OrgStructureModel
                {
                    Company = "Sellout",
                    Office = "Cheb",
                    Team = "MDTC"
                }
            };

        }
    }
    public static class TestHelpers
    {
        public static Dictionary<object, object>[] ReadConfig(string path)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .Build();

            return deserializer.Deserialize<Dictionary<object, object>[]>(File.ReadAllText(path));
        }
    }
}