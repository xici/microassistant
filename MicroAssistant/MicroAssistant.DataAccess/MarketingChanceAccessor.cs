﻿/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 10:29:43
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using MicroAssistant.Common;
using MicroAssistant.Meta;


namespace MicroAssistant.DataAccess
{
    public class MarketingChanceAccessor
    {
        private MySqlCommand cmdInsertMarketingChance;
        private MySqlCommand cmdDeleteMarketingChance;
        private MySqlCommand cmdUpdateMarketingChance;
        private MySqlCommand cmdLoadMarketingChance;
        private MySqlCommand cmdLoadAllMarketingChance;
        private MySqlCommand cmdGetMarketingChanceCount;
        private MySqlCommand cmdGetMarketingChance;

        private MarketingChanceAccessor()
        {
            #region cmdInsertMarketingChance

            cmdInsertMarketingChance = new MySqlCommand("INSERT INTO marketing_chance(idmarketing_chance,chance_type,customer_type,contact_name,remark,add_time,qq,email,tel,phone,rate,ent_id,user_id) values (@IdmarketingChance,@ChanceType,@CustomerType,@ContactName,@Remark,@AddTime,@Qq,@Email,@Tel,@Phone,@Rate,@EntId,@UserId)");

            cmdInsertMarketingChance.Parameters.Add("@IdmarketingChance", MySqlDbType.Int32);
            cmdInsertMarketingChance.Parameters.Add("@ChanceType", MySqlDbType.Int32);
            cmdInsertMarketingChance.Parameters.Add("@CustomerType", MySqlDbType.Int32);
            cmdInsertMarketingChance.Parameters.Add("@ContactName", MySqlDbType.String);
            cmdInsertMarketingChance.Parameters.Add("@Remark", MySqlDbType.String);
            cmdInsertMarketingChance.Parameters.Add("@AddTime", MySqlDbType.DateTime);
            cmdInsertMarketingChance.Parameters.Add("@Qq", MySqlDbType.String);
            cmdInsertMarketingChance.Parameters.Add("@Email", MySqlDbType.String);
            cmdInsertMarketingChance.Parameters.Add("@Tel", MySqlDbType.String);
            cmdInsertMarketingChance.Parameters.Add("@Phone", MySqlDbType.String);
            cmdInsertMarketingChance.Parameters.Add("@Rate", MySqlDbType.Int32);
            cmdInsertMarketingChance.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertMarketingChance.Parameters.Add("@UserId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateMarketingChance

            cmdUpdateMarketingChance = new MySqlCommand(" update marketing_chance set idmarketing_chance = @IdmarketingChance,chance_type = @ChanceType,customer_type = @CustomerType,contact_name = @ContactName,remark = @Remark,add_time = @AddTime,qq = @Qq,email = @Email,tel = @Tel,phone = @Phone,rate = @Rate,ent_id = @EntId,user_id = @UserId where idmarketing_chance = @IdmarketingChance");
            cmdUpdateMarketingChance.Parameters.Add("@IdmarketingChance", MySqlDbType.Int32);
            cmdUpdateMarketingChance.Parameters.Add("@ChanceType", MySqlDbType.Int32);
            cmdUpdateMarketingChance.Parameters.Add("@CustomerType", MySqlDbType.Int32);
            cmdUpdateMarketingChance.Parameters.Add("@ContactName", MySqlDbType.String);
            cmdUpdateMarketingChance.Parameters.Add("@Remark", MySqlDbType.String);
            cmdUpdateMarketingChance.Parameters.Add("@AddTime", MySqlDbType.DateTime);
            cmdUpdateMarketingChance.Parameters.Add("@Qq", MySqlDbType.String);
            cmdUpdateMarketingChance.Parameters.Add("@Email", MySqlDbType.String);
            cmdUpdateMarketingChance.Parameters.Add("@Tel", MySqlDbType.String);
            cmdUpdateMarketingChance.Parameters.Add("@Phone", MySqlDbType.String);
            cmdUpdateMarketingChance.Parameters.Add("@Rate", MySqlDbType.Int32);
            cmdUpdateMarketingChance.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateMarketingChance.Parameters.Add("@UserId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteMarketingChance

            cmdDeleteMarketingChance = new MySqlCommand(" delete from marketing_chance where idmarketing_chance = @IdmarketingChance");
            cmdDeleteMarketingChance.Parameters.Add("@IdmarketingChance", MySqlDbType.Int32);
            #endregion

            #region cmdLoadMarketingChance

            cmdLoadMarketingChance = new MySqlCommand(@" select idmarketing_chance,chance_type,customer_type,contact_name,remark,add_time,qq,email,tel,phone,rate,ent_id,user_id from marketing_chance limit @PageIndex,@PageSize");
            cmdLoadMarketingChance.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadMarketingChance.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetMarketingChanceCount

            cmdGetMarketingChanceCount = new MySqlCommand(" select count(*)  from marketing_chance ");

            #endregion

            #region cmdLoadAllMarketingChance

            cmdLoadAllMarketingChance = new MySqlCommand(" select idmarketing_chance,chance_type,customer_type,contact_name,remark,add_time,qq,email,tel,phone,rate,ent_id,user_id from marketing_chance");

            #endregion

            #region cmdGetMarketingChance

            cmdGetMarketingChance = new MySqlCommand(" select idmarketing_chance,chance_type,customer_type,contact_name,remark,add_time,qq,email,tel,phone,rate,ent_id,user_id from marketing_chance where idmarketing_chance = @IdmarketingChance");
            cmdGetMarketingChance.Parameters.Add("@IdmarketingChance", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(MarketingChance e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertMarketingChance = cmdInsertMarketingChance.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertMarketingChance.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertMarketingChance.Parameters["@IdmarketingChance"].Value = e.IdmarketingChance;
                _cmdInsertMarketingChance.Parameters["@ChanceType"].Value = e.ChanceType;
                _cmdInsertMarketingChance.Parameters["@CustomerType"].Value = e.CustomerType;
                _cmdInsertMarketingChance.Parameters["@ContactName"].Value = e.ContactName;
                _cmdInsertMarketingChance.Parameters["@Remark"].Value = e.Remark;
                _cmdInsertMarketingChance.Parameters["@AddTime"].Value = e.AddTime;
                _cmdInsertMarketingChance.Parameters["@Qq"].Value = e.Qq;
                _cmdInsertMarketingChance.Parameters["@Email"].Value = e.Email;
                _cmdInsertMarketingChance.Parameters["@Tel"].Value = e.Tel;
                _cmdInsertMarketingChance.Parameters["@Phone"].Value = e.Phone;
                _cmdInsertMarketingChance.Parameters["@Rate"].Value = e.Rate;
                _cmdInsertMarketingChance.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertMarketingChance.Parameters["@UserId"].Value = e.UserId;

                _cmdInsertMarketingChance.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertMarketingChance.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertMarketingChance.Dispose();
                _cmdInsertMarketingChance = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int IdmarketingChance)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteMarketingChance = cmdDeleteMarketingChance.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteMarketingChance.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteMarketingChance.Parameters["@IdmarketingChance"].Value = IdmarketingChance;


                _cmdDeleteMarketingChance.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteMarketingChance.Dispose();
                _cmdDeleteMarketingChance = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(MarketingChance e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateMarketingChance = cmdUpdateMarketingChance.Clone() as MySqlCommand;
            _cmdUpdateMarketingChance.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateMarketingChance.Parameters["@IdmarketingChance"].Value = e.IdmarketingChance;
                _cmdUpdateMarketingChance.Parameters["@ChanceType"].Value = e.ChanceType;
                _cmdUpdateMarketingChance.Parameters["@CustomerType"].Value = e.CustomerType;
                _cmdUpdateMarketingChance.Parameters["@ContactName"].Value = e.ContactName;
                _cmdUpdateMarketingChance.Parameters["@Remark"].Value = e.Remark;
                _cmdUpdateMarketingChance.Parameters["@AddTime"].Value = e.AddTime;
                _cmdUpdateMarketingChance.Parameters["@Qq"].Value = e.Qq;
                _cmdUpdateMarketingChance.Parameters["@Email"].Value = e.Email;
                _cmdUpdateMarketingChance.Parameters["@Tel"].Value = e.Tel;
                _cmdUpdateMarketingChance.Parameters["@Phone"].Value = e.Phone;
                _cmdUpdateMarketingChance.Parameters["@Rate"].Value = e.Rate;
                _cmdUpdateMarketingChance.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateMarketingChance.Parameters["@UserId"].Value = e.UserId;

                _cmdUpdateMarketingChance.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateMarketingChance.Dispose();
                _cmdUpdateMarketingChance = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 根据条件分页获取指定数据
        /// <param name="pageIndex">当前页</param>
        /// <para>索引从0开始</para>
        /// <param name="pageSize">每页记录条数</param>
        /// <para>记录数必须大于0</para>
        /// </summary>
        public PageEntity<MarketingChance> Search(Int32 IdmarketingChance, Int32 ChanceType, Int32 CustomerType, String ContactName, String Remark, DateTime AddTime, String Qq, String Email, String Tel, String Phone, Int32 Rate, Int32 EntId, Int32 UserId, int pageIndex, int pageSize)
        {
            PageEntity<MarketingChance> returnValue = new PageEntity<MarketingChance>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadMarketingChance = cmdLoadMarketingChance.Clone() as MySqlCommand;
            MySqlCommand _cmdGetMarketingChanceCount = cmdGetMarketingChanceCount.Clone() as MySqlCommand;
            _cmdLoadMarketingChance.Connection = oc;
            _cmdGetMarketingChanceCount.Connection = oc;

            try
            {
                _cmdLoadMarketingChance.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadMarketingChance.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadMarketingChance.Parameters["@IdmarketingChance"].Value = IdmarketingChance;
                _cmdLoadMarketingChance.Parameters["@ChanceType"].Value = ChanceType;
                _cmdLoadMarketingChance.Parameters["@CustomerType"].Value = CustomerType;
                _cmdLoadMarketingChance.Parameters["@ContactName"].Value = ContactName;
                _cmdLoadMarketingChance.Parameters["@Remark"].Value = Remark;
                _cmdLoadMarketingChance.Parameters["@AddTime"].Value = AddTime;
                _cmdLoadMarketingChance.Parameters["@Qq"].Value = Qq;
                _cmdLoadMarketingChance.Parameters["@Email"].Value = Email;
                _cmdLoadMarketingChance.Parameters["@Tel"].Value = Tel;
                _cmdLoadMarketingChance.Parameters["@Phone"].Value = Phone;
                _cmdLoadMarketingChance.Parameters["@Rate"].Value = Rate;
                _cmdLoadMarketingChance.Parameters["@EntId"].Value = EntId;
                _cmdLoadMarketingChance.Parameters["@UserId"].Value = UserId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadMarketingChance.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new MarketingChance().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetMarketingChanceCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadMarketingChance.Dispose();
                _cmdLoadMarketingChance = null;
                _cmdGetMarketingChanceCount.Dispose();
                _cmdGetMarketingChanceCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<MarketingChance> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllMarketingChance = cmdLoadAllMarketingChance.Clone() as MySqlCommand;
            _cmdLoadAllMarketingChance.Connection = oc; List<MarketingChance> returnValue = new List<MarketingChance>();
            try
            {
                _cmdLoadAllMarketingChance.CommandText = string.Format(_cmdLoadAllMarketingChance.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllMarketingChance.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new MarketingChance().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllMarketingChance.Dispose();
                _cmdLoadAllMarketingChance = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public MarketingChance Get(int IdmarketingChance)
        {
            MarketingChance returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetMarketingChance = cmdGetMarketingChance.Clone() as MySqlCommand;

            _cmdGetMarketingChance.Connection = oc;
            try
            {
                _cmdGetMarketingChance.Parameters["@IdmarketingChance"].Value = IdmarketingChance;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetMarketingChance.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new MarketingChance().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetMarketingChance.Dispose();
                _cmdGetMarketingChance = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly MarketingChanceAccessor instance = new MarketingChanceAccessor();
        public static MarketingChanceAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}