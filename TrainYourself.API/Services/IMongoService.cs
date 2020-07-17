using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TrainYourself.API.Models;

namespace TrainYourself.API.Services
{
    public interface IMongoService<TCollection, in TContext>
        where TCollection : IMongoCommon
        where TContext : IMongoCommon, new()
    {
        Task<IEnumerable<TCollection>> GetAllAsync(IMongoCollection<TCollection> collection);
        Task<TCollection> GetOneAsync(IMongoCollection<TCollection> collection, TContext context);
        Task<TCollection> GetOneAsync(IMongoCollection<TCollection> collection, string id);

        Task<IEnumerable<TCollection>> GetManyAsync(IMongoCollection<TCollection> collection,
            IEnumerable<TContext> contexts);

        Task<IEnumerable<TCollection>> GetManyAsync(IMongoCollection<TCollection> collection, IEnumerable<string> ids);
        Task<bool> RemoveOneAsync(IMongoCollection<TCollection> collection, TContext context);
        Task<bool> RemoveOneAsync(IMongoCollection<TCollection> collection, string id);
        Task<bool> RemoveManyAsync(IMongoCollection<TCollection> collection, IEnumerable<TContext> contexts);
        Task<bool> RemoveManyAsync(IMongoCollection<TCollection> collection, IEnumerable<string> ids);

    }
}
