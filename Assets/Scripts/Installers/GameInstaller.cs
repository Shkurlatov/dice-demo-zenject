using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DiceDemo.Scenery;
using DiceDemo.Gameplay;
using DiceDemo.Gui;
using DiceDemo.System;
using DiceDemo.Signals;

namespace DiceDemo.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        readonly Settings _settings = null;

        [SerializeField]
        Transform _gameCanvas;        
        [SerializeField]
        Transform _diceSpawnPoint;

        public override void InstallBindings()
        {
            InstallScenery();
            InstallGameplay();
            InstallGui();
            InstallSystem();
            InstallSignals();
        }

        void InstallScenery()
        {
            Container.Bind<Theme>().FromNewScriptableObjectResource("Themes").AsCached();
            Container.Bind<Scenery.Environment>().FromComponentInHierarchy().AsSingle();
        }

        void InstallGameplay()
        {
            Container.Bind<Vector3>().FromInstance(_diceSpawnPoint.position);
            Container.Bind<ImpulseGenerator>().AsSingle();
            Container.Bind<DiceSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiceManager>().AsSingle();

            foreach (Die die in _settings.DiceSet)
            {
                Container
                    .Bind<Die>()
                    .FromComponentInNewPrefab(die)
                    .WithGameObjectName("Die")
                    .UnderTransformGroup("DiceSet")
                    .AsCached();
            }
        }

        void InstallGui()
        {
            Container.Bind<string>().FromResolveAllGetter<Theme>(x => x.Name);
            Container.Bind<Queue<DiceResultMessage>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiceResultMessagesTransformer>().AsSingle();
            Container.Bind<DiceResultMessagesManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ThemeChangeButtonsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GuiHandler>().AsSingle();

            Container
                .BindFactory<string, int, ThemeChangeButton, ThemeChangeButton.Factory>()
                .FromComponentInNewPrefab(_settings.ThemeChangeButtonPrefab)
                .WithGameObjectName("ThemeChangeButton")
                .UnderTransform(_gameCanvas);

            Container
                .BindInterfacesAndSelfTo<ThrowDiceButton>()
                .FromComponentInNewPrefab(_settings.ThrowDiceButtonPrefab)
                .WithGameObjectName("ThrowDiceButton")
                .UnderTransform(_gameCanvas)
                .AsSingle();

            Container
                .BindFactory<DiceResultMessage, DiceResultMessage.Factory>()
                .FromComponentInNewPrefab(_settings.DiceResultMessagePrefab)
                .WithGameObjectName("DiceResultMessage")
                .UnderTransform(_gameCanvas);
        }

        void InstallSystem()
        {
            Container.Bind<AudioPlayer>().AsSingle();
            Container.Bind<MockGameData>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameProcessor>().AsSingle();
        }

        void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ThemeChangeSignal>();
            Container.DeclareSignal<ThrowDiceSignal>();
            Container.DeclareSignal<DiceTouchSurfaceSignal>();
            Container.DeclareSignal<DiceResultSignal>();

            Container
                .BindSignal<ThemeChangeSignal>()
                .ToMethod<GameProcessor>(x => x.OnThemeChangeCommand)
                .FromResolve();

            Container
                .BindSignal<ThrowDiceSignal>()
                .ToMethod<GameProcessor>(x => x.OnThrowDiceCommand)
                .FromResolve();

            Container
                .BindSignal<DiceTouchSurfaceSignal>()
                .ToMethod<GameProcessor>(x => x.OnDiceTouchSurface)
                .FromResolve();

            Container
                .BindSignal<DiceResultSignal>()
                .ToMethod<GameProcessor>(x => x.OnDiceResult)
                .FromResolve();          
        }

        [Serializable]
        public class Settings
        {
            [Header("Gameplay prefabs")]
            public List<Die> DiceSet;

            [Header("Gui prefabs")]
            public ThrowDiceButton ThrowDiceButtonPrefab;
            public ThemeChangeButton ThemeChangeButtonPrefab;
            public DiceResultMessage DiceResultMessagePrefab;
        }
    }
}
