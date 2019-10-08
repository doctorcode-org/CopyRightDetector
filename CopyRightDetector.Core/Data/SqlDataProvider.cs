using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;

namespace CopyRightDetector.Core.Data
{
    public class SqlDataProvider
    {
        string strConnection;

        public SqlDataProvider()
        {
            strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return strConnection; }
        }

        private async Task<int> RunCommandAsync(SqlCommand cmd)
        {
            using (SqlConnection Cnn = new SqlConnection(strConnection))
            {
                try
                {
                    await Cnn.OpenAsync();
                    cmd.Connection = Cnn;
                    SqlParameter returnValue = new SqlParameter("@RETURN_VALUE", -1);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);
                    await cmd.ExecuteNonQueryAsync();
                    return (int)returnValue.Value;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        private async Task<int> RunCommandAsync(string commandName)
        {
            SqlCommand cmd = new SqlCommand(commandName);
            cmd.CommandType = CommandType.StoredProcedure;
            return await RunCommandAsync(cmd);
        }

        public async Task<bool> RunAsync(List<SqlParameter> param, string commandName)
        {
            SqlCommand cmd = new SqlCommand(commandName);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter par in param)
            {
                if (par.Value == null)
                    par.Value = DBNull.Value;
                cmd.Parameters.Add(par);
            }

            return (await RunCommandAsync(cmd) > 0);
        }

        public async Task<bool> RunAsync(string commandName)
        {
            SqlCommand cmd = new SqlCommand(commandName);
            cmd.CommandType = CommandType.StoredProcedure;
            return (await RunCommandAsync(cmd) > 0);
        }

        public async Task<bool> RunAsync(SqlParameter param, string comandName)
        {
            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param);
            return (await RunCommandAsync(cmd) > 0);
        }

        //___________________________________________________________________________________________________________________________________

        public async Task<Tuple<DataTable, int>> ExportTupleAync(List<SqlParameter> param, string comandName)
        {
            int runResult = -1;

            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter par in param)
            {
                if (par.Value == null)
                {
                    par.Value = DBNull.Value;
                }
                cmd.Parameters.Add(par);
            }

            SqlParameter returnValue = new SqlParameter("@RETURN_VALUE", -1);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            DataTable table = new DataTable();

            using (SqlConnection Cnn = new SqlConnection(strConnection))
            {
                try
                {
                    await Cnn.OpenAsync();
                    cmd.Connection = Cnn;
                    var reader = await cmd.ExecuteReaderAsync(CommandBehavior.Default);
                    table.Load(reader);

                    runResult = (int)returnValue.Value;
                }
                catch (Exception)
                {
                }
            }

            return new Tuple<DataTable, int>(table, runResult);
        }

        public async Task<DataTable> ExportAync(List<SqlParameter> param, string comandName)
        {
            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter par in param)
            {
                if (par.Value == null)
                {
                    par.Value = DBNull.Value;
                }
                cmd.Parameters.Add(par);
            }

            return await ExportAync(cmd);
        }

        public async Task<DataTable> ExportAync(SqlParameter param, string comandName)
        {
            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param);
            return await ExportAync(cmd);
        }

        public async Task<DataTable> ExportAync(string comandName)
        {
            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            return await ExportAync(cmd);
        }

        public async Task<DataTable> ExportAync(SqlCommand command)
        {
            var table = new DataTable();
            using (SqlConnection Cnn = new SqlConnection(strConnection))
            {
                try
                {
                    await Cnn.OpenAsync();
                    command.Connection = Cnn;
                    var reader = await command.ExecuteReaderAsync(CommandBehavior.Default);
                    table.Load(reader);
                }
                catch (Exception)
                {
                }
                return table;
            }
        }

        //___________________________________________________________________________________________________________________________________

        public DataTable Export(List<SqlParameter> param, string comandName)
        {
            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter par in param)
            {
                if (par.Value == null)
                {
                    par.Value = DBNull.Value;
                }
                cmd.Parameters.Add(par);
            }

            return Export(cmd);
        }

        public DataTable Export(SqlParameter param, string comandName)
        {
            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param);
            return Export(cmd);
        }

        public DataTable Export(string comandName)
        {
            SqlCommand cmd = new SqlCommand(comandName);
            cmd.CommandType = CommandType.StoredProcedure;
            return Export(cmd);
        }

        public DataTable Export(SqlCommand command)
        {
            var table = new DataTable();
            using (SqlConnection Cnn = new SqlConnection(strConnection))
            {
                try
                {
                    Cnn.Open();
                    command.Connection = Cnn;
                    var reader = command.ExecuteReader(CommandBehavior.Default);
                    table.Load(reader);
                }
                catch (Exception)
                {
                }
                return table;
            }
        }

        //___________________________________________________________________________________________________________________________________

        public async Task<object> GetValueAsync(string commandName)
        {
            return await GetValueAsync(new SqlCommand(commandName));
        }

        public async Task<object> GetValueAsync(SqlParameter parameter, string commandName)
        {
            SqlCommand cmd = new SqlCommand(commandName);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(parameter);
            return await GetValueAsync(cmd);
        }

        public async Task<T> GetValueAsync<T>(SqlParameter parameter, string commandName)
        {
            return (T)(await GetValueAsync(parameter, commandName));
        }

        public async Task<object> GetValueAsync(List<SqlParameter> parameter, string commandName)
        {
            SqlCommand cmd = new SqlCommand(commandName);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameter)
            {
                if (param.Value == null)
                {
                    param.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param);
            }

            return await GetValueAsync(cmd);
        }

        public async Task<T> GetValueAsync<T>(List<SqlParameter> parameter, string commandName)
        {
            return (T)(await GetValueAsync(parameter, commandName));
        }

        public async Task<object> GetValueAsync(SqlCommand command)
        {
            object result = 0;
            using (SqlConnection Cnn = new SqlConnection(strConnection))
            {
                try
                {
                    await Cnn.OpenAsync();
                    command.Connection = Cnn;

                    result = await command.ExecuteScalarAsync();
                }
                catch (Exception)
                {
                }
                return result;
            }
        }


        public object GetValue(SqlCommand command)
        {
            object result = 0;
            using (SqlConnection Cnn = new SqlConnection(strConnection))
            {
                try
                {
                    Cnn.Open();
                    command.Connection = Cnn;

                    result = command.ExecuteScalar();
                }
                catch (Exception)
                {
                }
                return result;
            }
        }

        public object GetValue(List<SqlParameter> parameter, string commandName)
        {
            SqlCommand cmd = new SqlCommand(commandName);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameter)
            {
                if (param.Value == null)
                {
                    param.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param);
            }

            return GetValue(cmd);
        }

        public object GetValue(SqlParameter parameter, string commandName)
        {
            SqlCommand cmd = new SqlCommand(commandName);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(parameter);

            return GetValue(cmd);
        }

        //___________________________________________________________________________________________________________________________________


        public async Task<string> UploadStream(List<SqlParameter> param, string commandName)
        {
            string streamId = null;

            using (var cnn = new SqlConnection(strConnection))
            {
                try
                {
                    await cnn.OpenAsync();
                    var cmd = new SqlCommand(commandName, cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var par in param)
                    {
                        if (par.Value == null || (par.Value.GetType() == typeof(String) && String.IsNullOrEmpty(par.Value.ToString())))
                            par.Value = DBNull.Value;

                        cmd.Parameters.Add(par);
                    }


                    var result = await cmd.ExecuteScalarAsync();
                    if (result != DBNull.Value)
                    {
                        streamId = result.ToString();
                    }

                    cmd.Dispose();
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (cnn != null && cnn.State == ConnectionState.Open)
                    {
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }

            return streamId;
        }



        //___________________________________________________________________________________________________________________________________

        protected dynamic Cast(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return null;
            }

            Type objType = obj.GetType();
            return Cast(obj, objType);
        }

        protected dynamic Cast(dynamic obj, Type castTo)
        {
            if (castTo == null)
            {
                throw new ArgumentNullException("conversionType");
            }
            if (castTo.IsGenericType && castTo.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (obj == null || obj is DBNull)
                {
                    return null;
                }
                NullableConverter nullableConverter = new NullableConverter(castTo);
                castTo = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(obj, castTo);
        }

        protected SqlParameter MakeTableIdList(string name, int[] ids)
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            if (ids != null)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    var row = dt.NewRow();
                    row["Id"] = ids[i];
                    dt.Rows.Add(row);
                }
            }

            var param = new SqlParameter()
            {
                ParameterName = name,
                Value = dt,
                SqlDbType = SqlDbType.Structured
            };

            return param;

        }

        protected string GetDataStream(object data)
        {
            Guid result;
            if (Guid.TryParse(data.ToString(), out result))
            {
                var value = result.ToString();
                return value;
            }

            return null;
        }


    }
}
