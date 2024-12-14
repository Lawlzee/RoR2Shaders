using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2Shaders
{
    [Serializable]
    public class ShaderPreset
    {
        public ShaderPresets preset;
        public HueSaturationValuePreset hsv;
        public SharpenessPreset sharpeness;
        public DitherPreset dither;
        public ColorBandingPreset colorBanding;
        public GrayscalePreset grayscale;
        public OutlinePreset outline;
    }

    [Serializable]
    public class HueSaturationValuePreset
    {
        public bool enabled;
        public Color hueShift = Color.red;
        [Range(-10, 10)]
        public float minSaturation = 0;
        [Range(-10, 10)]
        public float maxSaturation = 1;
        [Range(-10, 10)]
        public float minValue = 0;
        [Range(-10, 10)]
        public float maxValue = 1;
    }

    [Serializable]
    public class SharpenessPreset
    {
        public bool enabled;
        [Range(0, 1)]
        public float amount = 0;
        [Range(0, 10)]
        public float thickness = 0;
    }

    [Serializable]
    public class DitherPreset
    {
        public bool enabled;
        [Range(0, 1)]
        public float spread = 0.2f;
        [Range(0, 2)]
        public int bayerLevel = 0;
    }

    [Serializable]
    public class ColorBandingPreset
    {
        public bool enabled;
        [Range(8, 128)]
        public int bins = 64;
    }

    [Serializable]
    public class GrayscalePreset
    {
        public bool enabled;
        [Range(0, 1)]
        public int blend = 1;
    }

    [Serializable]
    public class OutlinePreset
    {
        public bool enabled;
        public Color color = Color.black;
        [Range(0.1f, 25)]
        public float thinness = 2;
        [Range(0, 1)]
        public float density = 0.25f;
    }
}
