using Zach.DataBase.Repository;
using Zach.Util;
using Zach.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Linq.Expressions;

namespace Zach.Service
{
    public class ObjectdefineService : RepositoryFactory
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Objectdefine> GetList()
        {
            try
            {
                return this.BaseRepository().FindList<Objectdefine>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public int Insert(List<Objectdefine> objectdefines)
        {
            try
            {
                int count = this.BaseRepository().Insert<Objectdefine>(objectdefines);
                return count;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public int Delete(List<Objectdefine> objectdefines)
        {
            try
            {
                int count = this.BaseRepository().Delete<Objectdefine>(objectdefines);
                return count;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
    }
}
