using NeuralNetworks.Core.Genes;

namespace NeuralNetworks.Core;

public interface ISoma
{
    double Bias { get; set; }
    IList<Synapse> Dendrites { get; set; }

    ISummationFunction SummationFunction { get; set; }
    double Value { get; }

    double CalculateSummation();

    SomaGene GetGenes();
}