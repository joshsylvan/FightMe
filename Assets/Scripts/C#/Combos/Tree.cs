using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree {

	TreeNode rootNode;

	public Tree(TreeNode rootNode){
		this.rootNode = rootNode;
	}

	public TreeNode GetRootNode(){
		return rootNode;
	}

	public void CreateTreeFromCombo(Combo c){
		List<int> c1 = c.GetCombo ();
		c1.RemoveAt (0);
		c.SetCombo( c1 );
		rootNode.CreateTreeFromCombo ( c );
	}



}
