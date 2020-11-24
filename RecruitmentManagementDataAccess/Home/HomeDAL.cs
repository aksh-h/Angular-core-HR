using RecruitmentManagementDataAccess.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecruitmentManagementDataAccess.Home
{
    public static class HomeDAL
    {
        private readonly static RecruitmentManagementContext dbContext;
        static HomeDAL()
        {
            dbContext = new RecruitmentManagementContext();
        }

       
    }
}
