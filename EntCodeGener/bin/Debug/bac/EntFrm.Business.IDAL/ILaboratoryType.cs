using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ILaboratoryType
  {
        LaboratoryTypeCollections GetAllRecords();
        LaboratoryTypeCollections GetRecordsByClassNo(string sClassNo);
        LaboratoryTypeCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(LaboratoryType info);
        int UpdateRecord(LaboratoryType info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        LaboratoryTypeCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

