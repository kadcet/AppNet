using AppNET.App;
using AppNET.Domain.Entities;
using AppNET.Infrastructure;
using AppNET.Infrastructure.Controls;

namespace AppNET.Presentation.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ICategoryService categoryService = IOCContainer.Resolve<ICategoryService>();
        IProductService productService=IOCContainer.Resolve<IProductService>();

        
        private void FillProductGrid()
        {
            grdProduct.DataSource = productService.GetAllProduct();
        }
        private void FillCategoryGrid()
        {
            grdCategory.DataSource = categoryService.GetAllCategory();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FillProductGrid();
            FillCategoryGrid();
            var data = categoryService.GetAllCategory().ToList();
            cmbCategortList.DataSource = data;
            cmbCategortList.DisplayMember = nameof(Category.Name);
        }
        

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (btnSaveCategory.Text == "KAYDET")
            {
                int id = Convert.ToInt32(txtCategoryId.Text);
                categoryService.Created(id, txtCategoryName.Text);
            }
            else
            {
                categoryService.Update(Convert.ToInt32(txtCategoryId.Text), txtCategoryName.Text);
                btnSaveCategory.Text = "KAYDET";
                groupBox1.Text = "Yeni Kategori";
                txtCategoryId.Enabled = true;
            }
            txtCategoryId.Text = "";
            txtCategoryName.Text = "";
            
            FillCategoryGrid();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoryName = grdCategory.CurrentRow.Cells["Name"].Value.ToString();
            DialogResult result= MessageBox.Show($"{categoryName} kategorisini silmek istediðinizden emin misiniz?", "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            Category category=new Category();
            category.Id= Convert.ToInt32(grdCategory.CurrentRow.Cells["Id"].Value);
            category.Name = grdCategory.CurrentRow.Cells["Name"].Value.ToString();
            //int id = Convert.ToInt32(grdCategory.CurrentRow.Cells["Id"].Value);
            categoryService.Delete(category);
            FillCategoryGrid();
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
               
                productService.Created(id, selectedCategoryName,Convert.ToString(MyExtensions.FirstLetterUppercase(txtProductName.Text)) , Convert.ToInt32(txtProductStock.Text), Convert.ToDecimal(txtProductPrice.Text));
                
            }
            else
            {
                productService.Update(Convert.ToInt32(txtProductId.Text),cmbCategortList.Text, MyExtensions.FirstLetterUppercase(txtProductName.Text),Convert.ToInt32(txtProductStock.Text),Convert.ToDecimal(txtProductPrice.Text));

                btnSaveProduct.Text = "KAYDET";
                groupBox2.Text = "Yeni Ürün";
                txtProductId.Enabled = true;
            }

            txtProductId.Text = "";
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductStock.Clear();

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
            FillProductGrid();
        }

        private void duzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string id = grdProduct.CurrentRow.Cells["Id"].Value.ToString();
            var categoryName = grdProduct.CurrentRow.Cells["CategoryName"].Value.ToString();
            string productName = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
            var stock = Convert.ToInt32(grdProduct.CurrentRow.Cells["Stock"].Value.ToString());
            var price = Convert.ToDecimal(grdProduct.CurrentRow.Cells["Price"].Value.ToString());


            txtProductId.Text = id;
            txtProductName.Text = productName;
            cmbCategortList.Text = categoryName;
            txtProductStock.Text = Convert.ToString(stock);
            txtProductPrice.Text = Convert.ToString(price);

            

            txtProductId.Enabled = false;
            btnSaveProduct.Text = "GÜNCELLE";
            groupBox2.Text = "Ürün Güncelle";
        }
    }
}