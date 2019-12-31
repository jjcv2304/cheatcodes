namespace Reports.All.Database

open System.Data
open Dapper
open System.Data.SQLite
open Reports.All.Models


module Database =
    
    let inline (=>) a b = a, box b

    let private mapRowsToRecords (reader: IDataReader): Category list =
        let idIndex = reader.GetOrdinal "Id"
        let nameIndex = reader.GetOrdinal "Name"
        let descriptionIndex = reader.GetOrdinal "Description"
        [
            while reader.Read() do
                yield {
                    Id = reader.GetInt32 idIndex
                    Name = reader.GetString nameIndex
                    Description = reader.GetString descriptionIndex
                }
        ]
        
    let get (connStr: string) (id: int): Async<Category option> =
        let sql = "SELECT * FROM CATEGORY WHERE [Id] = @id"
        let data = [
            "Id" => id
        ]
        let goodData = dict [
            "Id", box id
        ]
        async {
            use conn = new SQLiteConnection(connStr)
            use! reader = conn.ExecuteReaderAsync(sql, goodData) |> Async.AwaitTask

            return mapRowsToRecords reader |> Seq.tryHead
        }     
            
    let list (connStr: string): Async<Category list> =        
        let sql = "SELECT * FROM CATEGORY"
        
        async {            
            use conn = new SQLiteConnection(connStr)
            use! reader = conn.ExecuteReaderAsync(sql) |> Async.AwaitTask
            
            return mapRowsToRecords reader
        }

        

//
//create table Category
//(
//    Id integer
//        primary key autoincrement,
//    Name TEXT not null,
//    Description TEXT,
//    ParentId BIGINT
//        references Category
//);
//
//create table Field
//(
//    Id integer not null
//        constraint Field_pk
//            primary key autoincrement,
//    Name text not null,
//    Description text
//);
//
//create table CategoryField
//(
//    CategoryId integer not null
//        references Category,
//    FieldId integer not null
//        references Field,
//    Value text not null,
//    constraint CategoryField_pk
//        primary key (CategoryId, FieldId)
//);
//
//create unique index Field_Id_uindex
//    on Field (Id);


