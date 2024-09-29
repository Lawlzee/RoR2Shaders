using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProceduralStages
{
    public class DelayedInstantier : MonoBehaviour
    {
        public GameObject prefab;
        private bool instantied;

        public void OnDisable()
        {
            instantied = false;
        }

        public void Update()
        {
            if (!instantied)
            {
                Instantiate(prefab);
                instantied = true;
            }
        }
    }
}