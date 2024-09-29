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
    [PostProcess(typeof(GrayscaleRenderer), PostProcessEvent.AfterStack, "Custom/Grayscale")]
    public sealed class Grayscale : PostProcessEffectSettings
    {
        [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
        public FloatParameter blend = new FloatParameter { value = 0.5f };

        public static void Init(ConfigFile config)
        {
            ConfigEntry<bool> grayscaleEnabled = config.Bind("Grayscale", "Grayscale Enabled", true, "Toggles the grayscale shader on or off");
            ModSettingsManager.AddOption(new CheckBoxOption(grayscaleEnabled));

            ConfigEntry<float> grayscaleBlend = config.Bind("Grayscale", "Grayscale Blend", 1f, "Adjusts the intensity of the grayscale effect. 1 = full grayscale, 0 = no effect");
            ModSettingsManager.AddOption(new SliderOption(grayscaleBlend, new SliderConfig { min = 0, max = 1, formatString = "{0:0.##}" }));

            PostProcessProfileManager.AddHandler<Grayscale>(profile =>
            {
                if (!profile.TryGetSettings(out Grayscale grayscale))
                {
                    grayscale = CreateInstance<Grayscale>();
                    profile.AddSettings(grayscale);
                }

                grayscale.enabled.Override(grayscaleEnabled.Value);
                grayscale.blend.Override(grayscaleBlend.Value);
            });

            grayscaleEnabled.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Grayscale>();
            grayscaleBlend.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<Grayscale>();
        }
    }
}
