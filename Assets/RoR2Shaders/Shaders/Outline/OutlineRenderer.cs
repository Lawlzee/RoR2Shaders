using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace RoR2Shaders
{
    public sealed class OutlineRenderer : PostProcessEffectRenderer<Outline>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Outline"));
            sheet.properties.SetColor("_Color", settings.color);
            sheet.properties.SetFloat("_Thinness", settings.thinness);
            sheet.properties.SetFloat("_DensityInverse", settings.densityInverse);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
