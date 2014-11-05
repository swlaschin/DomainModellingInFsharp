// ================================================
// Records
//
// Records are data structures with named fields
//
// ================================================


// They are defined with the "type" keyword
type MyRecord = {id:int; name:string}     // Try running this

// they are instantiated in the same way, but using "let" and assigned values for each field
let myRecord = {id=1; name="Alice"}       // Try running this

// you can clone a new record based on an old one using "with" keyword
let myRecord2 = {myRecord with name="Bob"}       // Try running this

// to get data out, you can use pattern matching similar to tuples
let {id=myId; name=myName} = myRecord 

// or you can dot into them as well, if the compiler knows which type you are using
let myName4 = myRecord.name

