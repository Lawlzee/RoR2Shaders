using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace RoR2Shaders
{
    public sealed class HueSaturationValueRenderer : PostProcessEffectRenderer<HueSaturationValue>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/HueSaturationValue"));
            sheet.properties.SetFloat("_HueShift", settings.hueShift);
            sheet.properties.SetFloat("_MinSaturation", settings.minSaturation);
            sheet.properties.SetFloat("_MaxSaturation", settings.maxSaturation);
            sheet.properties.SetFloat("_MinValue", settings.minValue);
            sheet.properties.SetFloat("_MaxValue", settings.maxValue);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
