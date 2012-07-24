using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.DB
{
    public class CDBConst
    {
        #region TABLE NAMES
        public const string CONST_TABLE_OBJECTS = "T_OBJ";
        public const string CONST_TABLE_OBJECTS_ATTR = "T_OBJ_ATTR";
        public const string CONST_TABLE_OBJECTS_LINKS = "T_OBJ_LINKS";
        public const string CONST_TABLE_SEQUENCE_OBJECTS = "T_KEYSEQ";
        public const string CONST_TABLE_SEQUENCE_RDS = "T_RDS_ELEM_KEYSEQ";
        public const string CONST_TABLE_SEQUENCE_VERSIONS = "T_RDS_ELEM_VERSEQ";
        public const string CONST_TABLE_RDS_LINKS = "T_RDS_LINKS";
        public const string CONST_TABLE_RDS_ATTRIBUTES = "T_RDS_ATTR";
        public const string CONST_TABLE_RDS_ELEM = "T_RDS_ELEM";
        public const string CONST_TABLE_RDS_ELEM_DATA = "T_RDS_ELEM_DATA";
        public const string CONST_TABLE_META = "T_META";
        public const string CONST_TABLE_METAOBJECT_SECURITY = "T_META_SEC";
        public const string CONST_TABLE_SESSION = "T_SESSION";
        public const string CONST_TABLE_NAMES = "T_OBJ_NAMES";
        public const string CONST_TABLE_COMMENTS = "T_OBJ_COMMENTS";
        public const string CONST_TABLE_ORDERS = "T_ORDERS";
        public const string CONST_TABLE_ORDERS_AMOUNTS = "T_ORDERS_AMOUNTS";
        public const string CONST_TABLE_LANGUAGES = "T_LANGUAGES";
        #endregion

        #region OBJECTS TABLE
        public const string CONST_TABLE_FIELD_OBJ_KEY = "F_OBJ_KEY";
        public const string CONST_TABLE_FIELD_OBJ_ID = "F_OBJ_ID";
        public const string CONST_TABLE_FIELD_OBJ_PARENT = "F_OBJ_PARENT";
        public const string CONST_TABLE_FIELD_OBJ_SYSTEM = "F_OBJ_SYSTEM";
        public const string CONST_TABLE_FIELD_OBJ_CLASS = "F_OBJ_CLASS";
        public const string CONST_TABLE_FIELD_OBJ_MOD = "F_OBJ_MOD";
        public const string CONST_TABLE_FIELD_OBJ_STATUS = "F_OBJ_STATUS";
        #endregion

        #region OBJECT ATTRIBUTES TABLE
        public const string CONST_TABLE_FIELD_OBJ_ATTR_ID = "F_OBJ_ATTR_ID";
        public const string CONST_TABLE_FIELD_OBJ_ATTR_VALUE = "F_OBJ_ATTR_VALUE";
        public const string CONST_TABLE_FIELD_OBJ_ATTR_LOCALE = "F_LOCALE";
        #endregion

        #region OBJECT LINKS TABLE
        public const string CONST_TABLE_FIELD_LINK_OBJ_KEY = "F_LINK_OBJ_KEY";
        public const string CONST_TABLE_FIELD_LINK_OBJ_VALUE = "F_LINK_VALUE";
        public const string CONST_TABLE_FIELD_LINK_OBJ_TYPE = "F_LINK_TYPE";
        #endregion

        #region RDS LINKS
        public const string CONST_TABLE_FIELD_RDS_LINK_KEY = "F_LINK_KEY";
        public const string CONST_TABLE_FIELD_RDS_LINK_ID = "F_LINK_ID";
        public const string CONST_TABLE_FIELD_RDS_LINK_NAME = "F_LINK_NAME";
        public const string CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY = "F_LINK_ATTR_KEY";
        public const string CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY = "F_LINK_LINKED_ATTR_KEY";
        #endregion

        #region RDS ATTRIBUTES TABLE
        public const string CONST_TABLE_FIELD_RDS_ATTR_KEY = "F_ATTR_KEY";
        public const string CONST_TABLE_FIELD_RDS_ATTR_ID = "F_ATTR_ID";
        public const string CONST_TABLE_FIELD_RDS_ATTR_NAME = "F_ATTR_NAME";
        public const string CONST_TABLE_FIELD_RDS_ATTR_TYPE = "F_ATTR_TYPE";
        public const string CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN = "F_ATTR_IS_HIDDEN";
        public const string CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY = "F_ATTR_DICT_KEY";
        #endregion

        #region RDS ELEMENTS TABLE
        public const string CONST_TABLE_FIELD_RDS_ELEM_KEY = "F_ELEM_KEY";
        public const string CONST_TABLE_FIELD_RDS_ELEM_VERSION = "F_ELEM_VERSION";
        public const string CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY = "F_ELEM_DICT_KEY";
        public const string CONST_TABLE_FIELD_RDS_ELEM_PARENT = "F_ELEM_PARENT";
        #endregion

        #region RDS DATA TABLE
        public const string CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE = "F_VALUE";
        public const string CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE = "F_LANG";
        #endregion

        #region SEQUENCE TABLE
        public const string CONST_TABLE_FIELD_SEQUENCE_ID = "F_ID";
        public const string CONST_TABLE_FIELD_SEQUENCE_VALUE = "F_VALUE";
        #endregion

        #region SESSION TABLE
        public const string CONST_TABLE_FIELD_SESSION_KEY = "F_SES_KEY";
        public const string CONST_TABLE_FIELD_SESSION_ID = "F_SES_ID";
        public const string CONST_TABLE_FIELD_SESSION_USER = "F_USER";
        public const string CONST_TABLE_FIELD_SESSION_DEVICE = "F_DEVICE";
        public const string CONST_TABLE_FIELD_SESSION_DEADLINE = "F_DEADLINE";
        public const string CONST_TABLE_FIELD_SESSION_TOKEN = "F_TOKEN";
        public const string CONST_TABLE_FIELD_SESSION_STATUS = "F_STATUS";
        #endregion

        #region META TABLE
        public const string CONST_TABLE_FIELD_META_ID = "F_META_ID";
        public const string CONST_TABLE_FIELD_META_FIELD = "F_META_FIELD";
        public const string CONST_TABLE_FIELD_META_BINDING_XML = "F_META_BINDING_XML";
        #endregion

        #region METAOBJECTS SECURITY TABLE
        public const string CONST_TABLE_FIELD_METAOBJSEC_USER_KEY = "F_USER_KEY";
        public const string CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY = "F_METAOBJECT_KEY";
        public const string CONST_TABLE_FIELD_METAOBJSEC_RIGHTS = "F_RIGHTS";
        #endregion

        #region NAMES TABLE
        public const string CONST_TABLE_FIELD_NAMES_OBJ_KEY = "F_OBJ_KEY";
        public const string CONST_TABLE_FIELD_NAMES_LOCALE = "F_LOCALE";
        public const string CONST_TABLE_FIELD_NAMES_VALUE = "F_VALUE";
        #endregion

        #region COMMENTS TABLE
        public const string CONST_TABLE_FIELD_COMMENTS_OBJ_KEY = "F_OBJ_KEY";
        public const string CONST_TABLE_FIELD_COMMENTS_KEY = "F_COM_KEY";
        public const string CONST_TABLE_FIELD_COMMENTS_ID = "F_COM_ID";
        public const string CONST_TABLE_FIELD_COMMENTS_AUTHOR = "F_COM_AUTHOR";
        public const string CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL = "F_COM_AUTHOR_MAIL";
        public const string CONST_TABLE_FIELD_COMMENTS_DT = "F_COM_DT";
        public const string CONST_TABLE_FIELD_COMMENTS_COMMENT = "F_COM_VALUE";
        public const string CONST_TABLE_FIELD_COMMENTS_PARENT = "F_COM_PARENT";
        #endregion

        #region ORDERS
        public const string CONST_TABLE_FIELD_ORDERS_KEY = "F_ORDER_KEY";
        public const string CONST_TABLE_FIELD_ORDERS_ID = "F_ORDER_ID";
        public const string CONST_TABLE_FIELD_ORDERS_SESSION_KEY = "F_SESSION_KEY";
        public const string CONST_TABLE_FIELD_ORDERS_DT = "F_ORDER_DT";
        public const string CONST_TABLE_FIELD_ORDERS_USER_KEY = "F_USER_KEY";

        public const string CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY = "F_ORDER_KEY";
        public const string CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY = "F_AMOUNT_KEY";
        public const string CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT = "F_AMOUNT";
        #endregion

        #region LANGUAGES
        public const string CONST_TABLE_FIELD_LANGUAGES_CODE = "F_LAN_CODE";
        public const string CONST_TABLE_FIELD_LANGUAGES_NAME = "F_LAN_NAME";
        #endregion
    }
}
