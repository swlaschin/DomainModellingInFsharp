
// ================================================
// Exercise 1: Modeling some domains
//
//
// ================================================

(*
Exercise 1a: create types that model a Tic-Tac-Toe game

type BoardLocation = ???

type BoardState = ???

type Player = ???

type Move = ???


*)


(*
Exercise 1b: create types that model a domain that you are expert in.

Are there any business/domain rules that you can encode in types?

When you're done, we can do a show and tell!


*)


// ================================================
// Exercise 2: Refactoring designs to use states
// ================================================

(*
Many existing designs have implicit states that you can recognize by fields called "IsSomething", or nullable date

This is a sign that state transitions are present but not being modelled properly.
*)

// Exercise 2a - redesign this type into two states: RegisteredCustomer and GuestCustomer
type Customer = 
   {
   CustomerName: string
   IsGuest: bool
   RegistrationId: int option
   }


// Exercise 2b - redesign this type into two states: Connected and Disconnected
type Connection = 
   {
   IsConnected: bool
   ConnectionStartedUtc: Nullable<System.DateTime>
   ConnectionHandle: int
   ReasonForDisconnection: string
   }



// ================================================
// Exercise 3: Modeling e-commerce shopping cart transitions
//
// See shopping_cart_transition_diagram.png
//
// ================================================

(*
Exercise: create types that model an e-commerce shopping cart 

Rule: "You can't remove an item from an empty cart"
Rule: "You can't change a paid cart"
Rule: "You can't pay for a cart twice"

States are:
* Empty
* ActiveCartData
* PaidCartData

// 1) Start with the domain types that are independent of state

type Product = string     // placeholder for now
type Cart = Product list  // placeholder for now 

// 2) Create a type to represent the data stored for each type

type Empty = no data to store, so not needed?
type Active = what data to store??
type Paid = what data to store??

// 3) Create a type that represent the choice of all the states

type ShoppingCart = 
    | what?
    | what?
    | what?

// 4) Create transition functions that transition from one state to another

// "initCart" creates a new cart when adding the first item
// The function signature should be 
//     string -> ShoppingCart

let initCart itemToAdd = what??

// "addToActive" creates a new state from active data and a new item
// function signature should be 
//     string -> ActiveCartData -> ShoppingCart

let addToActive itemToAdd activeCartData = 

// "pay" creates a new state from active data and a payment amount
// function signature should be 
//     float -> ActiveCartData -> ShoppingCart

let pay paymentAmount activeCartData = 


// "removeFromActive" creates a new state from active data after removing an item
// function signature should be 
//     string -> ActiveCartData -> ShoppingCart

// removeItem is tricky -- you need to test the card contents after removal to find out what the new state is!

// you'll need this helper for removeItem transition
let removeItemFromCartHelper (productToRemove:Product) (cart:Cart) :Cart =
    cart |> List.filter (fun prod -> prod <> productToRemove)

let removeFromActive itemToRemove activeCartData  = 


// 5) Clients write functions using the state union type 

// "clientAddItem" changes the cart state after adding an item
// function signature should be 
//     string -> ShoppingCart-> ShoppingCart

let clientAddItem newItem cart = 
    match state with
    // | empty ->         
        let new cart contents = what??
        return what new state

    // | active -> 
        let new cart contents = what??
        return what new state

    // | paid -> ignore?

// "clientPayForCart " changes the cart state after paying
// function signature should be 
//     float -> ShoppingCart-> ShoppingCart

let clientPayForCart payment cart = 
    match cart with
    // | empty -> ignore?
    // | active -> return new state
    // | paid -> ignore?

*)





