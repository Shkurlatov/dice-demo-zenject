using System;
using System.Collections.Generic;

namespace DiceDemo.Gui
{
    public class DiceResultMessagesManager
    {
        private readonly Settings _settings;
        private readonly DiceResultMessagesTransformer _diceResultMessagesTransformer;
        private readonly DiceResultMessage.Factory _diceResultMessageFactory;
        private readonly Queue<DiceResultMessage> _diceResultMessages;

        public DiceResultMessagesManager(
            Settings settings,
            DiceResultMessagesTransformer diceResultMessagesTransformer,
            DiceResultMessage.Factory diceResultMessageFactory, 
            Queue<DiceResultMessage> diceResultMessages)
        {
            _settings = settings;
            _diceResultMessagesTransformer = diceResultMessagesTransformer;
            _diceResultMessageFactory = diceResultMessageFactory;
           _diceResultMessages = diceResultMessages;
        }

        public void PublishDiceResultMessage(int[] diceResult)
        {
            DiceResultMessage diceResultMessage;

            if (_diceResultMessages.Count < _settings.MaxMessagesCount)
            {
                diceResultMessage = _diceResultMessageFactory.Create();
            }
            else
            {
                diceResultMessage = _diceResultMessages.Dequeue();
            }

            diceResultMessage.MessageText = ConvertDiceResultToText(diceResult);
            _diceResultMessagesTransformer.PlaceDiceResultMessageAsync(diceResultMessage);
        }

        private string ConvertDiceResultToText(int[] diceResult)
        {
            string text = diceResult[0].ToString();

            if (diceResult.Length > 1)
            {
                int sum = diceResult[0];

                for (int i = 1; i < diceResult.Length; i++)
                {
                    text += $" + {diceResult[i]}";
                    sum += diceResult[i];
                }

                text += $" = {sum}";
            }

            return text;
        }

        [Serializable]
        public class Settings
        {
            public int MaxMessagesCount;
        }
    }
}
