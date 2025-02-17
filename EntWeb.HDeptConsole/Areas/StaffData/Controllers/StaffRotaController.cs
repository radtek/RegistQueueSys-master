﻿using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.StaffData.Controllers
{
    public class StaffRotaController : frmMainController
    {
        public StaffRotaController()
        {
            viewExpress = PublicHelper.Get_ViewExpress();
            viewExpress.ViewTag = "StaffRota";
            ViewBag.ViewExpress = viewExpress;
        }

        // GET: StaffData/StaffRota
        override
        public ActionResult Index()
        {
            string sWorkingMode = PublicHelper.GetParamValue(PublicConsts.DEF_WORKINGMODE, "Others");
            if (sWorkingMode.Equals("STAFF"))
            {
                ViewBag.ItemList = PageService.GetStaffList(true);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "ServRota", new { Area = "ServData" });
            }
        }  

        public ActionResult getDataList_StaffRota(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                TableData tdata = new TableData();
                string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' ";

                if (!string.IsNullOrEmpty(condition) && !("99999999").Equals(condition))
                {
                    strWhere += " And StafferNo='" + condition + "' ";
                }

                ViewStafferRotaBLL infoBLL = new ViewStafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewStafferRotaCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, pageIndex, pageSize, strWhere);
                int count = infoBLL.GetCountByCondition(Condition);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertStaffRotas(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditRota()
        {
            ViewBag.StaffList = PageService.GetStaffList(false);
            ViewBag.WTimeList = PageService.GetWorkTimeList(true);
            return View();
        }

        public void SaveRota(string stafferNo, string week1, string week2, string week3, string week4, string week5, string week6, string week7)
        {
            string SuNo = ((LoginerInfo)Session["loginUser"]).UserNo;

            StafferRotaBLL infoBLL = new StafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            StafferRota info = new StafferRota();
             
            info.sRotaNo = CommonHelper.Get_New12ByteGuid();
            info.sStafferNo = stafferNo;
            info.iRotaType = PublicConsts.ROTATYPE2;  //临时排班
            info.dStartDate = DateTime.Now;
            info.dEnditDate = DateTime.Now;
            info.iWeekDay1 = int.Parse(week1);
            info.iWeekDay2 = int.Parse(week2);
            info.iWeekDay3 = int.Parse(week3);
            info.iWeekDay4 = int.Parse(week4);
            info.iWeekDay5 = int.Parse(week5);
            info.iWeekDay6 = int.Parse(week6);
            info.iWeekDay7 = int.Parse(week7);
            info.sRotaFormat = "";
            info.dRegisteFees = 0;

            info.sBranchNo = PublicHelper.Get_BranchNo();
            info.sAddOptor = SuNo;
            info.dAddDate = DateTime.Now;
            info.sModOptor = SuNo;
            info.dModDate = DateTime.Now;
            info.iValidityState = 1;
            info.sComments = "";
            info.sAppCode = PublicHelper.Get_AppCode() + ";";

            if (infoBLL.AddNewRecord(info))
            {
                Response.Write("SUCCESS");
            }
            else
            {
                Response.Write("ERROR");
            }
        }

        public void DelRota(string ids)
        {
            try
            {
                string[] idList = ids.Split(';');

                StafferRotaBLL infoBLL = new StafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                infoBLL.SoftDeleteRecord(idList);

                Response.Write("SUCCESS");
            }
            catch (Exception ex)
            {
                Response.Write("ERROR");
            }
        }
    }
}