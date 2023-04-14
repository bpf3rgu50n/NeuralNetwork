namespace NeuralNetwork.Core.SummationFunctions;

public class AverageSummation : ISummationFunction
{
    public double CalculateSummation(IList<Synapse> dendrites, double bias)
    {
        double average = bias;
        if (dendrites.Count == 0)
        {
            return average;
        }
        foreach (Synapse synapse in dendrites)
        {
            average += synapse.Axon.Value * synapse.Weight;
        }
        return average / dendrites.Count;
    }
}