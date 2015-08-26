using WebAPIApplication.Models;
using System.Collections.Generic;
using MongoDB.Bson;

namespace WebAPIApplication
{
    public interface ISpeakerRepository
    {
        IEnumerable<Speaker> AllSpeakers();

        Speaker GetById(ObjectId id);

        void Add(Speaker speaker);

        void Update(Speaker speaker);

        bool Remove(ObjectId id);
    }
}