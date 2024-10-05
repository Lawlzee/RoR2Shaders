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
    [PostProcess(typeof(SharpenessRenderer), PostProcessEvent.AfterStack, "Custom/Sharpeness")]
    public sealed class Sharpeness : PostProcessEffectSettings
    {
        [Range(0, 1)]
        public FloatParameter amount = new FloatParameter { value = 1 };

        [Range(1, 10)]
        public FloatParameter thickness = new FloatParameter { value = 2 };

        public static void Init(ConfigFile config)
        {
            ConfigEntry<bool> sharpenessEnabled = config.Bind("Sharpeness", "Sharpenessing Enabled", true, "Toggles the Sharpeness shader on or off");
            ModSettingsManager.AddOption(new CheckBoxOption(sharpenessEnabled));
            
            ConfigEntry<float> sharpenessAmount = config.Bind("Sharpeness", "Sharpeness Amount", 1f, "Controls the sharpness level of the image.");
            ModSettingsManager.AddOption(new SliderOption(sharpenessAmount, new SliderConfig { min = 0, max = 1, formatString = "{0:0.##}" }));

            ConfigEntry<float> sharpenessThickness = config.Bind("Sharpeness", "Sharpeness Thickness", 2f, "Adjusts the thickness of the contrast effect; higher values result in bolder contrast outlines.");
            ModSettingsManager.AddOption(new SliderOption(sharpenessThickness, new SliderConfig { min = 0, max = 10, formatString = "{0:0.##}" }));

            PostProcessProfileManager.OverrideConfig<Sharpeness>(sharpeness =>
            {
                sharpeness.enabled.Override(sharpenessEnabled.Value);
                sharpeness.amount.Override(sharpenessAmount.Value);
                sharpeness.thickness.Override(sharpenessThickness.Value);
            });

            sharpenessEnabled.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Sharpeness>();
            sharpenessAmount.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Sharpeness>();
            sharpenessThickness.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Sharpeness>();
        }
    }
}
