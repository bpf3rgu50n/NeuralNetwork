using NeuralNetwork.Core.ActivationFunctions;
using NeuralNetwork.Core.Genes;
using NeuralNetwork.Core.WeightInitializer;

namespace NeuralNetwork.Core.Factories;

public class NeuralNetworkFactory : INeuralNetworkFactory
{
    private readonly ISomaFactory _somaFactory;
    private readonly IAxonFactory _axonFactory;
    private readonly ISynapseFactory _hiddenSynapseFactory;
    private readonly ISynapseFactory _inputOutputSynapseFactory;
    private readonly IWeightInitializer _biasInitiliazer;
    private readonly INeuronFactory _neuronFactory;

    private NeuralNetworkFactory(ISomaFactory somaFactory, IAxonFactory axonFactory, ISynapseFactory hiddenSynapseFactory, ISynapseFactory inputOutputSynapseFactory, IWeightInitializer biasInitializer, INeuronFactory neuronFactory)
    {
        _somaFactory = somaFactory;
        _axonFactory = axonFactory;
        _hiddenSynapseFactory = hiddenSynapseFactory;
        _inputOutputSynapseFactory = inputOutputSynapseFactory;
        _biasInitiliazer = biasInitializer;
        _neuronFactory = neuronFactory;
    }

    public static NeuralNetworkFactory GetInstance(ISomaFactory somaFactory, IAxonFactory axonFactory, ISynapseFactory hiddenSynapseFactory, ISynapseFactory inputOutputSynapseFactory, IWeightInitializer biasInitializer, INeuronFactory neuronFactory)
    {
        return new NeuralNetworkFactory(somaFactory, axonFactory, hiddenSynapseFactory, inputOutputSynapseFactory, biasInitializer, neuronFactory);
    }

    public static NeuralNetworkFactory GetInstance()
    {
        ISomaFactory somaFactory = SomaFactory.GetInstance(new SimpleSummation());
        IAxonFactory axonFactory = AxonFactory.GetInstance(new TanhActivationFunction());
        Random random = new();
        RandomWeightInitializer randomInit = new(random);
        ISynapseFactory synapseFactory = SynapseFactory.GetInstance(randomInit, axonFactory);
        ISynapseFactory ioSynapseFactory = SynapseFactory.GetInstance(new ConstantWeightInitializer(1.0), AxonFactory.GetInstance(new IdentityActivationFunction()));
        INeuronFactory neuronFactory = NeuronFactory.GetInstance();
        return new NeuralNetworkFactory(somaFactory, axonFactory, synapseFactory, ioSynapseFactory, randomInit, neuronFactory);
    }

    internal INeuron CreateNeuron(Dictionary<int, Dictionary<int, IList<Synapse>>> mapping, int layerIndex, int neuronIndex)
    {
        IList<Synapse> dendrites = (layerIndex > 0) ? GetDendritesForSoma(layerIndex, neuronIndex, mapping) : mapping[layerIndex][neuronIndex];

        ISoma soma = _somaFactory.Create(dendrites, _biasInitiliazer.InitializeWeight());

        IList<Synapse> terminals = mapping[layerIndex + 1][neuronIndex];
        IAxon axon = _axonFactory.Create(terminals);

        return _neuronFactory.Create(soma, axon);
    }

    //Used for input/output lists
    internal static IList<Synapse> GetAllSynapsesFromLayerMapping(Dictionary<int, IList<Synapse>> layerMapping)
    {
        IList<Synapse> synapses = new List<Synapse>();
        foreach (int key in layerMapping.Keys)
        {
            IList<Synapse> terminals = layerMapping[key];
            foreach (Synapse terminal in terminals)
            {
                synapses.Add(terminal);
            }
        }
        return synapses;
    }

    internal ILayer CreateLayer(Dictionary<int, Dictionary<int, IList<Synapse>>> synapseMapping, int layerInNetwork, int numberOfNeurons)
    {
        IList<INeuron> layerNeurons = new List<INeuron>();
        for (int i = 0; i < numberOfNeurons; i++)
        {
            layerNeurons.Add(CreateNeuron(synapseMapping, layerInNetwork, i));
        }
        return Layer.GetInstance(layerNeurons);
    }

    public INeuralNetwork Create(int numInputs, int numOutputs, int numHiddenLayers, int numHiddenPerLayer)
    {
        //layer number + position in layer --> list of terminals
        Dictionary<int, Dictionary<int, IList<Synapse>>> mapping = CreateSynapses(numInputs, numOutputs, numHiddenLayers, numHiddenPerLayer);

        IList<Synapse> inputs = GetAllSynapsesFromLayerMapping(mapping[0]);
        IList<Synapse> outputs = GetAllSynapsesFromLayerMapping(mapping[mapping.Keys.Count - 1]);

        ILayer inputLayer = CreateLayer(mapping, 0, numInputs);

        //Hidden layers
        IList<ILayer> hiddenLayers = new List<ILayer>();
        for (int h = 0; h < numHiddenLayers; h++)
        {
            hiddenLayers.Add(CreateLayer(mapping, h + 1, numHiddenPerLayer));
        }

        ILayer outputLayer = CreateLayer(mapping, numHiddenLayers + 1, numOutputs);

        return NeuralNetwork.GetInstance(inputs, inputLayer, hiddenLayers, outputLayer, outputs);
    }

    public INeuralNetwork Create(int numInputs, int numOutputs, IList<int> hiddenLayerSpecs)
    {
        //layer number + position in layer --> list of terminals
        Dictionary<int, Dictionary<int, IList<Synapse>>> mapping = CreateSynapses(numInputs, numOutputs, hiddenLayerSpecs);

        IList<Synapse> inputs = GetAllSynapsesFromLayerMapping(mapping[0]);
        IList<Synapse> outputs = GetAllSynapsesFromLayerMapping(mapping[mapping.Keys.Count - 1]);

        ILayer inputLayer = CreateLayer(mapping, 0, numInputs);

        //Hidden layers
        IList<ILayer> hiddenLayers = new List<ILayer>();
        for (int h = 0; h < hiddenLayerSpecs.Count; h++)
        {
            hiddenLayers.Add(CreateLayer(mapping, h + 1, hiddenLayerSpecs[h]));
        }

        ILayer outputLayer = CreateLayer(mapping, hiddenLayerSpecs.Count + 1, numOutputs);

        return NeuralNetwork.GetInstance(inputs, inputLayer, hiddenLayers, outputLayer, outputs);
    }

    internal Dictionary<int, Dictionary<int, IList<Synapse>>> CreateSynapses(int numInputs, int numOutputs, IList<int> hiddenLayerSpecs)
    {
        //layer number + position in layer --> list of terminals
        Dictionary<int, Dictionary<int, IList<Synapse>>> mapping = new()
        {
            //Input synapses
            [0] = CreateSynapseMapLayer(_inputOutputSynapseFactory, numInputs, 1),

            //Input neuron terminals
            [1] = CreateSynapseMapLayer(_hiddenSynapseFactory, numInputs, hiddenLayerSpecs[0])
        };

        //Hidden layers 0 to (n-1)
        for (int h = 0; h < hiddenLayerSpecs.Count - 1; h++)
        {
            mapping[h + 2] = CreateSynapseMapLayer(_hiddenSynapseFactory, hiddenLayerSpecs[h], hiddenLayerSpecs[h + 1]);
        }

        //Hidden layer n
        mapping[hiddenLayerSpecs.Count + 1] = CreateSynapseMapLayer(_hiddenSynapseFactory, hiddenLayerSpecs[hiddenLayerSpecs.Count - 1], numOutputs);

        //Output layer
        mapping[hiddenLayerSpecs.Count + 2] = CreateSynapseMapLayer(_inputOutputSynapseFactory, numOutputs, 1);

        return mapping;
    }

    internal Dictionary<int, Dictionary<int, IList<Synapse>>> CreateSynapses(int numInputs, int numOutputs, int numHiddenLayers, int numHiddenPerLayer)
    {
        List<int> hiddenSpecs = new();
        for (int i = 0; i < numHiddenLayers; i++)
        {
            hiddenSpecs.Add(numHiddenPerLayer);
        }
        return CreateSynapses(numInputs, numOutputs, hiddenSpecs);
    }

    internal static Dictionary<int, IList<Synapse>> CreateSynapseMapLayer(ISynapseFactory synapseFactory, int numberOfNeuronsInLayer, int numberOfTerminalsPerNeuron)
    {
        Dictionary<int, IList<Synapse>> mapLayer = new();
        for (int i = 0; i < numberOfNeuronsInLayer; i++)
        {
            mapLayer[i] = CreateTerminals(synapseFactory, numberOfTerminalsPerNeuron);
        }
        return mapLayer;
    }

    internal static IList<Synapse> CreateTerminals(ISynapseFactory synapseFactory, int numberOfSynapses)
    {
        List<Synapse> terminals = new();
        for (int t = 0; t < numberOfSynapses; t++)
        {
            terminals.Add(synapseFactory.Create());
        }
        return terminals;
    }

    private static IList<Synapse> GetDendritesForSoma(int layer, int terminalIndexInNeuron, Dictionary<int, Dictionary<int, IList<Synapse>>> mapping)
    {
        //get entire layer before, then grab the nth synapse from each list
        if (layer < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(layer), "layer must be > 0");
        }

        Dictionary<int, IList<Synapse>> neuronMappings = mapping[layer];
        IList<Synapse> dendrites = new List<Synapse>();
        foreach (int neuron in neuronMappings.Keys)
        {
            dendrites.Add(neuronMappings[neuron][terminalIndexInNeuron]);
        }

        return dendrites;
    }

    public INeuralNetwork Create(NeuralNetworkGene genes)
    {
        Dictionary<int, Dictionary<int, IList<Synapse>>> mapping = CreateSynapsesFromGenes(genes);

        IList<Synapse> inputs = GetAllSynapsesFromLayerMapping(mapping[0]);
        IList<Synapse> outputs = GetAllSynapsesFromLayerMapping(mapping[mapping.Keys.Count - 1]);

        ILayer inputLayer = CreateLayerFromGene(genes.InputGene, mapping, 0);

        //Hidden layers
        IList<ILayer> hiddenLayers = new List<ILayer>();
        for (int h = 0; h < genes.HiddenGenes.Count; h++)
        {
            hiddenLayers.Add(CreateLayerFromGene(genes.HiddenGenes[h], mapping, h + 1));
        }

        ILayer outputLayer = CreateLayerFromGene(genes.OutputGene, mapping, genes.HiddenGenes.Count + 1);

        return NeuralNetwork.GetInstance(inputs, inputLayer, hiddenLayers, outputLayer, outputs);
    }

    internal Dictionary<int, Dictionary<int, IList<Synapse>>> CreateSynapsesFromGenes(NeuralNetworkGene genes)
    {
        //layer number + position in layer --> list of terminals
        Dictionary<int, Dictionary<int, IList<Synapse>>> mapping = new()
        {
            //Input synapses
            [0] = CreateSynapseMapLayer(_inputOutputSynapseFactory, genes.InputGene.Neurons.Count, 1),

            //Input neuron terminals
            [1] = CreateSynapseMapLayerFromLayerGene(_inputOutputSynapseFactory, genes.InputGene)
        };

        //Hidden layers
        for (int h = 0; h < genes.HiddenGenes.Count; h++)
        {
            mapping[h + 2] = CreateSynapseMapLayerFromLayerGene(_hiddenSynapseFactory, genes.HiddenGenes[h]);
        }

        //Output layer
        mapping[genes.HiddenGenes.Count + 2] = CreateSynapseMapLayer(_hiddenSynapseFactory, genes.OutputGene.Neurons.Count, 1);

        return mapping;
    }

    internal static Dictionary<int, IList<Synapse>> CreateSynapseMapLayerFromLayerGene(ISynapseFactory synapseFactory, LayerGene layerGene)
    {
        Dictionary<int, IList<Synapse>> mapLayer = new();
        for (int i = 0; i < layerGene.Neurons.Count; i++)
        {
            mapLayer[i] = CreateTerminalsFromWeightList(synapseFactory, layerGene.Neurons[i].Axon.Weights);
        }
        return mapLayer;
    }

    internal static IList<Synapse> CreateTerminalsFromWeightList(ISynapseFactory synapseFactory, IList<double> weights)
    {
        List<Synapse> terminals = new();
        for (int t = 0; t < weights.Count; t++)
        {
            terminals.Add(synapseFactory.Create(weights[t]));
        }
        return terminals;
    }

    internal ILayer CreateLayerFromGene(LayerGene layerGene, Dictionary<int, Dictionary<int, IList<Synapse>>> synapseMapping, int layerInNetwork)
    {
        IList<INeuron> layerNeurons = new List<INeuron>();
        for (int i = 0; i < layerGene.Neurons.Count; i++)
        {
            layerNeurons.Add(CreateNeuronFromGene(layerGene.Neurons[i], synapseMapping, layerInNetwork, i));
        }
        return Layer.GetInstance(layerNeurons);
    }

    internal INeuron CreateNeuronFromGene(NeuronGene neuronGene, Dictionary<int, Dictionary<int, IList<Synapse>>> mapping, int layerIndex, int neuronIndex)
    {
        IList<Synapse> dendrites = (layerIndex > 0) ? GetDendritesForSoma(layerIndex, neuronIndex, mapping) : mapping[layerIndex][neuronIndex];

        ISoma soma = _somaFactory.Create(dendrites, neuronGene.Soma.Bias, neuronGene.Soma.SummationFunction);

        IList<Synapse> terminals = mapping[layerIndex + 1][neuronIndex];
        IAxon axon = _axonFactory.Create(terminals, neuronGene.Axon.ActivationFunction);

        return _neuronFactory.Create(soma, axon);
    }
}