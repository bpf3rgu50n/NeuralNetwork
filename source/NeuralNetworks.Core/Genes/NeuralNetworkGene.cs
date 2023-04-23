namespace NeuralNetworks.Core.Genes;

public class NeuralNetworkGene : IEquatable<NeuralNetworkGene>
{
    public IList<LayerGene> HiddenGenes { get; set; }
    public LayerGene InputGene { get; set; }
    public LayerGene OutputGene { get; set; }

    public NeuralNetworkGene(IList<LayerGene> hiddenGenes, LayerGene inputGene, LayerGene outputGene)
    {
        HiddenGenes = hiddenGenes;
        InputGene = inputGene;
        OutputGene = outputGene;
    }

    #region Equality Members

    public static bool operator !=(NeuralNetworkGene? a, NeuralNetworkGene? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns true if the fields of the NeuralNetworkGene objects are the same.
    /// </summary>
    /// <param name="a">The NeuralNetworkGene object to compare.</param>
    /// <param name="b">The NeuralNetworkGene object to compare.</param>
    /// <returns>
    /// True if the objects are the same, are both null, or have the same values;
    /// false otherwise.
    /// </returns>
    public static bool operator ==(NeuralNetworkGene? a, NeuralNetworkGene? b)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        // If one or the other is null, return false.
        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    /// <summary>
    /// Returns true if the fields of the NeuralNetworkGene objects are the same.
    /// </summary>
    /// <param name="obj">The NeuralNetworkGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the NeuralNetworkGene objects are the same; false otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals(obj as NeuralNetworkGene);
    }

    /// <summary>
    /// Returns true if the fields of the NeuralNetworkGene objects are the same.
    /// </summary>
    /// <param name="neuralNetworkGene">The NeuralNetworkGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the NeuralNetworkGene objects are the same; false otherwise.
    /// </returns>
    public bool Equals(NeuralNetworkGene? neuralNetworkGene)
    {
        if (neuralNetworkGene is null)
        {
            return false;
        }

        if (neuralNetworkGene.InputGene != InputGene ||
            neuralNetworkGene.OutputGene != OutputGene ||
            neuralNetworkGene.HiddenGenes.Count != HiddenGenes.Count)
        {
            return false;
        }

        return !HiddenGenes.Where((t, i) => t != neuralNetworkGene.HiddenGenes[i]).Any();
    }

    // Following this algorithm: http://stackoverflow.com/a/263416
    /// <summary>
    /// Returns the hash code of the NeuralNetworkGene.
    /// </summary>
    /// <returns>The hash code of the NeuralNetworkGene.</returns>
    public override int GetHashCode()
    {
        int hash = HashCode.Combine(typeof(NeuralNetworkGene), InputGene, HiddenGenes, OutputGene);
        return hash;
    }

    #endregion Equality Members
}