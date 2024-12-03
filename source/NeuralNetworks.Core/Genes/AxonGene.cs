namespace NeuralNetworks.Core.Genes;

public class AxonGene : IEquatable<AxonGene>
{
    public Type? ActivationFunction { get; set; }

    public IList<double> Weights { get; set; }

    public AxonGene() : this(null, [])
    {
    }

    public AxonGene(Type? activationFunction, IList<double> weights)
    {
        ActivationFunction = activationFunction;
        Weights = weights;
    }

    #region Equality Members

    public static bool operator !=(AxonGene? a, AxonGene? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns true if the fields of the AxonGene objects are the same.
    /// </summary>
    /// <param name="a">The AxonGene object to compare.</param>
    /// <param name="b">The AxonGene object to compare.</param>
    /// <returns>
    /// True if the objects are the same, are both null, or have the same values;
    /// false otherwise.
    /// </returns>
    public static bool operator ==(AxonGene? a, AxonGene? b)
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
    /// Returns true if the fields of the AxonGene objects are the same.
    /// </summary>
    /// <param name="obj">The AxonGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the AxonGene objects are the same; false otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals(obj as AxonGene);
    }

    /// <summary>
    /// Returns true if the fields of the AxonGene objects are the same.
    /// </summary>
    /// <param name="axonGene">The AxonGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the AxonGene objects are the same; false otherwise.
    /// </returns>
    public bool Equals(AxonGene? axonGene)
    {
        if (axonGene is null)
        {
            return false;
        }

        if (axonGene.ActivationFunction != ActivationFunction || axonGene.Weights.Count != Weights.Count)
        {
            return false;
        }

        const double tolerance = 0.00001;
        return !Weights.Where((t, i) => Math.Abs(t - axonGene.Weights[i]) > tolerance).Any();
    }

    // Following this algorithm: http://stackoverflow.com/a/263416
    /// <summary>
    /// Returns the hash code of the AxonGene.
    /// </summary>
    /// <returns>The hash code of the AxonGene.</returns>
    public override int GetHashCode()
    {
        int hash = HashCode.Combine(typeof(AxonGene), ActivationFunction, Weights);
        return hash;
    }

    #endregion Equality Members
}