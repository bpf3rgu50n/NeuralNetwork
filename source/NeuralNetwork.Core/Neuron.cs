using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

[Serializable]
public class Neuron : INeuron
{
    public IAxon Axon { get; set; }

    public Guid Identifier { get; } = Guid.NewGuid();

    public ISoma Soma { get; set; }

    public Neuron(ISoma soma, IAxon axon)
    {
        Soma = soma;
        Axon = axon;
    }

    public static INeuron GetInstance(ISoma soma, IAxon axon)
    {
        return new Neuron(soma, axon);
    }

    public NeuronGene GetGenes()
    {
        SomaGene soma = Soma.GetGenes();
        AxonGene axon = Axon.GetGenes();

        return new NeuronGene(soma, axon);
    }

    public virtual void Process()
    {
        Axon.ProcessSignal(Soma.CalculateSummation());
    }
}