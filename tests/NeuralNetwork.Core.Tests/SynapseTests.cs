namespace NeuralNetwork.Core.Tests;

public class SynapseTests
{
    [Fact]
    public void Ctor_Returns_Valid()
    {
        //Axon
        Mock<IAxon> mock = new();
        Synapse sut = new(mock.Object, 1.0d);

        sut.Should().NotBeNull();
        sut.Should().BeOfType<Synapse>();
        sut.Should().BeAssignableTo<Synapse>();
    }
}