using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace DapperWebAPI.Services
{
    public class DatabaseHelper
    {
        //
        private readonly SqlConnection dbConnection;
        protected IConfiguration configuration;

        public DatabaseHelper(IConfiguration cf)
        {
            this.configuration = cf;
            this.dbConnection = new SqlConnection(cf["ConnectionStrings:Default"]);
        }


        public async Task<int> Execute(string sql, DynamicParameters parameters = null)
        {
            try
            {
                if (String.IsNullOrEmpty(sql)) throw new NoNullAllowedException();

                using (SqlConnection connection = dbConnection )
                {
                    await connection.OpenAsync();
                    return connection?.ExecuteAsync(sql, parameters).Result ?? throw new NullReferenceException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IDictionary<string, object> ExecuteOutput(string sql, DynamicParameters  parameters, params string[] outs) 
        {
            
            try
            {
                IDictionary<string, object> outputs = new Dictionary<string, object>();
                using (SqlConnection connection = dbConnection)
                {
                    connection.Open();
                    connection.Execute(sql,param: parameters,commandType: CommandType.StoredProcedure);
                    
                    foreach (string p in outs)
                    {
                        outputs.Add(new KeyValuePair<string, object>(p, parameters.Get<dynamic>(p)));
                    }  
                }
                return outputs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> Query<T>(string sql, DynamicParameters parameters = null)
        {
            try
            {
                if (String.IsNullOrEmpty(sql)) throw new NoNullAllowedException();
                using (SqlConnection connection = dbConnection)
                {
                    SetupColumnMapping<T>();

                    await connection.OpenAsync();
                    return connection?.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure).Result.ToList() ?? throw new NullReferenceException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IDictionary<string,object>> QueryMulti(string sql, DynamicParameters parameters = null, params Type[] results ) 
        {

            try
            {
                if (String.IsNullOrEmpty(sql)) throw new NoNullAllowedException();
                IDictionary<string, object> outputs = new Dictionary<string,object>();
                using (SqlConnection connection = dbConnection)
                {
                    await connection.OpenAsync();

                    using (var multi = connection.QueryMultipleAsync(sql, parameters, null, null, commandType: CommandType.StoredProcedure).Result)
                    {
                        foreach (Type typ in results)
                        {

                            SetupColumnMapping(typ);
                            outputs.Add(typ.Name, multi.Read(typ).ToList());
                        }

                    }
                    
                }

                return outputs;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        private void SetupColumnMapping<T>()  =>  
            SqlMapper.SetTypeMap(
                typeof(T),new CustomPropertyTypeMap(
                typeof(T),(type, columnName) =>
                type.GetProperties().FirstOrDefault(prop =>
                prop.GetCustomAttributes(false)
                .OfType<ColumnAttribute>()
                .Any(attr => attr.Name == columnName))
                )
            );

        private void SetupColumnMapping(Type typ) =>
            SqlMapper.SetTypeMap(
                typ, new CustomPropertyTypeMap(
                typ, (type, columnName) =>
                 type.GetProperties().FirstOrDefault(prop =>
                 prop.GetCustomAttributes(false)
                 .OfType<ColumnAttribute>()
                 .Any(attr => attr.Name == columnName))
                )
            );
    }

   


}
