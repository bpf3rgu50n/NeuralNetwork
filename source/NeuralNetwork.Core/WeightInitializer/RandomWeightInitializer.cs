namespace NeuralNetwork.Core.WeightInitializer;

public class RandomWeightInitializer : IWeightInitializer
{
    private readonly Random _random;

    public RandomWeightInitializer() : this(new Random())
    { }

    public RandomWeightInitializer(Random random)
    {
        _random = random;
    }

    public double InitializeWeight()
    {
        double val = _random.NextDouble();
        if (_random.NextDouble() < 0.5)
        {
            // 50% chance of being negative, being between -1 and 1
            val = 0 - val;
        }
        return val;
    }
}