using NeuralNetwork.Core.WeightInitializer;

namespace NeuralNetwork.Core.Factories
{
    public class SynapseFactory : ISynapseFactory
    {
        private readonly IWeightInitializer _weightInitializer;
        private readonly IAxonFactory _axonFactory;

        private SynapseFactory(IWeightInitializer weightInitializer, IAxonFactory axonFactory)
        {
            _weightInitializer = weightInitializer;
            _axonFactory = axonFactory;
        }

        public static ISynapseFactory GetInstance(IWeightInitializer weightInitializer, IAxonFactory axonFactory)
        {
            return new SynapseFactory(weightInitializer, axonFactory);
        }

        public Synapse Create()
        {
            IAxon axon = _axonFactory.Create();
            double weight = _weightInitializer.InitializeWeight();

            return new Synapse(axon, weight);
        }

        public Synapse Create(double weight)
        {
            IAxon axon = _axonFactory.Create();

            return new Synapse(axon, weight);
        }
    }
}