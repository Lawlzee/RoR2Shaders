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
    [PostProcess(typeof(OutlineRenderer), PostProcessEvent.AfterStack, "Custom/Outline")]
    public sealed class Outline : PostProcessEffectSettings
    {
        public ColorParameter color = new ColorParameter { value = Color.black };

        public FloatParameter thinness = new FloatParameter { value = 2f };

        [Range(0, 1)]
        public FloatParameter densityInverse = new FloatParameter { value = 0.25f };

        public static void Init(ConfigFile config)
        {
            ConfigEntry<bool> outlineEnabled = config.Bind("Outline", "Outline Enabled", true, "Toggles the outline shader on or off");
            ModSettingsManager.AddOption(new CheckBoxOption(outlineEnabled));

            ConfigEntry<Color> outlineColor = config.Bind("Outline", "Outline Color", Color.black, "Specifies the color of the outline");
            ModSettingsManager.AddOption(new ColorOption(outlineColor));

            ConfigEntry<float> outlineThiness = config.Bind("Outline", "Outline Thiness", 2f, "Controls the thickness of the outline; a higher value results in a thinner outline.");
            ModSettingsManager.AddOption(new SliderOption(outlineThiness, new SliderConfig { min = 0.1f, max = 25, formatString = "{0:0.##}" }));
            
            ConfigEntry<float> outlineDensity = config.Bind("Outline", "Outline Density", 0.75f, "Adjusts the density of the outline effect; a higher value increases the quantity of outlines.");
            ModSettingsManager.AddOption(new SliderOption(outlineDensity, new SliderConfig { min = 0f, max = 1, formatString = "{0:0.##}" }));

            PostProcessProfileManager.OverrideConfig<Outline>(outline =>
            {
                outline.enabled.Override(outlineEnabled.Value);
                outline.color.Override(outlineColor.Value);
                outline.thinness.Override(outlineThiness.Value);
                outline.densityInverse.Override(1 - outlineDensity.Value);
            });

            outlineEnabled.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Outline>();
            outlineColor.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Outline>();
            outlineThiness.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Outline>();
            outlineDensity.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Outline>();
        }
    }
}
