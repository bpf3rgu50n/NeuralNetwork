using NeuralNetwork.Core.ActivationFunctions;
using NeuralNetwork.Core.Genes;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Core
{
    public class Axon : IAxon
    {
        public IList<Synapse> Terminals { get; set; }
        public IActivationFunction ActivationFunction { get; set; }
        public double Value { get; protected set; }

        public Axon(IList<Synapse> terminals, IActivationFunction activationFunction)
        {
            ActivationFunction = activationFunction;
            Terminals = terminals;
            Value = 0.0;
            foreach (Synapse synapse in terminals)
            {
                synapse.Axon = this;
            }
        }

        public static IAxon GetInstance(IList<Synapse> terminals, IActivationFunction activationFunction)
        {
            return new Axon(terminals, activationFunction);
        }

        public virtual void ProcessSignal(double signal)
        {
            Value = CalculateActivation(signal);
        }

        internal double CalculateActivation(double signal)
        {
            return ActivationFunction.CalculateActivation(signal);
        }

        public AxonGene GetGenes()
        {
            return new AxonGene
            {
                ActivationFunction = ActivationFunction.GetType(),
                Weights = Terminals.Select(d => d.Weight).ToList()
            };
        }
    }
}