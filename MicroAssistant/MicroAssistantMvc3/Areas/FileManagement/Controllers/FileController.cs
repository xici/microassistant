﻿using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace MicroAssistantMvc.Areas.FileManagement.Controllers
{
    public class FileController : MicControllerBase
    {
        //
        // GET: /FileManagement/File
        private static Regex anymouseAllowAccess = new Regex(@"img[\\/]{1,2}Adimg[\\/]{1,2}([^\.]*?)\.([^\.]*?)$", RegexOptions.IgnoreCase);

        public JsonResult SourceFileClipOrThumb(string sourcePath, PicType fileType, List<FileSaveInfo> saveInfo)
        {
            var Res = new JsonResult();
            AdvancedResult<List<FileSaveResult>> result = new AdvancedResult<List<FileSaveResult>>();
            result.Data = new List<FileSaveResult>();
            try
            {
                if (saveInfo != null)
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                        sourcePath = Server.MapPath(sourcePath);

                        string itempath = FileHelper.GetFileSavePath(userid, fileType, null);
                        string itemfullpath = Server.MapPath(itempath);
                        string rootpath = Server.MapPath(FileHelper.GetRootSavePath());
                        if (!System.IO.File.Exists(sourcePath) || (!sourcePath.StartsWith(rootpath) && !anymouseAllowAccess.IsMatch(sourcePath)))
                        {
                            result.Error = AppError.ERROR_PERSON_NOT_FOUND;
                            result.ExMessage = result.ErrorMessage = "源文件不存在或没有访问权限";
                        }
                        else
                        {
                            string saveName = FileHelper.GetFileSaveName(".png");
                            foreach (FileSaveInfo item in saveInfo)
                            {
                                item.SavePrefix = item.SavePrefix != null ? item.SavePrefix : string.Empty;
                                if (item.IsClipping == 1)
                                {
                                    FileHelper.ClipAndSaveFile(sourcePath, itemfullpath, item.SavePrefix + saveName, item);
                                }
                                else
                                {
                                    FileHelper.ThumbAndSaveFile(sourcePath, itemfullpath, item.SavePrefix + saveName, item);
                                }
                                result.Data.Add(new FileSaveResult()
                                {
                                    FileName = item.SavePrefix + saveName,
                                    FilePath = itempath,
                                    FileUrl = FileHelper.GetFileSavePath(userid, fileType, item.SavePrefix + saveName)
                                });
                            }

                            result.Error = AppError.ERROR_SUCCESS;
                            result.ErrorMessage = result.ExMessage = "成功";
                        }
                    }
                    else
                    {
                        result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                        result.ExMessage = result.ErrorMessage = "用户未登陆，不允许进行该操作！";
                    }
                }
                else
                {
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = result.ExMessage = ex.Message;
                result.Error = AppError.ERROR_FAILED;
            }
            Res.Data = result;
            return Res;
        }

        public JsonResult UploadFile(int saveSource,PicType fileType)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase file = Request.Files[0];

                int seat = file.FileName.LastIndexOf('.');

                //返回位于String对象中指定位置的子字符串并转换为小写.  
                string extension = file.FileName.Substring(seat).ToLower();
                string fileSaveName = FileHelper.GetFileSaveName(extension);

                string path;
                if (saveSource == 1)
                {
                    path = FileHelper.GetFileSavePath(0, fileType, null);
                }
                else
                {
                    path = FileHelper.GetFileSavePath(0, PicType.Ignore, null);
                }

                string savepath = FileHelper.CreatePath(Server.MapPath(path));
                string fullpath = Path.Combine(savepath, fileSaveName);
                file.SaveAs(fullpath);




                ResPic pic = new ResPic();
                if (saveSource == 1)
                {
                    pic.PicUrl = FileHelper.GetFileSavePath(0, fileType, fileSaveName);
                }
                else
                {
                    pic.PicUrl = FileHelper.GetFileSavePath(0, PicType.Ignore, fileSaveName);
                }
                result.Data = pic;
                result.Error = AppError.ERROR_SUCCESS;
            }
            else
            {
                result.Error = AppError.ERROR_FAILED;
                result.ErrorMessage = result.ExMessage = "请选择上传的文件！";
            }

            Res.Data = result;
            return Res;
        }

        public JsonResult GetPic(int picid)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    result.Data =ResPicAccessor.Instance.Get(picid);
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

        public JsonResult AddPic(string sourcePath,PicType picType,string description)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    ResPic pic = new ResPic();
                    pic.ObjId = userid;
                    pic.ObjType = picType;
                    pic.PicUrl = sourcePath;
                    pic.PicDescription = description;
                    pic.State = StateType.Active;

                    int picid = ResPicAccessor.Instance.Insert(pic);
                    pic.PicId = picid;
                    result.Data = pic;
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }


        public JsonResult DeleteFile(int picId)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    ResPicAccessor.Instance.Delete(picId);
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

        public JsonResult SearchResPicByProId(int ObjId, int objType)
        {
            var Res = new JsonResult();
            AdvancedResult<List<ResPic>> result = new AdvancedResult<List<ResPic>>();
            List<ResPic> ress = new List<ResPic>();
            try
            {

                ress = ResPicAccessor.Instance.Search(ObjId, (PicType)objType);
                result.Data = ress;
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

    }
}
