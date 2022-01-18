using UnityEngine;

public class Camera_Functionality : MonoBehaviour
{
    #region Camera Functionality
    private enum CameraPosition { top, bottom, front, back, left, right };
    private const float CAMERA_DISTANCE_Y = 20f;
    private const float CAMERA_DISTANCE_Z = 20f;
    private static Camera mainCamera;
    private static CameraPosition currentCameraPosition = CameraPosition.front;
    private static Transform mainCameraTransform;

    //Called to initialise camera functionality
    public static void Setup_Camera()
    {
        mainCamera = Camera.main;
        if (!mainCamera)
            Debug.LogError("Failed to find main camera object");
        else
            mainCameraTransform = mainCamera.transform;
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
        float positionX = Dirt_Inc_Settings.Get_Active_Ground() * Dirt_Inc_Settings.Get_X_Ground_Distance();

        switch (_c)
        {
            case CameraPosition.top:
                mainCameraTransform.position = new Vector3(positionX, CAMERA_DISTANCE_Y, 0);
                currentCameraPosition = CameraPosition.top;
                Look_Camera_At_Centre();
                break;
            case CameraPosition.bottom:
                mainCameraTransform.position = new Vector3(positionX, -CAMERA_DISTANCE_Y, 0);
                currentCameraPosition = CameraPosition.bottom;
                Look_Camera_At_Centre();
                break;
            case CameraPosition.front:
                mainCameraTransform.position = new Vector3(positionX, 0, -CAMERA_DISTANCE_Z);
                currentCameraPosition = CameraPosition.front;
                Look_Camera_At_Centre();
                break;
            case CameraPosition.back:
                mainCameraTransform.position = new Vector3(positionX, 0, CAMERA_DISTANCE_Z);
                currentCameraPosition = CameraPosition.back;
                Look_Camera_At_Centre();
                break;
            case CameraPosition.left:
                mainCameraTransform.position = new Vector3(positionX, 0, -CAMERA_DISTANCE_Z);
                Look_Camera_At_Centre();
                break;
            case CameraPosition.right:
                mainCameraTransform.position = new Vector3(positionX, 0, -CAMERA_DISTANCE_Z);
                Look_Camera_At_Centre();
                break;
            default:
                Debug.LogError("Invalid Camera position");
                return;
        }
    }

    private static void Move_Camera_Up()
    {
        switch (currentCameraPosition)
        {
            case CameraPosition.top:
                //Set_Camera_To_Position(CameraPosition.back);
                break;
            case CameraPosition.bottom:
                //Set_Camera_To_Position(CameraPosition.front);
                break;
            case CameraPosition.front:
                Set_Camera_To_Position(CameraPosition.top);
                break;
            case CameraPosition.back:
                //Set_Camera_To_Position(CameraPosition.top);
                break;
            case CameraPosition.left:
                //Set_Camera_To_Position(CameraPosition.top);
                break;
            case CameraPosition.right:
                //Set_Camera_To_Position(CameraPosition.top);
                break;
        }
    }

    private static void Move_Camera_Down()
    {
        switch (currentCameraPosition)
        {
            case CameraPosition.top:
                Set_Camera_To_Position(CameraPosition.front);
                break;
            case CameraPosition.bottom:
                //Set_Camera_To_Position(CameraPosition.back);
                break;
            case CameraPosition.front:
                //Set_Camera_To_Position(CameraPosition.bottom);
                break;
            case CameraPosition.back:
                //Set_Camera_To_Position(CameraPosition.bottom);
                break;
            case CameraPosition.left:
                //Set_Camera_To_Position(CameraPosition.bottom);
                break;
            case CameraPosition.right:
                //Set_Camera_To_Position(CameraPosition.bottom);
                break;
        }
    }

    private static void Move_Camera_Left()
    {
        Dirt_Inc_Settings.Set_Active_Ground(Dirt_Inc_Settings.Get_Active_Ground() - 1);
        Set_Camera_To_Position(CameraPosition.left);
    }

    private static void Move_Camera_Right()
    {
        Dirt_Inc_Settings.Set_Active_Ground(Dirt_Inc_Settings.Get_Active_Ground() + 1);
        Set_Camera_To_Position(CameraPosition.right);
    }

    private static void Look_Camera_At_Centre()
    {
        Vector3 centerActiveGround = Vector3.right * (Dirt_Inc_Settings.Get_Active_Ground() * Dirt_Inc_Settings.Get_X_Ground_Distance());
        mainCameraTransform.LookAt(centerActiveGround);
    }
    #endregion

}
