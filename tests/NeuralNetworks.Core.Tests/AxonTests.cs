using NeuralNetworks.Core.ActivationFunctions;

namespace NeuralNetworks.Core.Tests;

public class AxonTests
{
    [Fact]
    public void Ctor_Returns_Valid()
    {
        IList<Synapse> terminals = [];
        Mock<IActivationFunction> activationFunctionMock = new();

        Axon sut = new(terminals, activationFunctionMock.Object);

        sut.Should().NotBeNull();
        sut.Should().BeOfType<Axon>();
        sut.Should().BeAssignableTo<IAxon>();
    }

    [Fact]
    public void Sigmoid_Ctor_Returns_Valid()
    {
        IActivationFunction activationFunction = new SigmoidActivationFunction();

        IList<Synapse> terminals = [
            new () { Weight = .5d },
            new () { Weight = .5d },
        ];

        IAxon sut = Axon.GetInstance(terminals, activationFunction);

        sut.Should().NotBeNull();
        sut.Should().BeOfType<Axon>();
        sut.Should().BeAssignableTo<IAxon>();
    }

    [Fact]
    public void GetInstance_Returns_Valid()
    {
        IList<Synapse> terminals = [];
        Mock<IActivationFunction> activationFunctionMock = new();

        IAxon sut = Axon.GetInstance(terminals, activationFunctionMock.Object);

        sut.Should().NotBeNull();
        sut.Should().BeOfType<Axon>();
        sut.Should().BeAssignableTo<IAxon>();
    }
}