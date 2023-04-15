﻿namespace NeuralNetwork.Core;

public class Synapse
{
    public IAxon? Axon { get; set; }

    public double Weight { get; set; }

    public Synapse() : this(null, 0d)
    {
    }

    public Synapse(double weight) : this(null, weight)
    {
    }

    public Synapse(IAxon? axon) : this(axon, 0d)
    {
    }

    public Synapse(IAxon? axon, double weight)
    {
        Axon = axon;
        Weight = weight;
    }
}