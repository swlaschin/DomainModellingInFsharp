module OpaqueApiExample

//============================
// This module demonstrates how you can hide the implementation of a type
// and force clients to use only the constructor functions you provide
//
// This is the interface side (the signature file)
//============================

type String50 
type EmailAddress 

val createString50 : s:string -> String50 option
val createEmailAddress : s:string -> EmailAddress option

