using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

public interface ISoma
{
    IList<Synapse> Dendrites { get; set; }

    ISummationFunction SummationFunction { get; set; }

    double Bias { get; set; }

    double Value { get; }

    double CalculateSummation();

    SomaGene GetGenes();
}