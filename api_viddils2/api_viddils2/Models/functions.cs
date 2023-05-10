using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_viddils2.Models;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace api_viddils2.Models
{
    public class Function
    {
        private static User_items this_user= new User_items();
        public static User_items return_user() { return this_user; }
        public static void Set_user(User_items u) { this_user=new User_items(u); }
        public static void Del_user() { this_user = new User_items(); }


       public static List<Viddil_item> Sorting(List<Viddil_item> items, string atr, string type)
        {
            var result = new List<Viddil_item>();
            var temp = new List<Viddil_item>();

            if (type != "up" && type != "down") return result;
         
            if (type == "down")
            {
                temp = new List<Viddil_item>(
                items.OrderByDescending(x => x.GetType().GetProperty(atr).GetValue(x, null)).ToList());
            }
            else
            {
                temp = new List<Viddil_item>(
                items.OrderBy(x => x.GetType().GetProperty(atr).GetValue(x, null)).ToList());
            }
            return temp;
        }

        public static List<Viddil_item> Find(List<Viddil_item> items, string search)
        {
            var result = new List<Viddil_item>();
            foreach (var v in items)
            {
                foreach (var pole in v.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (pole.Name.ToString() != "state")
                    {
                        if (v.GetType().GetProperty(pole.Name).GetValue(v, null).ToString().ToLower().Contains(search.ToLower()))
                        {
                            result.Add(v);
                            break;
                        }
                    }
                }

            }
            return result;
        }

        public static List<string> Get_polya<T>(T clas)
        {

            List<string> l = new List<string>();
            foreach (var pole in clas.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var fieldName = pole.Name;
                l.Add(fieldName);
            }
            return l;
        }


        public static List<Viddil_item> Check_role(List<Viddil_item> list_)
        {
            if (Function.return_user().role == "User")
            {
                List<Viddil_item> result = new List<Viddil_item>();
                foreach (var item in list_)
                {
                    if (item.state == "Published") result.Add(item);
                }
                return result;
            }
            else { return list_; }
        }

        public static string Random_email()
        {
            var allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var length = random.Next(6, 16);
            var result = new string(
                Enumerable.Repeat(allowedChars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result + "@gmail.com)";
        }
    }

    


}
