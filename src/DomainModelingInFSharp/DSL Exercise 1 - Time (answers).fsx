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
type DateScale = Hour | Hours | Day | Days | Week | Weeks
type DateDirection = Ago | Hence

// define a function that matches on the vocabulary
let getDate (interval:int) (scale:DateScale) (direction:DateDirection) =
    let absHours = match scale with
                   | Hour | Hours -> 1 * interval
                   | Day | Days -> 24 * interval
                   | Week | Weeks -> 24 * 7 * interval
    let signedHours = match direction with
                      | Ago -> -1 * absHours 
                      | Hence ->  absHours 
    System.DateTime.Now.AddHours(float signedHours)

// test some examples
let example1 = getDate 5 Days Ago
let example2 = getDate 1 Hour Hence


