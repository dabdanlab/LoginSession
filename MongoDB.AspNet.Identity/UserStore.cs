using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.AspNet.Identity
{
    /// <summary>
    ///     Class UserStore.
    /// </summary>
    /// <typeparam name="TUser">The type of the t user.</typeparam>
    public class UserStore<TUser> :
        IUserStore<TUser>,
        IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserEmailStore<TUser>,
        IUserLockoutStore<TUser, string>,
        IUserTwoFactorStore<TUser, string>
        where TUser : IdentityUser
    {
        private readonly IMongoDatabase db;

        private bool _disposed;

        private const string collectionName = "AspNetUsers";

        private IMongoDatabase GetDatabaseFromSqlStyle(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            MongoClientSettings settings = MongoClientSettings.FromUrl(mongoUrl);
            IMongoClient client = new MongoClient(settings);
            if (mongoUrl.DatabaseName == null)
            {
                throw new Exception("No database name specified in connection string");
            }
            return client.GetDatabase(mongoUrl.DatabaseName);
        }

        private IMongoDatabase GetDatabaseFromUrl(MongoUrl url)
        {
            IMongoClient client = new MongoClient(url);
            if (url.DatabaseName == null)
            {
                throw new Exception("No database name specified in connection string");
            }
            return client.GetDatabase(url.DatabaseName);
        }

        private IMongoDatabase GetDatabase(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            return client.GetDatabase(dbName);
        }

        public UserStore()
            : this("DefaultConnection")
        { 
        }

        
    }