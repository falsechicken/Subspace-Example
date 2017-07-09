using System;
using System.Collections.Generic;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;

using FC.Libs.Subspace;

namespace SubspaceExample1
{
	/**
	 * Implement the subspace interface.
	 */
	public class SubspaceExample1 : RocketPlugin, ISubspaceInterface
	{
		/**
		 * The message object to send.
		 */
		private SubspaceMessage subspaceMessage;

		/**
		 * The channel to send the message to. Plugins that are subscribed to that channel will
		 * get the message.
		 */
		private string messageChannel = "example";

		/**
		 * The message code. Used for quick lookups and can be anything you want
		 * as long as the other plugin knows how to interpret it.
		 */
		private string messageCode = "1";

		/**
		 * Simple delay counter to prevent spamming messages.
		 */
		private int sendDelayCounter;

		public SubspaceExample1 ()
		{
			/**
			 * When creating a message the channel must be provided. Everything else can be "empty".
			 */
			subspaceMessage = new SubspaceMessage(messageChannel, messageCode, "MSG_SENT", "SOME ARGUMENT", new List<UnturnedPlayer>()); 

			/**
			 * Subscribe this plug-in to receive Subspace messages on channel "example".
			 * Any plug-in that implements the ISubspaceInterface can subscribe.
			 */
			Subspace.SubscribeToChannel(messageChannel, this);
		}

		void FixedUpdate()
		{
			/**
			 * Increment delay counter. Just used to prevent spam. Not required.
			 */
			sendDelayCounter++;

			if (sendDelayCounter > 250)
			{
				/**
				 * Send the message through Subspace.
				 */
				Subspace.SendSubspaceMessage(subspaceMessage);

				/**
				 * Reset the delay counter to 0.
				 */
				sendDelayCounter = 0;

			}
		}

		/**
		 * All plug-ins using Subspace must implement this function. It gets called when
		 * a message is sent along a channel.
		 */
		public Boolean ReceiveMessage(SubspaceMessage _message)
		{
			/**
			 * Use some data in the message. Here we are getting the message code. Which is simply
			 * any string you would like to use so other plugins know what "type" of message this is
			 * and do the proper logic. In this case we are using it to see if the message is from the
			 * second example plug-in to prevent showing our own message as well.
			 */
			if (_message.GetMessageCode() == "2") 
			{
				Logger.Log("Received message. Channel " + _message.GetMessageChannel()
				           + " Message Code: " + _message.GetMessageCode()); //Log the message info to console.
			
				/**
			 	 * Stop the message from propagating any further through the interfaces subscribed to the channel.
			 	 */
				return true; 
			}

			return false;
		}
	}
}

