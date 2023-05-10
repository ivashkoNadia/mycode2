using System.Reflection;

namespace api_viddils2.Models
{
    public class Viddil_item
    {
        public int id { get; set; }
        public string title { get; set; }
        public string directorname { get; set; }
        public string phonenumber { get; set; }
        public int monthlybudget { get; set; } 
        public int yearlybudget { get; set; }
        public string websiteurl { get; set; }
        public string state { get; set; }
        public Viddil_item()
        {
            this.id = 0;
            this.title = "";
            this.directorname = "";
            this.phonenumber = "";
            this.monthlybudget = 0;
            this.yearlybudget = 0;
            this.websiteurl = "";
            this.state = "Draft";
        }

        public override string ToString()
        {
            string str = "";
            FieldInfo[] fields = typeof(Viddil_item).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                str = str + field.GetValue(this).ToString() + ", ";
            }
            string[] arr = str.Split(',');
            List<string> list = new List<string>(arr);
            str = "";
            for (int i = 0; i < list.Count - 2; i++) str = str + list[i] + ",";
            return str;
        }

    }
}
