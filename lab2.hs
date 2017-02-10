--f x y = x + y
--g (x, y) = x + y

--q a b c = a + b + c
--q1 (a, b) c = a + b + c
--q2 a (b, c) = a + b + c
--q3 (a, b, c) = a + b + c

--question3 a b c d = a + b + b + c

inorder [] = True
inorder [x] = True
inorder (x:y:t) = x <= y && inorder (y:t)

insert x [] = [x]
insert x (h:t) 
	| x <= h = x:h:t
	| otherwise = h:insert x t

sort [] = []
sort (h:t) = insert h (sort t)

data BinaryTree a = EmptyTree | Node a (BinaryTree a) (BinaryTree a) deriving (Show)

toList EmptyTree = []
toList (Node v l r) = (toList l) ++ [v] ++ (toList r)

treeInorder EmptyTree = True
treeInorder (Node v l r) = inorder (toList (Node v l r))

insertTree x EmptyTree = Node x EmptyTree EmptyTree;
insertTree x (Node v l r)
	| x <= v = Node v (insertTree x l) r
	| otherwise = Node v l (insertTree x r) 

preOrderTree EmptyTree = []
preOrderTree (Node v l r) = v: (preOrderTree l) ++ (preOrderTree r)

postOrderTree EmptyTree = []
postOrderTree (Node v l r) = (postOrderTree l) ++ (postOrderTree r) ++ [v]

revList [] = []
revList (h:t) = (revList t) ++ [h]

revTree EmptyTree = EmptyTree
revTree (Node v l r) = Node v (revTree r) (revTree l)

