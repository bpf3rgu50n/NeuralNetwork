﻿namespace NeuralNetworks.Core.Genes;

public class SomaGene : IEquatable<SomaGene>
{
    public double Bias { get; set; }
    public Type? SummationFunction { get; set; }

    public SomaGene() : this(0.0d, null)
    {
    }

    public SomaGene(double bias, Type? summationFunction)
    {
        Bias = bias;
        SummationFunction = summationFunction;
    }

    #region Equality Members

    public static bool operator !=(SomaGene? a, SomaGene? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns true if the fields of the SomaGene objects are the same.
    /// </summary>
    /// <param name="a">The SomaGene object to compare.</param>
    /// <param name="b">The SomaGene object to compare.</param>
    /// <returns>
    /// True if the objects are the same, are both null, or have the same values;
    /// false otherwise.
    /// </returns>
    public static bool operator ==(SomaGene? a, SomaGene? b)
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
    /// Returns true if the fields of the SomaGene objects are the same.
    /// </summary>
    /// <param name="obj">The SomaGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the SomaGene objects are the same; false otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals(obj as SomaGene);
    }

    /// <summary>
    /// Returns true if the fields of the SomaGene objects are the same.
    /// </summary>
    /// <param name="somaGene">The SomaGene object to compare with.</param>
    /// <returns>
    /// True if the fields of the SomaGene objects are the same; false otherwise.
    /// </returns>
    public bool Equals(SomaGene? somaGene)
    {
        if (somaGene is null)
        {
            return false;
        }

        if (somaGene.Bias != Bias || somaGene.SummationFunction != SummationFunction)
        {
            return false;
        }

        return true;
    }

    // Following this algorithm: http://stackoverflow.com/a/263416
    /// <summary>
    /// Returns the hash code of the SomaGene.
    /// </summary>
    /// <returns>The hash code of the SomaGene.</returns>
    public override int GetHashCode()
    {
        int hash = HashCode.Combine(typeof(SomaGene), Bias, SummationFunction);
        return hash;
    }

    #endregion Equality Members
}