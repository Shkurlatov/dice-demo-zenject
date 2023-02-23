namespace DiceDemo.Signals
{
    public class DiceResultSignal
    {
        public readonly int[] DiceResult;

        public DiceResultSignal(int[] diceResult)
        {
            DiceResult = diceResult;
        }
    }
}
