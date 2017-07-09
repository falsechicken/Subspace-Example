using System;
using System.Collections.Generic;

using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;

using FC.Libs.Subspace;

namespace SubspaceExample2
{
	public class SubspaceExample2 : RocketPlugin, ISubspaceInterface
	{
		private SubspaceMessage subspaceMessage;

		private string messageChannel = "example";

		private string messageCode = "2";

		private int sendDelayCounter;

		public SubspaceExample2 ()
		{
			subspaceMessage = new SubspaceMessage(messageChannel, messageCode, "MSG_SENT2", "SOME ARGUMENT2", new List<UnturnedPlayer>()); 

			Subspace.SubscribeToChannel(messageChannel, this);
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

		public Boolean ReceiveMessage(SubspaceMessage _message)
		{
			if (_message.GetMessageCode() == "1")
			{
				Logger.Log("Received message. Channel " + _message.GetMessageChannel() + " Message Code: " + _message.GetMessageCode()); //Log the message info to console.

				/**
			 	 * Stop the message from propagating any further through the interfaces subscribed to the channel.
			 	 */
				return true;
			}

			return false;
		}
	}
}

