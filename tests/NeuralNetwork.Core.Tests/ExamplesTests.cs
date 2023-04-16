using NeuralNetwork.Core.ActivationFunctions;
using NeuralNetwork.Core.Factories;
using NeuralNetwork.Core.WeightInitializer;

namespace NeuralNetwork.Core.Tests;

public class ExamplesTests
{
    [Fact]
    public void NeuralNetwork()
    {
        RandomWeightInitializer randomInit = new RandomWeightInitializer(new Random());

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

        double[] inputs = new double[] { 1.4d, 2.04045d, 4.2049558d };

        network.SetInputs(inputs);
        network.Process();

        double[] outputs = network.GetOutputs();

        outputs.Should().NotBeNull();
        outputs.Should().HaveCount(1);
        double output = outputs.First();

        output.Should().BeApproximately(0.99d, 0.01d);
    }

    [Fact]
    public void NeuralNetwork_Process_Returns_Outputs()
    {
        int numInputs = 3;
        int numOutputs = 1;
        int numHiddenLayers = 1;
        int numNeuronsInHiddenLayer = 5;

        NeuralNetworkFactory factory = NeuralNetworkFactory.GetInstance();

        INeuralNetwork network = factory.Create(numInputs, numOutputs, numHiddenLayers, numNeuronsInHiddenLayer);

        double[] inputs = new double[] { 1.4d, 2.04045d, 4.2049558d };

        network.SetInputs(inputs);
        network.Process();

        double[] outputs = network.GetOutputs();

        outputs.Should().NotBeNull();
        outputs.Should().HaveCount(1);
        double output = outputs.First();

        output.Should().BeApproximately(0.99d, 0.01d);
    }
}