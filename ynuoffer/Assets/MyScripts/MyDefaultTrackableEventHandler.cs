//脚本作用：识别到物体，丢失物体时执行一些操作
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class MyDefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    AudioSource auido;

    public bool AppHasInitialized;
    public _DisplayHelper displayHelper;
    // public _MapManager _MapManager;
    public GameObject gatePrefab;//门
    public GameObject placePrefab;//地面
    public GameObject display;//地图和建筑展示界面的父节点
    public bool isMovedOut = true;//是否脱卡
    public bool hasLoadedMap=false;//是否已经加载过地图
    #region PROTECTED_MEMBER_VARIABLES
    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        isMovedOut = true;
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        auido = this.GetComponent<AudioSource>();
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        if (isMovedOut)
        {
            //判断原来是否处于托卡状态，
            displayHelper.OnMoveIn();
            isMovedOut = false;
        }

        // if (!AppHasInitialized)
        //     return;
        if (!hasLoadedMap)
        {
            if (!auido.isPlaying)
            {
                auido.Play();
            }
            GameObject gate = GameObject.Instantiate(gatePrefab, transform.position - new Vector3(0, 0.273f, 0), transform.rotation);
            gate.GetComponent<Gate>().displayHelper=displayHelper;
            gate.transform.Rotate(0, 180, 0);
            gate.transform.parent = display.transform;

            GameObject place = GameObject.Instantiate(placePrefab, transform.position, transform.rotation);
            place.transform.Rotate(0, 180, 0);
            place.transform.parent = display.transform;

            GameObject Canvas = GameObject.Find("StartCanvas");
            if (Canvas)
            {
                for(int i=0;i<Canvas.transform.childCount-1;i++)
                {
                    Destroy(Canvas.transform.GetChild(i).gameObject);

                }
            }
        }
        
        hasLoadedMap = true;
    }
    protected virtual void OnTrackingLost()
    {
        if (hasLoadedMap)
        {
            displayHelper.OnMoveOut();
            isMovedOut=true;
        }
    }
    #endregion // PROTECTED_METHODS
}
