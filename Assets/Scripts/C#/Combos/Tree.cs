using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implementation of Decision Trees.
/// </summary>
public class Tree {

	TreeNode rootNode; // Root node of tree

	/// <summary>
	/// Initializes a new instance of the <see cref="Tree"/> class.
	/// </summary>
	/// <param name="rootNode">Root node.</param>
	public Tree(TreeNode rootNode){
		this.rootNode = rootNode;
	}

	/// <summary>
	/// Gets the root node.
	/// </summary>
	/// <returns>The root node.</returns>
	public TreeNode GetRootNode(){
		return rootNode;
	}

	/// <summary>
	/// Creates the tree from combo.
	/// </summary>
	/// <param name="c">C.</param>
	public void CreateTreeFromCombo(Combo c){
		List<int> c1 = c.GetCombo ();
		c1.RemoveAt (0);
		c.SetCombo( c1 );
		rootNode.CreateTreeFromCombo ( c );
	}



}
