﻿namespace NeuralNetworks.Core.SummationFunctions;

public class MaxSummation : ISummationFunction
{
    public double CalculateSummation(IList<Synapse> dendrites, double bias)
    {
        double max = bias;

        foreach (Synapse synapse in dendrites)
        {
            double weightedValue = synapse.Axon?.Value ?? 0d * synapse.Weight;

            if (weightedValue > max)
            {
                max = weightedValue;
            }
        }

        return max;
    }
}