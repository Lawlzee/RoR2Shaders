using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace RoR2Shaders
{
    public sealed class SharpenessRenderer : PostProcessEffectRenderer<Sharpeness>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Sharpeness"));
            sheet.properties.SetFloat("_Amount", settings.amount);
            sheet.properties.SetFloat("_Thickness", settings.thickness);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
