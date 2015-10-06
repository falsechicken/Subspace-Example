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
		 * The message title. Used so other plug-ins know what "type" of message this is and 
		 * can be anything you want as long as the other plug-in knows how to interpret it.
		 */
		private string messageTitle = "From_Example_1";

		private int sendDelayCounter; //Simple delay counter to prevent spamming messages.

		public SubspaceExample1 ()
		{
			/**
			 * When creating a message the channel and message title must be provided.
			 * As mentioned above these can be anything you wish.
			 */
			subspaceMessage = new SubspaceMessage(messageChannel, messageTitle); 

			/**
			 * Subscribe this plug-in to receive Subspace messages on channel 0.
			 * Any plug-in that implements the ISubspaceInterface can subscribe.
			 */
			Subspace.SubscribeToChannel(0, this);
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
		 * a message is sent along a channel the plug-in is subscribed to.
		 */
		public void ReceiveMessage(SubspaceMessage _message)
		{
			/**
			 * Use some data in the message. Here we are getting the message title. Which is simply
			 * any string you would like to use so other plugins know what "type" of message this is
			 * and do the proper logic. In this case we are using it to see if the message is from the
			 * second example plug-in to prevent showing our own message as well.
			 */
			if (_message.GetMessageTitle().Equals("From_Example_2")) 
			{
				Logger.Log("Received message. Channel " + _message.GetMessageChannel()
				           + " | Message Title: " + _message.GetMessageTitle()); //Log the message info to console.
			}
		}
	}
}

