using NeuralNetwork.Core.Genes;
using System.Collections.Generic;

namespace NeuralNetwork.Core
{
    public interface ILayer
    {
        void Process();

        LayerGene GetGenes();

        IList<INeuron> NeuronsInLayer { get; set; }
    }
}