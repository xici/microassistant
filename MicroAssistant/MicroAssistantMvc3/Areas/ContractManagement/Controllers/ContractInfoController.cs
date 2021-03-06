﻿using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.ContractManagement.Controllers
{
    public class ContractInfoController : MicControllerBase
    {
        //
        // GET: /ContractManagement/ContractInfo/

     


        /// <summary>
        /// 根据企业ID查询合同查询（token）返回 合同列表{合同名称，合同编号，客户，合同金额，合同时间}
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult GetContractInfoByEID(int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<ContractInfo>> result = new AdvancedResult<PageEntity<ContractInfo>>();
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                if (!CheckUserFunction(23))
                {
                    result.Error = AppError.ERROR_PERMISSION_FORBID;
                    Res.Data = result;
                    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return Res;
                }

                UserType userType = UserType.User;
                int sid = CurrentUser.UserId;

                if (CheckUserFunction(27))
                {
                    userType = UserType.Boss;
                    sid = CurrentUser.EntId;
                }
                if (CheckUserFunction(25))
                {
                    userType = UserType.Admin;
                    sid = CurrentUser.EntId;
                }
                    
                try
                {
                    PageEntity<ContractInfo> list = new PageEntity<ContractInfo>();
                    list = ContractInfoAccessor.Instance.Search(userType, sid, pageIndex, pageSize);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }

                result.Error = AppError.ERROR_SUCCESS;
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }


            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 添加企业合同(合同编号，合同名称，客户名称，合同有效期1，合同有效期2，合同承办人，合同签订时间，合同金额，付款方式（xml)
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public JsonResult AddContractInfo(int ChanceId,String ContractNo, String CName, string CustomerName, DateTime StartTime, DateTime EndTime,
            DateTime ContractTime, double Amount, int HowToPay, List<ContractHowtopay> HowtopayList, List<int> attachments)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            int _conid = 0;
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                if (!CheckUserFunction(24))
                {
                    result.Error = AppError.ERROR_PERMISSION_FORBID;
                    Res.Data = result;
                    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return Res;
                }
                int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                try
                {
                    ContractInfo con = new ContractInfo();
                    con = ContractInfoAccessor.Instance.Get(ContractNo);
                    if (con != null)
                    {
                        result.Error = AppError.ERROR_CONTRACTNO_EXIST;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }

                    ContractInfo co = new ContractInfo();
                    co.OwnerId = Convert.ToInt32(ownerid);
                    co.CName = CName;
                    co.ContractNo = ContractNo;
                    co.CustomerName = CustomerName;
                    co.StartTime = StartTime;
                    co.EndTime = EndTime;
                    co.ContractTime = ContractTime;
                    co.Amount = Amount;
                    co.Howtopay = HowToPay;
                    co.HowtopayList = HowtopayList;
                    co.EntId = CurrentUser.EntId;
                    co.ChanceId = ChanceId;
                    //if (entid == 0)
                    //{
                    _conid = ContractInfoAccessor.Instance.Insert(co);

                    try
                    {
                        for (int i = 0; i < HowtopayList.Count; i++)
                        {
                            HowtopayList[i].EntId = CurrentUser.EntId;
                            ContractHowtopayAccessor.Instance.Insert(HowtopayList[i]);
                        }
                    }
                    catch
                    {
                    }
                        //}
                        //else
                        //{
                        //    _entid = entid;

                        //    ce.EntId = _entid;
                        //    ContractInfoAccessor.Instance.Update(ce);
                        //}
                    try
                    {
                        for (int i = 0; i < attachments.Count; i++)
                        {
                            ResPic oldpic = ResPicAccessor.Instance.Get(attachments[i]);
                            oldpic.ObjId = _conid;
                            oldpic.ObjType = PicType.ContractFile;
                            ResPicAccessor.Instance.Update(oldpic);
                        }
                    }
                    catch
                    {
                    }
                        result.Error = AppError.ERROR_SUCCESS;
                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }



            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

        //根据合同编号获取合同信息（合同编号，token）返回（合同名称，合同编号，客户名称，合同金额，付款方式（xml），合同有效期，合同承办人，合同时间，附件{附件url1，附件url2}）
        public JsonResult GetContractInfoByContractNo(string contractNo)
        {
            var Res = new JsonResult();
            AdvancedResult<ContractInfo> result = new AdvancedResult<ContractInfo>();
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                if (!CheckUserFunction(23))
                {
                    result.Error = AppError.ERROR_PERMISSION_FORBID;
                    Res.Data = result;
                    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return Res;
                }
               // int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                try
                {
                    ContractInfo con = new ContractInfo();
                    con = ContractInfoAccessor.Instance.Get(contractNo);
                    con.HowtopayList = ContractHowtopayAccessor.Instance.Search(contractNo, 0);
                    con.Chance = MarketingChanceAccessor.Instance.Get(con.ChanceId);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = con;

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }

                result.Error = AppError.ERROR_SUCCESS;
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }


            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        ///// <summary>
        ///// 通过合同编号获取合同分期付款细节
        ///// </summary>
        ///// <param name="ContractNo"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public JsonResult GetHowtopayListByContractNo(string ContractNo, string token)
        //{
        //    var Res = new JsonResult();
        //    AdvancedResult<List<ContractHowtopay>> result = new AdvancedResult<List<ContractHowtopay>>();
        //    if (CacheManagerFactory.GetMemoryManager().Contains(token))
        //    {
        //        // int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
        //        try
        //        {
        //            List<ContractHowtopay> con = new List<ContractHowtopay>();
        //            con =  ContractHowtopayAccessor.Instance.Search(ContractNo);
        //            result.Error = AppError.ERROR_SUCCESS;
        //            result.Data = con;

        //        }
        //        catch (Exception e)
        //        {
        //            result.Error = AppError.ERROR_FAILED;
        //            result.ExMessage = e.ToString();
        //        }

        //        result.Error = AppError.ERROR_SUCCESS;
        //    }
        //    else
        //    {
        //        result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
        //    }


        //    Res.Data = result;
        //    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    return Res;
        //}

    }
}
