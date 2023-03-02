using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DiceDemo.Signals;

namespace DiceDemo.Gameplay
{
    public enum DiceState
    {
        Falling,
        Rolling,
        None
    }

    public class DiceManager : IFixedTickable
    {
        private readonly DiceSpawner _diceSpawner;
        private readonly List<Die> _diceSet;
        private readonly SignalBus _signalBus;

        private DiceState _diceState = DiceState.None;

        public DiceManager(DiceSpawner diceSpawner, List<Die> diceSet, SignalBus signalBus)
        {
            if (diceSet.Count == 0) throw new UnassignedReferenceException($"No one {typeof(Die)} has been assigned to {nameof(diceSet)}");

            _diceSpawner = diceSpawner;
            _diceSet = diceSet;
            _signalBus = signalBus;
        }

        public void FixedTick()
        {
            switch (_diceState)
            {
                case DiceState.Falling:
                    FixedUpdateFalling();
                    break;

                case DiceState.Rolling:
                    FixedUpdateRolling();
                    break;
            }
        }

        public async void ThrowDice()
        {
            await _diceSpawner.RespawnDiceAsync();

            _diceState = DiceState.Falling;
        }

        private void FixedUpdateFalling()
        {
            foreach (Die die in _diceSet)
            {
                if (die.IsTouchSurface)
                {
                    _signalBus.Fire<DiceTouchSurfaceSignal>();
                    _diceState = DiceState.Rolling;

                    return;
                }
            }
        }

        private void FixedUpdateRolling()
        {
            foreach (Die die in _diceSet)
            {
                if (die.Magnitude != 0.0f)
                {
                    return;
                }
            }

            _signalBus.Fire(new DiceResultSignal(GetDiceResult()));
            _diceState = DiceState.None;
        }

        private int[] GetDiceResult()
        {
            int[] diceResult = new int[_diceSet.Count];

            for (int i = 0; i < _diceSet.Count; i++)
            {
                diceResult[i] = _diceSet[i].GetResult();
            }

            return diceResult;
        }
    }
}
