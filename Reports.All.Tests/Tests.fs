module Tests

open System
open Microsoft.AspNetCore.Mvc
open Reports.All.Controllers
open System.Net
open System.Net.Http
open Xunit
open Swensen.Unquote

type BankAccount =
    { Number: string
      Name: string
      Balance: double }

let myAccount =
    { Number = "XDASDF"
      Name = "Kit"
      Balance = 32. }


let convertsTo<'a> candidate =
    match box candidate with
    | :? 'a as converted -> Some converted
    | _ -> None

[<Fact>]
let ``My test``() =
    Assert.True(true)

[<Theory>]
[<InlineData("1")>]
[<InlineData("2")>]
let ``My test with parameters`` (id: string) =
    Assert.Equal(id, "1")

[<Fact>]
let ``My test with Unquote``() =
    let var1 = "var1"
    let var2 = "var1"
    test <@ var1 = var2 @>

//[<Fact>]
//let ``Post returns correct result `` () =
//    let sut = new ValuesController ()
//    let value = "test post"
//
//    let actual = sut.Post value
//
//    test <@
//             actual
//             |> convertsTo<ObjectResult>
//             |> Option.map (fun x -> ((int32) x.StatusCode))
//             |> Option.exists  ((=)  HttpStatusCode.Accepted) @>

type Description =
    { Name: string
      PartNumber: int }

type Part =
    | Item of Description
    | Repeat of Part * int
    | Compound of Description * Part list


let testingStuff =
    let clip =
        Item
            { Name = "clip name"
              PartNumber = 1221 }

    let brakePiston =
        Item
            { Name = "clip name 2"
              PartNumber = 1222 }

    let calliperBody =
        Item
            { Name = "clip name 3"
              PartNumber = 1223 }

    let calliper =
        Compound
            ({ Name = "Calliper"
               PartNumber = 9111 },
             [ calliperBody
               Repeat(brakePiston, 2) ])

    None
