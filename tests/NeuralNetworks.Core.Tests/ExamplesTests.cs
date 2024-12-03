using NeuralNetworks.Core.ActivationFunctions;
using NeuralNetworks.Core.Factories;
using NeuralNetworks.Core.Genes;
using NeuralNetworks.Core.WeightInitializer;

namespace NeuralNetworks.Core.Tests;

public class ExamplesTests
{
    [Fact]
    public void Another_Custom_NeuralNetwork()
    {
        ISummationFunction simpleSum = new SimpleSummation();
        ISomaFactory simpleSumSomaFactory = SomaFactory.GetInstance(simpleSum);

        IActivationFunction tanhActivation = new TanhActivationFunction();
        IAxonFactory tanhAxonFactory = AxonFactory.GetInstance(tanhActivation);

        Random random = new();
        RandomWeightInitializer randomWeightInit = new(random);

        ISynapseFactory hiddenSynapseFactory = SynapseFactory.GetInstance(randomWeightInit, tanhAxonFactory);

        ConstantWeightInitializer constantWeightInit = new(1.0);

        IActivationFunction identityActivation = new IdentityActivationFunction();
        IAxonFactory identityAxonFactory = AxonFactory.GetInstance(identityActivation);

        ISynapseFactory ioSynapseFactory = SynapseFactory.GetInstance(constantWeightInit, identityAxonFactory);

        INeuronFactory neuronFactory = NeuronFactory.GetInstance();

        NeuralNetworkFactory factory = NeuralNetworkFactory.GetInstance(simpleSumSomaFactory, tanhAxonFactory, hiddenSynapseFactory, ioSynapseFactory, randomWeightInit, neuronFactory);

        int numInputs = 3;
        int numOutputs = 1;
        int numHiddenLayers = 1;
        int numNeuronsInHiddenLayer = 5;

        INeuralNetwork network1 = factory.Create(numInputs, numOutputs, numHiddenLayers, numNeuronsInHiddenLayer);
        INeuralNetwork network2 = factory.Create(numInputs, numOutputs, numHiddenLayers, numNeuronsInHiddenLayer);

        double[] inputs = [1.4d, 2.04045d, 4.2049558d];

        network1.SetInputs(inputs);
        network1.Process();

        double[] outputs = network1.GetOutputs();

        outputs.Should().NotBeNull();
        outputs.Should().HaveCount(1);
        double output = outputs.First();

        output.Should().BeApproximately(0.99d, 0.01d);

        NeuralNetworkGene genes1_1 = network1.GetGenes();
        NeuralNetworkGene genes1_2 = network1.GetGenes();

        genes1_1.Should().BeEquivalentTo(genes1_2);

        NeuralNetworkGene genes2_1 = network2.GetGenes();
        genes1_1.Should().NotBeEquivalentTo(genes2_1);
    }

    [Fact]
    public void Custom_NeuralNetwork()
    {
        RandomWeightInitializer randomInit = new(new Random());

        ISomaFactory somaFactory = SomaFactory.GetInstance(new SimpleSummation());
        IAxonFactory axonFactory = AxonFactory.GetInstance(new TanhActivationFunction());
        ISynapseFactory hiddenSynapseFactory = SynapseFactory.GetInstance(randomInit, axonFactory);
        ISynapseFactory ioSynapseFactory = SynapseFactory.GetInstance(new ConstantWeightInitializer(1.0), axonFactory);
        INeuronFactory neuronFactory = NeuronFactory.GetInstance();

        RandomWeightInitializer biasInitializer = randomInit;

        INeuralNetworkFactory factory = NeuralNetworkFactory.GetInstance(somaFactory, axonFactory, hiddenSynapseFactory, ioSynapseFactory, biasInitializer, neuronFactory);

        int numInputs = 3;
        int numOutputs = 1;
        int numHiddenLayers = 1;
        int numNeuronsInHiddenLayer = 5;
        INeuralNetwork network = factory.Create(numInputs, numOutputs, numHiddenLayers, numNeuronsInHiddenLayer);

        double[] inputs = [1.4d, 2.04045d, 4.2049558d];

        network.SetInputs(inputs);
        network.Process();

        double[] outputs = network.GetOutputs();

        outputs.Should().NotBeNull();
        outputs.Should().HaveCount(1);
        double output = outputs.First();

        output.Should().BeApproximately(0.99d, 0.01d);
    }

    [Fact]
    public void Default_NeuralNetwork()
    {
        int numInputs = 3;
        int numOutputs = 1;
        int numHiddenLayers = 1;
        int numNeuronsInHiddenLayer = 5;

        NeuralNetworkFactory factory = NeuralNetworkFactory.GetInstance();

        INeuralNetwork network = factory.Create(numInputs, numOutputs, numHiddenLayers, numNeuronsInHiddenLayer);

        double[] inputs = [1.4d, 2.04045d, 4.2049558d];

        network.SetInputs(inputs);
        network.Process();

        double[] outputs = network.GetOutputs();

        outputs.Should().NotBeNull();
        outputs.Should().HaveCount(1);
        double output = outputs.First();

        output.Should().BeApproximately(0.99d, 0.01d);
    }
}