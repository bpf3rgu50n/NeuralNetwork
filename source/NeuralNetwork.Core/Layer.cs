using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core;

[Serializable]
public class Layer : ILayer
{
    public IList<INeuron> NeuronsInLayer { get; set; }

    public Layer(IList<INeuron> neuronsInLayer)
    {
        NeuronsInLayer = neuronsInLayer;
    }

    public static ILayer GetInstance(IList<INeuron> neuronsInLayer)
    {
        return new Layer(neuronsInLayer);
    }

    public void Process()
    {
        foreach (INeuron n in NeuronsInLayer)
        {
            n.Process();
        }
    }

    public LayerGene GetGenes()
    {
        return new LayerGene
        {
            Neurons = NeuronsInLayer.Select(n => n.GetGenes()).ToList()
        };
    }
}