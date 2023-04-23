using NeuralNetworks.Core.Genes;

namespace NeuralNetworks.Core;

public interface ILayer
{
    IList<INeuron> NeuronsInLayer { get; set; }

    LayerGene GetGenes();

    void Process();
}