using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace RoR2Shaders
{
    public sealed class ColorBandingRenderer : PostProcessEffectRenderer<ColorBanding>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/ColorBanding"));
            sheet.properties.SetInt("_Bins", settings.bins);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
