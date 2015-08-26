using System;
using MongoDB.Driver;

namespace WebAPIApplication
{
    public class Settings
    {
        public string Database { get; set; }
        public string MongoConnection { get; set; }
    }
}