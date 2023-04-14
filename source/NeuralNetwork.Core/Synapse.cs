namespace NeuralNetwork.Core;

public class Synapse
{
    public Synapse(IAxon axon, double weight)
    {
        Axon = axon;
        Weight = weight;
    }

    public IAxon Axon { get; set; }

    public double Weight { get; set; }
}