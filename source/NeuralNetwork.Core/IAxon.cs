using NeuralNetwork.Core.ActivationFunctions;
using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface IAxon
{
    IList<Synapse> Terminals { get; set; }
    IActivationFunction ActivationFunction { get; set; }

    void ProcessSignal(double signal);

    double Value { get; }

    AxonGene GetGenes();
}