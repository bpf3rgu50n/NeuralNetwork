namespace NeuralNetwork.Core.Factories;

public interface INeuronFactory
{
    INeuron Create(ISoma soma, IAxon axon);
}