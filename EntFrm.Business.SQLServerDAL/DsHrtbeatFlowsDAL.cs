using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class DsHrtbeatFlowsDAL: IDsHrtbeatFlows
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DsHrtbeatFlows Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DsHrtbeatFlows Where   AppCode like @AppCode And   ValidityState=1 And BFlowNo=@BFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select PlayerCode From DsHrtbeatFlows Where   AppCode like @AppCode And   ValidityState=1 And BFlowNo=@BFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into DsHrtbeatFlows
                                              (BFlowNo,PlayerNo,PlayerCode,IpAddress,LocalPort,RegistDate,CheckState,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@BFlowNo,@PlayerNo,@PlayerCode,@IpAddress,@LocalPort,@RegistDate,@CheckState,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DsHrtbeatFlows set
                                                 BFlowNo=@BFlowNo,PlayerNo=@PlayerNo,PlayerCode=@PlayerCode,IpAddress=@IpAddress,LocalPort=@LocalPort,RegistDate=@RegistDate,CheckState=@CheckState,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And BFlowNo=@BFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DsHrtbeatFlows Where   AppCode like @AppCode And   BFlowNo=@BFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DsHrtbeatFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And BFlowNo=@BFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From DsHrtbeatFlows Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update DsHrtbeatFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DsHrtbeatFlows Where    AppCode like @AppCode And   ValidityState=1 And PlayerNo=@PlayerNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DsHrtbeatFlows Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_BFLOWNO = "@BFlowNo";
        private const string PARAM_PLAYERNO = "@PlayerNo";
        private const string PARAM_PLAYERCODE = "@PlayerCode";
        private const string PARAM_IPADDRESS = "@IpAddress";
        private const string PARAM_LOCALPORT = "@LocalPort";
        private const string PARAM_REGISTDATE = "@RegistDate";
        private const string PARAM_CHECKSTATE = "@CheckState";
        private const string PARAM_ADDOPTOR = "@AddOptor";
        private const string PARAM_ADDDATE = "@AddDate";
        private const string PARAM_MODOPTOR = "@ModOptor";
        private const string PARAM_MODDATE = "@ModDate";
        private const string PARAM_VALIDITYSTATE = "@ValidityState";
        private const string PARAM_COMMENTS = "@Comments";
        private const string PARAM_APPCODE = "@AppCode";
        private const string PARAM_VERSION = "@Version";
        #endregion

        private string connStr;
        private string appCode;

        public DsHrtbeatFlowsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DsHrtbeatFlowsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsHrtbeatFlowsCollections infos = null;
            DsHrtbeatFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                 };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS,paras);

                if (reader.HasRows)
                {
                    infos = new DsHrtbeatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsHrtbeatFlows();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 查询所有记录(DAL层|GetAllRecords)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public DsHrtbeatFlowsCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsHrtbeatFlowsCollections infos = null;
            DsHrtbeatFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new DsHrtbeatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsHrtbeatFlows();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sClassNo查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public DsHrtbeatFlowsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsHrtbeatFlowsCollections infos = null;
            DsHrtbeatFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DsHrtbeatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsHrtbeatFlows();
                        //设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public string GetRecordNameByNo(string sNo)
        {
            SqlConnection connection = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return (string)SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL_GET_NAME_BY_NO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录名称(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int AddNewRecord(DsHrtbeatFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.Int),
                    new SqlParameter(PARAM_REGISTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sBFlowNo;
                paras[1].Value = info.sPlayerNo;
                paras[2].Value = info.sPlayerCode;
                paras[3].Value = info.sIpAddress;
                paras[4].Value = info.iLocalPort;
                paras[5].Value = info.dRegistDate;
                paras[6].Value = info.iCheckState;
                paras[7].Value = info.sAddOptor;
                paras[8].Value = info.dAddDate;
                paras[9].Value = info.sModOptor;
                paras[10].Value = info.dModDate;
                paras[11].Value = info.iValidityState;
                paras[12].Value = info.sComments;
                paras[13].Value = info.sAppCode;

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_ADD_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 添加(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int UpdateRecord(DsHrtbeatFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.Int),
                    new SqlParameter(PARAM_REGISTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sBFlowNo;
                paras[1].Value = info.sPlayerNo;
                paras[2].Value = info.sPlayerCode;
                paras[3].Value = info.sIpAddress;
                paras[4].Value = info.iLocalPort;
                paras[5].Value = info.dRegistDate;
                paras[6].Value = info.iCheckState;
                paras[7].Value = info.sAddOptor;
                paras[8].Value = info.dAddDate;
                paras[9].Value = info.sModOptor;
                paras[10].Value = info.dModDate;
                paras[11].Value = info.iValidityState;
                paras[12].Value = info.sComments;
                paras[13].Value = info.sAppCode;
                paras[14].Value = StringHelper.ConvertToBytes(info.sVersion);

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_UPDATE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 更新记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int HardDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_HARD_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 硬删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int SoftDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_SOFT_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 软删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

public int HardDeleteByCondition(string sCondtion)
{
    SqlConnection connection = null;
    try
    {
        string sql = SQL_HARD_DELETE_BY_CONDTION;
        if (!string.IsNullOrEmpty(sCondtion))
        {
           sql += " And " + sCondtion;
       }
       SqlParameter[] paras = new SqlParameter[]
      {
      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
       };
       paras[0].Value = "%" + appCode + ";%";
      connection = SqlHelper.GetConnection(connStr);
       return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);
  }
  catch (Exception ex)
  {
      throw new Exception("硬删除记录(DAL层)时出错; " + ex.Message);
  }
  finally
  {
      if (connection != null)
          connection.Dispose();
  }
 }
public int SoftDeleteByCondition(string sCondtion)
{
   SqlConnection connection = null;
    try
   {
       string sql = SQL_SOFT_DELETE_BY_CONDTION;
       if (!string.IsNullOrEmpty(sCondtion))
       {
           sql += " And " + sCondtion;
       }
       SqlParameter[] paras = new SqlParameter[]
       {
      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
      };
      paras[0].Value = "%" + appCode + ";%";
      connection = SqlHelper.GetConnection(connStr);
      return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);
  }
  catch (Exception ex)
 {
     throw new Exception(" 硬删除记录(DAL层)时出错; " + ex.Message);
  }
  finally
  {
     if (connection != null)
         connection.Dispose();
 }
 }
        public DsHrtbeatFlowsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsHrtbeatFlowsCollections infos = null;
            DsHrtbeatFlows info = null;

            try
            {
                 if (s_model.sCondition.Length==0)
                {
                    s_model.sCondition = " Where  AppCode like '%" + appCode + ";%' And ValidityState=1";
                }
                else
                {
                    s_model.sCondition = " Where   AppCode like '%" + appCode + ";%' And ValidityState=1 And  " + s_model.sCondition;
                }

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new DsHrtbeatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsHrtbeatFlows();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 分页查询(DAL层)记录时出错;;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int GetCountByCondition(string sCondition)
        {
            SqlConnection connection = null;

            try
            {
                string strSql = SQL_GET_COUNT_BY_CONDITION;
                if(sCondition.Length>0)
                {
                    strSql +="  And " + sCondition;
                }

                SqlParameter[] paras = new SqlParameter[]
                {
                   new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.Text, strSql, paras));
            }
            catch (Exception ex)
            {
                throw new Exception(" 计算记录总数(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        #region PutObjectProperty 设置对象属性
        /// <summary>
        /// 从 SqlDataReader 类对象中读取并设置对象属性
        /// </summary>
       /// <param name=" obj_info">主题对象</param>
        /// <param name="dr">读入数据</param>
        internal static void PutObjectProperty(DsHrtbeatFlows obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sBFlowNo= reader["BFlowNo"].ToString();
            obj_info.sPlayerNo= reader["PlayerNo"].ToString();
            obj_info.sPlayerCode= reader["PlayerCode"].ToString();
            obj_info.sIpAddress= reader["IpAddress"].ToString();
            obj_info.iLocalPort= int.Parse(reader["LocalPort"].ToString());
            obj_info.dRegistDate= DateTime.Parse(reader["RegistDate"].ToString());
            obj_info.iCheckState= int.Parse(reader["CheckState"].ToString());
            obj_info.sAddOptor= reader["AddOptor"].ToString();
            obj_info.dAddDate= DateTime.Parse(reader["AddDate"].ToString());
            obj_info.sModOptor= reader["ModOptor"].ToString();
            obj_info.dModDate= DateTime.Parse(reader["ModDate"].ToString());
            obj_info.iValidityState= int.Parse(reader["ValidityState"].ToString());
            obj_info.sComments= reader["Comments"].ToString();
            obj_info.sAppCode= reader["AppCode"].ToString();
            obj_info.sVersion= StringHelper.ConvertToString((byte[])reader["Version"]);
        }
        #endregion
    }
}
