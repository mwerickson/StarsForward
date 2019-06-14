using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarsForward.Data.Helpers;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Enums;

namespace StarsForward.Data.Repositories
{
    public class EventDataStore : IEventRepository
    {
        public Event GetById(string id)
        {
            var db = RealmHelper.GetInstance();
            return db.Find<Event>(id);
        }

        public IEnumerable<Event> List()
        {
            var db = RealmHelper.GetInstance();
            return db.All<Event>().ToList();
        }

        public IEnumerable<Event> List(Expression<Func<Event, bool>> predicate)
        {
            var db = RealmHelper.GetInstance();
            return db.All<Event>().Where(predicate).ToList();
        }

        public Event Add(Event entity)
        {
            entity.RecordStatus = RecordStatusType.Active;
            var db = RealmHelper.GetInstance();

            db.Write(() =>
            {
                if (string.IsNullOrEmpty(entity.Id))
                {
                    entity.Id = Guid.NewGuid().ToString();
                }
                db.Add(entity);
            });
            return entity;
        }

        public void Delete(Event entity)
        {
            var db = RealmHelper.GetInstance();
            db.Write(() =>
            {
                entity.RecordStatus = RecordStatusType.Deleted;
            });
        }

        public void DeleteById(string id)
        {
            var db = RealmHelper.GetInstance();
            db.Write(() =>
            {
                var e = db.Find<Event>(id);
                e.RecordStatus = RecordStatusType.Deleted;
            });
        }

        public Event Find(Expression<Func<Event, bool>> predicate)
        {
            var db = RealmHelper.GetInstance();
            return db.All<Event>().Where(predicate).FirstOrDefault();
        }

        public int DeleteExported(string eventId)
        {
            var db = RealmHelper.GetInstance();
            var evnt = db.Find<Event>(eventId);
            var i = 0;
            if (evnt != null)
            {
                var donorsToDelete = evnt.Donors.Where(x => x.DateExported != null).ToList();
                db.Write(() =>
                {
                    foreach (var donor in donorsToDelete)
                    {
                        db.Remove(donor);
                        i++;
                    }
                });
            }

            return i;
        }

        public void Update(Event entity)
        {
            var db = RealmHelper.GetInstance();
            db.Write(() =>
            {
                var e = db.Find<Event>(entity.Id);
                if (e != null)
                {
                    e.Name = entity.Name;
                    e.StartDate = entity.StartDate;
                    e.EndDate = entity.EndDate;
                }
            });
        }
    }
}