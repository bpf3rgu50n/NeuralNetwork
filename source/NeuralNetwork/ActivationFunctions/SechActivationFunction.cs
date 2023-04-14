using System;

namespace NeuralNetwork.Core.ActivationFunctions
{
    public class SechActivationFunction : IActivationFunction
    {
        public double CalculateActivation(double signal)
        {
            return (1 / Math.Cosh(signal));
        }
    }
}