using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

namespace _07Lab1
{
    public partial class FrmUpdateMember : Form
    {
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;

        private int ID;
        private string FirstName, MiddleName, LastName, Gender, Program, Age;
        private long StudentID;
        public DataTable dataTableFill;
        private long[] studentIds;

        public DataTable dataTable;
        public BindingSource bindingSource;

        private string connectionString;

        private void cbStudID_SelectedIndexChanged(object sender, EventArgs e)
        {
            StudentID = long.Parse(cbStudID.Text);

            getValues();

            txtLastName.Text = LastName;
            txtFirstName.Text = FirstName;
            txtMiddleName.Text = MiddleName;
            txtAge.Text = Age;
            cbGender.Text = Gender;
            cbProgram.Text = Program;

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand("UPDATE ClubMembers SET StudentId = @StudentId, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId ='" + StudentID + "'", sqlConnect);
            sqlCommand.Parameters.Add("@StudentId", SqlDbType.BigInt).Value = cbStudID.Text;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = txtFirstName.Text;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = txtMiddleName.Text;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text = LastName; ;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = txtAge.Text;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = cbGender.Text;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = cbProgram.Text;
            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\User\\Large Files\\Visual Studio Files\\07Lab1\\07Lab1\\ClubDB.mdf\";Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();

            ShowClubMembers();
        }

        void ShowClubMembers()
        {
            DisplayList();
            
            foreach(long l in ShowStudIds())
            {
                cbStudID.Items.Add(l);
            }
        }

        void getValues()
        {
            dataTableFill = new DataTable();
            dataTableFill = FillFields(cbStudID.Text);

            FirstName = dataTableFill.Rows[0]["FirstName"].ToString();
            MiddleName = dataTableFill.Rows[0]["MiddleName"].ToString();
            LastName = dataTableFill.Rows[0]["LastName"].ToString();
            Age = dataTableFill.Rows[0]["Age"].ToString();
            Gender = dataTableFill.Rows[0]["Gender"].ToString();
            Program = dataTableFill.Rows[0]["Program"].ToString();
        }

        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT StudentId FROM ClubMembers";
            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);

            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;


            return true;
        }

        public long[] ShowStudIds()
        {
            int rowCount = dataTable.Rows.Count;
            studentIds = new long[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                studentIds[i] = long.Parse(dataTable.Rows[i]["StudentId"].ToString());
            }

            return studentIds;
        }

        public DataTable FillFields(string StudId)
        {
            string ViewClubMembers = "SELECT FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers WHERE StudentId = '" + StudId + "'";
            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);

            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;

            return dataTable;
        }
    }
}
