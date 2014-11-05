// ================================================
// FSM Exercise: Modeling package delivery transitions
//
// See Shipments transition diagram.png
///
// ================================================

(*
Exercise: create types that model package delivery transitions

Rule: "You can't put a package on a truck if it is already out for delivery"
Rule: "You can't sign for a package that is already delivered"

States are:
* Undelivered
* OutForDelivery
* Delivered
*)

open System


// 1) Start with the domain types that are independent of state

type Package = string // placeholder for now
type DeliveredUtc = DateTime
type Signature = string

// 2) Create types to represent the data stored for each state

type UndeliveredData = what?
type OutForDeliveryData =
type DeliveredData =

// 3) Create a type that represent the choice of all the states

type Shipment = 
    | what?
    | what?
    | what?

// 4) Create transition functions that transition from one state type to another

let sendOutForDelivery (package:Package) (anythingElse:whatType) :whatReturnType = 
    what??

let addressNotFound (outForDelivery:whatInputType) :whatReturnType = 
    what??

let signedFor (outForDelivery:whatInputType) :whatReturnType =
    what??

// ================================================
// Now write some client code that uses this API
// ================================================

let putShipmentOnTruck extraData state = 
    match state with
    // | undelivered -> change state
    // | outfordelivery -> ignore?
    // | delivered -> ignore?


let markAsDelivered (signature:Signature) state = 
    // | undelivered -> ignore?
    // | outfordelivery -> change state
    // | delivered -> ignore?


let package = "My Package"
let newShipment = UndeliveredState package

let truckId = 123
let outForDelivery = 
    newShipment |> putShipmentOnTruck truckId 

let signature = "Scott"
let delivered = 
    outForDelivery |> markAsDelivered signature 

// errors when using the wrong state
delivered |> markAsDelivered signature 
delivered |> putShipmentOnTruck truckId 