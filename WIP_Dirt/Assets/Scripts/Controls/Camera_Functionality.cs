using UnityEngine;

public class Camera_Functionality : MonoBehaviour
{
    #region Camera Functionality
    private enum CameraPosition { top, bottom, front, back, left, right };
    private const float CAMERA_DISTANCE_Y = 20f;
    private const float CAMERA_DISTANCE_Z = 20f;
    private const float CAMERA_OFFSET_Y = 10f;
    private static Camera mainCamera;
    private static Transform mainCameraTransform;

    //Called to initialise camera functionality
    public static void Setup_Camera()
    {
        mainCamera = Camera.main;
        if (!mainCamera)
            Debug.LogError("Failed to find main camera object");
        else
        {
            mainCameraTransform = mainCamera.transform;
            Set_Camera_To_Position(CameraPosition.front);
        }
    }

    public static void Move_Camera(Vector2 _dir)
    {
        if (mainCamera)
        {
            int x = (int)_dir.x;
            int y = (int)_dir.y;

            if (x == 0 && y == 1)
            {
                Move_Camera_Up();
            }
            else if (x == 0 && y == -1)
            {
                Move_Camera_Down();
            }
            else if (x == -1 && y == 0)
            {
                Move_Camera_Left();
            }
            else if (x == 1 && y == 0)
            {
                Move_Camera_Right();
            }
            else
            {
                Debug.LogError("Invalid camera movement direction");
            }
        }
        else
            Debug.LogError($"Camera object: {mainCamera} / transform: {mainCameraTransform} not found!");
    }

    private static void Set_Camera_To_Position(CameraPosition _c)
    {
        //Calculate x position based on the current active ground
        float positionX = Ground_Settings.Get_Active_Ground() * Ground_Settings.Get_X_Ground_Distance();
        //Get the center positon everything is based off
        Vector3 newCamPosition = Ground_Settings.Get_World_Center_Pos();
        //Set the new position x to the calculate x position
        newCamPosition.x = positionX;

        //Set the z and y based on the new position
        if(_c == CameraPosition.top)
        {
            newCamPosition.z = 0;
            newCamPosition.y += CAMERA_DISTANCE_Y;
        }
        else
        {
            newCamPosition.z -= CAMERA_DISTANCE_Z;
            newCamPosition.y += CAMERA_OFFSET_Y;
        }

        //Move the camera to the new position
        mainCameraTransform.position = newCamPosition;
        //Make sure we are looking at the center of the correct ground
        Look_Camera_At_Center(positionX);
    }

    private static void Move_Camera_Up()
    {
        Set_Camera_To_Position(CameraPosition.top);
    }

    private static void Move_Camera_Down()
    {
        Set_Camera_To_Position(CameraPosition.front);
    }

    private static void Move_Camera_Left()
    {
        Ground_Settings.Set_Active_Ground(Ground_Settings.Get_Active_Ground() - 1);
        Set_Camera_To_Position(CameraPosition.left);
    }

    private static void Move_Camera_Right()
    {
        Ground_Settings.Set_Active_Ground(Ground_Settings.Get_Active_Ground() + 1);
        Set_Camera_To_Position(CameraPosition.right);
    }

    private static void Look_Camera_At_Center(float positionX)
    {
        Vector3 centerActiveGround = Ground_Settings.Get_World_Center_Pos();
        centerActiveGround.x = positionX;
        mainCameraTransform.LookAt(centerActiveGround);
    }
    #endregion

}
