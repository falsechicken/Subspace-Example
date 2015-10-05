using System;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using FC.Libs.Subspace;

namespace SubspaceExample1
{
	public class SubspaceExample1 : RocketPlugin, ISubspaceInterface //Implement the subspace interface.
	{
		private SubspaceMessage subspaceMessage; //The message object to send.

		/**
		 * The channel to send the message to. Plug-ins that are subscribed to that channel will
		 * get the message.
		 */
		private short messageChannel = 0;

		/**
		 * The message code. Used for quick lookups and can be anything you want
		 * as long as the other plugin knows how to interpret it.
		 */
		private int messageCode = 1;

		private int sendDelayCounter; //Simple delay counter to prevent spamming messages.

		public SubspaceExample1 ()
		{
			/**
			 * When creating a message the channel and message code must be provided.
			 * As mentioned above these can be anything you wish.
			 */
			subspaceMessage = new SubspaceMessage(messageChannel, messageCode); 
		}

		void FixedUpdate()
		{
			sendDelayCounter++; //Increment delay counter. Just used to prevent spam. Not required.

			if (sendDelayCounter > 250)
			{
				Subspace.SendSubspaceMessage(subspaceMessage); //Send the message through Subspace.
				sendDelayCounter = 0;
			}
		}

		/**
		 * All plug-ins using Subspace must implement this function. It gets called when
		 * a message is sent along a channel.
		 */
		public void ReceiveMessage(SubspaceMessage _message)
		{
			Logger.Log("Received message. Channel " + _message.GetMessageChannel()
			           + " Message Code: " + _message.GetMessageCode()); //Log the message info to console.
		}
	}
}

