using NeuralNetworks.Core.Genes;

namespace NeuralNetworks.Core;

public class Soma : ISoma
{
    public double Bias { get; set; }

    public IList<Synapse> Dendrites { get; set; }

    public Guid Identifier { get; } = Guid.NewGuid();

    public ISummationFunction SummationFunction { get; set; }

    public double Value { get; protected set; }

    public Soma(IList<Synapse> dendrites, ISummationFunction summationFunction, double bias)
    {
        Dendrites = dendrites;
        SummationFunction = summationFunction;
        Bias = bias;
    }

    public static ISoma GetInstance(IList<Synapse> dendrites, ISummationFunction summationFunction, double bias)
    {
        return new Soma(dendrites, summationFunction, bias);
    }

    public virtual double CalculateSummation()
    {
        return Value = SummationFunction.CalculateSummation(Dendrites, Bias);
    }

    public SomaGene GetGenes()
    {
        Type summationFunction = SummationFunction.GetType();

        return new SomaGene(Bias, summationFunction);
    }
}