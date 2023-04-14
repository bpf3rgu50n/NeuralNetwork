namespace NeuralNetwork.Core.SummationFunctions;

public class MinSummation : ISummationFunction
{
    public double CalculateSummation(IList<Synapse> dendrites, double bias)
    {
        double min = bias;
        foreach (Synapse synapse in dendrites)
        {
            double weightedValue = synapse.Axon.Value * synapse.Weight;

            if (weightedValue < min)
            {
                min = weightedValue;
            }
        }
        return min;
    }
}