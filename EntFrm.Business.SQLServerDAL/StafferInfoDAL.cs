using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class StafferInfoDAL: IStafferInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From StafferInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From StafferInfo Where   AppCode like @AppCode And   ValidityState=1 And StafferNo=@StafferNo";
        private const string SQL_GET_NAME_BY_NO = @"Select StafferName From StafferInfo Where   AppCode like @AppCode And   ValidityState=1 And StafferNo=@StafferNo";
        private const string SQL_ADD_RECORD = @"Insert into StafferInfo
                                              (StafferNo,StafferName,LoginId,Password,CounterNo,OrganizNo,OrganizName,StarLevel,HeadPhoto,Ranks,Posts,Summary,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@StafferNo,@StafferName,@LoginId,@Password,@CounterNo,@OrganizNo,@OrganizName,@StarLevel,@HeadPhoto,@Ranks,@Posts,@Summary,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update StafferInfo set
                                                 StafferNo=@StafferNo,StafferName=@StafferName,LoginId=@LoginId,Password=@Password,CounterNo=@CounterNo,OrganizNo=@OrganizNo,OrganizName=@OrganizName,StarLevel=@StarLevel,HeadPhoto=@HeadPhoto,Ranks=@Ranks,Posts=@Posts,Summary=@Summary,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And StafferNo=@StafferNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From StafferInfo Where   AppCode like @AppCode And   StafferNo=@StafferNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update StafferInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And StafferNo=@StafferNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From StafferInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update StafferInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From StafferInfo Where    AppCode like @AppCode And   ValidityState=1 And OrganizNo=@OrganizNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From StafferInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_STAFFERNO = "@StafferNo";
        private const string PARAM_STAFFERNAME = "@StafferName";
        private const string PARAM_LOGINID = "@LoginId";
        private const string PARAM_PASSWORD = "@Password";
        private const string PARAM_COUNTERNO = "@CounterNo";
        private const string PARAM_ORGANIZNO = "@OrganizNo";
        private const string PARAM_ORGANIZNAME = "@OrganizName";
        private const string PARAM_STARLEVEL = "@StarLevel";
        private const string PARAM_HEADPHOTO = "@HeadPhoto";
        private const string PARAM_RANKS = "@Ranks";
        private const string PARAM_POSTS = "@Posts";
        private const string PARAM_SUMMARY = "@Summary";
        private const string PARAM_BRANCHNO = "@BranchNo";
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

        public StafferInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public StafferInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            StafferInfoCollections infos = null;
            StafferInfo info = null;

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
                    infos = new StafferInfoCollections();
                    while (reader.Read())
                    {
                        info = new StafferInfo();
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

        public StafferInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            StafferInfoCollections infos = null;
            StafferInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_ORGANIZNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new StafferInfoCollections();
                    while (reader.Read())
                    {
                        info = new StafferInfo();
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

        public StafferInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            StafferInfoCollections infos = null;
            StafferInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new StafferInfoCollections();
                    while (reader.Read())
                    {
                        info = new StafferInfo();
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
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(StafferInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STAFFERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOGINID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PASSWORD,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STARLEVEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_RANKS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_POSTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sStafferNo;
                paras[1].Value = info.sStafferName;
                paras[2].Value = info.sLoginId;
                paras[3].Value = info.sPassword;
                paras[4].Value = info.sCounterNo;
                paras[5].Value = info.sOrganizNo;
                paras[6].Value = info.sOrganizName;
                paras[7].Value = info.sStarLevel;
                paras[8].Value = info.sHeadPhoto;
                paras[9].Value = info.sRanks;
                paras[10].Value = info.sPosts;
                paras[11].Value = info.sSummary;
                paras[12].Value = info.sBranchNo;
                paras[13].Value = info.sAddOptor;
                paras[14].Value = info.dAddDate;
                paras[15].Value = info.sModOptor;
                paras[16].Value = info.dModDate;
                paras[17].Value = info.iValidityState;
                paras[18].Value = info.sComments;
                paras[19].Value = info.sAppCode;

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

        public int UpdateRecord(StafferInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STAFFERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOGINID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PASSWORD,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STARLEVEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_RANKS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_POSTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sStafferNo;
                paras[1].Value = info.sStafferName;
                paras[2].Value = info.sLoginId;
                paras[3].Value = info.sPassword;
                paras[4].Value = info.sCounterNo;
                paras[5].Value = info.sOrganizNo;
                paras[6].Value = info.sOrganizName;
                paras[7].Value = info.sStarLevel;
                paras[8].Value = info.sHeadPhoto;
                paras[9].Value = info.sRanks;
                paras[10].Value = info.sPosts;
                paras[11].Value = info.sSummary;
                paras[12].Value = info.sBranchNo;
                paras[13].Value = info.sAddOptor;
                paras[14].Value = info.dAddDate;
                paras[15].Value = info.sModOptor;
                paras[16].Value = info.dModDate;
                paras[17].Value = info.iValidityState;
                paras[18].Value = info.sComments;
                paras[19].Value = info.sAppCode;
                paras[20].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
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
        public StafferInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            StafferInfoCollections infos = null;
            StafferInfo info = null;

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
                    infos = new StafferInfoCollections();
                    while (reader.Read())
                    {
                        info = new StafferInfo();
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
        internal static void PutObjectProperty(StafferInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sStafferNo= reader["StafferNo"].ToString();
            obj_info.sStafferName= reader["StafferName"].ToString();
            obj_info.sLoginId= reader["LoginId"].ToString();
            obj_info.sPassword= reader["Password"].ToString();
            obj_info.sCounterNo= reader["CounterNo"].ToString();
            obj_info.sOrganizNo= reader["OrganizNo"].ToString();
            obj_info.sOrganizName= reader["OrganizName"].ToString();
            obj_info.sStarLevel= reader["StarLevel"].ToString();
            obj_info.sHeadPhoto= reader["HeadPhoto"].ToString();
            obj_info.sRanks= reader["Ranks"].ToString();
            obj_info.sPosts= reader["Posts"].ToString();
            obj_info.sSummary= reader["Summary"].ToString();
            obj_info.sBranchNo= reader["BranchNo"].ToString();
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
