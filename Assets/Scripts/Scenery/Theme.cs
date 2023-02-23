using UnityEngine;

namespace DiceDemo.Scenery
{
    [CreateAssetMenu]
    public class Theme : ScriptableObject
    {
        public ThemeType Type;

        public Material BorderMaterial;
        public Material FloorMaterial;
        public Material SurfaceMaterial;
    }
}
