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

module ShipmentsDomain = 

    // 1) Start with the domain types that are independent of state

    type Package = string // placeholder for now
    type TruckId = int
    type SentUtc = DateTime
    type DeliveredUtc = DateTime
    type Signature = string

    // 2) Create types to represent the data stored for each state

    type UndeliveredData = Package 
    type OutForDeliveryData = Package * TruckId * SentUtc
    type DeliveredData = Package * Signature * DeliveredUtc 

    // 3) Create a type that represent the choice of all the states

    type Shipment = 
        | UndeliveredState of UndeliveredData 
        | OutForDeliveryState of OutForDeliveryData 
        | DeliveredState of DeliveredData 

    // 4) Create transition functions that transition from one state type to another

    let sendOutForDelivery (package:UndeliveredData) (truckId:TruckId)  :Shipment = 
        let utcNow = System.DateTime.UtcNow
        // return new state
        let outForDeliveryData = package, truckId, utcNow 
        OutForDeliveryState outForDeliveryData 

    let addressNotFound (outForDeliveryData:OutForDeliveryData) :Shipment = 
        let package, truckId, utcNow = outForDeliveryData 
        // return new state
        let undeliveredData = package
        UndeliveredState undeliveredData

    let signedFor (outForDeliveryData:OutForDeliveryData) (signature:Signature) :Shipment = 
        let utcNow = System.DateTime.UtcNow
        let package, truckId, utcNow = outForDeliveryData 
        // return new state
        let deliveredData = package,signature,utcNow 
        DeliveredState deliveredData


// ================================================
// Now write some client code that uses this API
// ================================================

module ShipmentsClient = 
    open ShipmentsDomain

    let putShipmentOnTruck (truckId:TruckId) state = 
        match state with
        | UndeliveredState package -> 
            sendOutForDelivery package truckId
        | OutForDeliveryState _ -> 
            printfn "package already out"
            // return original state
            state
        | DeliveredState _ -> 
            printfn "package already delivered"
            // return original state
            state

    let markAsDelivered (signature:Signature) state = 
        match state with
        | UndeliveredState _  -> 
            printfn "package not out"
            // return original state
            state
        | OutForDeliveryState data -> 
            signedFor data signature 
        | DeliveredState _ -> 
            printfn "package already delivered"
            // return original state
            state


// ================================================
// Now write some test code 
// ================================================

open ShipmentsDomain
open ShipmentsClient

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