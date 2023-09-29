using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Lobby : MonoBehaviour
{
    public Canvas canvas;
    public GameObject prefabRoom;
    public Transform RoomBoard;
    int RoomCount=0;
    int ShowRoomNumber;
    public class Room
    {
        int roomID;
        string roomName;
        public Room(int newroomID, string newroomName)
        {
            roomID = newroomID;
            roomName = newroomName;
        }
    }
    List<int> ValuesList = new List<int>();
    List<int> usedValues = new List<int>();
    List<Room> RoomList = new List<Room>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<99;i++)
        {
            ValuesList.Add(i);
        }
        canvas.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(()=>CreateRoom());
        canvas.transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(()=>ExitRoom());
    }

    void CreateRoom()
    {   //note for easy follow
        var RoomView = canvas.transform.GetChild(0).GetChild(0);
        var txtID = canvas.transform.GetChild(0).GetChild(3);
        var btnExit = canvas.transform.GetChild(0).GetChild(4);
        var btnCreate = canvas.transform.GetChild(0).GetChild(1);
        ////////////

        //generate random number range base on pre-created number list
        //if the number appear in usedValues list => generate again
        var ranNumber = Random.Range(0,ValuesList.Count-1);
        while(usedValues.Contains(ranNumber))
        {
            ranNumber= Random.Range(0,ValuesList.Count-1);
        }
        usedValues.Add(ranNumber);
        var RoomID = ranNumber;
        //////////////

        var room = Instantiate(prefabRoom, RoomBoard) as GameObject;
        room.SetActive(true);
        Debug.Log(RoomID);
        room.transform.GetChild(0).GetComponent<Text>().text = "Phong so " + RoomCount.ToString();
        room.transform.GetChild(2).GetComponent<Text>().text = RoomCount.ToString();
        room.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate {
            canvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            int.TryParse(room.transform.GetChild(2).GetComponent<Text>().text, out ShowRoomNumber);
            canvas.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "Phong so " + ShowRoomNumber.ToString();
            txtID.GetComponent<Text>().text = "ID: " + RoomID.ToString();
            txtID.gameObject.SetActive(true);
            RoomView.gameObject.SetActive(false);
            btnExit.gameObject.SetActive(true);
            btnCreate.gameObject.SetActive(false);
        });
        var _Room = new Room(RoomID,"Phong so " + RoomCount.ToString());
        RoomList.Add(_Room);
        RoomCount++;
    }

    void ExitRoom()
    {
        canvas.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
