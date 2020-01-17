using CRMHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigurationGenerator
{
    public partial class FrmMain : Form
    {
        RegistryHelper _registryHelper = null;
        CRMHandler.CRMHandler _crmHandler = null;
        private string _openFileDialogFileName="";
        private List<string> _allEntityLogicalNames;
        private SortedList<string, List<string>> _allEntities = null;
        public FrmMain()
        {
            InitializeComponent();
            _registryHelper = new RegistryHelper(Project.Company.ToString(), System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Choose CSV file",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "CSV file (*.csv)|*.csv",
                FilterIndex = 0,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
                
            };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                _openFileDialogFileName = openFileDialog.FileName;
                _registryHelper.AddValueToRegistry(Project.regSelectedCSVFile.ToString(), openFileDialog.FileName);
                LoadCSVFile();
            };
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetLoginUserForm();
            Login();
            SetCSVFileNameFromRegistry();
            LoadCSVFile();
        }
        private void SetCSVFileNameFromRegistry()
        {
           _openFileDialogFileName = _registryHelper.ReadValueFromRegistry(Project.regSelectedCSVFile.ToString());
        }

        private void LoadCSVFile()
        {
            var CSVReader = new TableReader.CSVReader(_openFileDialogFileName);
            DataTable dt = CSVReader.Read().Table;
            if (dt != null)
            {
                dgvCSVData.DataSource = dt;
                dgvSetting.DataSource = null;
                dgvSetting.Rows.Clear();
                dgvSetting.Columns.Clear();
                //
                string colCSVFileField_Name = "CSV File Field";
                string colFieldType_Name = "CSV File Field Type";
                string colFieldType_Field = "Field";
                string colFieldType_Entity = "Entity";
                dgvSetting.Columns.Add(colCSVFileField_Name, colCSVFileField_Name);
                dgvSetting.Columns[colCSVFileField_Name].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //
                DataGridViewComboBoxColumn dgvCmbCSVFileFieldType = new DataGridViewComboBoxColumn();
                dgvCmbCSVFileFieldType.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCmbCSVFileFieldType.HeaderText = colFieldType_Name;
                dgvCmbCSVFileFieldType.Name = colFieldType_Name;
                dgvCmbCSVFileFieldType.Items.Add(colFieldType_Field);
                dgvCmbCSVFileFieldType.Items.Add(colFieldType_Entity);
                dgvSetting.Columns.Add(dgvCmbCSVFileFieldType);
                //
                DataGridViewComboBoxColumn dgvCmbEntity = new DataGridViewComboBoxColumn();
                dgvCmbEntity.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCmbEntity.HeaderText = colFieldType_Entity;
                dgvCmbEntity.Name = colFieldType_Entity;
                foreach (var logicalName in _allEntityLogicalNames)
                {
                    dgvCmbEntity.Items.Add(logicalName);
                }
                dgvSetting.Columns.Add(dgvCmbEntity);
                //
                dgvSetting.Rows.Add(dgvCSVData.Columns.Count);
                for (int i=0; i< dgvCSVData.Columns.Count; i++)
                {
                    dgvSetting.Rows[i].Cells[colCSVFileField_Name].Value = dgvCSVData.Columns[i].Name;
                    dgvSetting.Rows[i].Cells[colFieldType_Name].Value = dgvCmbCSVFileFieldType.Items[0];
                }
                //

                //
                for (int i = 0; i < dgvSetting.Columns.Count - 1; i++)
                {
                    dgvSetting.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvSetting.Columns[dgvSetting.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void Login()
        {
            cmbEntityLogicalNames.Items.Clear();
            _crmHandler = new CRMHandler.CRMHandler(GetLogin().Uri, GetLogin().Credentials);
            _allEntities = _crmHandler.GetAllEntities();
            _allEntityLogicalNames = _allEntities.Keys.ToList();
            foreach (var logicalName in _allEntityLogicalNames)
            {
                cmbEntityLogicalNames.Items.Add(logicalName);
            }
            cmbEntityLogicalNames.SelectedIndex = 0;
            _registryHelper.AddValueToRegistry(Project.regUser.ToString(), txtUser.Text);
            _registryHelper.AddValueToRegistry(Project.regPassword.ToString(), txtPassword.Text);
            _registryHelper.AddValueToRegistry(Project.regUrl.ToString(), txtURL.Text);
        }

        private void SetLoginUserForm()
        {
            if (Environment.UserName.ToLower() == Project.PrincipalDeveloper.ToString())
            {
                CRMAdminLogin crmAdminLogin = new CRMAdminLogin();
                txtUser.Text = crmAdminLogin.GetUser();
                txtPassword.Text = crmAdminLogin.GetPassword();
                txtURL.Text = crmAdminLogin.GetURL();
            }
            else
            {
                txtUser.Text = _registryHelper.ReadValueFromRegistry(Project.regUser.ToString());
                txtPassword.Text = _registryHelper.ReadValueFromRegistry(Project.regPassword.ToString());
                txtURL.Text = _registryHelper.ReadValueFromRegistry(Project.regUrl.ToString());
            }
        }
        private Login GetLogin()
        {
            Login login = null;
            CRMAdminLogin crmAdminLogin = new CRMAdminLogin();
            login = new Login() { Credentials = GetFormUserCredentials(), Uri = GetFormUserUri() };
            return login;
        }

        public ClientCredentials GetFormUserCredentials()
        {
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = txtUser.Text;
            clientCredentials.UserName.Password = txtPassword.Text;
            return clientCredentials;
        }
        public System.Uri GetFormUserUri()
        {
            return new System.Uri(txtURL.Text);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            TCControl.SelectedIndex = (TCControl.SelectedIndex + 1 < TCControl.TabCount) ? TCControl.SelectedIndex + 1 : TCControl.SelectedIndex;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            TCControl.SelectedIndex = (TCControl.SelectedIndex > 0) ? TCControl.SelectedIndex -1 : TCControl.SelectedIndex ;
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            if(txtPassword.PasswordChar== '\0')
            {
                txtPassword.PasswordChar = '*';
            }
            else
            {
                txtPassword.PasswordChar = '\0';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}
