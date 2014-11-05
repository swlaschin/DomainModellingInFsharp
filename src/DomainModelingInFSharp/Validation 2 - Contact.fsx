// if using in Visual Studio, this helps to set the current directory correctly
System.IO.Directory.SetCurrentDirectory __SOURCE_DIRECTORY__

// load the Railway Oriented Programming utility library
// See http://fsharpforfunandprofit.com/rop
// See https://github.com/swlaschin/Railway-Oriented-Programming-Example
#load "Rop.fsx"


// ==============================================
// Set up the primitive types: String10, EmailAddress, etc
// ==============================================

module PrimitiveTypes =

    // ------------------------------
    // String10

    // NOTE: this type would normally be opaque
    type String10 = String10 of string

    // Create a String10 from a string.
    // Pass in an error message to use in case of error
    let createString10 errorStr (s:string) =
        match s with
        | null -> Rop.fail errorStr 
        | _ when s.Length > 10 -> Rop.fail errorStr 
        | _ -> Rop.succeed (String10 s)

    // apply a function to the contents of the String10
    let applyString10 f (String10 s) =  f s

    // ------------------------------
    // EmailAddress

    // NOTE: this type would normally be opaque
    type EmailAddress  = EmailAddress of string

    // Create a EmailAddress from a string
    let createEmailAddress (s:string) =
        match s with
        | null -> 
            Rop.fail "Email must not be null"
        | _ when s.Length > 20 -> 
            Rop.fail "Email must not be more than 20 chars"
        | _ -> 
            if s.Contains("@") then
                Rop.succeed (EmailAddress s)
            else
                Rop.fail "Email must contain @ sign"

    // apply a function to the contents of the EmailAddress
    let applyEmailAddress f (EmailAddress s) =  f s

    // ------------------------------
    // ContactId

    // NOTE: this type would normally be opaque
    type ContactId = ContactId of int

    // Create a ContactId from an int
    let createContactId (i: int) =
        if i < 1 then
            Rop.fail "ContactId must be positive integer"
        else 
            Rop.succeed (ContactId i)

    // apply a function to the contents of the ContactId
    let applyContactId f (ContactId s) =  f s

// ==============================================
// Set up the domain level types: PersonalName, Contact
// ==============================================

module ContactDomain = 
    open PrimitiveTypes 

    // NOTE: these types do NOT have to be opaque
    type PersonalName = {
        FirstName: String10
        LastName: String10 
    }

    type Contact = {
        Id: ContactId
        Name: PersonalName
        Email: EmailAddress
    }

    let createFirstName firstName = 
        let errMsg = "First name is required and must be less than 10 chars"
        createString10 errMsg firstName
    
    let createLastName lastName = 
        let errMsg = "Last name is required and must be less than 10 chars"
        createString10 errMsg lastName 

    let createPersonalName firstName lastName = 
        {FirstName = firstName; LastName = lastName}

    let createContact custId name email = 
        {Id = custId; Name = name; Email = email}


// ==============================================
// Set up the DTO types: PersonalName, Contact
// ==============================================

module ContactDTO =
    open PrimitiveTypes
    open ContactDomain


    /// Represents a DTO that is exposed on the wire.
    /// This is a regular POCO class which can be null. 
    /// To emulate the C# class, all the properties are initialized to null by default
    ///
    /// Note that in F# you have to make quite an effort to create nullable classes with nullable fields
    [<AllowNullLiteralAttribute>]
    type ContactDto() = 
        member val Id = 0 with get, set
        member val FirstName : string = null with get, set
        member val LastName : string = null with get, set
        member val Email : string  = null with get, set

    /// Convert a domain Contact into a DTO.
    /// There is no possibility of an error 
    /// because the Contact type has stricter constraints than DTO.
    let contactToDto(cust:Contact) =
        // extract the raw int id from the ContactId wrapper
        let custIdInt = cust.Id |> applyContactId id

        // create the object and set the properties
        let contactDto = ContactDto()
        contactDto.Id <- custIdInt 
        contactDto.FirstName <- cust.Name.FirstName |> applyString10 id
        contactDto.LastName <- cust.Name.LastName |> applyString10 id
        contactDto.Email <- cust.Email |> applyEmailAddress id
        contactDto

    /// Convert a DTO into a domain contact.
    ///
    /// We MUST handle the possibility of one or more errors
    /// because the Contact type has stricter constraints than ContactDto
    /// and the conversion might fail.
    let dtoToContact (dto: ContactDto) = 
        if dto = null then 
            Rop.fail "Contact is required"
        else
            // This is an example of the power of composition!
            // Each step returns a value OR an error.
            // These are then gradually combined to make bigger things, all the while preserving any errors 
            // that happen.

            // if the id is not valid, the createContactId function will return a Failure
            // hover over idOrError and you can see it has type RopResult<ContactId,DomainEvent> rather than just ContactId
            let idOrError = createContactId dto.Id

            // similarly for first and last name
            let firstNameOrError = createFirstName dto.FirstName
            let lastNameOrError = createLastName dto.LastName

            // the "createPersonalName" functions takes normal inputs, not inputs with errors, 
            // but we can use the "lift" function to convert it into one that does handle error input
            // the output has also changed from a normal name to one with errors
            let personalNameOrError = Rop.lift2R createPersonalName firstNameOrError lastNameOrError 

            // similarly try to make an email
            let emailOrError = createEmailAddress dto.Email

            // finally add them all together to make a contacts
            // the "createContact" takes three params, so use lift3 to convert it
            let contactOrError = Rop.lift3R createContact idOrError personalNameOrError emailOrError
            contactOrError 

    // The code above is very explicit and was designed for beginners to understand.
    // Below is a more idiomatic version which uses the <!> and <*> operators rather than "lift".
    //
    // The <!> and <*> operators make it look complicated, but in fact it is always the same pattern.
    //  <!> is used for the first param
    //  <*> is used for the subsequent params
    //
    // so for example:
    //   existingFunction <!> firstParam <*> secondParam <*> thirdParam
    let (<!>) = Rop.(<!>)
    let (<*>) = Rop.(<*>)

    let dtoToContactIdiomatic (dto: ContactDto) =
        if dto = null then 
            Rop.fail "Contact is required"
        else
            let contactIdOrError = 
                createContactId dto.Id

            let nameOrError = 
                createPersonalName
                <!> createFirstName dto.FirstName
                <*> createLastName dto.LastName

            createContact 
            <!> contactIdOrError 
            <*> nameOrError
            <*> createEmailAddress dto.Email //inline this one

// ==============================================
// examples
// ==============================================
open ContactDomain
open ContactDTO

let goodDto = ContactDto(Id=1,FirstName="Alice",LastName="Adams",Email="me@example.com")
goodDto |> ContactDTO.dtoToContact

let badDto = ContactDto(Id=0,FirstName=null,LastName="Adams",Email="xample.com")
badDto |> ContactDTO.dtoToContact
