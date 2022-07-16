using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject player;

    public List<GameObject> rootRooms;
    public List<GameObject> roomPrefabs;

    public int numRooms = 15;
    private static int MAX_ATTEMPTS = 100;


    private List<GameObject> placedRooms = new List<GameObject>();
    private List<Door> frontier = new List<Door>();

    //public bool generateRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        PlaceRootRoom();

        int attempts = 0;
        while (placedRooms.Count < numRooms && attempts < MAX_ATTEMPTS && frontier.Count > 0)
        {
            attempts++;
            TryAddingRoom();
        }

        foreach(Door door in frontier)
        {
            door.SetSealed();
        }

        PlacePlayer();
    }

    private void PlacePlayer()
    {
        GameObject lastRoom = placedRooms.Last();
        Vector3 roomCenter = lastRoom.GetComponent<Collider>().bounds.center;
        player.transform.position = roomCenter;
    }

    private void TryAddingRoom()
    {
        Door selectedFrontierDoor = RandomFrom(frontier);

        GameObject selectedRoomPrefab = RandomFrom(roomPrefabs);

        GameObject instantiatedRoom = Instantiate(selectedRoomPrefab);

        Door selectedRoomDoor = RandomFrom(instantiatedRoom.GetComponentsInChildren<Door>());

        PositionRoomFacingDoor(selectedFrontierDoor, instantiatedRoom, selectedRoomDoor);

        if (CollidesWithExistingRooms(instantiatedRoom))
        {
            Destroy(instantiatedRoom);
            return;
            //Debug.LogWarning("Collision detected");
        }
        else
        {
            //Debug.Log("No collision");
        }

        RegisterNewRoom(instantiatedRoom);

        selectedFrontierDoor.pairedDoor = selectedRoomDoor;
        selectedRoomDoor.pairedDoor = selectedFrontierDoor;

        frontier.Remove(selectedFrontierDoor);
        frontier.Remove(selectedRoomDoor);
    }

    private bool CollidesWithExistingRooms(GameObject newRoom)
    {
        Physics.SyncTransforms();
        Bounds b = newRoom.GetComponent<Collider>().bounds;
        foreach(GameObject room in placedRooms)
        {
            Bounds otherBounds = room.GetComponent<Collider>().bounds;
            if (b.Intersects(otherBounds))
            {
                //LogBoundsCollision(b, otherBounds);
                return true;
            }
        }
        return false;
    }

    private void LogBoundsCollision(Bounds b, Bounds otherBounds)
    {
        Debug.Log("Collision between " + FormatBounds(b) + " and " + FormatBounds(otherBounds));
    }

    private string FormatBounds(Bounds b)
    {
        return "[" + formatInterval(b.min.x, b.max.x)
            + ", " + formatInterval(b.min.y, b.max.y)
            + ", " + formatInterval(b.min.z, b.max.z) + "]";
    }

    private string formatInterval(float min, float max)
    {
        return min + " -- " + max;
    }

    private void PositionRoomFacingDoor(Door frontierDoor, GameObject room, Door roomDoor)
    {
        Quaternion frontierDoorRotation = frontierDoor.transform.rotation;
        Quaternion targetRotation = frontierDoorRotation * Quaternion.Euler(Vector3.up * 180);

        Quaternion rotationToApply = Quaternion.Inverse(roomDoor.transform.rotation) * targetRotation;

        room.transform.Rotate(rotationToApply.eulerAngles);

        room.transform.Translate(-roomDoor.transform.position, Space.World);
        room.transform.Translate(frontierDoor.transform.position, Space.World);
    }

    private void PlaceRootRoom()
    {
        GameObject rootRoom = Instantiate(RandomFrom(rootRooms));
        int rotation = Random.Range(0, 4) * 90;
        rootRoom.transform.Rotate(Vector3.up, rotation);
        RegisterNewRoom(rootRoom);
    }

    private void RegisterNewRoom(GameObject room)
    {
        placedRooms.Add(room);
        foreach (Door door in room.GetComponentsInChildren<Door>())
        {
            frontier.Add(door);
        }
        room.GetComponent<Room>().OnCreation();
    }

    private T RandomFrom<T>(List<T> list)
    {
        int idx = Random.Range(0, list.Count);
        return list[idx];
    }

    private T RandomFrom<T>(T[] array)
    {
        int idx = Random.Range(0, array.Length);
        return array[idx];
    }

    private void Update()
    {
        //if (generateRoom)
        //{
        //    generateRoom = false;
        //    TryAddingRoom();
        //}
    }

}
