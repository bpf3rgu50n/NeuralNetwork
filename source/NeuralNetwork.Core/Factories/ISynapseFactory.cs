namespace NeuralNetwork.Core.Factories;

public interface ISynapseFactory
{
    Synapse Create();

    Synapse Create(double weight);
}