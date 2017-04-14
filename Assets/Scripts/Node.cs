using UnityEngine;
using System.Collections;

public class Node {
	private int gCost;
	private int hCost;
	private int fCost{
		get{ 
			return gCost + hCost;
		}
	}

	private int gridX;
	private int gridY;

	private bool walkable;
	private Vector3 worldPos;

	private Node parent;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY){
		walkable = _walkable;
		worldPos = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public Vector3 GetWorldPos(){
		return worldPos;
	}

	public bool GetWalkable(){
		return walkable;
	}

	public int GethCost(){
		return hCost;
	}

	public void SethCost(int cost){
		hCost = cost;
	}

	public int GetgCost(){
		return gCost;
	}

	public void SetgCost(int cost){
		gCost = cost;
	}

	public int GetfCost(){ 
		return fCost;
	}

	public int GetGridX(){
		return gridX;
	}

	public int GetGridY(){
		return gridY;
	}

	public void SetParent(Node _parent){
		parent = _parent;
	}

	public Node GetParent(){
		return parent;
	}

}
