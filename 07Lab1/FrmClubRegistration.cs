using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07Lab1
{
    public partial class FrmClubRegistration : Form
    {
        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private FrmUpdateMember frmUpdateMember;
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentId;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmUpdateMember = new FrmUpdateMember();
            frmUpdateMember.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            StudentId = long.Parse(txtStudID.Text);
            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Age = Convert.ToInt32(txtAge.Text);
            Gender = cbGender.Text;
            Program = cbProgram.Text;
            clubRegistrationQuery.RegisterStudent(RegistrationID(), StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);
        }


        void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            DGVClubMembers.DataSource = clubRegistrationQuery.bindingSource;
        }

        int RegistrationID ()
        {
            count += 1;
            return count;
        }
    }
}
