using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode {

	int ID;
	List<TreeLink> links;

	public TreeNode(int ID){
		this.ID = ID;
		links = new List<TreeLink> ();
	}

	/// <summary>
	/// Adds the link.
	/// </summary>
	/// <param name="link">Link.</param>
	public void AddLink(TreeLink link){
		this.links.Add (link);
	}

	/// <summary>
	/// Gets the node.
	/// </summary>
	/// <returns>The node.</returns>
	public int GetNode(){
		return ID;
	}

	/// <summary>
	/// Creates the tree from combo.
	/// </summary>
	/// <param name="c">C.</param>
	public void CreateTreeFromCombo(Combo c){
		if (c.GetCombo ().Count > 0) {
			TreeLink l = new TreeLink ();
			TreeNode n = new TreeNode (c.GetCombo () [0]);
			l.SetTailNode (n);

			// Check if link exists
			int result = DoesLinkExist(l);
			if(result == -1){
				this.AddLink (l);
				List<int> c1 = c.GetCombo ();
				c1.RemoveAt (0);
				c.SetCombo( c1 );
				n.CreateTreeFromCombo (c);

			} else {
			
				List<int> c1 = c.GetCombo ();
				c1.RemoveAt (0);
				c.SetCombo (c1);
				links [result].GetTailNode ().CreateTreeFromCombo (c);

			}

			// Recalculate weights
			// Get Total Links
			int totalQ = GetNumberOfRelations();
			foreach(TreeLink li in links){
				li.SetWeight( ((float) li.GetFrequency()) / ((float) totalQ) );
			}
		}
	}

	/// <summary>
	/// Gets the links.
	/// </summary>
	/// <returns>The links.</returns>
	public List<TreeLink> GetLinks(){
		return links;
	}

	/// <summary>
	/// Gets the number of relations.
	/// </summary>
	/// <returns>The number of relations.</returns>
	public int GetNumberOfRelations(){
		int totalQ = 0;
		foreach (TreeLink li in links) {
			totalQ += li.GetFrequency ();
		}
		return totalQ;
	}

	/// <summary>
	/// Checks if a link already exists when added to a tree so the wieghts can be ajusted.
	/// </summary>
	/// <returns>The link exist.</returns>
	/// <param name="l">L.</param>
	public int DoesLinkExist(TreeLink l){
		for (int i = 0; i < links.Count; i++) {
				if (links [i].GetTailNode ().GetNode () == l.GetTailNode ().GetNode ()) {
					links [i].IncrementFrequency ();
					return i;
				}

		}
		return -1;
	}

}
