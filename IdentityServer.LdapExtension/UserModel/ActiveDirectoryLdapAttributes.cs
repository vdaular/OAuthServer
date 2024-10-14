﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IdentityServer.LdapExtension.UserModel
{
    public enum ActiveDirectoryLdapAttributes
    {
        [Description("displayName")]
        DisplayName,
        [Description("distinguishedName")]
        DistinguishedName,
        [Description("givenName")]
        FirstName,
        [Description("sn")] // Surname
        LastName,
        [Description("description")]
        Description,
        [Description("title")]
        Title,
        [Description("department")]
        Department,
        [Description("telephoneNumber")]
        TelephoneNumber,
        [Description("name")] // Also used as user name
        Name,
        [Description("whenCreated")]
        CreatedOn,
        [Description("whenChanged")]
        UpdatedOn,
        [Description("sAMAccountName")]
        UserName,
        [Description("mail")]
        EMail,
        [Description("memberOf")] // Groups attribute that can appears multiple time
        MemberOf
    }

    public static class LdapAttributesExtensions
    {
        /// <summary>
        /// Create from an <see cref="Enum"/> the description array.
        /// </summary>
        /// <typeparam name="T">An enum type</typeparam>
        /// <returns>An Array of the descriptions (no duplicate)</returns>
        /// <exception cref="ArgumentException">T must be an enumerated type</exception>
        public static Array ToDescriptionArray<T>()
            where T : IConvertible //,struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            List<string> result = new List<string>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = attributes[0].Description;
                if (!result.Contains(description))
                {
                    result.Add(description);
                }
            }

            return result.ToArray();
        }

        public static string ToDescriptionString(this ActiveDirectoryLdapAttributes val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
