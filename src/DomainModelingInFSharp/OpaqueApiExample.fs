module OpaqueApiExample

//============================
// This module demonstrates how you can hide the implementation of a type
// and force clients to use only the constructor functions you provide
//
// This is the implementation side.
//============================

open System.Text.RegularExpressions

type String50 = String50 of string
type EmailAddress = EmailAddress of string

let createString50 (s:string) = 
    if s.Length <= 50
        then Some (String50 s)
        else None
// val createString50 : s:string -> String50 option

let createEmailAddress (s:string) = 
    if Regex.IsMatch(s,@"^\S+@\S+\.\S+$") 
        then Some (EmailAddress s)
        else None
// val createEmailAddress : s:string -> EmailAddress option

