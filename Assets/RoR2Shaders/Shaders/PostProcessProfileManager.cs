using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RoR2Shaders
{
    public class PostProcessProfileManager
    {
        private static readonly List<PostProcessProfile> _postProcessProfiles = new List<PostProcessProfile>();
        private static readonly Dictionary<Type, Action<PostProcessProfile>> _shaderHandler = new Dictionary<Type, Action<PostProcessProfile>>();

        public static void Init()
        {
            CameraRigController.onCameraEnableGlobal += cameraRig =>
            {
                PostProcessProfile profile = cameraRig.transform.GetChild(0).Find("GlobalPostProcessVolume").GetComponent<PostProcessVolume>().profile;
                
                bool alreadyContainsProfile = _postProcessProfiles.Contains(profile);
                _postProcessProfiles.Add(profile);

                if (alreadyContainsProfile)
                {
                    return;
                }

                foreach (var handler in _shaderHandler.Values)
                {
                    handler(profile);
                }
            };

            CameraRigController.onCameraDisableGlobal += cameraRig =>
            {
                PostProcessProfile profile = cameraRig.transform.GetChild(0).Find("GlobalPostProcessVolume").GetComponent<PostProcessVolume>().profile;
                if (_postProcessProfiles.Remove(profile))
                {
                    if (_postProcessProfiles.Contains(profile))
                    {
                        return;
                    }

                    foreach (Type type in _shaderHandler.Keys)
                    {
                        profile.RemoveSettings(type);
                    }
                }
            };
        }

        public static void AddHandler<T>(Action<T> handler)
            where T : PostProcessEffectSettings
        {
            _shaderHandler[typeof(T)] = profile =>
            {
                if (!profile.TryGetSettings(out T settings))
                {
                    settings = ScriptableObject.CreateInstance<T>();
                    profile.AddSettings(settings);
                }

                handler(settings);
            };
            OnChange<T>();
        }

        public static void OnChange<T>()
            where T : PostProcessEffectSettings
        {
            if (!_shaderHandler.TryGetValue(typeof(T), out var handler))
            {
                return;
            }

            foreach (PostProcessProfile profile in _postProcessProfiles.Distinct())
            {
                handler(profile);
            }
        }
    }
}
