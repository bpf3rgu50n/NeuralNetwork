using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface INeuralNetwork
{
    IList<ILayer> HiddenLayers { get; set; }

    ILayer InputLayer { get; set; }

    IList<Synapse> Inputs { get; set; }

    ILayer OutputLayer { get; set; }

    IList<Synapse> Outputs { get; set; }

    NeuralNetworkGene GetGenes();

    double[] GetOutputs();

    void Process();

    void SetInputs(double[] inputs);
}