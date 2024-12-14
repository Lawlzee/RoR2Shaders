using BepInEx.Configuration;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RiskOfOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;

namespace RoR2Shaders
{
    public delegate ref T A<T>(ShaderPreset preset);

    public static class ShaderPresetManager
    {
        private static Action<ShaderPreset> _setShaderProperties = _ => { };

        public static void Init(ConfigFile config)
        {
            ConfigEntry<ShaderPresets> presetConfig = config.Bind("Configuration", "Preset", ShaderPresets.Default, "");
            ModSettingsManager.AddOption(new ChoiceOption(presetConfig));

            presetConfig.SettingChanged += (o, e) =>
            {
                ShaderPreset preset = ShaderPresetCollection.instance.presets
                    .First(x => x.preset == presetConfig.Value);

                _setShaderProperties(preset);
            };
        }

        public static void Bind<T>(ConfigEntry<T> config, Func<ShaderPreset, T> property)
        {
            _setShaderProperties += preset => config.Value = property(preset);
        }
    }
}
