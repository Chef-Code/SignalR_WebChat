/*!
 * ASP.NET SignalR JavaScript Library v2.2.1
 * http://signalr.net/
 *
 * Copyright (c) .NET Foundation. All rights reserved.
 * Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
 *
 */

/// <reference path="..\..\SignalR.Client.JS\Scripts\jquery-1.6.4.js" />
/// <reference path="jquery.signalR.js" />
(function ($, window, undefined) {
    /// <param name="$" type="jQuery" />
    "use strict";

    if (typeof ($.signalR) !== "function") {
        throw new Error("SignalR: SignalR is not loaded. Please ensure jquery.signalR-x.js is referenced before ~/signalr/js.");
    }

    var signalR = $.signalR;

    function makeProxyCallback(hub, callback) {
        return function () {
            // Call the client hub method
            callback.apply(hub, $.makeArray(arguments));
        };
    }

    function registerHubProxies(instance, shouldSubscribe) {
        var key, hub, memberKey, memberValue, subscriptionMethod;

        for (key in instance) {
            if (instance.hasOwnProperty(key)) {
                hub = instance[key];

                if (!(hub.hubName)) {
                    // Not a client hub
                    continue;
                }

                if (shouldSubscribe) {
                    // We want to subscribe to the hub events
                    subscriptionMethod = hub.on;
                } else {
                    // We want to unsubscribe from the hub events
                    subscriptionMethod = hub.off;
                }

                // Loop through all members on the hub and find client hub functions to subscribe/unsubscribe
                for (memberKey in hub.client) {
                    if (hub.client.hasOwnProperty(memberKey)) {
                        memberValue = hub.client[memberKey];

                        if (!$.isFunction(memberValue)) {
                            // Not a client hub function
                            continue;
                        }

                        subscriptionMethod.call(hub, memberKey, makeProxyCallback(hub, memberValue));
                    }
                }
            }
        }
    }

    $.hubConnection.prototype.createHubProxies = function () {
        var proxies = {};
        this.starting(function () {
            // Register the hub proxies as subscribed
            // (instance, shouldSubscribe)
            registerHubProxies(proxies, true);

            this._registerSubscribedHubs();
        }).disconnected(function () {
            // Unsubscribe all hub proxies when we "disconnect".  This is to ensure that we do not re-add functional call backs.
            // (instance, shouldSubscribe)
            registerHubProxies(proxies, false);
        });

        proxies['chat'] = this.createHubProxy('chat'); 
        proxies['chat'].client = { };
        proxies['chat'].server = {
            joinGroupAsync: function (groupName) {
            /// <summary>Calls the JoinGroupAsync method on the server-side chat hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupName\" type=\"String\">Server side type is System.String</param>
                return proxies['chat'].invoke.apply(proxies['chat'], $.merge(["JoinGroupAsync"], $.makeArray(arguments)));
             },

            joinRoom: function (room) {
            /// <summary>Calls the JoinRoom method on the server-side chat hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"room\" type=\"String\">Server side type is System.String</param>
                return proxies['chat'].invoke.apply(proxies['chat'], $.merge(["JoinRoom"], $.makeArray(arguments)));
             },

            leaveGroupAsync: function (groupName) {
            /// <summary>Calls the LeaveGroupAsync method on the server-side chat hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"groupName\" type=\"String\">Server side type is System.String</param>
                return proxies['chat'].invoke.apply(proxies['chat'], $.merge(["LeaveGroupAsync"], $.makeArray(arguments)));
             },

            sendMessage: function (message) {
            /// <summary>Calls the SendMessage method on the server-side chat hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['chat'].invoke.apply(proxies['chat'], $.merge(["SendMessage"], $.makeArray(arguments)));
             },

            sendMessageData: function (data) {
            /// <summary>Calls the SendMessageData method on the server-side chat hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"data\" type=\"Object\">Server side type is SignalR_WebChat.SendData</param>
                return proxies['chat'].invoke.apply(proxies['chat'], $.merge(["SendMessageData"], $.makeArray(arguments)));
             },

            sendMessageToRoom: function (room, message) {
            /// <summary>Calls the SendMessageToRoom method on the server-side chat hub.&#10;Returns a jQuery.Deferred() promise.</summary>
            /// <param name=\"room\" type=\"String\">Server side type is System.String</param>
            /// <param name=\"message\" type=\"String\">Server side type is System.String</param>
                return proxies['chat'].invoke.apply(proxies['chat'], $.merge(["SendMessageToRoom"], $.makeArray(arguments)));
             }
        };

        proxies['monitor'] = this.createHubProxy('monitor'); 
        proxies['monitor'].client = { };
        proxies['monitor'].server = {
        };

        return proxies;
    };

    signalR.hub = $.hubConnection("/signalr", { useDefaultPath: false });
    $.extend(signalR, signalR.hub.createHubProxies());

}(window.jQuery, window));