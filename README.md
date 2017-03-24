> ***Please note:*** From v8.0.0 this package has been superceded by [Shuttle.Core.Ninject](https://github.com/Shuttle/Shuttle.Core.Ninject).

# Shuttle.Esb.Ninject

Ninject implementation of the `IMessageHandlerFactory` for use with Shuttle.Esb.

# NinjectMessageHandlerFactory

The `NinjectMessageHandlerFactory` inherits from the abstract `MessageHandlerFactory` class in order to implement the `IMessageHandlerFactory` interface.  This class will provide the message handlers from the `StandardKernel`.

~~~c#
	bus = ServiceBus
		.Create
		(
			c => c.MessageHandlerFactory(new NinjectMessageHandlerFactory(new StandardKernel()))
		)
		.Start();
~~~
