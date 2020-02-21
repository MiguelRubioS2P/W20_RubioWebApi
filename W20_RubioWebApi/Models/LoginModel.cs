using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W20_RubioWebApi.Models
{
    public class LoginModel
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _login;
        public int Login
        {
            get { return _login; }
            set { _login = value; }
        }
    }
}