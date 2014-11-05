// ==============================================
// This is a utility library for managing Success/Failure results
//
// See http://fsharpforfunandprofit.com/rop
// See https://github.com/swlaschin/Railway-Oriented-Programming-Example
// ==============================================

/// A Result is a success or failure
/// The Success case has a success value
/// The Failure case has a list of messages
type RopResult<'TSuccess> =
    | Success of 'TSuccess 
    | Failure of string list

/// create a Success with no messages
let succeed x =
    Success x

/// create a Failure with a message
let fail msg =
    Failure [msg]

/// given a function that generates a new RopResult
/// apply it only if the result is on the Success branch
/// merge any existing messages with the new result
let bindR f result =
    match result with
    | Success x -> f x
    | Failure errors -> Failure errors 

/// given a function wrapped in a result
/// and a value wrapped in a result
/// apply the function to the value only if both are Success
let applyR f result =
    match f,result with
    | Success f, Success x -> 
        f x |> Success 
    | Failure errs, Success _ 
    | Success _, Failure errs -> 
        errs |> Failure
    | Failure errs1, Failure errs2 -> 
        errs1 @ errs2 |> Failure 

/// infix version of apply
let (<*>) = applyR

/// given a function that transforms a value
/// apply it only if the result is on the Success branch
let liftR f result =
    let f' =  f |> succeed
    applyR f' result 

/// given two values wrapped in results apply a function to both
let lift2R f result1 result2 =
    let f' = liftR f result1
    applyR f' result2 

/// given three values wrapped in results apply a function to all
let lift3R f result1 result2 result3 =
    let f' = lift2R f result1 result2 
    applyR f' result3

/// given four values wrapped in results apply a function to all
let lift4R f result1 result2 result3 result4 =
    let f' = lift3R f result1 result2 result3 
    applyR f' result4

/// infix version of liftR
let (<!>) = liftR

/// synonym for liftR
let mapR = liftR


