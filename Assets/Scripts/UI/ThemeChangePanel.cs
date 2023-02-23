using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiceDemo.UI
{
    public class ThemeChangePanel : MonoBehaviour
    {
        [SerializeField] private ThemeChangeButton _themeChangeButton;

        private const float _elementsInterval = 30.0f;

        private List<ThemeChangeButton> _themeChangeButtons;

        public void Init(Action<ThemeType> processThemeChangeCommand)
        {
            if (processThemeChangeCommand is null) throw new ArgumentNullException(nameof(processThemeChangeCommand));

            _themeChangeButtons = new List<ThemeChangeButton>();
            float buttonHeight = _themeChangeButton.GetComponent<RectTransform>().rect.height;

            foreach (ThemeType themeType in Enum.GetValues(typeof(ThemeType)))
            {
                float positionY = -((buttonHeight + _elementsInterval) * (int)themeType + _elementsInterval);
                ThemeChangeButton button = Instantiate(_themeChangeButton, transform);
                button.transform.localPosition = new Vector3(-_elementsInterval, positionY, 0.0f);
                button.Init(themeType, processThemeChangeCommand);
                _themeChangeButtons.Add(button);
            }
        }

        public void ChangeIndication(ThemeType themeType)
        {
            foreach (ThemeChangeButton themeChangeButton in _themeChangeButtons)
            {
                themeChangeButton.UpdateCondition(themeType);
            }
        }
    }
}
