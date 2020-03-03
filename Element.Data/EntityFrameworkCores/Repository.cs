﻿using Element.Common.Common;
using Element.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Element.Data.EntityFrameworkCores
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        #region DI
        public DbcontextRepository Context;
        private readonly ILogger<IBaseRepository<TEntity>> _myLogger;
        public BaseRepository(ILogger<IBaseRepository<TEntity>> myLogger)
        {
            Context = DbcontextRepository.Context;
            Dbset = Context.Set<TEntity>();
            _myLogger = myLogger;
            var sqltype = "Sql:sqlType";
            var sqlstr = "Sql:str";
            var sql = JsonHelper.GetDbConnection(sqltype, sqlstr);
            var sqlrealType = "";
            if (sql.Item1 == "1")
            {
                sqlrealType = "sqlserver";
            }
            else if (sql.Item1 == "2")
            {
                sqlrealType = "Oracle";
            }
            else if (sql.Item1 == "3")
            {
                sqlrealType = "Mysql";
            }
            else if (sql.Item1 == "4")
            {
                sqlrealType = "sqlite";
            }

            _myLogger.LogInformation($"数据库类型是{sqlrealType}");
        }

        internal DbSet<TEntity> Dbset { get; set; }

        #endregion

        #region 1.0 新增实体，返回对象
        /// <summary>
        /// 1.0 新增实体，返回对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回受影响的行数</returns>
        public async Task<TEntity> Add(TEntity model)
        {
            _myLogger.LogInformation($"{typeof(TEntity)} Model 正在执行新增Model 返回行数");
            await Dbset.AddAsync(model);
            if (await Context.SaveChangesAsync()>0) {
                return model;
            }
           
            return model;
        }
        #endregion

        #region 1.0 新增实体，返回受影响的行数

        /// <summary>
        /// 1.0 新增实体，受影响的行数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddModel(TEntity model)
        {
            _myLogger.LogInformation($"{typeof(TEntity)} Model 正在执行增加Model 返回Model");
            await Dbset.AddAsync(model);
            return await Context.SaveChangesAsync();

        }

        #endregion

        #region 1.0根据条件查询
        /// <summary>
        /// 1.0根据条件查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<int> Count(Expression<Func<TEntity, bool>> whereLambda)
        {
            _myLogger.LogInformation($"{typeof(TEntity)} Model 正在执行查询返回受影响的行数");
            // .Count(whereLambda);
            return await Dbset.CountAsync(whereLambda);


        }
        #endregion

        #region 1.0 根据条件删除
        /// <summary>
        /// 1.0 根据条件删除
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DelBy(Expression<Func<TEntity, bool>> delWhere)
        {
            _myLogger.LogInformation($"{typeof(TEntity)}sModel 正在执行条件删除");
            var listDeleting = await Dbset.Where(delWhere).ToListAsync();
            listDeleting.ForEach(u =>
            {
                Dbset.Attach(u);  //先附加到EF 容器
                Dbset.Remove(u); //标识为删除状态
            });

            return await Context.SaveChangesAsync();

        }
        #endregion

        #region 1.0 根据条件查询单个model
        /// <summary>
        /// 4.0 根据条件查询单个model
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<TEntity> GetModelAsync(Expression<Func<TEntity, bool>> whereLambda)
        {
            _myLogger.LogInformation($"{typeof(TEntity)} Model 正在执行查询单个Model 返回行数");
            return await Dbset.Where(whereLambda).AsNoTracking().FirstOrDefaultAsync();
        }
        #endregion

        #region 1.0 分页查询
        /// <summary>
        /// 分页查询 + List<T> GetPagedList
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排序 lambda表达式</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderByLambda, bool isAsc = true)
        {
            _myLogger.LogInformation($"{typeof(TEntity)} Model 正在执行分页查询");
            if (isAsc)
            {
                return await Dbset.Where(whereLambda).OrderBy(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            }
            else
            {
                return await Dbset.Where(whereLambda).OrderByDescending(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            }





            //throw new NotImplementedException();

        }
        #endregion

        #region 1.0 根据条件查询
        /// <summary>
        /// 1.0 根据条件查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereLambda)
        {
            _myLogger.LogInformation($"{typeof(TEntity)} Model 正在执行查询");
            return await Dbset.Where(whereLambda).AsNoTracking().ToListAsync();
        }
        #endregion

        #region 1.0 修改实体
        public async Task<int> Modify(TEntity model)
        {
            EntityEntry entry = Context.Entry(model);
            entry.State = EntityState.Modified;
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Modify(TEntity model, params string[] propertyNames)
        {
            //将对象添加到EF中
            EntityEntry entry = Context.Entry(model);
            //先设置对象的包装状态为 Unchanged
            entry.State = EntityState.Unchanged;
            //循环被修改的属性名数组
            foreach (string propertyName in propertyNames)
            {
                //将每个被修改的属性的状态设置为已修改状态；这样在后面生成的修改语句时，就只为标识为已修改的属性更新
                entry.Property(propertyName).IsModified = true;
            }
            return await Context.SaveChangesAsync();
        }

        #endregion

        #region 1.0 更新 返回更新受影响的行数

        public async Task<int> UpdateContext(TEntity Model)
        {
            Context.Update(Model);
            return await Context.SaveChangesAsync();
        }

        #endregion

        #region  查询 Iqueryable
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereLambda)
        {
            _myLogger.LogInformation($"{typeof(TEntity)} Model 正在执行查询返回Iqueryable");
            return Dbset.Where(whereLambda).AsNoTracking();
        }
        #endregion
    }
}
