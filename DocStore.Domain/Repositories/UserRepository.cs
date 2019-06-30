using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DocStore.Contract.Entities;
using DocStore.Contract.Repositories;
using DocStore.Domain.Healper;

namespace DocStore.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseHelper helper;

        public UserRepository(DatabaseHelper helper)
        {
            this.helper = helper;
        }

        public bool ValidateUser(string userEmailId, string userPassword)
        {
            try
            {
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@userEmailId", userEmailId),
                    new SqlParameter("@userPassword", userPassword)
                };

                var table = helper.ExecuteSelectQuery("uspValidateUser", sqlParameters);
                if (table.Rows.Count == 0)
                    return false;

                return true;
            }
            catch
            {
                throw;
            }
        }

        public User FindByUserId(string userId)
        {
            try
            {
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@userId", userId),
                };

                var table = helper.ExecuteSelectQuery("uspGetUser", sqlParameters);
                if (table.Rows.Count == 0)
                    return null;

                var dataRow = table.Rows[0];
                return new User
                {
                    UserId = dataRow["userId"].ToString(),
                    UserEmailId = dataRow["userEmailId"].ToString(),
                    UserEmailIdVerified = Convert.ToBoolean(dataRow["userEmailIdVerified"]),
                    UserGender = Convert.ToInt16(dataRow["userGender"]),
                    UserIsActive = Convert.ToBoolean(dataRow["userIsActive"]),
                    UserProfilePicUrl = dataRow["userProfilePicUrl"].ToString(),
                    CreatedOn = Convert.ToDateTime(dataRow["createdOn"]),
                    ModifiedOn = Convert.ToDateTime(dataRow["modifiedOn"])
                };
            }
            catch
            {
                throw;
            }
        }

        public User FindByUserEmailId(string userEmailId)
        {
            try
            {
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@userEmailId", userEmailId),
                };

                var table = helper.ExecuteSelectQuery("uspGetUser", sqlParameters);
                if (table.Rows.Count == 0)
                    return null;

                var dataRow = table.Rows[0];
                return new User
                {
                    UserId = dataRow["userId"].ToString(),
                    UserEmailId = dataRow["userEmailId"].ToString(),
                    UserEmailIdVerified = Convert.ToBoolean(dataRow["userEmailIdVerified"]),
                    UserGender = Convert.ToInt16(dataRow["userGender"]),
                    UserIsActive = Convert.ToBoolean(dataRow["userIsActive"]),
                    UserProfilePicUrl = dataRow["userProfilePicUrl"].ToString(),
                    CreatedOn = Convert.ToDateTime(dataRow["createdOn"]),
                    ModifiedOn = Convert.ToDateTime(dataRow["modifiedOn"])
                };
            }
            catch
            {
                throw;
            }
        }

        public User AddUser(User user)
        {
            try
            {
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@userId", Guid.NewGuid().ToString()),
                    new SqlParameter("@userPassword", Guid.NewGuid()),
                    new SqlParameter("@userEmailId", user.UserEmailId),
                    new SqlParameter("@userGender", user.UserGender),
                    new SqlParameter("@userProfilePicUrl", user.UserProfilePicUrl??string.Empty)
                };

                var insertedRecord = helper.ExecuteQuery("uspInsertUser", sqlParameters);
                if (insertedRecord == 1)
                {
                    return FindByUserEmailId(user.UserEmailId);
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}