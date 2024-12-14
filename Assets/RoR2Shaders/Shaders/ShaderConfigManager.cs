using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;

namespace RoR2Shaders
{
    public class ShaderConfigManager
    {
        public ShaderPreset CreateFromProfile(PostProcessProfile profile)
        {

        }

        public void ApplyPreset(ShaderPreset preset)
        {

        }
    }

    public class ShaderConfigManagerBuilder
    {
        private readonly ConfigFile _configFile;

        public ShaderConfigManagerBuilder(ConfigFile configFile)
        {
            _configFile = configFile;
        }

        public ShaderConfigBuilder<TShader, TShaderConfig> AddShader<TShader, TShaderConfig>(RefFunc<ShaderPreset, TShaderConfig> configAccessor)
            where TShader : PostProcessEffectSettings
        {
            
        }
    }

    public class ShaderConfigBuilder<TShader, TShaderConfig>
        where TShader : PostProcessEffectSettings
    {
        private readonly ConfigFile _configFile;
        private readonly Action<TShader, TShaderConfig> _setShaderConfig;

        public ShaderConfigBuilder(ConfigFile configFile, Action<TShader, TShaderConfig> setShaderConfig)
        {
            _configFile = configFile;
            _setShaderConfig = (a, b) => { };
        }

        public void AddConfig<T>(Func<ConfigFile, ConfigEntry<T>> config, RefFunc<TShaderConfig, T> shaderConfigAccessor)
        {
            if (_configFile != null)
            {

            }
        }
    }
}
