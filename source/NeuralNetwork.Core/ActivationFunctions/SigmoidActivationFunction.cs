namespace NeuralNetwork.Core.ActivationFunctions;

public class SigmoidActivationFunction : IActivationFunction
{
    public double CalculateActivation(double signal)
    {
        return 1.0 / (1.0 + Math.Exp(-signal));
    }
}