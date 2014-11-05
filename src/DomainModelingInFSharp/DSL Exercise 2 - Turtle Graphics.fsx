// ================================================
// DSL Exercise: Create a DSL to move a turtle around
//
// ================================================


(*
A turtle has a Position, a Direction and a Color.

The Position is in a x,y coordinate grid
The Direction is one of Up, Down, Left, Right
The Color is one of black or red

You can instruct the turtle to do something using the following instructions:

* Move N, where n is an int
* Turn Left, which rotates the direction anti-clockwise
* Turn Right, which rotates the direction clockwise
* Color Black, which changes the pen color to black
* Color Red, which changes the pen color to red

Write code that will make the above instructions work.

Then write code that will take a list of instructions and apply them all

*)

// set up the vocabulary
type Position = what??
type Color = what??
type Direction = what??
type Turtle = what??   // hint: a record type

type TurnInstruction = what??

type TurtleInstruction =  what??

// define a function that changes the position given a distance and direction
let changePosition (distance:int) (direction:Direction) (pos:Position)  :Position =
    match direction with
    | what -> 
        what??

// define a function that changes the direction given a turn instruction
let turnDirection (turnInstruction:TurnInstruction) (direction:Direction) :Direction =
    match direction with
    | what -> 
        what??

// define a function that moves the turtle given a 
let moveTurtle (instruction:TurtleInstruction) (turtle:Turtle) :Turtle =
    match instruction with
    | what -> 
        what??


// test some examples
let turtle0 = { pos=0,0; direction=North; color=Black}

let instruction1 = Turn Left
let turtle1  = turtle0 |> moveTurtle instruction1 

let instruction2 = Move 100
let turtle2  = turtle1 |> moveTurtle instruction2 


// test a whole set of instructions
let instructions = [
    Turn Left
    Move 100
    SetColor Red
    Turn Right
    Move 10
    ]

// fold has parameters [action] [initialValue] [list]
//   - action has two params - the state and the new instruction
let foldAction turtle instruction =
    moveTurtle instruction turtle
let initialState = turtle0 
List.fold foldAction initialState instructions 