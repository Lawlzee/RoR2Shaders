using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace RoR2Shaders
{
    public sealed class DitherRenderer : PostProcessEffectRenderer<Dither>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Dither"));
            sheet.properties.SetFloat("_Spread", settings.spread);
            sheet.properties.SetInt("_BayerLevel", settings.bayerLevel);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
