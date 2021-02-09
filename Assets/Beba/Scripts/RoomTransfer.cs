using UnityEngine;

namespace bebaSpace
{
    public class RoomTransfer : MonoBehaviour
    {
        [SerializeField] private RoomPosition nextRoomPosition;

        private Transform cameraContainer;

        private void Start()
        {
            //get the current transform of CameraContainer
            cameraContainer = GameObject.Find("CameraContainer").transform;

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Vector3 newCameraBoundPos = Vector3.zero;

            if (collision.CompareTag("Player"))
            {
                //move camera bound according to where the next room is position from the current room
                switch (nextRoomPosition)
                {
                    case RoomPosition.Up:

                        newCameraBoundPos.y += 21;
                        cameraContainer.transform.position += newCameraBoundPos;

                        collision.transform.position += new Vector3(0, 2, 0);
                        break;

                    case RoomPosition.Down:
                      
                        newCameraBoundPos.y -= 21;
                        cameraContainer.transform.position += newCameraBoundPos;

                        collision.transform.position += new Vector3(0, -2, 0);
                        break;

                    case RoomPosition.Right:

                        newCameraBoundPos.x += 32;
                        cameraContainer.transform.position += newCameraBoundPos;

                        collision.transform.position += new Vector3(2, 0, 0);
                        break;

                    case RoomPosition.Left:

                        newCameraBoundPos.x -= 32;
                        cameraContainer.transform.position += newCameraBoundPos;

                        collision.transform.position += new Vector3(-2, 0, 0);
                        break;
                }

            }
        }

    }

    public enum RoomPosition { Up, Down, Left, Right}

}
