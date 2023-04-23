using NeuralNetworks.Core.ActivationFunctions;
using NeuralNetworks.Core.Genes;

namespace NeuralNetworks.Core;

public interface IAxon
{
    IActivationFunction ActivationFunction { get; set; }

    IList<Synapse> Terminals { get; set; }

    double Value { get; }

    AxonGene GetGenes();

    void ProcessSignal(double signal);
}