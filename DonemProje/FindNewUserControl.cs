using Microsoft.AspNet.SignalR.Client;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DonemProje
{
    public partial class FindNewUserControl : UserControl
    {
        string connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        int TargetUserID;
        string UserName;
        int UserID;
        public FindNewUserControl(string username,int ID)
        {
            UserName = username;
            UserID = ID;    
            InitializeComponent();
        }

        private void FindNewUserControl_Load(object sender, EventArgs e)
        {

        }
        private bool IsUserExist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               

                string UserIDFetch = "SELECT USER_ID  FROM users_table WHERE USERNAME = @USERNAME ";
                try
                {


                    using (SqlCommand command = new SqlCommand(UserIDFetch, connection))
                    {
                        command.Parameters.AddWithValue("@USERNAME", findFriendTextBox.Text.Trim());


                        connection.Open();
                        int result = Convert.ToInt32(command.ExecuteScalar());
                        if (result != null && result != 0)
                        {
                            TargetUserID = result;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("HATA İLE KARŞILAŞILDI" + ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }

            }



        
        }
        private int FindFriendCase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                string FindFriendCase = @"SELECT COUNT(*) FROM User_Relations_table
        WHERE 
    (
        (USER_ID = @USER_ID AND TARGET_USER_ID = @TARGET_USER_ID) 
		OR 
		(USER_ID = @TARGET_USER_ID AND TARGET_USER_ID = @USER_ID) 
    ) ";
                
                try
                {
                    int result = -1;
            MainPage mainPage = new MainPage(); 

                    using (SqlCommand command = new SqlCommand(FindFriendCase, connection))
                    {
                        command.Parameters.AddWithValue("@USER_ID", UserID);
                        command.Parameters.AddWithValue("@TARGET_USER_ID", TargetUserID);

                        connection.Open();
                        result = Convert.ToInt32(command.ExecuteScalar());
                        if (result !=-1)
                        {
                            
                            return result; // result 0 ise hiç ilişkiyok 
                                           // result 1 ise engellenmiş ya da zaten bir istek gönderilmiş
                                           // result 2 ise zaten arkadaşlar
                        }
                    
                        
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("HATA İLE KARŞILAŞILDI" + ex.Message);
                   // return false;
                }
                finally
                {
                    connection.Close();
                }
                return -1;
            }
        }
        private void DbAddRequest()
        {
            string insertQuery = @"
    INSERT INTO User_Relations_table (USER_ID, TARGET_USER_ID, _CASE) 
    VALUES (@USER_ID, @TARGET_USER_ID, @_CASE)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {MainPage mainPage = new MainPage();
                       
                        command.Parameters.AddWithValue("@USER_ID", UserID);         
                        command.Parameters.AddWithValue("@TARGET_USER_ID", TargetUserID); 
                        command.Parameters.AddWithValue("@_CASE", 'W');              

                        connection.Open(); 
                        command.ExecuteNonQuery(); 

                       
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close(); 
                }
            }
        }
        private async void findFriendsButton_Click(object sender, EventArgs e)
        {

            try
            {
                MainPage mainPage = new MainPage();

                if (IsUserExist() && findFriendTextBox.Text.Trim() != UserName.Trim())    // böyle bir kullanıcı varsa ve yazdığı kullanıcı adı kendisi değilse
                {
                    if (FindFriendCase() == 0)     // aralarında hiç ilişki yoksa istek gönderir
                    {
                        // database kaydı yapılacak
                        DbAddRequest();

                        MessageBox.Show("Friend request sent!");
                    }
                    else if (FindFriendCase() == 1)  // aralarında ilişki var  ve işlem yapılmaz
                    {
                        MessageBox.Show("We can't perform an action for this user.");
                        return;
                    }
                    else if (FindFriendCase() == 2) // arkadaşlar. Bu yüzden işlem yapılmaz
                    {
                        MessageBox.Show("You are already friends with this user");
                        return;

                    }

                   
                }else if (findFriendTextBox.Text.Trim() == UserName)
                {
                    MessageBox.Show("You cannot add yourself as a friend!");
                }
                else
                {
                    MessageBox.Show("There is no such user.");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR" + ex.Message);
            }
           
         
        }
      
    }
}
