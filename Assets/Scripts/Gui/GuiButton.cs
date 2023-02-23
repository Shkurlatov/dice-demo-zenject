using Zenject;

namespace DiceDemo.Gui
{
    public abstract class GuiButton : GuiElement
    {
        protected SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public abstract void OnClick();
    }
}
