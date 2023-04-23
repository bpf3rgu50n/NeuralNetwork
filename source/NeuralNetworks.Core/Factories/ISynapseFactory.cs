namespace NeuralNetworks.Core.Factories;

public interface ISynapseFactory
{
    Synapse Create();

    Synapse Create(double weight);
}