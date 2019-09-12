using Business.Common;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserRegistrationBAL
    {
        public void ins_UserRegistration(string connStr, object input)
        {
            DataAccess.SqlHelper sqlHelper = new DataAccess.SqlHelper(connStr);

            if (input.GetType() == typeof(UserRegistration))
            {
                UserRegistration objUserRegistration = (UserRegistration)input;

                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(sqlHelper.CreateParameter("@Activity", "InsUserRegistration", DbType.String));

                parameters.Add(sqlHelper.CreateParameter("@UserId", objUserRegistration.UserId, DbType.Int32));
                parameters.Add(sqlHelper.CreateParameter("@EmailId", objUserRegistration.EmailId, DbType.String));
                parameters.Add(sqlHelper.CreateParameter("@FirstName", objUserRegistration.FirstName, DbType.String));
                parameters.Add(sqlHelper.CreateParameter("@LastName", objUserRegistration.LastName, DbType.String));
                parameters.Add(sqlHelper.CreateParameter("@MobileNo", objUserRegistration.MobileNo, DbType.String));
                parameters.Add(sqlHelper.CreateParameter("@IPAddress", objUserRegistration.IPAddress, DbType.String));
                parameters.Add(sqlHelper.CreateParameter("@Password", objUserRegistration.Password, DbType.String));
                parameters.Add(sqlHelper.CreateParameter("@ConfirmPassword", objUserRegistration.ConfirmPassword, DbType.String));
                sqlHelper.Insert(Constant.Usp_UserRegistration, parameters.ToArray());
            }

        }
    }
}
