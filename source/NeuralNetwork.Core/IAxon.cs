using NeuralNetwork.Core.ActivationFunctions;
using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface IAxon
{
    IActivationFunction ActivationFunction { get; set; }

    IList<Synapse> Terminals { get; set; }

    double Value { get; }

    AxonGene GetGenes();

    void ProcessSignal(double signal);
}