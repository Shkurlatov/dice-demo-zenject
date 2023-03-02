using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DiceDemo.Scenery
{
    public class Environment : MonoBehaviour
    {
        [SerializeField] private Renderer _surface;
        [SerializeField] private Renderer _floor;
        [SerializeField] private Renderer[] _borders;

        private int _currentThemeIndex;
        private List<Theme> _themes;

        public int CurrentThemeIndex
        {
            get { return _currentThemeIndex; }
        }

        [Inject]
        public void Construct(List<Theme> themes)
        {
            _themes = themes;
        }

        public void SetTheme(int themeIndex)
        {
            try
            {
                SetEnvironmentMaterials(_themes[themeIndex]);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException($"{nameof(_themes)} does not contain {typeof(Theme)} with index {themeIndex}");
            }

            _currentThemeIndex = themeIndex;
        }

        private void SetEnvironmentMaterials(Theme theme)
        {
            _surface.material = theme.SurfaceMaterial;
            _floor.material = theme.FloorMaterial;

            foreach (Renderer border in _borders)
            {
                border.material = theme.BorderMaterial;
            }
        }
    }
}
