using NeuralNetwork.Core.ActivationFunctions;
using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public class Axon : IAxon
{
    public IActivationFunction ActivationFunction { get; set; }

    public Guid Identifier { get; } = Guid.NewGuid();

    public IList<Synapse> Terminals { get; set; }

    public double Value { get; protected set; }

    public Axon(IList<Synapse> terminals, IActivationFunction activationFunction)
    {
        ActivationFunction = activationFunction;
        Terminals = terminals;
        Value = 0.0d;

        foreach (Synapse synapse in terminals)
        {
            synapse.Axon = this;
        }
    }

    public static IAxon GetInstance(IList<Synapse> terminals, IActivationFunction activationFunction)
    {
        return new Axon(terminals, activationFunction);
    }

    public AxonGene GetGenes()
    {
        Type activationFunction = ActivationFunction.GetType();
        IList<double> weights = Terminals.Select(d => d.Weight).ToList();
        return new AxonGene(activationFunction, weights);
    }

    public virtual void ProcessSignal(double signal)
    {
        Value = CalculateActivation(signal);
    }

    internal double CalculateActivation(double signal)
    {
        return ActivationFunction.CalculateActivation(signal);
    }
}