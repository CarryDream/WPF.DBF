using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.DBF.Entity
{
    internal class DataGridViewDBFField
    {
        private string oName;
        private string oType;
        private int oLength;
        private bool isOutput;

        private string nName;
        private string nType;
        private int nLength;
        private string nDefaultValue;

        public DataGridViewDBFField()
        {
        }

        public DataGridViewDBFField(string oName, string oType, int oLength, bool isOutput)
        {
            this.OName = oName;
            this.OType = oType;
            this.OLength = oLength;
            this.IsOutput = isOutput;
        }

        public string OName { get => oName; set => oName = value; }
        public string OType { get => oType; set => oType = value; }
        public int OLength { get => oLength; set => oLength = value; }
        public bool IsOutput { get => isOutput; set => isOutput = value; }
        public string NName { get => nName; set => nName = value; }
        public string NType { get => nType; set => nType = value; }
        public int NLength { get => nLength; set => nLength = value; }
        public string NDefaultValue { get => nDefaultValue; set => nDefaultValue = value; }
    }
}
