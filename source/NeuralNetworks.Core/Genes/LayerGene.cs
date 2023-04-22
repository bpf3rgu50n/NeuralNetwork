namespace NeuralNetworks.Core.Genes;

public class LayerGene : IEquatable<LayerGene>
{
    public IList<NeuronGene> Neurons { get; set; }

    public LayerGene(IList<NeuronGene> neurons)
    {
        Neurons = neurons;
    }

    #region Equality Members

    public static bool operator !=(LayerGene? a, LayerGene? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns true if the fields of the LayerGene objects are the same.
    /// </summary>
    /// <param name="a">The LayerGene object to compare.</param>
    /// <param name="b">The LayerGene object to compare.</param>
    /// <returns>
    /// True if the objects are the same, are both null, or have the same values;
    /// false otherwise.
    /// </returns>
    public static bool operator ==(LayerGene? a, LayerGene? b)
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
    /// Returns true if the fields of the LayerGene objects are the same.
    /// </summary>
    /// <param name="obj">The LayerGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the LayerGene objects are the same; false otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals(obj as LayerGene);
    }

    /// <summary>
    /// Returns true if the fields of the LayerGene objects are the same.
    /// </summary>
    /// <param name="layerGene">The LayerGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the LayerGene objects are the same; false otherwise.
    /// </returns>
    public bool Equals(LayerGene? layerGene)
    {
        if (layerGene is null)
        {
            return false;
        }

        if (layerGene.Neurons.Count != Neurons.Count)
        {
            return false;
        }

        return !Neurons.Where((t, i) => t != layerGene.Neurons[i]).Any();
    }

    // Following this algorithm: http://stackoverflow.com/a/263416
    /// <summary>
    /// Returns the hash code of the LayerGene.
    /// </summary>
    /// <returns>The hash code of the LayerGene.</returns>
    public override int GetHashCode()
    {
        int hash = HashCode.Combine(typeof(LayerGene), Neurons);
        return hash;
    }

    #endregion Equality Members
}