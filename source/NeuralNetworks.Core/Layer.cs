using NeuralNetworks.Core.Genes;

namespace NeuralNetworks.Core;

[Serializable]
public class Layer : ILayer
{
    public Guid Identifier { get; } = Guid.NewGuid();

    public IList<INeuron> NeuronsInLayer { get; set; }

    public Layer(IList<INeuron> neuronsInLayer)
    {
        NeuronsInLayer = neuronsInLayer;
    }

    public static ILayer GetInstance(IList<INeuron> neuronsInLayer)
    {
        return new Layer(neuronsInLayer);
    }

    public LayerGene GetGenes()
    {
        IList<NeuronGene> neurons = NeuronsInLayer.Select(n => n.GetGenes()).ToList();

        return new LayerGene(neurons);
    }

    public void Process()
    {
        foreach (INeuron n in NeuronsInLayer)
        {
            n.Process();
        }
    }
}