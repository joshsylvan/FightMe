using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLink {

	public TreeNode tailNode;
	float weight;
	int frequency;

	public TreeLink(){
		weight = 1;
		frequency = 1;
	}

	public void SetTailNode(TreeNode tailNode){
		this.tailNode = tailNode;
	}

	public TreeNode GetTailNode(){
		return tailNode;
	}

	public void SetWeight(float weight){
		this.weight = weight;
	}

	public void SetFrequency(int frequency){
		this.frequency = frequency;
	}

	public void IncrementFrequency(){
		this.frequency++;
	}

	public float GetWeight(){
		return weight;
	}

	public int GetFrequency(){
		return frequency;
	}
}
