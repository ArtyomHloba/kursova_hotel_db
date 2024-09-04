using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace Hotel_db
{

    public partial class update : Form
    {

        private SQLiteConnection connection;
        private int id_client { get; set; }
        private int phone_number { get; set; }
        private int room_number { get; set; }
        public update()
        {
            InitializeComponent();
            ConnectToDatabase();
            GetClient();
            Get_Room();
            Get_Rent();
            Get_Phone();
            LoadroomType();
        }
        private void LoadroomType()
        {
            string query = "SELECT room_type FROM price";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            comboBox5.Items.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                comboBox5.Items.Add(row["room_type"].ToString());
            }
        }
        private void GetClient()
        {
            comboBox2.Items.Clear();
            string query = $"select id_client, name, surname, passport_number from client";
            SQLiteCommand command = new SQLiteCommand(@query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id_c = reader.GetInt32(reader.GetOrdinal("id_client"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    string surname = reader.GetString(reader.GetOrdinal("surname"));
                    int pass_numb = reader.GetInt32(reader.GetOrdinal("passport_number"));
                    comboBox2.Items.Add(id_c + " " + name + " " + surname + " " + pass_numb);
                    comboBox6.Items.Add(id_c + " " + name + " " + surname + " " + pass_numb);
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
                    comboBox7.Items.Add(room_number.ToString() + " - " + number_sleeping_places.ToString() + " - " + type);
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

        private void ConnectToDatabase()
        {
            string filePath = @"D:\Hotel\hotel.db";
            string connectionString = $"Data Source={filePath};Version=3;";

            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox5.SelectedIndex != -1 && int.TryParse(textBox1.Text, out _) && int.TryParse(textBox2.Text, out _))
            {

                string[] temp = comboBox1.SelectedItem.ToString().Split(' ');
                int room_number = Convert.ToInt32(temp[0]);

                string query = $"update room set room_number = {int.Parse(textBox1.Text)}, number_sleeping_places = {int.Parse(textBox2.Text)}, room_type = '{comboBox5.SelectedItem.ToString()}' where room_number = {room_number} ";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Дані змінено");
                    Get_Room();
                    Get_Rent();
                }
                else
                {
                    MessageBox.Show("Помилка оновлення");
                }
            }
            else
            {
                MessageBox.Show("Введіть дані");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && int.TryParse(textBox5.Text, out _) && int.TryParse(textBox6.Text, out _))
            {
                string[] temp = comboBox2.SelectedItem.ToString().Split(" ");
                int id_client = Convert.ToInt32(temp[0]);
                string query = $"update client set name = '{textBox3.Text}', surname = '{textBox4.Text}', date_of_birth = {int.Parse(textBox5.Text)}, passport_number = {int.Parse(textBox6.Text)} where id_client = {id_client}";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Дані оновлено");
                    GetClient();
                    Get_Phone();
                    Get_Rent();
                }
                else
                {
                    MessageBox.Show("Дані не оновлено");
                }
            }
            else
            {
                MessageBox.Show("Заповінть поля коректно");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] strings = comboBox3.SelectedItem.ToString().Split(' ');
            if (comboBox3.SelectedIndex != -1 && int.TryParse(textBox7.Text, out _))
            {
                string query = $"update phone_number set phone_number = {int.Parse(textBox7.Text)} where id_client = {strings[0]} and phone_number = {strings[4]}";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Дані оновлено");
                    Get_Phone();
                }
                else
                {
                    MessageBox.Show("Дані не оновлено");
                }
            }
            else
            {
                MessageBox.Show("Введіть дані коректно");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(comboBox4.SelectedIndex != -1 && comboBox6.SelectedIndex != -1 && comboBox7.SelectedIndex != -1)
            {
                string regexPattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])\s([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";
                if (Regex.IsMatch(textBox8.Text + " " + textBox9.Text + ":00", regexPattern) && (Regex.IsMatch(textBox10.Text + " " + textBox11.Text + ":00", regexPattern)))
                {
                    string[] chosen_rent = comboBox4.SelectedItem.ToString().Split(" | ");
                    string[] client = comboBox6.SelectedItem.ToString().Split(' ');
                    string[] room = comboBox7.SelectedItem.ToString().Split(' ');
                    string query = $"Update rent set room_number = {room[0]}, id_client = {client[0]}, check_in_datetime = @DateIn, check_out_datetime = @DateIn where id_client = {chosen_rent[0]} and room_number = {chosen_rent[2]}";
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@DateIn", textBox8.Text + " " + textBox9.Text + ":00");
                    command.Parameters.AddWithValue("@DateOut", textBox10.Text + " " + textBox11.Text + ":00");
                    command.ExecuteNonQuery();
                    MessageBox.Show("Бронь змінено", "Бронювання", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Get_Rent();
                }
                else
                {
                    MessageBox.Show("Не вдалося оновити бронь");
                }
            }
            else
            {
                MessageBox.Show("Неправильно обрані дані");
            }
        }
    }
}
