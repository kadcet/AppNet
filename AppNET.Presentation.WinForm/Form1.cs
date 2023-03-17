using AppNET.App;
using AppNET.Domain.Entities;
using AppNET.Infrastructure;
using AppNET.Infrastructure.Controls;
using System.Security.Cryptography.X509Certificates;

namespace AppNET.Presentation.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            
        }

        
        ICategoryService categoryService = IOCContainer.Resolve<ICategoryService>();
        IProductService productService = IOCContainer.Resolve<IProductService>();
        ICaseSevice caseSevice=IOCContainer.Resolve<ICaseSevice>();
        ILogService logService = IOCContainer.Resolve<ILogService>();


        private void FillProductGrid()
        {
            grdProduct.DataSource = productService.GetAllProduct();
        }
        private void FillCategoryGrid()
        {
            grdCategory.DataSource = categoryService.GetAllCategory();
        }

        private void FillCaseGrid()
        {
            grdCase.DataSource = caseSevice.CaseList();
        }

        private void FillBalance()
        {
            var data= caseSevice.Balance().ToString();
            if (caseSevice.Balance() < 0)
            {
                lblBalance.ForeColor = Color.Red;
                lblBalance.Text = caseSevice.Balance().ToString();
            }
            else
            {
                lblBalance.ForeColor = Color.Black;
                lblBalance.Text = caseSevice.Balance().ToString();
            }
                
        }

        private void FillCombobox()
        {
            var data = categoryService.GetAllCategory().ToList();
            cmbCategortList.DataSource = data;
            cmbCategortList.DisplayMember = nameof(Category.Name);
            cmbCategortList.Text = "";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FillProductGrid();
            FillCategoryGrid();
            FillCombobox();
            FillCaseGrid();
            FillBalance();
        }


        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (btnSaveCategory.Text == "KAYDET")
            {
                //int id = Convert.ToInt32(txtCategoryId.Text);
                categoryService.Created(Convert.ToInt32(txtCategoryId.Text), txtCategoryName.Text);
                logService.Information(" kategori eklendi");
            }
            else
            {
                categoryService.Update(Convert.ToInt32(txtCategoryId.Text), txtCategoryName.Text);
                btnSaveCategory.Text = "KAYDET";
                groupBox1.Text = "Yeni Kategori";
                txtCategoryId.Enabled = true;
                logService.Information("Kategori güncellendi");
            }
            txtCategoryId.Text = "";
            txtCategoryName.Text = "";
            //logService.Information("Ürün Alýndý");

            FillCombobox();
            FillCategoryGrid();
            
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoryName = grdCategory.CurrentRow.Cells["Name"].Value?.ToString() ?? "";
            var categoryId = Convert.ToInt32(grdCategory.CurrentRow.Cells["Id"].Value);
            if (string.IsNullOrEmpty(categoryName)) return;
            

            var data = productService.GetAllProduct().Where(x => x.CategoryName == categoryName);

            var userMsg = "";
            if (data.Count() != 0)
            {
                userMsg = $"{categoryName} Ýsimli Kategori ve Ona Baðlý {data.Count()} Adet Ürün Silinecektir.";
            }
            else
            {
                userMsg = $"{categoryName} Ýsimli Kategori Silinecek. Bu kategoriye baðlý ürün bulunmamaktadýr.";
            }


            DialogResult result = MessageBox.Show($"{userMsg} {Environment.NewLine} Devam Etmek istiyormusunu?", "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;
            
            #region Category Delete
            bool x = categoryService.Delete(categoryId);
            FillCategoryGrid();
            FillCombobox();
            #endregion Category Delete

            #region Product Delete
            if (data.Count()!=0)
            {
                var isDeleted = productService.DeleteProductsByCategory(categoryName);
                FillProductGrid();
            }


           


            #endregion Product Delete
        }

        private void duzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = grdCategory.CurrentRow.Cells["Id"].Value.ToString();
            string categoryName = grdCategory.CurrentRow.Cells["Name"].Value.ToString();

            txtCategoryId.Text = id;
            txtCategoryName.Text = categoryName;

            txtCategoryId.Enabled = false;
            btnSaveCategory.Text = "GÜNCELLE";
            groupBox1.Text = "Kategori Güncelle";


        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (btnSaveProduct.Text == "KAYDET")
            {
                int id = Convert.ToInt32(txtProductId.Text);
                //string selectedCategoryName =Convert.ToString(cmbCategortList.SelectedValue);
                var selectedCategoryName = cmbCategortList.Text;

                

                productService.Created(id, selectedCategoryName, Convert.ToString(MyExtensions.FirstLetterUppercase(txtProductName.Text)), Convert.ToInt32(txtProductAmount.Text), Convert.ToDecimal(txtProductPurchasePrice.Text), Convert.ToDecimal(txtProductSalesPrice.Text), Convert.ToDecimal(txtProductTotalPrice.Text));
                MessageBox.Show($"{MyExtensions.FirstLetterUppercase(txtProductName.Text.ToString()) } Ürününden {txtProductAmount.Text.ToString()} Adet satýn alýndý");
                logService.Information("Ürün eklendi");


                caseSevice.Exp(MyExtensions.FirstLetterUppercase((txtProductName.Text.ToString().ToString()+" Ürününden "+ Convert.ToInt32(txtProductAmount.Text.ToString())+" Adet Alýnmýþtýr.")), Convert.ToInt32(txtProductAmount.Text), (Convert.ToDecimal(txtProductAmount.Text) * Convert.ToDecimal(txtProductPurchasePrice.Text)));
                

                FillCaseGrid();
                FillBalance();

            }
            else if (btnSaveProduct.Text=="SATIÞ")
            {
                var list=productService.GetAllProduct().FirstOrDefault(x => x.Name == txtProductName.Text);

                if (Convert.ToInt32(txtProductAmount.Text) > list.Amount)
                {
                    MessageBox.Show($"En fazla {Convert.ToInt32(list.Amount)} adet ürün satýlabilir ");
                    logService.Error($"{Convert.ToInt32(txtProductAmount.Text)} Yetersiz Stok");
                    return;
                }

                productService.Update(Convert.ToInt32(txtProductId.Text), Convert.ToString(cmbCategortList.Text), MyExtensions.FirstLetterUppercase(txtProductName.Text), Convert.ToInt32(Convert.ToInt32(list.Amount)-Convert.ToInt32(txtProductAmount.Text)), Convert.ToDecimal(txtProductPurchasePrice.Text), Convert.ToDecimal(txtProductSalesPrice.Text), Convert.ToDecimal(list.TotalPrice)-Convert.ToDecimal(txtProductTotalPrice.Text));

                caseSevice.Inc(MyExtensions.FirstLetterUppercase((txtProductName.Text.ToString().ToString() + " Ürününden " + Convert.ToInt32(txtProductAmount.Text.ToString()) + " Adet Satýlmýþtýr.")), Convert.ToInt32(txtProductAmount.Text), (Convert.ToDecimal(txtProductAmount.Text) * Convert.ToDecimal(txtProductSalesPrice.Text)));
                btnSaveProduct.Text = "KAYDET";
                groupBox2.Text = "Yeni Ürün";
                txtCategoryName.Enabled = true;
                txtProductId.Enabled = true;
                FillCaseGrid();
                FillProductGrid();
                FillBalance();
            }
            else if(btnSaveProduct.Text=="GÜNCELLE")
            {
                productService.Update(Convert.ToInt32(txtProductId.Text),Convert.ToString(cmbCategortList.Text), MyExtensions.FirstLetterUppercase(txtProductName.Text),Convert.ToInt32(txtProductAmount.Text),Convert.ToDecimal(txtProductPurchasePrice.Text), Convert.ToDecimal(txtProductSalesPrice.Text),Convert.ToDecimal(txtProductTotalPrice.Text));

                btnSaveProduct.Text = "KAYDET";
                groupBox2.Text = "Yeni Ürün";
                txtProductId.Enabled = true;
            }

            txtProductId.Text = "";
            txtProductName.Clear();
            txtProductPurchasePrice.Clear();
            txtProductAmount.Clear();
            txtProductTotalPrice.Clear();
            txtProductSalesPrice.Clear();

            
            FillProductGrid();
        }

        private void silToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string productName = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
            DialogResult result = MessageBox.Show($"{productName} ürününü silmek istediðinizden emin misiniz?", "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            int id = Convert.ToInt32(grdProduct.CurrentRow.Cells["Id"].Value);
            productService.Deleted(id);
            caseSevice.Deleted(id);
            FillProductGrid();
        }

        private void duzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string id = grdProduct.CurrentRow.Cells["Id"].Value.ToString();
            var categoryName = grdProduct.CurrentRow.Cells["CategoryName"].Value.ToString();
            string productName = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
            var productAmount = grdProduct.CurrentRow.Cells["Amount"].Value.ToString();
            var productPurchasePrice = grdProduct.CurrentRow.Cells["PurchasePrice"].Value.ToString();
            var productSalesPrice= grdProduct.CurrentRow.Cells["SalesPrice"].Value.ToString();
            var totalPrice = grdProduct.CurrentRow.Cells["TotalPrice"].Value.ToString();
           


            txtProductId.Text = id;
            txtProductName.Text = productName;
            cmbCategortList.Text = categoryName;
            txtProductAmount.Text = productAmount;
            txtProductPurchasePrice.Text = productPurchasePrice;
            txtProductSalesPrice.Text = productSalesPrice;
            txtProductTotalPrice.Text = totalPrice;



            txtProductId.Enabled = false;
            btnSaveProduct.Text = "GÜNCELLE";
            groupBox2.Text = "Ürün Güncelle";
        }

        private void textProductAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtProductPurchasePrice.Text == "") txtProductPurchasePrice.Text = "0";
            if (txtProductAmount.Text == "") txtProductAmount.Text = "0";

            decimal alisFiyat =Convert.ToDecimal(txtProductPurchasePrice.Text);
            int alisAdet = Convert.ToInt32(txtProductAmount.Text);
            txtProductTotalPrice.Text = (alisFiyat * alisAdet).ToString();

        }

        private void txtProductPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            if (txtProductPurchasePrice.Text == "") txtProductPurchasePrice.Text = "0";
            if (txtProductAmount.Text == "") txtProductAmount.Text = "0";

            decimal alisFiyat = Convert.ToDecimal(txtProductPurchasePrice.Text);
            int alisAdet = Convert.ToInt32(txtProductAmount.Text);
            txtProductTotalPrice.Text = (alisFiyat * alisAdet).ToString();
        }

        private void satisYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = grdProduct.CurrentRow.Cells["Id"].Value.ToString();
            var categoryName = grdProduct.CurrentRow.Cells["CategoryName"].Value.ToString();
            string productName = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
            var productAmount = grdProduct.CurrentRow.Cells["Amount"].Value.ToString();
            var productPurchasePrice = grdProduct.CurrentRow.Cells["PurchasePrice"].Value.ToString();
            var productSalesPrice = grdProduct.CurrentRow.Cells["SalesPrice"].Value.ToString();
            var totalPrice = grdProduct.CurrentRow.Cells["TotalPrice"].Value.ToString();



            txtProductId.Text = id;
            txtProductName.Text = productName;
            cmbCategortList.Text = categoryName;
            txtProductAmount.Text = productAmount;
            txtProductPurchasePrice.Text = productPurchasePrice;
            txtProductSalesPrice.Text = productSalesPrice;
            txtProductTotalPrice.Text = totalPrice;

            txtProductId.Enabled = false;
            txtProductName.Enabled = false;
            txtCategoryName.Enabled = false;
            txtProductPurchasePrice.Enabled = false;
            
            btnSaveProduct.Text = "SATIÞ";
            groupBox2.Text = "Ürün Satýþý";
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            grdCase.DataSource = caseSevice.CaseListIncome();
        }

        private void btnAllCase_Click(object sender, EventArgs e)
        {
            FillCaseGrid();
        }

        private void btnExplatanion_Click(object sender, EventArgs e)
        {
            grdCase.DataSource = caseSevice.CaseListExplanation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logService.Information("Program Sonlandýrýldý");
            Application.Exit();
        }
    }
}