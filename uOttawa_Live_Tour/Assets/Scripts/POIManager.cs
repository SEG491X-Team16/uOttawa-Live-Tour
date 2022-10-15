using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using Google.XR.ARCoreExtensions;


/**
 * This manager handles resolving the cloud anchors for the POIs (tour stops).
 */
public class POIManager : MonoBehaviour
{

    //the scene's anchorManager
    public ARAnchorManager anchorManager;

    // The 3D object that represents a Cloud Anchor
    public GameObject CloudAnchorPrefab;

    // The time between enters AR View and ARCore session starts to host or resolve
    private const float _startPrepareTime = 3.0f;

    // Android 12 (S) SDK version
    private const int _androidSSDKVersion = 31;

    // The timer to indicate whether the AR View has passed the start prepare time
    private float _timeSinceStart;

    // A list of Cloud Anchors that have been created but are not yet ready to use
    private List<ARCloudAnchor> _pendingCloudAnchors = new List<ARCloudAnchor>();

    // A list for caching all Cloud Anchors
    private List<ARCloudAnchor> _cachedCloudAnchors = new List<ARCloudAnchor>();

    // A list of Cloud Anchors that will be used in resolving
    public HashSet<string> _resolvingSet = new HashSet<string>();

    //this event is triggered whenever one of the added POIs cloud anchors are resolved
    public UnityEvent poiAchieved;

    private AndroidJavaClass _versionInfo;

    public void Awake()
    {
        _versionInfo = new AndroidJavaClass("android.os.Build$VERSION");
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeSinceStart = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Give ARCore some time to prepare for hosting or resolving.
        if (_timeSinceStart < _startPrepareTime)
        {
            _timeSinceStart += Time.deltaTime;

            return;
        }

        if (_timeSinceStart >= _startPrepareTime)
        {
            DisplayTrackingDebugMessage();
        }

        ResolvingCloudAnchors();
        UpdatePendingCloudAnchors();
    }

    //Add a POI to attempt to acheive
    public void AddPOI(PointOfInterest poi) {
        _resolvingSet.Add(poi.CloudAnchorId);
        Debug.Log("Adding cloud anchor to resolve");
    }

    //Remove a POI and destroy any of it's instances in the Unity World
    public void RemovePOI(PointOfInterest poi) {
        _resolvingSet.Remove(poi.CloudAnchorId);

        if (_pendingCloudAnchors.Count > 0)
        {
            foreach (var anchor in _pendingCloudAnchors)
            {
                Destroy(anchor.gameObject);
            }

            _pendingCloudAnchors.Clear();
        }

        if (_cachedCloudAnchors.Count > 0)
        {
            foreach (var anchor in _cachedCloudAnchors)
            {
                Destroy(anchor.gameObject);
            }

            _cachedCloudAnchors.Clear();
        }
        Debug.Log("removing cloud anchor: "+poi.CloudAnchorId);
    }

    private void DisplayTrackingDebugMessage()
    {
        if (ARSession.notTrackingReason != NotTrackingReason.None)
        {
            switch (ARSession.notTrackingReason)
            {
                case NotTrackingReason.Initializing:
                    Debug.Log("Tracking is being initialized.");
                    return;
                case NotTrackingReason.Relocalizing:
                    Debug.Log("Tracking is resuming after an interruption.");
                    return;
                case NotTrackingReason.InsufficientLight:
                    if (_versionInfo.GetStatic<int>("SDK_INT") < _androidSSDKVersion)
                    {
                        Debug.Log("Too dark. Try moving to a well-lit area.");
                    }
                    else
                    {
                        Debug.Log("Too dark. Try moving to a well-lit area. " +
                                    "Also, make sure the Block Camera is set to off in system settings.");
                    }

                    return;
                case NotTrackingReason.InsufficientFeatures:
                    Debug.Log("Can't find anything. Aim device at a surface with more texture or color.");
                    return;
                case NotTrackingReason.ExcessiveMotion:
                    Debug.Log("Moving too fast. Slow down.");
                    return;
                case NotTrackingReason.Unsupported:
                    Debug.Log("Tracking lost reason is not supported.");
                    return;
                default:
                    Debug.Log(string.Format("Not tracking reason: {0}", ARSession.notTrackingReason));
                    return;
            }
        }
    }

    private void ResolvingCloudAnchors()
    {
        // No Cloud Anchor for resolving.
        if (_resolvingSet.Count == 0)
        {
            return;
        }

        // There are pending or finished resolving tasks.
        if (_pendingCloudAnchors.Count > 0 || _cachedCloudAnchors.Count > 0)
        {
            return;
        }

        // ARCore session is not ready for resolving.
        if (ARSession.state != ARSessionState.SessionTracking)
        {
            return;
        }

        foreach (string cloudId in _resolvingSet)
        {
            Debug.Log("Trying to resolve "+cloudId);

            ARCloudAnchor cloudAnchor = anchorManager.ResolveCloudAnchorId(cloudId);
            if (cloudAnchor == null)
            {
                Debug.LogFormat("Faild to resolve Cloud Anchor " + cloudId);
                OnAnchorResolvedFinished(false, cloudId);
            }
            else
            {
                _pendingCloudAnchors.Add(cloudAnchor);
            }
        }
    }

    private void OnAnchorResolvedFinished(bool success, string cloudId, string response = null)
    {
        if (success)
        {   
            Debug.Log(string.Format("Succeed to resolve the Cloud Anchor: {0}.", cloudId));
            _resolvingSet.Remove(cloudId);
            poiAchieved.Invoke();
        }
        else
        {
            Debug.Log("Failed to resolve Cloud Anchor: " + cloudId + (response == null ? "." : "with error " + response + "."));
        }
    }

    private void UpdatePendingCloudAnchors()
    {
        foreach (var cloudAnchor in _pendingCloudAnchors)
        {
            if (cloudAnchor.cloudAnchorState == CloudAnchorState.Success)
            {
                Debug.LogFormat("Succeed to resolve the Cloud Anchor: {0}", cloudAnchor.cloudAnchorId);
                OnAnchorResolvedFinished(true, cloudAnchor.cloudAnchorId);
                Instantiate(CloudAnchorPrefab, cloudAnchor.transform);

                _cachedCloudAnchors.Add(cloudAnchor);
            }
            else if (cloudAnchor.cloudAnchorState != CloudAnchorState.TaskInProgress)
            {
                Debug.LogFormat("Failed to resolve the Cloud Anchor {0} with error {1}.",
                    cloudAnchor.cloudAnchorId, cloudAnchor.cloudAnchorState);
                OnAnchorResolvedFinished(false, cloudAnchor.cloudAnchorId, cloudAnchor.cloudAnchorState.ToString());

                _cachedCloudAnchors.Add(cloudAnchor);
            }
        }

        _pendingCloudAnchors.RemoveAll(
            x => x.cloudAnchorState != CloudAnchorState.TaskInProgress);
    }
}
