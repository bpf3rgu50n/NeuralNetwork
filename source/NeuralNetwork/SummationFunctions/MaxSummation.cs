using System.Collections.Generic;

namespace NeuralNetwork.Core.SummationFunctions
{
    public class MaxSummation : ISummationFunction
    {
        public double CalculateSummation(IList<Synapse> dendrites, double bias)
        {
            double max = bias;
            foreach (Synapse synapse in dendrites)
            {
                var weightedValue = synapse.Axon.Value * synapse.Weight;
                if (weightedValue > max)
                {
                    max = weightedValue;
                }
            }
            return max;
        }
    }
}