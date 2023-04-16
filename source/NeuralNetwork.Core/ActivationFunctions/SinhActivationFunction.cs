namespace NeuralNetwork.Core.ActivationFunctions;

public class SinhActivationFunction : IActivationFunction
{
    public double CalculateActivation(double signal)
    {
        return Math.Sinh(signal);
    }
}