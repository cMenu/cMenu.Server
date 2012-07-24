using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.IO;

namespace cMenu.Metaobjects
{
    public class CMetaobjectLanguage
    {
        #region PROTECTED FIELDS
        protected int _languageCode = -1;
        protected string _languageName = "";
        #endregion

        #region PUBLIC FIELDS
        public int LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = value; }
        }
        public string LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int LanguageInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE, this._languageCode);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME, this._languageName);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_LANGUAGES;
            SQL += "(" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + ", " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME + ")";
            SQL += " VALUES ";
            SQL += "(@p" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + ", @p" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME + ")";

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int LanguageUpdate(IDatabaseProvider Provider)
        {
            var ExistingLanguge = CMetaobjectLanguage.sGetLanguage(this._languageCode, Provider);
            if (ExistingLanguge != null)
            {
                this.LanguageInsert(Provider);
                return -1;
            }

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE, this._languageCode);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME, this._languageName);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_LANGUAGES;
            SQL += "SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME + " = @p" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME;
            SQL += " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + " = @p" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? -1 : -2);
        }
        public int LanguageGet(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE, this._languageCode);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + ", " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME;
            SQL += " FROM " + CDBConst.CONST_TABLE_LANGUAGES;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + " = @p" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._languageName = T.Rows[0][1].CheckDBNULLValue<string>();

            return -1;
        }
        public int LanguageDelete(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE, this._languageCode);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_LANGUAGES;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + " = @p" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CMetaobjectLanguage> sGetAllLanguages(IDatabaseProvider Provider)
        {
            List<CMetaobjectLanguage> R = new List<CMetaobjectLanguage>();

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + ", " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME;
            SQL += " FROM " + CDBConst.CONST_TABLE_LANGUAGES;

            var T = Provider.QueryGetData(SQL, false, null);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Language = new CMetaobjectLanguage()
                {
                    LanguageCode = T.Rows[i][0].CheckDBNULLValue<int>(),
                    LanguageName = T.Rows[i][1].CheckDBNULLValue<string>()
                };
                R.Add(Language);
            }

            return R;
        }
        public static CMetaobjectLanguage sGetLanguage(int LanguageCode, IDatabaseProvider Provider)
        {
            CMetaobjectLanguage R = new CMetaobjectLanguage();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE, LanguageCode);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + ", " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_NAME;
            SQL += " FROM " + CDBConst.CONST_TABLE_LANGUAGES;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE + " = @p" + CDBConst.CONST_TABLE_FIELD_LANGUAGES_CODE;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return null;

            R.LanguageName = T.Rows[0][1].CheckDBNULLValue<string>();
            R.LanguageCode = T.Rows[0][0].CheckDBNULLValue<int>();

            return R;
        }
        #endregion
    }
}
