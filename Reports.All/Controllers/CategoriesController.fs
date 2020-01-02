namespace Reports.All.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Reports.All.Database
open Reports.All.Database
open Reports.All.Models

[<Route("api/[controller]")>]
[<ApiController>]
type CategoriesController() =
    inherit ControllerBase()

    let connectionString = "Data Source=D:\\GIT\CheatCodes\\Reports.All\\DatabaseAccess\\CheatCodesReportsDatabase.db"

    [<HttpGet>]
    member this.Get() =
        let asyncCategories = Database.list connectionString
        let categories = Async.RunSynchronously asyncCategories
        ActionResult<Category list>(categories)

    [<HttpGet("{id}")>]
    member this.Get(id: int) =
        let asyncCategory = Database.get connectionString id
        let category = Async.RunSynchronously asyncCategory
        ActionResult<Category option>(category)
