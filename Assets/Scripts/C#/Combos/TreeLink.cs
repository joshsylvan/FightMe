using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Tree link class that handles links between nodes in Decision Trees.
/// </summary>
public class TreeLink {

	public TreeNode tailNode; // tail node of link
	float weight; // weight of link
	int frequency;// frequency of a link

	/// <summary>
	/// Initializes a new instance of the <see cref="TreeLink"/> class. Inistilising the wight and frequency as one.
	/// </summary>
	public TreeLink(){
		weight = 1;
		frequency = 1;
	}

	/// <summary>
	/// Sets the tail node.
	/// </summary>
	/// <param name="tailNode">Tail node.</param>
	public void SetTailNode(TreeNode tailNode){
		this.tailNode = tailNode;
	}

	/// <summary>
	/// Gets the tail node.
	/// </summary>
	/// <returns>The tail node.</returns>
	public TreeNode GetTailNode(){
		return tailNode;
	}

	/// <summary>
	/// Sets the weight.
	/// </summary>
	/// <param name="weight">Weight.</param>
	public void SetWeight(float weight){
		this.weight = weight;
	}

	/// <summary>
	/// Sets the frequency.
	/// </summary>
	/// <param name="frequency">Frequency.</param>
	public void SetFrequency(int frequency){
		this.frequency = frequency;
	}

	/// <summary>
	/// Increments the frequency.
	/// </summary>
	public void IncrementFrequency(){
		this.frequency++;
	}

	/// <summary>
	/// Gets the weight.
	/// </summary>
	/// <returns>The weight.</returns>
	public float GetWeight(){
		return weight;
	}

	/// <summary>
	/// Gets the frequency.
	/// </summary>
	/// <returns>The frequency.</returns>
	public int GetFrequency(){
		return frequency;
	}
}
