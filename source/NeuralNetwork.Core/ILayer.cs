using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface ILayer
{
    IList<INeuron> NeuronsInLayer { get; set; }

    LayerGene GetGenes();

    void Process();
}