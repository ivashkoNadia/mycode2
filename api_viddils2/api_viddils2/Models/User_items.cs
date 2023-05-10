using System.Data;

namespace api_viddils2.Models
{
    public class User_items
    {

       public int id { get; set; }
        public string firstname { get; set; }   
        public string lastname { get; set; }    
        public string password { get; set; }
        public string email { get; set; }
        public string role { get; set; }

        public User_items()
        {
            this.id = 0;
            this.firstname = "";
            this.lastname = "";
            this.password = "";
            this.email = "";
            this.role = "User";
        }

        public User_items(int i, string f, string l, string p, string e, string r)
        {
            this.id = i;
            this.firstname = f;
            this.lastname = l;
            this.password = p;
            this.email = e;
            this.role = r;
        }

        public User_items(User_items other_u)
        {
            this.id = other_u.id;
            this.firstname = other_u.firstname;
            this.lastname = other_u.lastname;
            this.password = other_u.password;
            this.email = other_u.email;
            this.role = other_u.role;
        }
    }

    public class User_registr
    {
        public string password { get; set; }
        public string email { get; set; }
        public User_registr()
        {
            password = "";
            email = "";
        }
        public User_registr(string a, string b)
        {
            this.password = a;
            this.email = b;
        }
    }
}
