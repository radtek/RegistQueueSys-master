using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ICounterInfo
  {
        CounterInfoCollections GetAllRecords();
        CounterInfoCollections GetRecordsByClassNo(string sClassNo);
        CounterInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(CounterInfo info);
        int UpdateRecord(CounterInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        CounterInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

