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
    [PostProcess(typeof(HueSaturationValueRenderer), PostProcessEvent.AfterStack, "Custom/HueSaturationValue")]
    public sealed class HueSaturationValue : PostProcessEffectSettings
    {
        [Range(0f, 1f)]
        public FloatParameter hueShift = new FloatParameter { value = 0.5f };
        public FloatParameter minSaturation = new FloatParameter { value = 0 };
        public FloatParameter maxSaturation = new FloatParameter { value = 1 };
        public FloatParameter minValue = new FloatParameter { value = 0 };
        public FloatParameter maxValue = new FloatParameter { value = 1 };

        public static void Init(ConfigFile config)
        {
            ConfigEntry<bool> hsvEnabled = config.Bind("Hue Saturation Value", "Hue Saturation Value Enabled", false, "Toggles the Hue Saturation Value shader on or off");
            ModSettingsManager.AddOption(new CheckBoxOption(hsvEnabled));

            ConfigEntry<Color> hsvRedColor = config.Bind("Hue Saturation Value", "Hue Red Color", Color.red, "Defines the target color to which red hues are shifted, adjusting how much the image is color-shifted.");
            ModSettingsManager.AddOption(new ColorOption(hsvRedColor));

            ConfigEntry<float> hsvMinSaturation = config.Bind("Hue Saturation Value", "Min Saturation", 0f, "Sets the minimum saturation for the image. Adjusts how desaturated the colors can get, with a typical range of [0, 1], but can extend beyond this range to create extreme effects.");
            ModSettingsManager.AddOption(new SliderOption(hsvMinSaturation, new SliderConfig { min = -10, max = 10, formatString = "{0:0.##}" }));
            
            ConfigEntry<float> hsvMaxSaturation = config.Bind("Hue Saturation Value", "Max Saturation", 1f, "Sets the maximum saturation for the image. Adjusts how saturated the colors can get, with a typical range of [0, 1], but can extend beyond this range to create extreme effects.");
            ModSettingsManager.AddOption(new SliderOption(hsvMaxSaturation, new SliderConfig { min = -10, max = 10, formatString = "{0:0.##}" }));
            
            ConfigEntry<float> hsvMinValue = config.Bind("Hue Saturation Value", "Min Value", 0f, "Sets the minimum value (brightness) for the image. Adjusts how dark the colors can get, with a typical range of [0, 1], but can extend beyond this range to create extreme effects.");
            ModSettingsManager.AddOption(new SliderOption(hsvMinValue, new SliderConfig { min = -10, max = 10, formatString = "{0:0.##}" }));

            ConfigEntry<float> hsvMaxValue = config.Bind("Hue Saturation Value", "Max Value", 1f, "Sets the maximum value (brightness) for the image. Adjusts how bright the colors can get, with a typical range of [0, 1], but can extend beyond this range to create extreme effects.");
            ModSettingsManager.AddOption(new SliderOption(hsvMaxValue, new SliderConfig { min = -10, max = 10, formatString = "{0:0.##}" }));

            PostProcessProfileManager.OverrideConfig<HueSaturationValue>(hsv =>
            {
                hsv.enabled.Override(hsvEnabled.Value);
                Color.RGBToHSV(hsvRedColor.Value, out float hue, out float s, out float v);

                hsv.hueShift.Override(hue);
                hsv.minSaturation.Override(hsvMinSaturation.Value);
                hsv.maxSaturation.Override(hsvMaxSaturation.Value);
                hsv.minValue.Override(hsvMinValue.Value);
                hsv.maxValue.Override(hsvMaxValue.Value);
            });

            hsvEnabled.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<HueSaturationValue>();
            hsvRedColor.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<HueSaturationValue>();
            hsvMinSaturation.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<HueSaturationValue>();
            hsvMaxSaturation.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<HueSaturationValue>();
            hsvMinValue.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<HueSaturationValue>();
            hsvMaxValue.SettingChanged += (e, o) => PostProcessProfileManager.OnChange<HueSaturationValue>();
        }
    }
}
