﻿namespace NeuralNetwork.Core.Factories;

public interface IAxonFactory
{
    IAxon Create(IList<Synapse> terminals);

    IAxon Create();

    IAxon Create(IList<Synapse> terminals, Type activationFunction);
}