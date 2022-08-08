using PersonalApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalApp.Core
{
    public class EntityRepositoryBase<T> where T : class, IEntity, new()
    {
        public List<T> _entityList;
        public EntityRepositoryBase(List<T> entityList)
        {
            _entityList = entityList;
        }
        /// <summary>
        /// id ile entity'i getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(int id)
        {
            return _entityList.Find(entity => entity.Id == id);
        }
        /// <summary>
        /// Parametre olarak verilen entityi ekler
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            int id = 0;

            if (_entityList.Count > 0)
                id = _entityList.Max(x => x.Id) + 1;

            entity.Id = id;

            _entityList.Add(entity);
        }
        /// <summary>
        /// Entityleri listeler
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return _entityList;
        }
        /// <summary>
        /// Verilen entity'nin Id'sine göre göre entity'i bulur ve bulunan entity'nin propertyleri, parametre olarak gönderilen entity'nin parametrelerine eşitlenir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            var updateEntity = _entityList.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (updateEntity != null)
                return false;

            updateEntity = entity;

            return true;
        }
        /// <summary>
        /// Verilen Id'ye göre entity'i bulur ve siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var entity = _entityList.FirstOrDefault(x => x.Id == id);

            if (entity is null)
                return false;

            _entityList.Remove(entity);

            return true;
        }
        /// <summary>
        /// Verilen id'ye sahip entity var mı diye bakar, varsa true, yoksa false döner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Any(int id)
        {
            return _entityList.Any(entity => entity.Id == id);
        }
    }
}
