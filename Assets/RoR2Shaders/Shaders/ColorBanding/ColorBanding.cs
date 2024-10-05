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
    [PostProcess(typeof(ColorBandingRenderer), PostProcessEvent.AfterStack, "Custom/ColorBanding")]
    public sealed class ColorBanding : PostProcessEffectSettings
    {
        public IntParameter bins = new IntParameter { value = 16 };

        public static void Init(ConfigFile config)
        {
            ConfigEntry<bool> colorBandingEnabled = config.Bind("Color Banding", "Color Banding Enabled", true, "Toggles the Color Banding shader on or off");
            ModSettingsManager.AddOption(new CheckBoxOption(colorBandingEnabled));

            ConfigEntry<int> colorBandingBins = config.Bind("Color Banding", "Color Banding Bins", 64, "Sets the number of color bins used in the Color Banding shader; higher values increase color detail.");
            ModSettingsManager.AddOption(new IntSliderOption(colorBandingBins, new IntSliderConfig { min = 8, max = 128 }));

            PostProcessProfileManager.OverrideConfig<ColorBanding>(colorBanding =>
            {
                colorBanding.enabled.Override(colorBandingEnabled.Value);
                colorBanding.bins.Override(colorBandingBins.Value);
            });

            colorBandingEnabled.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<ColorBanding>();
            colorBandingBins.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<ColorBanding>();
        }
    }
}
