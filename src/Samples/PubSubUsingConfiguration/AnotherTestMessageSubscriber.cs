﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectExtensions.Azure.ServiceBus;
using NLog;
using System.Diagnostics;
using Microsoft.ServiceBus.Messaging;

namespace PubSubUsingConfiguration {

    [MessageHandlerConfiguration(
        DefaultMessageTimeToLive = 240, 
        LockDuration = 120, 
        MaxRetries = 2, 
        PrefetchCount = 20, 
        ReceiveMode = ReceiveMode.PeekLock, 
        Singleton=true)]
    public class AnotherTestMessageSubscriber : IHandleCompetingMessages<AnotherTestMessage> {

        static Logger logger = LogManager.GetCurrentClassLogger();

        static bool timerSet = false;

        static Stopwatch sw = new Stopwatch();

        public void Handle(IReceivedMessage<AnotherTestMessage> message) {
            if (!timerSet) {
                sw.Start();
                timerSet = true;
            }

            logger.Info("AnotherTestMessageSubscriber Message received: {0} {1} {2}", message.Message.Value, message.Message.MessageId, sw.ElapsedMilliseconds);
        }
    }
}
