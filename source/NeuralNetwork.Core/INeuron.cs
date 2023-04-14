using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface INeuron
{
    ISoma Soma { get; set; }
    IAxon Axon { get; set; }

    void Process();

    NeuronGene GetGenes();
}