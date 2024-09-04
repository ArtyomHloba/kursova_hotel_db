using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_db
{
    public partial class delete : Form
    {
        private SQLiteConnection connection;
        private int id_client { get; set; }
        private int phone_number { get; set; }
        private int room_number { get; set; }
        public delete()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadRoom();
            GetClient();
            Get_Room();
            Get_Phone();
            Get_Rent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $"delete from room where room_number = @number";
            string[] selected_room = comboBox1.SelectedItem.ToString().Split(' ');
            room_number = Convert.ToInt32(selected_room[0]);
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@number", room_number);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Кімнату видалено");
                    Get_Room();
                    DeleteRent(room_number);
                    Get_Rent();
                    comboBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Кімнату не було видалено");
                }
            }
        }
        private void DeleteRent(int value)
        {
            string query = $"Delete from rent where room_number = @value";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@value", value);
                int rowsAffected = command.ExecuteNonQuery();

            }
        }
        private void GetClient()
        {
            comboBox2.Items.Clear();
            string query = $"select id_client, name, surname from client";
            SQLiteCommand command = new SQLiteCommand(@query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id_c = reader.GetInt32(reader.GetOrdinal("id_client"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    string surname = reader.GetString(reader.GetOrdinal("surname"));
                    comboBox2.Items.Add(id_c + " " + name + " " + surname);
                }
            }
        }
        private void Get_Room()
        {
            comboBox1.Items.Clear();
            string query = $"select room_number, number_sleeping_places, room_type from room";
            SQLiteCommand command = new SQLiteCommand(@query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    room_number = reader.GetInt32(reader.GetOrdinal("room_number"));
                    int number_sleeping_places = reader.GetInt32(reader.GetOrdinal("number_sleeping_places"));
                    string type = reader.GetString(reader.GetOrdinal("room_type"));
                    comboBox1.Items.Add(room_number.ToString() + " - " + number_sleeping_places.ToString() + " - " + type);
                }
            }
        }
        private void Get_Phone()
        {
            comboBox3.Items.Clear();
            string query = $"select phone_number.id_client, phone_number.phone_number, client.name from phone_number JOIN client  ON phone_number.id_client = client.id_client";
            SQLiteCommand command = new SQLiteCommand(@query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    id_client = reader.GetInt32(reader.GetOrdinal("id_client"));
                    phone_number = reader.GetInt32(reader.GetOrdinal("phone_number"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    comboBox3.Items.Add(id_client.ToString() + " - " + name + " - " + phone_number);
                }
            }
        }

        private void Get_Rent()
        {
            comboBox4.Items.Clear();
            string query = $"select rent.id_client, client.name, rent.room_number, rent.check_in_datetime, rent.check_out_datetime from rent JOIN client ON client.id_client = rent.id_client";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id_client = reader.GetInt32(reader.GetOrdinal("id_client"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    int room = reader.GetInt32(reader.GetOrdinal("room_number"));
                    DateTime check_in = reader.GetDateTime(reader.GetOrdinal("check_in_datetime"));
                    DateTime check_out = reader.GetDateTime(reader.GetOrdinal("check_out_datetime"));
                    comboBox4.Items.Add(id_client + " | " + name + " | " + room + " | " + check_in + " | " + check_out);
                }
            }
        }

        private void LoadRoom()
        {
            string query = "SELECT room_number FROM room";
            comboBox3.Items.Clear();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int number = reader.GetInt32(reader.GetOrdinal("room_number"));
                    comboBox3.Items.Add(number);
                }
            }
        }
        private void ConnectToDatabase()
        {
            string filePath = @"D:\Hotel\hotel.db";
            string connectionString = $"Data Source={filePath};Version=3;";

            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex != -1)
            {
                string[] selected_client = comboBox2.SelectedItem.ToString().Split(' ');
                id_client = Convert.ToInt32(selected_client[0]);
                string query = $"Delete from client where id_client = {id_client}";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Клієнта видалено");
                        GetClient();
                        DeleteRent(id_client);
                        DeleteClientPhone();
                        Get_Rent();
                        comboBox2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Клієнта не видалено");
                    }
                }

            }
            else
            {
                MessageBox.Show("Оберіть Клієнта!");
            }
        }
        private void DeleteClientPhone()
        {
            if(comboBox2.SelectedIndex != -1)
            {
                string[] selected_client = comboBox2.SelectedItem.ToString().Split(' ');
                id_client = Convert.ToInt32(selected_client[0]);
                string query = $"delete from phone_number where id_client = {id_client}";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Телефони клієнта видалено");
                    }
                    else
                    {
                        MessageBox.Show("У клієнта не було телефона(-ів)");
                    }
                }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {
                string[] selected_phone = comboBox3.SelectedItem.ToString().Split(' ');
                id_client = Convert.ToInt32(selected_phone[0]);
                phone_number = Convert.ToInt32(selected_phone[4]);
                string query = $"Delete from phone_number where id_client = {id_client} and phone_number = {phone_number}";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Номер телефону клієнта видалено");
                        Get_Phone();
                        comboBox3.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Номер телефону клієнта не видалено");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                string[] selected_rent = comboBox4.SelectedItem.ToString().Split(" | ");
                id_client = Convert.ToInt32(selected_rent[0]);
                room_number = Convert.ToInt32(selected_rent[2]);
                string date_in = ConvertToSqlDateTimeFormat(selected_rent[3]);
                string date_out = ConvertToSqlDateTimeFormat(selected_rent[4]);
                string query = $"Delete from rent where id_client = {id_client} AND room_number = {room_number} AND check_in_datetime = '{date_in}' AND check_out_datetime = '{date_out}'";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Бронь видалено");
                        Get_Rent();
                        comboBox4.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Бронь не вдалося видалити");
                    }
                }
            }
        }
        private string ConvertToSqlDateTimeFormat(string str)
        {
            string[] strings = str.Split('.', ' ');
            str = strings[2] +"-"+ strings[1]+ "-" + strings[0] + " " + strings[3];
            return str;
        }
    }
}
