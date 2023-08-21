using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace InWebo.ApiDemo
{

    /// <summary>
    ///   This class describes a group of users of the service
    /// </summary>
    public class IWServiceGroup
    {
        public static readonly Hashtable HashedProperties;

        // static class constructor to initialize constant
        static IWServiceGroup()
        {
            HashedProperties = new Hashtable();
            foreach (PropertyInfo p in typeof(IWServiceGroup).GetProperties())
            {
                HashedProperties.Add(p.Name.ToLower(), p);
            }
        }

        // Properties
        public long? Id { get; set; }
        public string Name { get; set; }
        public string PolicyId { get; set; }

        public IWServiceGroup()
        {

        }

        public IWServiceGroup(long id)
            : this()
        {
            this.Id = id;
        }

        public void CopyNullableProperty(PropertyInfo serviceGroupProperty, object resultValue)
        {
            if (((long?)resultValue).HasValue)
            {
                serviceGroupProperty.SetValue(this, ((long?)resultValue).Value, null);
            }
        }
        public void CopyNullableProperty(PropertyInfo serviceGroupProperty, object resultValue, int index)
        {
            if (((long?[])resultValue)[index].HasValue)
            {
                serviceGroupProperty.SetValue(this, ((long?[])resultValue)[index].Value, null);
            }
        }

        public void CopyStandardProperty(PropertyInfo serviceGroupProperty, object resultValue)
        {
            serviceGroupProperty.SetValue(this, resultValue, null);
        }

        public void CopyStandardProperty(PropertyInfo serviceGroupProperty, object resultValue, int index)
        {
            serviceGroupProperty.SetValue(this, ((object[])resultValue)[index], null);
        }
    }

    public class IWServiceGroupList : List<IWServiceGroup>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IWServiceGroupList));
        
        public IWServiceGroupList() : base() { }

        /// <summary>
        /// Creates a list of groups from a serviceGroupsQuery
        /// In serviceGroupsQueryResult, each property is an array, containing the values for each user group.
        /// </summary>
        /// <param name="result">Result from serviceGroupsQuery</param>
        /// <seealso cref="InWebo.Api.Utils.Provisionning.serviceGroupsQuery"/>
        /// <returns>List of user group</returns>
        public static IWServiceGroupList FromServiceGroupsQuery(object result)
        {
            if (result == null) return null;
            IWServiceGroupList userGroups = new IWServiceGroupList();

            // We get the properties of the serviceGroupsQueryResult object and of UserGroup.
            PropertyInfo[] queryResultProperties = result.GetType().GetProperties();

            // For each serviceGroupsQueryResult property, we check that it exists in UserGroup
            // and we copy the value in LoginsQueryResult into the User property.
            foreach (PropertyInfo resultProperty in queryResultProperties)
            {
                PropertyInfo serviceGroupProperty = (PropertyInfo)IWServiceGroup.HashedProperties[resultProperty.Name];
                if (serviceGroupProperty == null) continue;

                // We found a matching property

                object resultValue = resultProperty.GetValue(result, null);

                Type underlyingType = resultProperty.PropertyType.GetElementType();

                /*if (underlyingType == null)
                {
                    // this is a single user.
                    IWUser user = IWUser.FromLoginResult(result);
                    if (user != null) users.Add(user);
                    return users;
                }*/

                // Check if the underlying type is Nullable
                bool nullable = false;
                int nbItems = 0;
                if (underlyingType.IsGenericType && (underlyingType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    nullable = true;
                    nbItems = ((long?[])resultValue).Length - 1; // the last element is null
                }
                else
                {
                    nullable = false;
                    nbItems = ((object[])resultValue).Length - 1; // the last element is null
                }

                // let's copy the values into corresponding new UserGroup objects' properties.
                for (int i = 0; i < nbItems; i++)
                {
                    if (userGroups.Count == i)
                    {
                        userGroups.Add(new IWServiceGroup());
                    }

                    // For Nullable properties, we must check HasValue before accessing the value;
                    if (nullable)
                    {
                        userGroups[i].CopyNullableProperty(serviceGroupProperty, resultValue, i);
                    }
                    else
                    {
                        userGroups[i].CopyStandardProperty(serviceGroupProperty, resultValue, i);
                    }
                }
            }

            return userGroups;
        }
    }
}