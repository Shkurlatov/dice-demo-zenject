using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiceDemo.Scenery
{
    public class Environment : MonoBehaviour
    {

        [SerializeField] private Renderer _surface;
        [SerializeField] private Renderer _floor;
        [SerializeField] private Renderer[] _borders;

        [SerializeField] private List<Theme> _themes;

        private ThemeType _currentThemeType;

        private Dictionary<ThemeType, Theme> _themesByType;

        void Awake()
        {
            _themesByType = new Dictionary<ThemeType, Theme>();

            foreach (ThemeType themeType in Enum.GetValues(typeof(ThemeType)))
            {
                Theme theme = _themes.Find(x => x.Type == themeType) ?? throw new UnassignedReferenceException($"{typeof(Theme)} of {themeType} type is not assigned to {nameof(_themes)}");
                _themesByType.Add(themeType, theme);
            }
        }

        public void SetTheme(ThemeType themeType)
        {
            _currentThemeType = themeType;
            SetMaterials(_themesByType.GetValueOrDefault(_currentThemeType));
        }

        public bool IsSetNewTheme(ThemeType themeType)
        {
            if (themeType == _currentThemeType)
            {
                return false;
            }

            SetTheme(themeType);

            return true;
        }

        private void SetMaterials(Theme theme)
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
