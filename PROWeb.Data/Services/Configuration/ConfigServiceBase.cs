using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Runtime.Serialization;
using System.Xml;

namespace PROWeb.Data.Services.Configuration
{
    public class ConfigServiceBase<TConfig> : EnvironmentServiceBase, IConfigServiceBase where TConfig : class
    {
        private readonly string _configFolder;
        protected Dictionary<string, TConfig> Configs { get; } = new Dictionary<string, TConfig>();

        public ConfigServiceBase(IWebHostEnvironment environment, string configFolder) : base(environment)
        {
            _configFolder = configFolder;
        }

        public async Task PreloadConfigAsync()
        {
            await Task.Run(() =>
            {
                LoadFromConfigFiles();
            }
            );
        }

        private void LoadFromConfigFiles()
        {
            if (Environment?.WebRootFileProvider.GetDirectoryContents(_configFolder) is not { } configFiles)
            {
                return;
            }

            foreach (IFileInfo fileInfo in configFiles)
            {
                if (SkeepFile(fileInfo))
                {
                    continue;
                }

                using (XmlReader rdr = XmlReader.Create(fileInfo.CreateReadStream()))
                {
                    var serializer = new DataContractSerializer(typeof(TConfig));

                    if (serializer.ReadObject(rdr) is TConfig config)
                    {
                        Configs.Add(Path.GetFileNameWithoutExtension(fileInfo.Name), config);
                    }
                }
            }
        }

        protected virtual bool SkeepFile(IFileInfo fileInfo)
        {
            return false;
        }

        protected virtual TConfig? GetConfigByName(string name)
        {
            if (Configs.TryGetValue(name, out TConfig? config))
            {
                return config;
            }

            return null;
        }
    }
}
