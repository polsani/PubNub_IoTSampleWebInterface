using System;
using PubNubMessaging.Core;

namespace PubNub_IoTSampleWebInterface.Services
{
    public class MessageService
    {
        private const string PUBLISH_KEY = "demo";
        private const string SUBSCRIBE_KEY = "demo";
        private const string CHANNEL_NAME = "ChannelArduino1";

        private string _message = "Default message";

        private readonly Pubnub _pubNub;
        
        public MessageService()
        {
            _pubNub = new Pubnub(PUBLISH_KEY, SUBSCRIBE_KEY);
        }

        public void SendMessage(string message)
        {
            _message = message;
            _pubNub.Subscribe<string>(CHANNEL_NAME, DisplaySubscribeReturnMessage, DisplaySubscribeConnectStatusMessage, DisplayErrorMessage);
        }

        private void DisplaySubscribeConnectStatusMessage(string connectMessage)
        {
            _pubNub.Publish<string>(CHANNEL_NAME, _message, DisplayReturnMessage, DisplayErrorMessage);
        }

        private void DisplaySubscribeReturnMessage(string result)
        {
            if (string.IsNullOrEmpty(result) || string.IsNullOrEmpty(result.Trim())) return;

            var deserializedMessage = _pubNub.JsonPluggableLibrary.DeserializeToListOfObject(result);

            if (deserializedMessage == null || deserializedMessage.Count <= 0) return;

            var subscribedObject = deserializedMessage[0];
            if (subscribedObject != null)
            {
                //IF CUSTOM OBJECT IS EXCEPTED, YOU CAN CAST THIS OBJECT TO YOUR CUSTOM CLASS TYPE
                var resultActualMessage = _pubNub.JsonPluggableLibrary.SerializeToJsonString(subscribedObject);
            }
        }

        private void DisplayErrorMessage(PubnubClientError pubnubError)
        {
            Console.WriteLine(pubnubError.StatusCode);
        }

        private void DisplayReturnMessage(string result)
        {
            Console.WriteLine("PUBLISH STATUS CALLBACK");
            Console.WriteLine(result);
        }
    }
}