using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using ObjectManipulation;
using System.Threading.Tasks;
using TMPro;
using TriLibCore;
using TriLibCore.General;
using UnityEngine;
using UnityEngine.UI;


namespace AppLifecycle
{

    /// <summary>
    /// Class that includes the TriLib for for loading glb files from URL.
    /// For more information about TriLib, see https://ricardoreis.net/
    /// </summary>
    public class ModelLoader : MonoBehaviour
    {
        public GameEvent onModelLoading;
        public GameEvent onModelLoaded;

        public Image progressBar;
        public TextMeshPro progressBarText;
        public ParticleSystem progressBarParticle;


        // Lets the user load a new model by clicking a GUI button.
        public async void startLoading()
        {
            // Wait for 1.5 seconds to finish audio output of "connected to session"
            await Task.Delay(1500);
            onModelLoading.TriggerEvent();

            // Creates an AssetLoaderOptions instance.
            // AssetLoaderOptions is a class used to configure many aspects of the loading process.
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();

            // The web-request contains information on how to download the model.
            var webRequest = AssetDownloader.CreateWebRequest(Session.modelURL);

            // Accept certificate received on https request without checking
            webRequest.certificateHandler = new BypassCertificate();

            // Important: If you're downloading models from files that are not Zipped, you must pass the model extension as the last parameter from this call (Eg: "fbx")
            Debug.Log($"Start loading model from URL: {Session.modelURL}");
            AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions, null, "glb");
        }


        // This event is called when the model loading progress changes.
        // You can use this event to update a loading progress-bar, for instance.
        // The "progress" value comes as a normalized float (goes from 0 to 1).
        // Platforms like UWP and WebGL don't call this method at this moment, since they don't use threads.
        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            Debug.Log($"OnProgress " + progress);

            // Set UI progress bar. Doubled input progress because model loader ends with 0.5
            progressBar.fillAmount = 2 * progress;

            // Get progress in percentage for UI text output
            var progressInPercentage = 2 * progress * 100;
            if (progressInPercentage <= 98.0f)
            {
                progressBarText.text = progressInPercentage.ToString("0") + "%";
            }
            // Manually set final UI progress display, during material loading
            else
            {
                progressBar.fillAmount = 1.0f;
                progressBar.gameObject.SetActive(false);
                progressBarParticle.gameObject.SetActive(true);
                progressBarText.text = "Loading materials...";
            }
        }


        // This event is called when there is any critical error loading your model.
        // You can use this to show a message to the user.
        private void OnError(IContextualizedError contextualizedError)
        {
            Debug.LogError($"An error ocurred while loading your Model: {contextualizedError}");
        }


        // This event is called when all model GameObjects and Meshes have been loaded.
        // There may still Materials and Textures processing at this stage.
        private void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("Model loaded. Loading materials.");

            // The root loaded GameObject is assigned to the "assetLoaderContext.RootGameObject" field.
            // If you want to make sure the GameObject will be visible only when all Materials and Textures have been loaded, you can disable it at this step.
            var myLoadedGameObject = assetLoaderContext.RootGameObject;
            myLoadedGameObject.SetActive(false);
        }


        // This event is called after OnLoad when all Materials and Textures have been loaded.
        // This event is also called after a critical loading error, so you can clean up any resource you want to.
        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            // The root loaded GameObject is assigned to the "assetLoaderContext.RootGameObject" field.
            // You can make the GameObject visible again at this step if you prefer to.
            var myLoadedGameObject = assetLoaderContext.RootGameObject;
            myLoadedGameObject.SetActive(true);

            // Add basic components to manipulate the GameObject
            MyGameObject.my3DModel = myLoadedGameObject;
            MyGameObject.my3DModel.AddComponent<BoxCollider>();
            MyGameObject.my3DModel.AddComponent<ObjectManipulator>();

            var objectManipulator = MyGameObject.my3DModel.GetComponent<ObjectManipulator>();
            objectManipulator.AllowedManipulations = TransformFlags.Move | TransformFlags.Rotate | TransformFlags.Scale;

            //            MyGameObject.my3DModel.AddComponent<BoundsControl>();
            //            objectManipulator.selectMode = UnityEngine.XR.Interaction.Toolkit.InteractableSelectMode.Multiple;
            //            objectManipulator.AllowedInteractionTypes = InteractionFlags.Near | InteractionFlags.Generic | InteractionFlags.Gaze | InteractionFlags.Ray;

            onModelLoaded.TriggerEvent();
        }
    }
}