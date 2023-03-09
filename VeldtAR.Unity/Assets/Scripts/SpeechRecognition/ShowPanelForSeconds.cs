using System;
using System.Collections;
using UnityEngine;


namespace SpeechRecognition
{
    /// <summary>
    /// 
    /// </summary>
    public class ShowPanelForSeconds : MonoBehaviour
    {
        private int _prevTime = 0;
        [SerializeField]
        private int _showTime = 0;

        void Update()
        {
            StartCoroutine(ActivatePanel());
        }

        IEnumerator ActivatePanel()
        {
            // Log only every second
            int currTime = (int)Time.time;
            if (_prevTime != currTime)
            {
                _prevTime = (int)Time.time;
                Debug.Log("Started Coroutine at timestamp : " + (int)Time.time);
            }

            // Yield on a new YieldInstruction that waits for XX seconds.
            yield return new WaitForSeconds(_showTime);
            gameObject.SetActive(false);

            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }
    }
}