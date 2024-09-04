using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;


namespace Hotel_db
{
    public partial class add : Form
    {
        private SQLiteConnection connection;
        private int id_client { get; set; }
        private int room_number { get; set; }
        public add()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadroomType();
            LoadPhones();
            LoadRoom();
            GetClient();
            Get_Room();
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
        private void LoadPhones()
        {
            string query = "SELECT id_client, name, surname FROM client";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            clients_box.Items.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                
                clients_box.Items.Add(row["id_client"].ToString() + " "+ row["name"] + " " + row["surname"]);
                comboBox4.Items.Add(row["id_client"].ToString());
            }
        }
        private void LoadroomType()
        {
            string query = "SELECT room_type FROM price";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            comboBox1.Items.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                comboBox1.Items.Add(row["room_type"].ToString());
            }
        }

        private void ConnectToDatabase()
        {
            string filePath = @"D:\Hotel\hotel.db";
            string connectionString = $"Data Source={filePath};Version=3;";

            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && (int)comboBox1.SelectedIndex != -1 && int.TryParse(textBox1.Text, out _) && int.TryParse(textBox2.Text, out _))
            {
                int roomNumb = int.Parse(textBox1.Text);
                if(RoomCheck(roomNumb))
                {
                    int sleeping_places = int.Parse(textBox2.Text);

                    string query = $"INSERT INTO room (room_number, number_sleeping_places, room_type) VALUES ({roomNumb}, {sleeping_places}, '{comboBox1.SelectedItem}')";
                    AddFunc(query);
                    MessageBox.Show("Кімната додана", "Додання кімнати", MessageBoxButtons.OK);
                    LoadRoom();
                }
                else
                {
                    MessageBox.Show("Кімната вже існує");
                }
            }
            else
            {
                MessageBox.Show("Введіть валідні дані", "Додання кімнати", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox6.Text) && !int.TryParse(textBox3.Text, out _) && !int.TryParse(textBox4.Text, out _) && int.TryParse(textBox5.Text, out _) && int.TryParse(textBox6.Text, out _))
            {
                int date = int.Parse(textBox5.Text);
                int passport = int.Parse(textBox6.Text);
                if (passport.ToString().Length != 9)
                {
                    MessageBox.Show("Некоректно введено паспортні дані");
                    return;
                }
                if (PassportCheck(passport))
                {
                    MessageBox.Show("Дані вже існують");

                    return;
                }
                string query = $"Insert into client (name, surname, date_of_birth, passport_number) values ('{textBox3.Text}', '{textBox4.Text}', {date}, {passport})";
                AddFunc(query);
                LoadPhones();
                GetClient();
                MessageBox.Show("Дані про клієнта збережені");
            }
            else
            {
                MessageBox.Show("Введіть валідні дані", "Додання клієнта", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool RoomCheck(int room) 
        {
            string query = $"select room_number from room where room_number = {room}";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            int rows = Convert.ToInt32(command.ExecuteScalar());
            return rows == 0; 
        }
        private void AddFunc(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();

        }
        private bool PassportCheck(int passport)
        {
            string q = $"select passport_number from client Where passport_number = {passport}";
            SQLiteCommand command = new SQLiteCommand(q, connection);
            command.ExecuteNonQuery();
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select phone_number from phone_number where id_client = @id";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", clients_box.SelectedItem.ToString());
            string phone_numb = "";
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    phone_numb = reader["phone_number"].ToString();
                }
            }
            this.phone_numb.Text = phone_numb;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(phone_numb.Text) && clients_box.SelectedIndex != -1 && int.TryParse(phone_numb.Text, out _))
            {
                string[] strings = clients_box.SelectedItem.ToString().Split(' ');
                int phone = int.Parse(phone_numb.Text);
                if (!CheckPhone(phone))
                {
                    string query = $"Insert into phone_number (id_client, phone_number) values ({strings[0]}, {phone})";
                    AddFunc(query);
                    LoadPhones();
                    MessageBox.Show("Номер додано");
                }
                else
                {
                    MessageBox.Show("Номер вже існує");
                }

            }
            else
            {
                MessageBox.Show("Оберіть валідні значення");
            }
        }
        private bool CheckPhone(int phone)
        {
            string q = $"select phone_number from phone_number Where phone_number = {phone}";
            SQLiteCommand command = new SQLiteCommand(q, connection);
            command.ExecuteNonQuery();
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
        private void GetClient()
        {
            comboBox4.Items.Clear();
            string query = $"select id_client, name, surname from client";
            SQLiteCommand command = new SQLiteCommand(@query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id_c = reader.GetInt32(reader.GetOrdinal("id_client"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    string surname = reader.GetString(reader.GetOrdinal("surname"));
                    comboBox4.Items.Add(id_c.ToString() + " " + name + " " + surname);
                }
            }
        }
        private void Get_Room()
        {
            comboBox3.Items.Clear();
            string query = $"select room_number, number_sleeping_places, room_type from room";
            SQLiteCommand command = new SQLiteCommand(@query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    room_number = reader.GetInt32(reader.GetOrdinal("room_number"));
                    int number_sleeping_places = reader.GetInt32(reader.GetOrdinal("number_sleeping_places"));
                    string type = reader.GetString(reader.GetOrdinal("room_type"));
                    comboBox3.Items.Add(room_number.ToString() + " - " + number_sleeping_places.ToString() + " - " + type);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox9.Text) && comboBox3.SelectedIndex != -1 && comboBox4.SelectedIndex != -1)
            {
                string regexPattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])\s([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";
                if (Regex.IsMatch(textBox8.Text + " " + textBox10.Text + ":00", regexPattern) && Regex.IsMatch(textBox9.Text + " " + textBox11.Text + ":00", regexPattern) )
                {
                    string selected_room = comboBox3.SelectedItem.ToString();
                    string[] room = selected_room.Split(' ');
                    string selected_client = comboBox4.SelectedItem.ToString();
                    string[] client = selected_client.Split(' ');

                    string query = $"INSERT INTO rent (room_number, id_client, check_in_datetime, check_out_datetime) values ({room[0]}, {client[0]}, @DateIn, @DateIn)";
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@DateIn", textBox8.Text + " " + textBox10.Text + ":00");
                    command.Parameters.AddWithValue("@DateOut", textBox9.Text + " " + textBox11.Text + ":00");
                    command.ExecuteNonQuery();
                    MessageBox.Show("Бронь записана", "Бронювання", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("Некоректно введена дата");
                }
            }
            else
            {
                MessageBox.Show("Заповніть поля!");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}