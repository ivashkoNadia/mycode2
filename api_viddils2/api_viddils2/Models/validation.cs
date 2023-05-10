using System.Text.RegularExpressions;

namespace api_viddils2.Models
{
    public class validation
    {
        public static List<string> Valid_viddil(Viddil_item todoItem)
        {
            var validationErrors = new List<string>();
            if (!Regex.IsMatch(todoItem.websiteurl, "^[A-Za-z0-9._%-]+\\.com$"))
            {
                validationErrors.Add("WebsiteUrl must match the pattern: ^[A-Za-z0-9._%-]+\\.com$");
            }
            if (!new[] { "Draft", "Published", "Moderation" }.Contains(todoItem.state))
            {
                validationErrors.Add("Status must be one of the following values: Draft, Published, Moderation");
            }
            if (!Regex.IsMatch(todoItem.directorname, "^[A-Za-z]+$"))
            {
                validationErrors.Add("DirectorName must have only letters");
            }
            if (!Regex.IsMatch(todoItem.phonenumber, "^0[0-9]{9}$"))
            {
                validationErrors.Add("Phonenumber must have 10 digits");
            }
            if (todoItem.monthlybudget < 0)
            {
                validationErrors.Add("Monthlybudget must be >=0");
            }
            if (todoItem.yearlybudget < 0)
            {
                validationErrors.Add("Yearlybudget must be >=0");
            }


            return validationErrors;
        }
        public static List<string> Valid_user(User_items todoItem)
        {
            var validationErrors = new List<string>();
            if (!Regex.IsMatch(todoItem.email, "^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$"))
            {
                validationErrors.Add("do not valid email");
            }
            
            if (!Regex.IsMatch(todoItem.firstname, "^[A-Za-z]+$"))
            {
                validationErrors.Add("FirstName must have only letters");
            }
            if (!Regex.IsMatch(todoItem.lastname, "^[A-Za-z]+$"))
            {
                validationErrors.Add("LastName must have only letters");
            }
            if (!Regex.IsMatch(todoItem.password, "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,20}$"))
            {
                validationErrors.Add("do not safe password (8+symbols)");
            }

            return validationErrors;
        }

    }
}
//select * from viddils