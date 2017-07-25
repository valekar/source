using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Text;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using NLog;
using System.Configuration;
namespace Stuart_V2.Models
{
    public class LDAPAuthentication
    {
        private string _message = "";

        public LDAPAuthentication(String path)
        {
            _path = path;
        }

        public string Message()
        {
            return _message;
        }

        public static string GetDomainName(string usernameDomain)
        {
            if (string.IsNullOrEmpty(usernameDomain))
            {
                throw (new ArgumentException("Argument can't be null.", "usernameDomain"));
            }

            if (usernameDomain.Contains("\\"))
            {
                int index = usernameDomain.IndexOf("\\");
                return usernameDomain.Substring(0, index);
            }
            else
                return "";
        }

        public static string GetUsername(string usernameDomain)
        {
            if (string.IsNullOrEmpty(usernameDomain))
            {
                //throw (new ArgumentException("Argument can't be null.", "usernameDomain"));
                return "";
            }
            if (usernameDomain.Contains("\\"))
            {
                int index = usernameDomain.IndexOf("\\");
                return usernameDomain.Substring(index + 1);
            }
            else
                return usernameDomain;
        }

        private String _path;
        private String _filterAttribute;

        private bool _isAdmin = false;
        private string _usrName = "";
        private string _pwd = "";
        private bool _isUser = false;
        private bool _isWriter = false;
        private string _AdminGroup = "";
        private string _UserGroup = "";
        private string _WriterGroup = "";
        private bool _isAuthenticated = false;
        private bool _hasGroup = false;
        private string _usrFullName = "";
        private string _usrDomain = "";
        private string _emailId = "";

        public string UserEmailId
        {
            get { return _emailId; }
        }

        public string UserFullName
        {
            get { return _usrFullName; }
        }

        public bool AuthenticatedOnly
        {
            get { return _isAuthenticated; }
        }

        public bool HasGroup
        {
            get { return _hasGroup; }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
        }

        public string UserName
        {
            set { _usrName = value; }
        }

        public string Password
        {
            set { _pwd = value; }
        }

        public bool IsUser
        {
            get { return _isUser; }
        }

        public string AdminGroup
        {
            get { return _AdminGroup; }
            set { _AdminGroup = value; }
        }

        public string UserGroup
        {
            get { return _UserGroup; }
            set { _UserGroup = value; }

        }

        public string WriterGroup
        {
            get { return _WriterGroup; }
            set { _WriterGroup = value; }

        }

        public bool IsWriter
        {
            get { return _isWriter; }
        }

        private Logger log = LogManager.GetCurrentClassLogger();
        public bool IsGroupsAuthenticated(string userName, string pwd)
        {
            bool _isAuthenticated = false;

            try
            {
                //string domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                //log.Info("Domain Name: " + domain + "User: " + userName);
                if (pwd == null)
                {
                    _isAuthenticated = true;
                }
                else
                {
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
                    {
                        if (ctx.ValidateCredentials(userName, pwd))
                        {
                            _isAuthenticated = true;
                        }
                    }
                }
                if (_isAuthenticated)
                {
                    string domainName = GetDomainName(userName).Trim();
                    IList<string> lstDom = new List<string>();
                    if (domainName != "") lstDom.Add(domainName + ".ri.redcross.net");
                    //domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                    //lstDom.Add(domainName); 
                    lstDom.Add("archq.ri.redcross.net");
                    lstDom.Add("bio.ri.redcross.net");
                    lstDom.Add("rc.ri.redcross.net");
                    lstDom.Add("arcdrohq.ri.redcross.net");

                    foreach (string str in lstDom)
                    {
                        using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, str))
                        {
                            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName);
                            if (user != null)
                            {
                                //var groups = user.GetGroups(ctx).ToList();
                                
                                //changed by srini
                                //_usrFullName = userName;
                                _usrFullName = user.SamAccountName;
                                //  _usrFullName = user.GivenName.ToUpper() + " " + user.MiddleName ?? "" + " " + user.Surname.ToUpper();
                                using (PrincipalContext ctxARCHQ = new PrincipalContext(ContextType.Domain, "archq.ri.redcross.net"))
                                {
                                    //var groups1 = user.GetGroups(ctxARCHQ);

                                    foreach (string strgrp in _UserGroup.Split(','))
                                    {
                                        try
                                        {
                                            if (strgrp.Trim() != "")
                                            {
                                                if (user.IsMemberOf(ctxARCHQ, IdentityType.Name, strgrp.Trim()))
                                                {

                                                    log.Info("User Group :" + _UserGroup);
                                                    _isUser = true;
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                    foreach (string strgrp in _WriterGroup.Split(','))
                                    {
                                        try
                                        {
                                            if (strgrp.Trim() != "")
                                            {
                                                if (user.IsMemberOf(ctxARCHQ, IdentityType.Name, strgrp.Trim()))
                                                {

                                                    log.Info("Writer Group :" + _WriterGroup);
                                                    _isWriter = true;
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                    foreach (string strgrp in _AdminGroup.Split(','))
                                    {
                                        try
                                        {
                                            if (strgrp.Trim() != "")
                                            {
                                                if (user.IsMemberOf(ctxARCHQ, IdentityType.Name, strgrp.Trim()))
                                                {

                                                    log.Info("Admin Group :" + _AdminGroup);
                                                    _isAdmin = true;
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                    break;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _message = "Error principal context " + ex.Message;
                if (ex.InnerException != null) _message += " Inner Exception: " + ex.InnerException;
                log.Info("User Name : " + userName + ", " + _message);
                _isAuthenticated = false;
            }
            return _isAuthenticated;
        }

        public bool IsAuthValid(String domainAndUsername, String pwd)
        {
            bool ret = false;
            try
            {
                string domainName = GetDomainName(domainAndUsername).Trim();
                string userName = GetUsername(domainAndUsername).Trim();
                string usrNTEmail = userName;
                int startIndex = userName.IndexOf("@");
                if (startIndex <= 0)
                {
                    usrNTEmail = userName + "@redcross.org";
                }


                string userNameWithDomain = _path.Replace(ConfigurationManager.AppSettings["DomainBase"], string.Empty).Replace("LDAP://", string.Empty) + @"\" + userName;
                using (DirectoryEntry entry = new DirectoryEntry(_path, userNameWithDomain,pwd))
                {
                    _isAuthenticated = false;
                    //Bind to the native AdsObject to force authentication.
                    log.Info("Authenticating user :" + entry.Username);
                    Object obj = entry.NativeObject;
                    _isAuthenticated = true;
                    log.Info("User Authenticated :" + entry.Username);

                    IList<string> lstDom = new List<string>();
                    if (domainName != "") lstDom.Add(domainName + ".ri.redcross.net");
                    lstDom.Add("archq.ri.redcross.net");
                    lstDom.Add("bio.ri.redcross.net");
                    lstDom.Add("rc.ri.redcross.net");
                    lstDom.Add("arcdrohq.ri.redcross.net");
                    //using (var forest = Forest.GetCurrentForest())
                    //{
                    //    foreach (Domain domain in forest.Domains)
                    //    {
                    //        var d = domain.Name;
                    //      domain.Dispose();
                    //}}
                    
                    foreach (string str in lstDom)
                    {
                        _path = @"LDAP://" + str;
                        log.Info("Path:" + _path + ", User : " + userName + ", User Email: " + usrNTEmail);
                        entry.Path = _path;
                        using (DirectorySearcher search = new DirectorySearcher(entry))
                        {
                            SearchResult result = null;
                            search.Filter = "(|(Mail=" + usrNTEmail + ")(SAMAccountName=" + userName + "))";
                            search.PropertiesToLoad.Add("cn");
                            search.PropertiesToLoad.Add("memberOf");
                            search.PropertiesToLoad.Add("SAMAccountName");
                            search.PropertiesToLoad.Add("mail");
                            result = search.FindOne();

                            if (null != result)
                            {
                                _usrDomain = str;
                                //Update the new path to the user in the directory.
                                _path = result.Path;
                                _filterAttribute = (String)result.Properties["cn"][0];
                                //_usrFullName = _filterAttribute.ToUpper();
                                //srini
                                _usrFullName = (String)result.Properties["SAMAccountName"][0];
                                _emailId = (String)result.Properties["mail"][0]; 
                                int propertyCount = result.Properties["memberOf"].Count;

                                String dn;
                                int equalsIndex, commaIndex;
                                string allGrps = "";
                                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                                {
                                    dn = (String)result.Properties["memberOf"][propertyCounter];

                                    equalsIndex = dn.IndexOf("=", 1);
                                    commaIndex = dn.IndexOf(",", 1);
                                    if (-1 == equalsIndex)
                                    {
                                        break;
                                    }
                                    string grp = dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1).ToUpper().Trim();
                                    allGrps += grp + ", ";
                                    if (_AdminGroup.ToUpper() == grp)
                                    {
                                        _isAdmin = true;
                                        ret = true;
                                    }
                                    else if (_UserGroup.ToUpper() == grp)
                                    {
                                        _isUser = true;
                                        ret = true;
                                    }
                                    else if (_WriterGroup.ToUpper() == grp)
                                    {
                                        _isWriter = true;
                                        ret = true;
                                    }
                                }
                                log.Info("User All Groups: " + allGrps);
                                //CheckGroup();

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = false;
                _message = "Error authenticating user " + ex.Message;
                if (ex.InnerException != null) _message += " Inner Exception: " + ex.InnerException;
                log.Info("Authentication error :" + _message);
            }

            return ret;
        }

        public bool IsGroupsAuthenticated(string userName)
        {            
            return IsGroupsAuthenticated(userName, null);
        }

        private void ReadAllUsersFromAD()
        {
            var PerLst = new List<PersonelInfo>();
            using (var forest = Forest.GetCurrentForest())
            {
                foreach (Domain domain in forest.Domains)
                {
                    var d = domain.Name;
                    using (var context = new PrincipalContext(ContextType.Domain, domain.Name))
                    {

                        using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                        {
                            foreach (var result in searcher.FindAll())
                            {
                                DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                                string str = "Domain: " + domain.Name +
                                "First Name: " + (de.Properties["givenName"].Value ?? "").ToString() +
                                "Last Name: " + (de.Properties["sn"].Value ?? "").ToString() +
                                "SA Name: " + (de.Properties["samAccountName"].Value ?? "").ToString() +
                                "Principal Name: " + (de.Properties["userPrincipalName"].Value ?? "").ToString();
                                try { System.IO.File.AppendAllText("c:\\temp\\TestUserName_" + domain.Name, str + Environment.NewLine); }
                                catch { }
                                //var per = new PersonelInfo();
                                //per.FirstName = (de.Properties["givenName"].Value ?? "").ToString();
                                //per.LastName = (de.Properties["sn"].Value ?? "").ToString();
                                //per.SAMAccountNmae = (de.Properties["samAccountName"].Value ?? "").ToString();
                                //per.userPrincipalName = (de.Properties["userPrincipalName"].Value ?? "").ToString();
                                //PerLst.Add(per);
                            }
                        }
                    }
                    domain.Dispose();
                }
            }            
        }
    }

    public class PersonelInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SAMAccountNmae { get; set; }
        public string userPrincipalName { get; set; }
    }
}