using UnityEngine;
using UnityEngine.UI;

namespace DiceDemo.UI
{
    public abstract class UIButton : MonoBehaviour
    {
        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}
