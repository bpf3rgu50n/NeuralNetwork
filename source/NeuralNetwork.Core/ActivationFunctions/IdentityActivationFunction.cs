﻿namespace NeuralNetwork.Core.ActivationFunctions;

public class IdentityActivationFunction : IActivationFunction
{
    public double CalculateActivation(double signal)
    {
        return signal;
    }
}