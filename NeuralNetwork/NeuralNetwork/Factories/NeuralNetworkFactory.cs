﻿using ArtificialNeuralNetwork.ActivationFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork.Factories
{
    public class NeuralNetworkFactory : INeuralNetworkFactory
    {
        //TODO: this class needs to create and setup the entire network
        private ISummationFunction _summationFunction;
        private IActivationFunction _activationFunction;
        private IWeightInitializer _weightInitializer;

        private NeuralNetworkFactory(ISummationFunction summationFunction, IActivationFunction activationFunction, IWeightInitializer weightInitializer)
        {
            _summationFunction = summationFunction;
            _activationFunction = activationFunction;
            _weightInitializer = weightInitializer;
        }

        public static NeuralNetworkFactory GetInstance(ISummationFunction summationFunction, IActivationFunction activationFunction, IWeightInitializer weightInitializer)
        {
            return new NeuralNetworkFactory(summationFunction, activationFunction, weightInitializer);
        }

        internal INeuron CreateNeuron(ISomaFactory somaFactory, IAxonFactory axonFactory, Dictionary<int, Dictionary<int, IList<Synapse>>> mapping, int layerIndex, int neuronIndex)
        {
            var dendrites = (layerIndex > 0) ? getDendritesForSoma(layerIndex, neuronIndex, mapping) : mapping[layerIndex][neuronIndex];

            var soma = somaFactory.Create(dendrites, _weightInitializer.InitializeWeight());

            var terminals = mapping[layerIndex + 1][neuronIndex];
            var axon = axonFactory.Create(terminals);

            return Neuron.GetInstance(soma, axon);
        }

        //Used for input/output lists
        internal IList<Synapse> GetAllSynapsesFromLayerMapping(Dictionary<int, IList<Synapse>> layerMapping)
        {
            IList<Synapse> synapses = new List<Synapse>();
            foreach(var key in layerMapping.Keys)
            {
                var terminals = layerMapping[key];
                foreach (var terminal in terminals)
                {
                    synapses.Add(terminal);
                }
            }
            return synapses;
        }

        public INeuralNetwork Create(int numInputs, int numOutputs, int numHiddenLayers, int numHiddenPerLayer)
        {
            var somaFactory = SomaFactory.GetInstance(_summationFunction);
            var axonFactory = AxonFactory.GetInstance(_activationFunction);

            //layer number + position in layer --> list of terminals
            var mapping = CreateSynapses(numInputs, numOutputs, numHiddenLayers, numHiddenPerLayer);

            var inputs = GetAllSynapsesFromLayerMapping(mapping[0]);
            var outputs = GetAllSynapsesFromLayerMapping(mapping[mapping.Keys.Count - 1]);

            //Input Layer
            IList<INeuron> inputLayerNeurons = new List<INeuron>();
            for (int i = 0; i < numInputs; i++)
            {
                inputLayerNeurons.Add(CreateNeuron(somaFactory, axonFactory, mapping, 0, i));
            }
            ILayer inputLayer = Layer.GetInstance(inputLayerNeurons);


            IList<ILayer> hiddenLayers = new List<ILayer>();
            //Hidden layers
            for (int h = 0; h < numHiddenLayers; h++)
            {
                IList<INeuron> hiddenNeuronsInLayer = new List<INeuron>();
                for (int i = 0; i < numHiddenPerLayer; i++)
                {
                    hiddenNeuronsInLayer.Add(CreateNeuron(somaFactory, axonFactory, mapping, h + 1, i));
                }
                hiddenLayers.Add(Layer.GetInstance(hiddenNeuronsInLayer));
            }

            //Output layer
            IList<INeuron> outputLayerNeurons = new List<INeuron>();
            for (int o = 0; o < numOutputs; o++)
            {
                outputLayerNeurons.Add(CreateNeuron(somaFactory, axonFactory, mapping, numHiddenLayers + 1, o));
            }
            ILayer outputLayer = Layer.GetInstance(outputLayerNeurons);

            return NeuralNetwork.GetInstance(inputs, inputLayer, hiddenLayers, outputLayer, outputs);
        }

        internal Dictionary<int, Dictionary<int, IList<Synapse>>> CreateSynapses(int numInputs, int numOutputs, int numHiddenLayers, int numHiddenPerLayer)
        {
            var synapseFactory = SynapseFactory.GetInstance(_weightInitializer);
            //layer number + position in layer --> list of terminals
            var mapping = new Dictionary<int, Dictionary<int, IList<Synapse>>>();

            //Input Layer
            IList<Synapse> inputs = new List<Synapse>();
            mapping[0] = new Dictionary<int, IList<Synapse>>();
            mapping[1] = new Dictionary<int, IList<Synapse>>();
            for (int i = 0; i < numInputs; i++)
            {
                var synapse = synapseFactory.Create();

                var dendrites = new[] { synapse };
                mapping[0][i] = dendrites;

                var terminals = new List<Synapse>();
                for (int t = 0; t < numHiddenPerLayer; t++)
                {
                    terminals.Add(synapseFactory.Create());
                }
                mapping[1][i] = terminals;
            }

            //Hidden layers 0 to (n-1)
            for (int h = 0; h < numHiddenLayers - 1; h++)
            {
                mapping[h + 2] = new Dictionary<int, IList<Synapse>>();
                for (int i = 0; i < numHiddenPerLayer; i++)
                {
                    var terminals = new List<Synapse>();
                    for (int t = 0; t < numHiddenPerLayer; t++)
                    {
                        terminals.Add(synapseFactory.Create());
                    }
                    mapping[h + 2][i] = terminals;
                }
            }

            //Hidden layer n
            mapping[numHiddenLayers + 1] = new Dictionary<int, IList<Synapse>>();
            for (int i = 0; i < numHiddenPerLayer; i++)
            {
                var terminals = new List<Synapse>();
                for (int t = 0; t < numOutputs; t++)
                {
                    terminals.Add(synapseFactory.Create());
                }
                mapping[numHiddenLayers + 1][i] = terminals;
            }

            //Output layer
            mapping[numHiddenLayers + 2] = new Dictionary<int, IList<Synapse>>();
            for (int o = 0; o < numOutputs; o++)
            {
                var synapse = synapseFactory.Create();
                var terminals = new List<Synapse>();
                for (int t = 0; t < numOutputs; t++)
                {
                    terminals.Add(synapseFactory.Create());
                }
                mapping[numHiddenLayers + 2][o] = terminals;
            }

            return mapping;
        }

        private IList<Synapse> getDendritesForSoma(int layer, int terminalIndexInLayer, Dictionary<int, Dictionary<int, IList<Synapse>>> mapping)
        {
            //get entire layer before, then grab the nth synapse from each list
            if (layer <= 1)
            {
                throw new ArgumentOutOfRangeException("layer must be > 1");
            }

            var neuronMappings = mapping[layer];
            IList<Synapse> dendrites = new List<Synapse>();
            foreach (var neuron in neuronMappings.Keys)
            {
                dendrites.Add(neuronMappings[neuron][terminalIndexInLayer]);
            }

            return dendrites;
        }


        public INeuralNetwork Create(Genes.NeuralNetworkGene genes)
        {
            throw new NotImplementedException();
        }
    }
}
