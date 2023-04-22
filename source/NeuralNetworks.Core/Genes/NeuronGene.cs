namespace NeuralNetworks.Core.Genes;

public class NeuronGene : IEquatable<NeuronGene>
{
    public AxonGene? Axon { get; set; }

    public SomaGene? Soma { get; set; }

    public NeuronGene()
    {
    }

    public NeuronGene(SomaGene soma, AxonGene axon)
    {
        Soma = soma;
        Axon = axon;
    }

    #region Equality Members

    public static bool operator !=(NeuronGene? a, NeuronGene? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns true if the fields of the NeuronGene objects are the same.
    /// </summary>
    /// <param name="a">The NeuronGene object to compare.</param>
    /// <param name="b">The NeuronGene object to compare.</param>
    /// <returns>
    /// True if the objects are the same, are both null, or have the same values;
    /// false otherwise.
    /// </returns>
    public static bool operator ==(NeuronGene? a, NeuronGene? b)
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
    /// Returns true if the fields of the NeuronGene objects are the same.
    /// </summary>
    /// <param name="obj">The NeuronGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the NeuronGene objects are the same; false otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals(obj as NeuronGene);
    }

    /// <summary>
    /// Returns true if the fields of the NeuronGene objects are the same.
    /// </summary>
    /// <param name="NeuronGene">The NeuronGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the NeuronGene objects are the same; false otherwise.
    /// </returns>
    public bool Equals(NeuronGene? neuronGene)
    {
        if (neuronGene is null)
        {
            return false;
        }

        if (neuronGene.Axon != Axon || neuronGene.Soma != Soma)
        {
            return false;
        }

        return true;
    }

    // Following this algorithm: http://stackoverflow.com/a/263416
    /// <summary>
    /// Returns the hash code of the NeuronGene.
    /// </summary>
    /// <returns>The hash code of the NeuronGene.</returns>
    public override int GetHashCode()
    {
        int hash = HashCode.Combine(typeof(NeuronGene), Axon, Soma);
        return hash;
    }

    #endregion Equality Members
}