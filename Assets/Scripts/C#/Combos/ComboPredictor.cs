using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class describes the combo predictor.
/// </summary>
public class ComboPredictor {

	List<Combo> combos; // List of current combos
	List<Tree> comboTrees; // List of trees used to predict combos
	int recentResult = -1;

	/// <summary>
	/// Initializes a new instance of the <see cref="ComboPredictor"/> class.
	/// </summary>
	/// <param name="combos">Combos.</param>
	public ComboPredictor(List<Combo> combos){
		this.combos = combos;
		comboTrees = new List<Tree> ();
	}

	/// <summary>
	/// Predicts the next ID of a getsure based on the input sequence using the Decision Tree.
	/// </summary>
	/// <returns>The sequence.</returns>
	/// <param name="sequence">Sequence.</param>
	public int PredictSequence(List<int> sequence){
		int prediction = -1;
		float highestProbability = 0;
		int treeIndex = -1;
		bool match = false;
		for (int i = 0; i < comboTrees.Count; i++) {
			if (comboTrees [i].GetRootNode ().GetNode () == sequence [0]) {
				treeIndex = i;
				match = true;
				break;
			}
		}
		if (match) {
			sequence.RemoveAt (0);
			TraverseTree (comboTrees [treeIndex].GetRootNode (), sequence);
		} else {
			recentResult = -1;
		}
		return recentResult;
	}

	/// <summary>
	/// Traverses the tree using a breadth first seach given a sequence.
	/// </summary>
	/// <param name="node">The current node</param>
	/// <param name="sequence">The current sequence to be predicted.</param>
	public void TraverseTree(TreeNode node, List<int> sequence){
		if (node.GetLinks ().Count > 0) {
			if (sequence.Count <= 0) {
				float bestP = 0;
				int bestIndex = -1;
				for (int i = 0; i < node.GetLinks ().Count; i++) {
					if (node.GetLinks () [i].GetWeight () > bestP) {
						bestP = node.GetLinks () [i].GetWeight ();
						bestIndex = i;
					}
				}
				recentResult = node.GetLinks () [bestIndex].GetTailNode ().GetNode ();
			} else {
				int linkIndex = -1;
				for (int i = 0; i < node.GetLinks ().Count; i++) {
					if (node.GetLinks () [i].GetTailNode ().GetNode () == sequence [0]) {
						linkIndex = i;
						sequence.RemoveAt (0);
						TraverseTree (node.GetLinks () [linkIndex].GetTailNode (), sequence);
						break;
					} else {
						recentResult = -1;
					}
				}
			}
		} else {
			recentResult = -1;
		}

	}

	/// <summary>
	/// Creates the trees from combos.
	/// </summary>
	public void CreateTreesFromCombos(){
		for (int i = 0; i < combos.Count; i++) {
			int result = DoesNodeExist (combos [i].GetCombo () [0]);
			if (result == -1) {
				TreeNode root = new TreeNode(combos [i].GetCombo () [0]);
				Tree t = new Tree (root);
				t.CreateTreeFromCombo (combos [i]);
				comboTrees.Add (t);
			} else {
				List<int> c1 = combos [i].GetCombo ();
				c1.RemoveAt (0);
				combos [i].SetCombo( c1 );
				comboTrees [result].GetRootNode ().CreateTreeFromCombo (combos [i]);
			}
		}
	}

	/// <summary>
	/// Checks if give a Gesture ID whether there is a tree with this Gesture as the rooot node.
	/// </summary>
	/// <returns>The node exist.</returns>
	/// <param name="ID">I.</param>
	public int DoesNodeExist(int ID){
		for(int i = 0; i < comboTrees.Count; i++){
			if (comboTrees[i].GetRootNode ().GetNode () == ID) {
				return i;
			}
		}
		return -1;
	}


}
