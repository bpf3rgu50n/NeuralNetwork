using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface INeuron
{
    IAxon Axon { get; set; }
    ISoma Soma { get; set; }

    NeuronGene GetGenes();

    void Process();
}