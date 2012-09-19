using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.Common;

namespace cMenu.Metaobjects.Linq
{
    public static class CMetaobjectLanguageEx
    {
        #region STATIC FUNCTIONS
        public static List<CMetaobjectLanguage> GetAllLanguages(this Table<CMetaobjectLanguage> Table)
        {
            return CMetaobjectLanguage.sGetAllLanguages(Table.Context);
        }
        public static CMetaobjectLanguage GetLanguage(this Table<CMetaobjectLanguage> Table, int LanguageCode)
        {
            return CMetaobjectLanguage.sGetLanguage(LanguageCode, Table.Context);
        }
        #endregion
    }

    [Serializable]
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
        public int LanguageInsert(DataContext Context)
        {
            var Langs = Context.GetTable<CMetaobjectLanguage>();

            Langs.InsertOnSubmit(this);

            return CErrors.ERR_SUC;
        }
        public int LanguageUpdate(DataContext Context)
        {
            this.LanguageCode = this.LanguageCode;

            return CErrors.ERR_SUC;
        }
        public int LanguageGet(DataContext Context)
        {
            var Langs = Context.GetTable<CMetaobjectLanguage>();
            var Query = from Lang in Langs
                        where Lang.LanguageCode == this._languageCode
                        select Lang;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            var Result = Query.ToList()[0];

            this._languageName = Result.LanguageName;

            return CErrors.ERR_SUC;
        }
        public int LanguageDelete(DataContext Context)
        {
            var Langs = Context.GetTable<CMetaobjectLanguage>();
            var Query = from Lang in Langs
                        where Lang.LanguageCode == this._languageCode
                        select Lang;

            Langs.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CMetaobjectLanguage> sGetAllLanguages(DataContext Context)
        {
            List<CMetaobjectLanguage> R = new List<CMetaobjectLanguage>();

            R = Context.GetTable<CMetaobjectLanguage>().ToList();

            return R;
        }
        public static CMetaobjectLanguage sGetLanguage(int LanguageCode, DataContext Context)
        {
            var Languages = Context.GetTable<CMetaobjectLanguage>();
            var Result = from Lang in Languages
                         where Lang.LanguageCode == LanguageCode
                         select Lang;

            return (Result.Count() == 0 ? null : Result.ToList()[0]);
        }
        #endregion
    }
}
