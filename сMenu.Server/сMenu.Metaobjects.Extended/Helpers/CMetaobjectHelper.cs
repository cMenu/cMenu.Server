using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Security;
using cMenu.Security.UsersManagement;

namespace cMenu.Metaobjects.Extended.Helpers
{
    public class CMetaobjectHelper
    {
        public static List<CMetaobject> sFilterObjectsByUser(List<CMetaobject> List, CSystemUser User, bool FilterByStatus = true)
        {
            List<CMetaobject> R = new List<CMetaobject>();
            foreach (CMetaobject Object in List)
            {
                var Rights = User.GetRightsForMetaobject(Object);
                if (Rights > 0)
                {
                    if (FilterByStatus && (Object.Status == EnMetaobjectStatus.EDisabled || Object.Status == EnMetaobjectStatus.EBanned))
                        continue;
                    Object.Children = sFilterObjectsByUser(Object.Children, User, FilterByStatus);
                    R.Add(Object);
                }
            }
            return R;
        }
        public static List<CMetaobjectLink> sFilterSourceLinksByUser(List<CMetaobjectLink> List, CSystemUser User)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();
            foreach (CMetaobjectLink Object in List)
            {
                if (User.SecurityRecords.Where(S => S.MetaobjectKey == Object.LinkedObjectKey && S.Rights > 0).ToList().Count != 0)
                    R.Add(Object);
            }
            return R;
        }
        public static List<CMetaobjectLink> sFilterDestinationLinksByUser(List<CMetaobjectLink> List, CSystemUser User)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();
            foreach (CMetaobjectLink Object in List)
            {
                if (User.SecurityRecords.Where(S => S.MetaobjectKey == Object.SourceObjectKey && S.Rights > 0).ToList().Count != 0)
                    R.Add(Object);
            }
            return R;
        }
        public static List<CMediaResource> sFilterMediaByUser(List<CMediaResource> List, CSystemUser User)
        {
            List<CMediaResource> R = new List<CMediaResource>();
            foreach (CMediaResource Object in List)
            {
                var Rights = User.GetRightsForMetaobject(Object);
                if (Rights > 0)
                {
                    if (Object.Status == EnMetaobjectStatus.EDisabled || Object.Status == EnMetaobjectStatus.EBanned)
                        continue;
                    Object.Children = sFilterObjectsByUser(Object.Children, User);
                    R.Add(Object);
                }
            }
            return R;
        }
        public static CMetaobject sFindObjectByKey(decimal Key, CMetaobject Root)
        {
            if (Root.Key == Key)
                return Root;
            foreach (CMetaobject Object in Root.Children)
            {
                var R = sFindObjectByKey(Key, Object);
                if (R != null)
                    return R;
            }
            return null;
        }
    }
}
