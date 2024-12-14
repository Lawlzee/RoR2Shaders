using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2Shaders
{
    [CreateAssetMenu(fileName = "ShaderPresetCollection", menuName = "RoR2Shaders/ShaderPresetCollection", order = 1)]
    public class ShaderPresetCollection : ScriptableObject
    {
        public ShaderPreset[] presets;

        public static ShaderPresetCollection instance;
    }
}
