// ================================================
// Validation -- this demonstrates how to convert untrusted input into a pure domain
//
// ================================================



// ==============================================
// Set up the core, pure domain
// ==============================================

module RomanNumeralDomain =

    type RomanNumeral = RomanNumeral of RomanDigit list
    and RomanDigit = I | IV | V | IX | X | XC | IC | C  
    // This is guaranteed clean and pure! 
    // I cannot have a bad value in the domain


// ==============================================
// Set up a service that interacts with the outside world
// ==============================================

module RomanNumeralService =
    open RomanNumeralDomain

    /// Convert a string like "CXCVII" into a RomanNumeral
    let toRomanNumeral (input:string) :RomanNumeral =
        // loop through the ASCII chars, accumulating into a list RomanDigits as we go
        let rec loopThroughChars (chars:char list) (digits:RomanDigit list) = 
            match chars with
            | [] -> 
                // End of list. Return the accumulated list of RomanDigits 
                // (but has to be reversed first!)
                digits |> List.rev  
            | 'i'::'v'::rest -> 
                let newDigits = IV :: digits
                loopThroughChars rest newDigits 
            | 'i'::'x'::rest -> 
                let newDigits = IX :: digits
                loopThroughChars rest newDigits 
            | 'i'::'c'::rest -> 
                let newDigits = IC :: digits
                loopThroughChars rest newDigits 
            | 'x'::'c'::rest -> 
                let newDigits = XC :: digits
                loopThroughChars rest newDigits 
            | 'i'::rest -> 
                let newDigits = I::digits
                loopThroughChars rest newDigits 
            | 'v'::rest -> 
                let newDigits = V::digits
                loopThroughChars rest newDigits 
            | 'x'::rest -> 
                let newDigits = X::digits
                loopThroughChars rest newDigits 
            | 'c'::rest -> 
                let newDigits = C::digits
                loopThroughChars rest newDigits 
            
        // convert the input into a list of chars
        let chars = input.ToLower().ToCharArray() |> List.ofArray

        // loop through the list of chars, returning a list of Digits
        let digits = loopThroughChars chars []

        // wrap the digits in a RomanNumeral 
        RomanNumeral digits 

    /// Convert a RomanNumeral into a number
    let toInt (input:RomanNumeral) :int =

        let rec loopThroughDigits (digits:RomanDigit list) (sum:int)= 
            match digits with
            | [] -> sum
            | I::rest ->  loopThroughDigits rest (sum+1)
            | IV::rest ->  loopThroughDigits rest (sum+4)
            | V::rest ->  loopThroughDigits rest (sum+5)
            | IX::rest ->  loopThroughDigits rest (sum+9)
            | X::rest ->  loopThroughDigits rest (sum+10)
            | XC::rest ->  loopThroughDigits rest (sum+90)
            | IC::rest ->  loopThroughDigits rest (sum+99)
            | C::rest ->  loopThroughDigits rest (sum+100)

        let (RomanNumeral digits) = input
        let sum = loopThroughDigits digits 0
        sum 


// ==============================================
// test
// ==============================================

open RomanNumeralDomain

// good cases
"cxciii" |> RomanNumeralService.toRomanNumeral 

RomanNumeral [C;XC;I;I;I] |> RomanNumeralService.toInt


// bad case
"cxa" |> RomanNumeralService.toRomanNumeral 


// ==============================================
// Version 2 - Set up a service with error handling
// ==============================================

module RomanNumeralService_V2 =
    open RomanNumeralDomain

    type RomanNumeralResult = 
        | Success of RomanNumeral
        | Failure of string

    /// Convert a string like "CXCVII" into a RomanNumeral OR error
    let toRomanNumeral (input:string) :RomanNumeralResult =
        let rec loopThroughChars (chars:char list) (digits:RomanDigit list) :RomanNumeralResult = 
            match chars with
            | [] -> 
                digits |> List.rev |> RomanNumeral |> Success
            | 'i'::'v'::rest -> 
                let newDigits = IV :: digits
                loopThroughChars rest newDigits 
            | 'i'::'x'::rest -> 
                let newDigits = IX :: digits
                loopThroughChars rest newDigits 
            | 'i'::'c'::rest -> 
                let newDigits = IC :: digits
                loopThroughChars rest newDigits 
            | 'x'::'c'::rest -> 
                let newDigits = XC :: digits
                loopThroughChars rest newDigits 
            | 'i'::rest -> 
                let newDigits = I::digits
                loopThroughChars rest newDigits 
            | 'v'::rest -> 
                let newDigits = V::digits
                loopThroughChars rest newDigits 
            | 'x'::rest -> 
                let newDigits = X::digits
                loopThroughChars rest newDigits 
            | 'c'::rest -> 
                let newDigits = C::digits
                loopThroughChars rest newDigits 
            | badChar::_ -> 
                let errMsg = sprintf "Error parsing '%s': '%c' is not a valid roman digit" input badChar
                errMsg |> Failure
            
        let chars = input.ToLower().ToCharArray() |> List.ofArray
        let result = loopThroughChars chars []
        result


// ==============================================
// test again
// ==============================================


// good cases
"cxciii" |> RomanNumeralService_V2.toRomanNumeral 

// bad case
"cxa" |> RomanNumeralService_V2.toRomanNumeral 
