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