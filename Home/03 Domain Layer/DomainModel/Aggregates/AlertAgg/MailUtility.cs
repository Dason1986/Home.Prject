using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.AlertAgg
{
    public static class MailUtility
    {
        public static void IsEmail(string str)
        {
            if (string.IsNullOrEmpty(str)) throw new ValidationException("輸入爲空");
            var arr = str.Split(Sp, StringSplitOptions.RemoveEmptyEntries);
            foreach (var mail in arr)
            {
                var flag = System.Text.RegularExpressions.Regex.IsMatch(mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

                if (!flag) throw new ValidationException("不是Email格式");
            }
        }

        private static readonly char[] Sp = new char[] { ';' };
    }
}