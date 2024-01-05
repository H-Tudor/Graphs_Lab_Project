# MAP Lab Final Project

## About

### General Description

The end-of-semester project for the Advanced Programming Methods course lab. 
The course mainly focusses on graphs

### Requirements

- Visual Application in which you can write and draw (WFA + pictureBox + listBox)
- Static Base Class (Engine, Graph)
- LoadGraph and SaveGraph functions (edge list and adjacency matrix)
- DrawGraph Matrix
- DepthFirstSearch & BreathFirstSearch (one will use the edge list and the other will use the adjacency matrix)
- 2 Algorithm (Dykstra, Hamiltonian, Eulerian etc) with separate activation buttons and text and visual output (one will use the edge list and the other will use the adjacency matrix)

## ToDo list

TODO: Create App Interface

- Main Form
- Picture Box
- List Box
- File Select Pop-up
- Buttons

TODO: Create LoadGraph

- params:
	- filename: str
	- mode: bool, default=true (true - adjacency matrix, false - edge list)

- returns:
	- Graph Object

Input File Format
```txt
5			# nr of nodes
0 32 43		# node_id x y 
1 45 12
2 89 15
3 39 59
4 55 82

--- edge list
0 1	5	# (= 1 0), (start_node end_node weight)
0 3	15
0 4 20
3 5 2

--- adjecency matrix
0 5 0 15 20
5 0 0 0 0
0 0 0 0 0
15 0 0 0 2
20 0 0 2 0
```


TODO: Create SaveGraph

- params:
	- graph: Graph
	- filename: str
	- mode: bool, default=true (true - adjacency matrix, false - edge list)

Output File Format
```txt
5			# nr of nodes
0 32 43		# node_id x y 
1 45 12
2 89 15
3 39 59
4 55 82

--- edge list
0 1	5	# (= 1 0), (start_node end_node weight)
0 3	15
0 4 20
3 5 2

--- adjecency matrix
0 5 0 15 20
5 0 0 0 0
0 0 0 0 0
15 0 0 0 2
20 0 0 2 0
```

TODO: Create DrawGraph

- params:
	- graph: Graph

draw points and draw line between points if edge / non zero value in adjacency matrix

TODO: Create DepthFirstSearch

TODO: Create BreathFirstSearch

TODO: Create HamiltonianRoad

TODO: CreateDykstra

