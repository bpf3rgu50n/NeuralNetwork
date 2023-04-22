namespace NeuralNetworks.Core.ActivationFunctions;

public class InverseActivationFunction : IActivationFunction
{
    public double CalculateActivation(double signal)
    {
        return (1 / signal);
    }
}