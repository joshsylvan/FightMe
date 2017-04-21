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

	public void AddLink(TreeLink link){
		this.links.Add (link);
	}

	public int GetNode(){
		return ID;
	}

	public void CreateTreeFromCombo(Combo c){
		if (c.GetCombo ().Count > 0) {
//			Debug.Log (c.GetCombo () [0]);
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
//				Debug.Log (li.GetFrequency () + "  /  " + totalQ);
				li.SetWeight( ((float) li.GetFrequency()) / ((float) totalQ) );
			}
//			Debug.Log ("ID " + ID + " : " + totalQ);

		}
	}

	public List<TreeLink> GetLinks(){
		return links;
	}

	public int GetNumberOfRelations(){
		int totalQ = 0;
		foreach (TreeLink li in links) {
			totalQ += li.GetFrequency ();
		}
		return totalQ;
	}

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
