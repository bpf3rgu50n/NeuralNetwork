namespace NeuralNetwork.Core;

public class SimpleSummation : ISummationFunction
{
    public double CalculateSummation(IList<Synapse> dendrites, double bias)
    {
        double sum = bias;
        foreach (Synapse synapse in dendrites)
        {
            sum += synapse.Axon.Value * synapse.Weight;
        }
        return sum;
    }
}