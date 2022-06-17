using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using BitServicesDesktopApp.Helpers;

namespace BitServicesDesktopApp.Models
{
    public class Skill : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _skillName;
        private SQLHelper _db;
        public event PropertyChangedEventHandler PropertyChanged;
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }
        public string this[string propertyName]
        {
            get
            {
                string result = null;
                switch (propertyName)
                {
                    case "SkillName":
                        if (string.IsNullOrEmpty(this.SkillName))
                        {
                            result = "Skill name cannot be left empty";
                        }
                        else if (this.SkillName.Length > 32)
                        {
                            result = "Skill name cannot be more than 32 characters";
                        }
                        break;
                }
                if (result != null && !ErrorCollection.ContainsKey(propertyName))
                {
                    ErrorCollection.Add(propertyName, result);
                    OnPropertyChanged("ErrorCollection");
                }
                return result;
            }
        }
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public string SkillName
        {
            get { return _skillName; }
            set
            {
                _skillName = value;
                OnPropertyChanged("SkillName");
            }
        }
        public Skill()
        {
            _db = new SQLHelper();
        }
        public Skill(DataRow dr)
        {
            _db = new SQLHelper();
            this.SkillName = dr["skill_name"].ToString();
        }

        public int InsertSkill()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            _db = new SQLHelper();
            string sql = "usp_AddSkill";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@SkillName", DbType.String)
            {
                Value = this.SkillName
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
    }
}
