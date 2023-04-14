namespace NeuralNetwork.Core.ActivationFunctions
{
    public class StepActivationFunction : IActivationFunction
    {
        public double CalculateActivation(double signal)
        {
            if (signal >= 0)
            {
                return 1.0;
            }
            else
            {
                return 0.0;
            }
        }
    }
}