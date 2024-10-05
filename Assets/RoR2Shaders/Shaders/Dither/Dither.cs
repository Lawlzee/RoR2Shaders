using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RiskOfOptions;
using BepInEx.Configuration;

namespace RoR2Shaders
{

    [Serializable]
    [PostProcess(typeof(DitherRenderer), PostProcessEvent.AfterStack, "Custom/Dither")]
    public sealed class Dither : PostProcessEffectSettings
    {
        [Range(0, 1)]
        public FloatParameter spread = new FloatParameter { value = 0.5f };

        [Range(0, 2)]
        public IntParameter bayerLevel = new IntParameter { value = 0 };

        public static void Init(ConfigFile config)
        {
            ConfigEntry<bool> ditherEnabled = config.Bind("Dithering", "Dithering Enabled", true, "Toggles the Dithering shader on or off");
            ModSettingsManager.AddOption(new CheckBoxOption(ditherEnabled));
            
            ConfigEntry<float> ditheringSpread = config.Bind("Dithering", "Dither Spread", 0.2f, "Adjust how much the dithering affects the colors.");
            ModSettingsManager.AddOption(new SliderOption(ditheringSpread, new SliderConfig { min = 0, max = 1, formatString = "{0:0.##}" }));

            ConfigEntry<int> ditherLevel = config.Bind("Dithering", "Dithering Level", 0, "Controls the size of the dithering pattern. Higher values create a wider dither pattern, lower values make it more compact.");
            ModSettingsManager.AddOption(new IntSliderOption(ditherLevel, new IntSliderConfig { min = 0, max = 2 }));

            PostProcessProfileManager.OverrideConfig<Dither>(dither =>
            {
                dither.enabled.Override(ditherEnabled.Value);
                dither.spread.Override(ditheringSpread.Value);
                dither.bayerLevel.Override(ditherLevel.Value);
            });

            ditherEnabled.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Dither>();
            ditheringSpread.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Dither>();
            ditherLevel.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Dither>();
        }
    }
}
