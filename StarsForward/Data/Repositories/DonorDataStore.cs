using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Mappers;
using StarsForward.Data.Helpers;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Enums;

namespace StarsForward.Data.Repositories
{
    public class DonorDataStore : IDonorRepository
    {
        public Donor GetById(string id)
        {
            var db = RealmHelper.GetInstance();
            return db.Find<Donor>(id);
        }

        public IEnumerable<Donor> List()
        {
            var db = RealmHelper.GetInstance();
            return db.All<Donor>().ToList();
        }

        public IEnumerable<Donor> List(Expression<Func<Donor, bool>> predicate)
        {
            var db = RealmHelper.GetInstance();
            return db.All<Donor>().Where(predicate).ToList();
        }

        public Donor Add(Donor entity)
        {
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

        public void Delete(Donor entity)
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
                var donor = db.Find<Donor>(id);
                if (donor != null)
                {
                    donor.RecordStatus = RecordStatusType.Deleted;
                }
            });
        }

        public Donor Find(Expression<Func<Donor, bool>> predicate)
        {
            var db = RealmHelper.GetInstance();
            return db.All<Donor>().Where(predicate).FirstOrDefault();
        }

        public void AddToEvent(Event e, Donor entity)
        {
            var db = RealmHelper.GetInstance();
            db.Write(() =>
            {
                var evnt = db.Find<Event>(e.Id);
                entity.Id = Guid.NewGuid().ToString();
                evnt.Donors.Add(entity);
            });
        }

        public void Exported(List<Donor> entities)
        {
            var db = RealmHelper.GetInstance();
            db.Write(() =>
            {
                foreach (var entity in entities)
                {
                    var donor = db.Find<Donor>(entity.Id);
                    donor.DateExported = DateTimeOffset.Now;
                }
            });

        }
    }
}