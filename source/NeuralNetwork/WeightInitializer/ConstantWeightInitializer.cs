namespace NeuralNetwork.Core.WeightInitializer
{
    public class ConstantWeightInitializer : IWeightInitializer
    {
        private readonly double _value;

        public ConstantWeightInitializer(double constantValue)
        {
            _value = constantValue;
        }

        public double InitializeWeight()
        {
            return _value;
        }
    }
}