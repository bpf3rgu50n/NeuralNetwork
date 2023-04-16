﻿using NeuralNetwork.Core.Genes;

namespace NeuralNetwork.Core.Factories;

public interface INeuralNetworkFactory
{
    INeuralNetwork Create(int numInputs, int numOutputs, int numHiddenLayers, int numHiddenPerLayer);

    INeuralNetwork Create(int numInputs, int numOutputs, IList<int> hiddenLayerSpecs);

    INeuralNetwork Create(NeuralNetworkGene genes);
}