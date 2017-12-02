/*
 *              CameraFollow.cs
 *              
 *      Purpose
 *          Responsible for controlling camera movement.
 *          Provides a reference to the camera for other classes.
 *      
 *      Dependencies
 *          PlayerManager:      Static Class
 *          Camera:             Component
 *          
 *      Referenced By
 *          Player:             Component
 */

using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraFollow : MonoBehaviour
{

    #region Variables

    // 
    private static CameraFollow _instance;

    new public Camera camera;

    public float movementSmoothFactor;
    public float rotationSmoothFactor;
    public Vector3 offset;

    #endregion

    #region Properties

    public static CameraFollow instance
    { get { return _instance; } }

    private Transform localPlayer
    { get { return PlayerMan.localPlayer == null ? null : PlayerMan.localPlayer.transform; } }

    #endregion

    #region Methods

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else Destroy(gameObject);

        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (localPlayer != null)
        {
            UpdatePosition();
            UpdateRotation();
        }
    }

    void UpdatePosition()
    { transform.position = Vector3.Lerp(transform.position, localPlayer.position + offset, movementSmoothFactor * Time.deltaTime); }

    void UpdateRotation()
    { transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((localPlayer.position - transform.position).normalized), rotationSmoothFactor * Time.deltaTime); }

    #endregion

}
