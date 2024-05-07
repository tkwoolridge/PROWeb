using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Runtime.Serialization;

namespace PROWeb.Data.Services.Configuration
{
    public abstract class ConfigServiceBase<TConfig> : EnvironmentServiceBase, IConfigServiceBase where TConfig : class
    {
        private readonly string _configFolder;

        protected Dictionary<string, TConfig> Configs { get; } = new Dictionary<string, TConfig>();

        protected ConfigServiceBase(IWebHostEnvironment environment, string configFolder) : base(environment)
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
                if (SkipFile(fileInfo))
                {
                    continue;
                }

                using (Stream stream = fileInfo.CreateReadStream())
                {
                    if (CreateConfig(stream) is TConfig config)
                    {
                        Configs.Add(Path.GetFileNameWithoutExtension(fileInfo.Name), config);
                    }
                }
            }
        }

        protected virtual TConfig? CreateConfig(Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(TConfig));

            return serializer.ReadObject(stream) as TConfig;
        }

        protected virtual bool SkipFile(IFileInfo fileInfo)
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
