using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;


namespace AppLifecycle
{
    /// <summary>
    /// Class to handle shutdown procedure of the app, without leaving an 
    /// application window open on the hololens. 
    /// </summary>
    public class ShutdownHandling: MonoBehaviour
    {
        public async void QuitApp()
        {
            Debug.Log($"Quit Application");
            // Wait 1 second to play "approved" sound
            await Task.Delay(1000);

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }
}
