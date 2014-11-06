module OpaqueApiClient

//============================
// This module demonstrates how a client can use a opaque type defined in the previous module.
//
// The client is forced to use only the constructor functions provided
//
//============================

// won't compile
let s50_bad = OpaqueApiExample.String50 "123"

// will compiler
let s50_good = OpaqueApiExample.createString50 "123"

let email1 = OpaqueApiExample.createEmailAddress "bad"
let email2 = OpaqueApiExample.createEmailAddress "abc@example.com"



// ================================
// Another quick way of hiding implementation is
// to create a "private" module with your code in
// and then have a "public" module that exposes only
// the functions that you want clients to use.
// ================================
module _PrivateImplementation =
    let privateAdd x y = 
        x + y
    let publicPrintAdd x y = 
        privateAdd x y |> printfn "%i + %i = %i" x y

module PublicInterface =
    // Documentation for public interface
    let printAdd = _PrivateImplementation.publicPrintAdd 

module Client =
    open PublicInterface
    printAdd 1 2 
