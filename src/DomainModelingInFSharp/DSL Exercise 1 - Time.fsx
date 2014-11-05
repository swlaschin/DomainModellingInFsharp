// ================================================
// DSL Exercise: Create a DSL to report the relative time
//
// ================================================

(*
// syntax examples
let example1 = getDate 5 Days Ago
let example2 = getDate 1 Hour Hence

// the C# equivalent would probably be more like this:
// getDate().Interval(5).Days().Ago()
// getDate().Interval(1).Hour().Hence()

*)

// set up the vocabulary
type DateScale = what??
type DateDirection = what??

// define a function that matches on the vocabulary
let getDate (interval:int) (scale:DateScale) (direction:DateDirection) =
    what??


// test some examples
let example1 = getDate 5 Days Ago
let example2 = getDate 1 Hour Hence
