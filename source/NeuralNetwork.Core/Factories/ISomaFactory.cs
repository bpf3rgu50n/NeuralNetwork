namespace NeuralNetwork.Core.Factories;

public interface ISomaFactory
{
    ISoma Create(IList<Synapse> dendrites, double bias);

    ISoma Create(IList<Synapse> dendrites, double bias, Type summationFunction);
}