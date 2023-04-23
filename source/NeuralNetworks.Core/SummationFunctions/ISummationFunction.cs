namespace NeuralNetworks.Core;

public interface ISummationFunction
{
    double CalculateSummation(IList<Synapse> dendrites, double bias);
}