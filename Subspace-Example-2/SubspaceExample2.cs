using System;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using FC.Libs.Subspace;

namespace SubspaceExample2
{
	public class SubspaceExample2 : RocketPlugin, ISubspaceInterface
	{
		private SubspaceMessage subspaceMessage;

		private short messageChannel = 0;

		private string messageTitle = "From_Example_2";

		private int sendDelayCounter;

		public SubspaceExample2 ()
		{
			subspaceMessage = new SubspaceMessage(messageChannel, messageTitle);

			Subspace.SubscribeToChannel(0, this);
		}

		void FixedUpdate()
		{
			sendDelayCounter++;

			if (sendDelayCounter > 250)
			{
				Subspace.SendSubspaceMessage(subspaceMessage);
				sendDelayCounter = 0;
			}
		}

		public void ReceiveMessage(SubspaceMessage _message)
		{
			if (_message.GetMessageTitle().Equals("From_Example_1"))
			{
				Logger.Log("Received message. Channel " 
				           + _message.GetMessageChannel() + " | Message Title: " + _message.GetMessageTitle());
			}
		}
	}
}

