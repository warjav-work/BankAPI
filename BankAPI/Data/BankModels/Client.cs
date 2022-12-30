using System;
using System.Collections.Generic;
using BankAPI.Data;

namespace BankAPI.Data.BankModels
{
    public partial class Client
    {
        public Client()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }


//public static class ClientEndpoints
//{
//	public static void MapClientEndpoints (this IEndpointRouteBuilder routes)
//    {
//        routes.MapGet("/api/Client", async (BankContext db) =>
//        {
//            return await db.Clients.ToListAsync();
//        })
//        .WithName("GetAllClients")
//        .Produces<List<Client>>(StatusCodes.Status200OK);

//        routes.MapGet("/api/Client/{id}", async (int Id, BankContext db) =>
//        {
//            return await db.Clients.FindAsync(Id)
//                is Client model
//                    ? Results.Ok(model)
//                    : Results.NotFound();
//        })
//        .WithName("GetClientById")
//        .Produces<Client>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);

//        routes.MapPut("/api/Client/{id}", async (int Id, Client client, BankContext db) =>
//        {
//            var foundModel = await db.Clients.FindAsync(Id);

//            if (foundModel is null)
//            {
//                return Results.NotFound();
//            }
            
//            db.Update(client);

//            await db.SaveChangesAsync();

//            return Results.NoContent();
//        })   
//        .WithName("UpdateClient")
//        .Produces(StatusCodes.Status404NotFound)
//        .Produces(StatusCodes.Status204NoContent);

//        routes.MapPost("/api/Client/", async (Client client, BankContext db) =>
//        {
//            db.Clients.Add(client);
//            await db.SaveChangesAsync();
//            return Results.Created($"/Clients/{client.Id}", client);
//        })
//        .WithName("CreateClient")
//        .Produces<Client>(StatusCodes.Status201Created);


//        routes.MapDelete("/api/Client/{id}", async (int Id, BankContext db) =>
//        {
//            if (await db.Clients.FindAsync(Id) is Client client)
//            {
//                db.Clients.Remove(client);
//                await db.SaveChangesAsync();
//                return Results.Ok(client);
//            }

//            return Results.NotFound();
//        })
//        .WithName("DeleteClient")
//        .Produces<Client>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);
//    }
//}
}
