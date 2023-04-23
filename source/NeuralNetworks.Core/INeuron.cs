using NeuralNetworks.Core.Genes;

namespace NeuralNetworks.Core;

public interface INeuron
{
    IAxon Axon { get; set; }
    ISoma Soma { get; set; }

    NeuronGene GetGenes();

    void Process();
}