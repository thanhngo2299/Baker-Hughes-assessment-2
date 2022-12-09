using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public List<UserData> GetAllUser()
    {
        List<UserData> userCollection = new List<UserData>();
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        string sql = "SELECT * FROM TestWCF";

        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();

            using (SqlCommand command = new SqlCommand(sql, con))
            {

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        UserData tbl = new UserData();

                        tbl.VIN = (int)reader["VIN"];

                        tbl.comboBox_Vehicle_Maker.selectedIndex = (int)reader["vehicle_maker"]; 
  
                        tbl.vehicle_year = (int)reader["vehicle_year"]; 

                        tbl.vehicle_model = Convert.IsDBNull(reader["vehicle_model"]) ? null : (string)reader["vehicle_model"];

                        tbl.inspection_date = (DateTime)reader["inspection_date"]; 

  				  tbl.inspector_name = Convert.IsDBNull(reader["inspector_name"]) ? null : (string)reader["inspector_name"];

                        tbl.comboBox_inspection_result.selectedIndex = (int)reader["inspection_result"]; 

				  tbl.notes = Convert.IsDBNull(reader["notes"]) ? null : (string)reader["notes"];

                        userCollection.Add(tbl);
                    }
                }
            }


            con.Close();
        }
        return userCollection;
    }

    public void Insert(int VIN, int maker, int year, string model, DateTime inspection_date, string inspector_name, int inspection_result, string notes);
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO TestWCF (VIN, vehicle_maker, vehicle_year, 
													vehicle_model, inspection_date, inspector_name, inspection_result, notes) 
									VALUES (@VIN, @vehicle_maker, @vehicle_year, 
										  @vehicle_model, @inspection_date, @inspector_name, @inspection_result, @notes)  "))
            {
                cmd.Parameters.AddWithValue("@VIN", VIN);
                cmd.Parameters.AddWithValue("@vehicle_maker", maker);
                cmd.Parameters.AddWithValue("@vehicle_year", year);
                cmd.Parameters.AddWithValue("@vehicle_model", model);
                cmd.Parameters.AddWithValue("@inspection_date", inspection_date);
                cmd.Parameters.AddWithValue("@inspector_name", inspector_name);
                cmd.Parameters.AddWithValue("@inspection_result", inspection_result);
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    public void Update(int VIN, int maker, int year, string model, DateTime inspectionDate, string inspector_name, int inspection_result, string notes);
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE TestWCF SET vehicle_maker = @vehicle_maker 
												    vehicle_year = @vehicle_year,
												    vehicle_model = @vehicle_model,
												    inspection_date = @inspection_date,
												    inspector_name = @inspector_name,
												    inspection_result = @inspection_result,
												    notes = @notes
 												    
                                                    WHERE VIN = @VIN"))
            {
                cmd.Parameters.AddWithValue("@VIN", VIN);
                cmd.Parameters.AddWithValue("@vehicle_maker", maker);
                cmd.Parameters.AddWithValue("@vehicle_year", year);
                cmd.Parameters.AddWithValue("@vehicle_model", model);
                cmd.Parameters.AddWithValue("@inspection_date", inspection_date);
                cmd.Parameters.AddWithValue("@inspector_name", inspector_name);
                cmd.Parameters.AddWithValue("@inspection_result", inspection_result);
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    public void Delete(int VIN)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM TestWCF WHERE VIN = @VIN"))
            {
                cmd.Parameters.AddWithValue("@VIN", VIN);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
