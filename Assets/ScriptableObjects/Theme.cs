using UnityEngine;

namespace DiceDemo.Scenery
{
    [CreateAssetMenu(menuName = "DiceDemo/Theme")]
    public class Theme : ScriptableObject
    {
        public string Name;

        public Material BorderMaterial;
        public Material FloorMaterial;
        public Material SurfaceMaterial;
    }
}
