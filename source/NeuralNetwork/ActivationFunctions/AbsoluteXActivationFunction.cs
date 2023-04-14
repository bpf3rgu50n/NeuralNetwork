using System;

namespace NeuralNetwork.Core.ActivationFunctions
{
    public class AbsoluteXActivationFunction : IActivationFunction
    {
        public double CalculateActivation(double signal)
        {
            return Math.Abs(signal);
        }
    }
}