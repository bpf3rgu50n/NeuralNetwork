using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface ILayer
{
    void Process();

    LayerGene GetGenes();

    IList<INeuron> NeuronsInLayer { get; set; }
}