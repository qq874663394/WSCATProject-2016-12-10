using HelperUtility.Encrypt;
using InterfaceLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSCATProject.WareHouse;

namespace WSCATProject.Warehouse
{
    public partial class WareHuseStorageRack : Form
    {
        CodingHelper ch = new CodingHelper();
        public WareHuseStorageRack()
        {
            InitializeComponent();
        }

        #region  绑定数据到下拉框

        private void WareHuseStorageRack_Load(object sender, EventArgs e)
        {
            comboBoxEx2.SelectedIndex = 0;
            comboBoxEx3.SelectedIndex = 0;
            comboBoxEx4.SelectedIndex = 0;
            comboBoxEx5.SelectedIndex = 0;
            //绑定仓库下拉框
            StorageInterface sif = new StorageInterface();
            DataTable dt = ch.DataTableReCoding(sif.SelStorage());
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx1.DisplayMember = "name";
            comboBoxEx1.ValueMember = "code";
            comboBoxEx1.DataSource = dt;
        }

        private void comboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedValue == null)
            {
                comboBoxEx2.DataSource = null;
                return;
            }
            //根据仓库code绑定货架下拉框
            StorageRackInterface srif = new StorageRackInterface();
            DataTable dt = ch.DataTableReCoding(srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx1.SelectedValue.ToString())));
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx2.DisplayMember = "name";
            comboBoxEx2.ValueMember = "code";
            comboBoxEx2.DataSource = dt;
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx2.SelectedValue == null)
            {
                comboBoxEx3.DataSource = null;
                return;
            }
            //根据货架code绑定排的下拉框
            StorageRackInterface srif = new StorageRackInterface();
            DataTable dt = ch.DataTableReCoding(srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx2.SelectedValue.ToString())));
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx3.DisplayMember = "name";
            comboBoxEx3.ValueMember = "code";
            comboBoxEx3.DataSource = dt;
        }

        private void comboBoxEx3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEx3.SelectedValue == null)
            {
                comboBoxEx4.DataSource = null;
                return;
            }
            //根据排的code绑定格的下拉框
            StorageRackInterface srif = new StorageRackInterface();
            DataTable dt = ch.DataTableReCoding(srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx3.SelectedValue.ToString())));
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx4.DisplayMember = "name";
            comboBoxEx4.ValueMember = "code";
            comboBoxEx4.DataSource = dt;
        }

        private void comboBoxEx4_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEx4.SelectedValue == null)
            {
                comboBoxEx5.DataSource = null;
                return;
            }
            //根据排的code绑定格的下拉框
            StorageRackInterface srif = new StorageRackInterface();
            DataTable dt = ch.DataTableReCoding(srif.SelStorageRackByCode(XYEEncoding.strCodeHex(comboBoxEx4.SelectedValue.ToString())));
            DataRow dr = dt.NewRow();
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);

            comboBoxEx5.DisplayMember = "name";
            comboBoxEx5.ValueMember = "code";
            comboBoxEx5.DataSource = dt;
        }
        #endregion

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            WareHouseIn whi = (WareHouseIn)this.Owner;
            if (checkBoxX1.Checked == false || comboBoxEx2.Text == "请选择")
            {
                if (MessageBox.Show(Owner, "是否临时存放", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    //是临时存放
                    whi.StorageCode = comboBoxEx1.SelectedValue == null ? "" : comboBoxEx1.SelectedValue.ToString();
                    whi.Storage = comboBoxEx1.Text;
                    whi.StorageQuyu = "临时存放地";
                    Close();
                    Dispose();
                    return;
                }
                else
                {
                    //不是临时存放
                    whi.StorageCode = comboBoxEx1.SelectedValue == null ? "" : comboBoxEx1.SelectedValue.ToString();
                    whi.Storage = comboBoxEx1.Text;
                    whi.StorageQuyuCode = comboBoxEx2.SelectedValue == null ? "" : comboBoxEx2.SelectedValue.ToString();
                    whi.StorageQuyu = comboBoxEx2.Text;
                    whi.StorageRackCode = comboBoxEx3.SelectedValue == null ? "" : comboBoxEx3.SelectedValue.ToString();
                    whi.StorageRack = comboBoxEx3.Text;
                    whi.StoragePaiCode = comboBoxEx4.SelectedValue == null ? "" : comboBoxEx4.SelectedValue.ToString();
                    whi.StoragePai = comboBoxEx4.Text;
                    whi.StorageGeCode = comboBoxEx5.SelectedValue == null ? "" : comboBoxEx5.SelectedValue.ToString();
                    whi.StorageGe = comboBoxEx5.Text;
                    Close();
                    Dispose();
                    return;
                }
            }
            whi.StorageCode = comboBoxEx1.SelectedValue == null ? "" : comboBoxEx1.SelectedValue.ToString();
            whi.Storage = comboBoxEx1.Text;
            whi.StorageQuyuCode = comboBoxEx2.SelectedValue == null ? "" : comboBoxEx2.SelectedValue.ToString();
            whi.StorageQuyu = comboBoxEx2.Text;
            whi.StorageRackCode = comboBoxEx3.SelectedValue == null ? "" : comboBoxEx3.SelectedValue.ToString();
            whi.StorageRack = comboBoxEx3.Text;
            whi.StoragePaiCode = comboBoxEx4.SelectedValue == null ? "" : comboBoxEx4.SelectedValue.ToString();
            whi.StoragePai = comboBoxEx4.Text;
            whi.StorageGeCode = comboBoxEx5.SelectedValue == null ? "" : comboBoxEx5.SelectedValue.ToString();
            whi.StorageGe = comboBoxEx5.Text;
            Close();
            Dispose();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
