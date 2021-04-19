using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.classes
{
    public class Bank : SuperClass
    {
        private long money_account;
        public Bank(string type, string nameless, string rest) : base(type, nameless, rest)
        {
            this.money_account = Int64.Parse(this.dict["money_account"]);
        }

        public long getMoneyAccount()
        {
            return this.money_account;
        }

        public void setMoneyAccount(long l)
        {
            this.money_account = l;
            this.dict["money_account"] = l.ToString();
        }
    }
}
