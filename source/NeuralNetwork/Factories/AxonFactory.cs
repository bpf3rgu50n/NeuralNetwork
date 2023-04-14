﻿using NeuralNetwork.Core.ActivationFunctions;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core.Factories
{
    public class AxonFactory : IAxonFactory
    {
        private readonly IActivationFunction _activationFunction;

        private AxonFactory(IActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
        }

        public static IAxonFactory GetInstance(IActivationFunction activationFunction)
        {
            return new AxonFactory(activationFunction);
        }

        public IAxon Create(IList<Synapse> terminals)
        {
            return Axon.GetInstance(terminals, _activationFunction);
        }

        public IAxon Create()
        {
            return Axon.GetInstance(new List<Synapse>(), _activationFunction);
        }

        public IAxon Create(IList<Synapse> terminals, Type activationFunction)
        {
            object functionObj = Activator.CreateInstance(activationFunction);
            if (!(functionObj is IActivationFunction))
            {
                throw new NotSupportedException(
                    $"{activationFunction} is not a supported activation function type for Create() as it does not implement IActivationFunction");
            }
            IActivationFunction function = functionObj as IActivationFunction;
            return Axon.GetInstance(terminals, function);
        }
    }
}