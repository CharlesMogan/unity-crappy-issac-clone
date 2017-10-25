﻿//rooms can be made.









using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMaker : MonoBehaviour {
	
	class Cell{
		int xLocationInRoom;
		int yLocationInRoom;
		int absoluteXLocation;
		int absoluteyLocation;
		bool isOn;
		bool isOuterWall;
		bool isDoor;
		GameObject myCube;
		public Cell(bool isOn, bool isOuterWall, bool isDoor, int xInRoom, int yInRoom, int xOffset, int yOffset){
			this.isOn = isOn;
			this.isOuterWall = isOuterWall; 
			this.isDoor = isDoor;
			this.xLocationInRoom = xInRoom;
			this.yLocationInRoom = yInRoom;
			this.absoluteXLocation = xInRoom + xOffset;
			this.absoluteyLocation = yInRoom + yOffset;
			// if it is not on the outer wall I need to add health at somepoint.
		}

		public bool IsOn{
			get{return isOn;}

			set{isOn = value;}
		}

		public bool IsOuterWall{
			get{return isOuterWall;}

			set{isOuterWall = value;}
		}

		public bool IsDoor{
			get{return isDoor;}

			set{isDoor = value;}
		}



		public void Draw(){
			Destroy(myCube);
			if(isDoor){
				Debug.Log("implement me!");
			}else if(isOuterWall){
				myCube = Instantiate(Resources.Load("OuterCuber") as GameObject, new Vector3(absoluteXLocation,-20,absoluteyLocation), Quaternion.identity);
			}else if(isOn){
				myCube = Instantiate(Resources.Load("Cuber") as GameObject, new Vector3(absoluteXLocation,-20,absoluteyLocation), Quaternion.identity);
			}
		}
	}




	class World{
		public World(){


			//make a room 
			//make a touching room off of one of the sides
			//propose a third room at a location
			//check if that conflicts with other rooms.




			GameObject gameManager = GameObject.FindWithTag("GameController");
       		GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
       		/*int ranRoomSize = gameManagerScript.NextRandom(40,125);
       		int ranRoomSize2 = gameManagerScript.NextRandom(40,75);
       		int ranRoomSize3 = gameManagerScript.NextRandom(60,125);
       		int ranRoomSize4 = gameManagerScript.NextRandom(40,125);
       		int ranRoomSize5 = gameManagerScript.NextRandom(40,125);
       		int ranRoomSize6 = gameManagerScript.NextRandom(40,125);
       		int ranRoomSize7 = gameManagerScript.NextRandom(40,125);*/
			/*Room firstRoom = new Room(250,250,0,0);
			Room secondRoom = new Room(250,250,250,0);
			Room thirdRoom = new Room(250,250,0,250);
			Room fourthRoom = new Room(250,250,250,250);*/
			//firstRoom.getDoors();


			//first room
			Room firstRoom = new Room(250,25,0,0);
			//second room
			//Room firstRoom = new Room(ranRoomSize,ranRoomSize2,0,0);



			//Room secondRoom = new Room(ranRoomSize3,ranRoomSize4,ranRoomSize,0);
			//Room thirdRoom = new Room(ranRoomSize5,ranRoomSize6,0,ranRoomSize2);
			//Room fourthRoom = new Room(ranRoomSize,ranRoomSize,ranRoomSize,ranRoomSize);


		}
	}
	


	class Room{
		int fill = 32;

		GameObject gameManager;
       	GameManager gameManagerScript;
		public GameObject cube;
		int xOffset;
		int yOffset;
		int xDimension;
		int yDimension;
		Cell[,] room;

		public Room(int xDimension, int yDimension, int xOffset, int yOffset){
			gameManager = GameObject.FindWithTag("GameController");
       		gameManagerScript = gameManager.GetComponent<GameManager>();
       		this.xOffset = xOffset;
       		this.yOffset = yOffset;
			this.xDimension = xDimension;
			this.yDimension = yDimension;

			room = new Cell[xDimension,yDimension];
			for (int i = 0; i < xDimension; i++){
				for (int j = 0; j < yDimension; j++){
					int ranNum = gameManagerScript.NextRandom(0,100);
					if( i == 0 || j == 0 || i == xDimension-1 || j == yDimension-1){
						room[i,j]= new Cell(true,true,false,i,j,xOffset,yOffset);
					}else if(ranNum < fill){
						room[i,j]= new Cell(true,false,false,i,j,xOffset,yOffset);
					}else{
						room[i,j]= new Cell(false,false,false,i,j,xOffset,yOffset);
					}
				}
			}

			for(int i = 0; i < 5; i++){  //i do ever even
				nextGeneration();
			}
				Draw();	
		}




		void Draw(){
			for (int i = 0; i < xDimension; i++){
				for (int j = 0; j < yDimension; j++){
					room[i,j].Draw();
				}
			}	
		}




		void nextGeneration(){
			bool[,] tempRoom = new bool[xDimension,yDimension];
			

			for (int i = 0; i < xDimension; i++){
				for (int j = 0; j < yDimension; j++){
					tempRoom[i,j] = room[i,j].IsOn;
				}
			}


			for (int i = 0; i < xDimension; i++){
				for (int j = 0; j < yDimension; j++){
					tempRoom[i,j] = B45678S45678(i,j);
				}
			}


			for (int i = 1; i < xDimension-1; i++){    //so the sides are uneffected
				for (int j = 1; j < yDimension-1; j++){
					room[i,j].IsOn = tempRoom[i,j];
				}
			}
			
		}



		bool B3S23(int x, int y){                             //conway's rules
			int neighbors = getMooreNeighborhood(x,y);
					if(neighbors < 2){
						return false;
					}else if(neighbors > 3){
						return false;
					}else if(room[x,y].IsOn == false && neighbors == 3){
						return true;
					}
				return false;
		}

		bool B45678S45678(int x, int y){
			int neighbors = getMooreNeighborhood(x,y);
			if(neighbors < 4){
				return false;
			}else{
				return true;
			}

		}

	public int[] NESWBounds{
		get{return new int[4]{yDimension+yOffset,xDimension+xOffset,0+yOffset,0+xOffset};}///////////////// might be backwoards
	}


		



		int getMooreNeighborhood(int x, int y){
				int sum = 0;
				for(int i = -1; i <= 1; i++){
					for(int j = -1; j <= 1; j++){
						//Debug.Log("tryingTo acsess"+ (x+i)+" "+(y+j));
						if(x+i>-1 && y+j>-1 && x+i < xDimension && y+j < yDimension){           ///this is real bad, need to fix
							if(room[x+i,y+j].IsOn){
								sum += 1;
							}
						}
					}
				}
				if (room[x,y].IsOn){
					sum-=1;
				}
				return sum;
			}






			

	}	




	// Use this for initialization
	void Start () {
		World myWorld = new World();

	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
