using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

using Grpc.Core;
using MagicOnion;
using MagicOnion.Client;
using ServerShared.Hubs;
using ServerShared.Services;


namespace AppLifecycle
{
    /// <summary>
    /// Class to handle, validate, and connect to a specific session given by a QR code deep link
    /// </summary>
    public class Session : MonoBehaviour, IVeldtHubReceiver
    {
        private CancellationTokenSource _shutdownCancellation = new CancellationTokenSource();
        private ChannelBase _channel;

        private static string s_mySession;

        private IVeldtHub streamingClient;
        private IVeldtService client;

        public GameEvent onConnected;
        public GameEvent onQRCodeInvalid;
        public GameEvent onSessionExpired;
        public GameEvent onSessionInvalid;
        public GameEvent onUserJoined;

        public static string modelURL;
        public TMP_InputField nameInputField;


        #region Handling and validation of QR code and sessionID
        // Check if the QR code is in the correct format
        // veldtar:sessionID
        private bool QRCodeValid(string qrcode)
        {
            string[] deepLink = qrcode.Split(":");

            if (deepLink.Length == 2)
            {
                Debug.Log("QR code valid");
                return true;
            }
            else
            {
                Debug.Log("QR code invalid or unexpected format");
                onQRCodeInvalid.TriggerEvent();
                return false;
            }
        }


        // QR code deep link processing, which splits the deep link and sets the 
        // parameters respectivly. 
        private void onDeepLinkActivated(string qrcode)
        {
            string[] deepLink = qrcode.Split(":");
            s_mySession = deepLink[1];
        }


        // Check sessionID via response code from GET request
        private bool CheckSessionID()
        {
            var sessionURL = "https://funcveldtdev.azurewebsites.net/api/file/check/" + s_mySession;
            Debug.Log($"SessionID " + s_mySession);
            Debug.Log($"SessionURL " + sessionURL);
            using var webRequest = UnityWebRequest.Get(sessionURL);
            var operation = webRequest.SendWebRequest();

            // Waiting for get request to finish, but only print it once to log
            bool flag = false;
            while (!operation.isDone)
                if (!flag)
                {
                    Debug.Log($"Waiting for GET request to finish");
                    flag = true;
                }

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Session " + s_mySession + " is valid.");
                modelURL = "https://funcveldtdev.azurewebsites.net/api/file/download/" + s_mySession;
                return true;
            }
            else if (webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"Session " + s_mySession + " is not valid.");
                Debug.LogError($"Error\n" + webRequest.error);
                onSessionInvalid.TriggerEvent();
                return false;
            }
            // **************************  TODO  **********************************
            // Specific Error for expired session
            /*
            else if (webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"Session expired.");
                Debug.LogError($"Error\n" + webRequest.error);
                onSessionExpired.TriggerEvent();
                return false;
            }
            */
            else
            {
                Debug.LogError($"Error\n" + webRequest.error);
                return false;
            }
        }


        // If QR code or session are either invalid or expired, start error handling.
        // The user can now either exit the app or enter a valid sessionID manually.
        public void InvalidHandling()
        {
            s_mySession = "";
            Debug.Log("Please visit www.veldt.ar to get a valid QR code or enter a valid session ID manually.");
        }


        void UpdateInputField(string sessionID)
        {
            s_mySession = sessionID;
            Debug.Log("Set session ID manually " + s_mySession);
        }
        #endregion



        #region Server connection
        // Start server connection after entering session ID manually, including sessionID check
        public async void setIDmanually()
        {
            // Validate current session ID
            if (CheckSessionID())
            {
                await this.InitializeClientAsync();
                SendMessage("Connection to server is established.");
                OnJoin();
            }
        }


        // Initialize connection to server
        private async Task InitializeClientAsync()
        {
            // Initialize the Hub
            // NOTE: If you want to use SSL/TLS connection, see InitialSettings.OnRuntimeInitialize method.
            this._channel = GrpcChannelx.ForAddress(s_mySession);

            // TODO: Add cancel Session initalization or automatic
            // error handling if no connection is possible
            while (!_shutdownCancellation.IsCancellationRequested)
            {
                try
                {
                    Debug.Log($"Connecting to the server...");
                    this.streamingClient = await StreamingHubClient.ConnectAsync<IVeldtHub, IVeldtHubReceiver>(this._channel, this, cancellationToken: _shutdownCancellation.Token);

                    Debug.Log($"Connection is established.");
                    onConnected.TriggerEvent();
                    break;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

                Debug.Log($"Failed to connect to the server. Retry after 5 seconds...");
                await Task.Delay(5 * 1000);
            }
            this.client = MagicOnionClient.Create<IVeldtService>(this._channel);
        }
        #endregion


        #region Unity lifecycle functions
        private void Awake()
        {
            // Set up listener for manually entered sessionID from text field
            nameInputField.onValueChanged.AddListener(UpdateInputField);
        }


        // Validate QR code deep link as well as session ID
        // If session is valid initiate server communication
        private async void Start()
        {
            string qrcode;

            // Only for testing in unity editor
            #if UNITY_EDITOR
                // qrcode = "veldtar:X:X25";        // QR cdode invalid
                // qrcode = "veldtar:111111";       // Session invalid
                // qrcode = "veldtar:111123";       // Session expired
                qrcode = "veldtar:e166f98a-40de-4949-afb1-26599bcc8f3e";                // Session valid
    
                if (QRCodeValid(qrcode))
                {
                    // Handle deep link and set variables respectively
                    onDeepLinkActivated(qrcode);
                    
                    // Validate current session ID as well as the expiration date
                    if (CheckSessionID())
                    {
                        // Connect to session
                        await this.InitializeClientAsync();
                        OnJoin();
                        SendMessage("Connection to server is established.");
                    }
                }
            #else
                // QR code deep link processing
                Application.deepLinkActivated += onDeepLinkActivated;
                qrcode = Application.absoluteURL;

                if (QRCodeValid(qrcode))
                {
                    // Handle deep link and set variables respectively
                    onDeepLinkActivated(qrcode);

                    // Validate current session ID 
                    if (CheckSessionID())
                    {
                        // Connect to session
                        await this.InitializeClientAsync();
                        OnJoin();
                        SendMessage("Connection to server is established.");
                    }
                }
            #endif
        }


        // Handle cleanup at exit
        // TODO: Not used so far
        private async void OnDestroy()
        {
            // Clean up Hub and _channel
            OnLeave();

            this._shutdownCancellation.Cancel();

            if (streamingClient != null)
            {
                await streamingClient.DisposeAsync();
            }
            if (this._channel != null)
            {
                await this._channel.ShutdownAsync();
            }
        }
        #endregion


        // TODO check again when multiplayer mode active
        #region Server -> Client (Streaming)
        public void OnJoin()
        {
            SendMessage("Connection to server is established.");
            SendMessage("User entered the room.");
            Debug.Log("User entered the room.");
        }


        public void OnLeave()
        {
            SendMessage("User left the room.");
            Debug.Log("User left the room.");
        }

        /*
        public void OnSendMessage(MessageResponse message)
        {
            Debug.Log($"{message.UserName} : {message.Message} : {message.UserCount}");
        }
        */
        #endregion


        #region Client -> Server (Unary)
        public async void SendMessage(string message)
        {
            await this.client.SendReportAsync(message);
        }
        #endregion
    }
}
    