namespace NeuralNetworks.Core.ActivationFunctions;

public class TanhActivationFunction : IActivationFunction
{
    public double CalculateActivation(double signal)
    {
        return Math.Tanh(signal);
    }
}