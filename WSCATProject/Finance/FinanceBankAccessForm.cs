using HelperUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Finance
{
    public partial class FinanceBankAccessForm : TestForm
    {
        public FinanceBankAccessForm()
        {
            InitializeComponent();
        }
        #region 数据字段
        /// <summary>
        /// 银行存取code
        /// </summary>
        private  string  _financeBankAccessCode;
        #endregion

        private void FinanceBankAccessForm_Load(object sender, EventArgs e)
        {
            _financeBankAccessCode = BuildCode.ModuleCode("FBA");
            textBoxOddNumbers.Text = _financeBankAccessCode;
            barcodeXYE.Code128 _Code = new barcodeXYE.Code128();
            _Code.ValueFont = new Font("微软雅黑", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(textBoxOddNumbers.Text, barcodeXYE.Code128.Encode.Code128A);
            pictureBoxtiaoxingma.Image = imgTemp;
        }

        /// <summary>
        /// 关标定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceBankAccessForm_Activated(object sender, EventArgs e)
        {
            labtxtDanJuType.Focus();
        }
    }
}
