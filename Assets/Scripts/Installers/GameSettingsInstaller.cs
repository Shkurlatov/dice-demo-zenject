using System;
using UnityEngine;
using Zenject;
using DiceDemo.Gameplay;
using DiceDemo.Gui;
using DiceDemo.System;

namespace DiceDemo.Installers
{
    [CreateAssetMenu(menuName = "DiceDemo/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings GameInstaller;
        public GameplaySettings Gameplay;
        public GuiSettings Gui;
        public SystemSettings System;

        [Serializable]
        public class GameplaySettings
        {
            public ImpulseGenerator.Settings ImpulseGenerator;
            public DiceSpawner.Settings DiceSpawner;
        }

        [Serializable]
        public class GuiSettings
        {
            public GuiElement.Settings GuiElement;
            public DiceResultMessagesManager.Settings DiceResultMessagesManager;
            public DiceResultMessagesTransformer.Settings DiceResultMessagesTransformer;
        }

        [Serializable]
        public class SystemSettings
        {
            public AudioPlayer.Settings AudioHandler;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller);
            Container.BindInstance(Gameplay.ImpulseGenerator);
            Container.BindInstance(Gameplay.DiceSpawner);
            Container.BindInstance(Gui.GuiElement);
            Container.BindInstance(Gui.DiceResultMessagesManager);
            Container.BindInstance(Gui.DiceResultMessagesTransformer);
            Container.BindInstance(System.AudioHandler);
        }
    }
}
