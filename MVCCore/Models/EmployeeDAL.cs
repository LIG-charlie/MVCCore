using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MVCCore.Models
{
    public class EmployeeDAL
    {
        string connectionString = "Data Source=LAPTOP-I0PP50M9\\MSSQLSERVER2;Initial Catalog=sampleDB;Persist Security Info=False;User ID=sa;password=269842327";
        public IEnumerable<EmployeeInfo> GetAllEmployee()
        {
            List<EmployeeInfo> empList = new List<EmployeeInfo>();
            using (SqlConnection con = new SqlConnection(connectionString)) {
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) {
                    EmployeeInfo emp = new EmployeeInfo();
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Company = dr["Company"].ToString();
                    emp.Department = dr["Department"].ToString();

                    empList.Add(emp);
                }
                con.Close();
            }
            return empList;
        }
        public void AddEmployee(EmployeeInfo emp)
        {
            List<EmployeeInfo> empList = new List<EmployeeInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Company", emp.Company);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateEmployee(EmployeeInfo emp)
        {
            List<EmployeeInfo> empList = new List<EmployeeInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", emp.ID);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Company", emp.Company);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void DeleteEmployee(int? empId)
        {
            List<EmployeeInfo> empList = new List<EmployeeInfo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", empId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public EmployeeInfo GetEmployeeById(int? empId)
        {
            EmployeeInfo emp = new EmployeeInfo();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", empId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Company = dr["Company"].ToString();
                    emp.Department = dr["Department"].ToString();
                }
                con.Close();
            }
            return emp;
        }
    }
}
