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
