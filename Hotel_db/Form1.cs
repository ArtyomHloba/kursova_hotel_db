using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Hotel_db
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        List <string> _room = new List<string> {"Номер", "Кількість ліжок", "Тип", "Ціна/день" };
        List<string> _client = new List<string> { "Прізвище Ім'я", "Рік народження", "Номер паспорта", "Телефон(и)"};
        List<string> _rent = new List<string> { "Номер", "Прізвище Ім'я", "Дата в'їзду", "Дата виїзду" };
        List<string> _phone = new List<string> { "Прізвище Ім'я", "Номер телефону" };
        List<string> _price = new List<string> { "Тип", "Ціна/день" };

        public Form1()
        {
            InitializeComponent();
            ConnectToDatabase();
            comboBox1.Items.Add("Кімнати");
            comboBox1.Items.Add("Клієнти");
            comboBox1.Items.Add("Телефони");
            comboBox1.Items.Add("Розцінка");
            comboBox1.Items.Add("Бронь");
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
            comboBox2.SelectedIndex = -1;
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            string query = "";
            string selectedTable = comboBox1.SelectedItem.ToString();
            ifFieldIsNull(selectedTable);
        }
        private void executeQuery(string query)
        {
            DataTable data = new DataTable();
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
            {
                adapter.Fill(data);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = data;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            string query = "";
            if (comboBox2.DataSource == _client)
            {
                query = "SELECT client.id_client, client.surname || ' '" +
                        "|| client.name AS ПрізвищеІмя, " +
                    "client.date_of_birth AS Рікнародження, client.passport_number AS Номерпаспорта, " +
                    "GROUP_CONCAT(IFNULL(phone_number.phone_number, ''), ', ') AS Телефони " +
                    "FROM client LEFT JOIN phone_number ON client.id_client = phone_number.id_client ";
                if (int.TryParse(search, out _))
                {
                    int int_search = int.Parse(search);
                    query += $" WHERE client.date_of_birth = {int_search} or phone_number.phone_number = {int_search} " +
                        $"or passport_number = {int_search}";
                }
                else
                {
                    query += $" WHERE client.name LIKE '%{search}%' or client.surname LIKE '%{search}%' ";
                }
                executeQuery(query);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Прізвище Ім'я";
                dataGridView1.Columns[2].HeaderText = "Рік народження";
                dataGridView1.Columns[3].HeaderText = "Номер паспорта";
            }
            else if(comboBox2.DataSource == _room)
            {
                query = "select room.room_number as Номер, room.number_sleeping_places as Кількість_спальних_місць , " +
                        "room.room_type as Тип, " +
                "price.price_per_day Ціна_за_день from room JOIN price ON room.room_type = price.room_type"; ;
                if (int.TryParse(search, out _))
                {
                    int int_search = int.Parse(search);
                    query += $" WHERE room.room_number LIKE {int_search} or room.number_sleeping_places LIKE {int_search} " +
                        $"or price.price_per_day LIKE {int_search}";
                }
                else
                {
                    query += $" WHERE room.room_type LIKE '%{search}%'";
                }
                executeQuery(query);
                dataGridView1.Columns[1].HeaderText = "Кількість спальних місць";
                dataGridView1.Columns[3].HeaderText = "Ціна за день";
            }
            else if(comboBox2.DataSource == _rent)
            {
                query = "select distinct rent.room_number, room.room_type, rent.id_client, client.surname || ' ' || client.name, rent.check_in_datetime, " +
              "check_out_datetime from rent " +
              " Right JOIN client on rent.id_client = client.id_client JOIN room on rent.room_number = room.room_number";
                if(int.TryParse(search, out _))
                {
                    int int_search = int.Parse(search);
                    query += $" WHERE rent.room_number = {int_search}"; 
                }
                else
                {
                    query += $" WHERE room.room_type LIKE '%{search}%' or client.name LIKE '%{search}%' or client.surname LIKE '%{search}%'" +
                        $" or rent.check_in_datetime LIKE '%{search}%' or rent.check_out_datetime LIKE '%{search}%'";
                }
                executeQuery(query);
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[0].HeaderText = "Номер";
                dataGridView1.Columns[1].HeaderText = "Тип";
                dataGridView1.Columns[3].HeaderText = "Прізвище Ім'я";
                dataGridView1.Columns[4].HeaderText = "Дата заїзду";
                dataGridView1.Columns[5].HeaderText = "Дата виїзду";

            }
            else if(comboBox2.DataSource == _phone)
            {
                query = "select phone_number.id_client,client.surname || ' '|| client.name AS ПрізвищеІмя, " +
                        " phone_number.phone_number as Телефони " +
               " from phone_number JOIN client ON phone_number.id_client = client.id_client";
                if (int.TryParse(search, out int int_search))
                {
                    query += $" WHERE phone_number.phone_number = {int_search}";       
                }
                else
                {
                    query += $" WHERE client.name LIKE '%{search}%' or client.surname LIKE '%{search}%'";
                }

                executeQuery(query);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Прізвище Ім'я";
            }
            if(textBox1.Text == "")
            {
                comboBox2.DataSource = null;
                string selectedTable = comboBox1.SelectedItem.ToString();
                ifFieldIsNull(selectedTable);
            }
        }
        private void ifFieldIsNull(string table)
        {
            string query = "";
            switch (table)
            {
                
                case "Кімнати":
                    comboBox2.DataSource = _room;

                    query = "select room.room_number as Номер, room.number_sleeping_places as Кількість_спальних_місць , " +
                        "room.room_type as Тип, " +
                "price.price_per_day Ціна_за_день from room JOIN price ON room.room_type = price.room_type";
                    executeQuery(query);
                    dataGridView1.Columns[1].HeaderText = "Кількість спальних місць";
                    dataGridView1.Columns[3].HeaderText = "Ціна за день";
                    break;
                case "Клієнти":
                    comboBox2.DataSource = _client;

                    query = "SELECT client.id_client, client.surname || ' '" +
                        "|| client.name AS ПрізвищеІмя, " +
                    "client.date_of_birth AS Рікнародження, client.passport_number AS Номерпаспорта, " +
                    "GROUP_CONCAT(IFNULL(phone_number.phone_number, ''), ', ') AS Телефони " +
                    "FROM client LEFT JOIN phone_number ON client.id_client = phone_number.id_client " +
                    "GROUP BY client.id_client, ПрізвищеІмя, Рікнародження, Номерпаспорта";
                    executeQuery(query);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Прізвище Ім'я";
                    dataGridView1.Columns[2].HeaderText = "Рік народження";
                    dataGridView1.Columns[3].HeaderText = "Номер паспорта";
                    break;
                case "Телефони":
                    comboBox2.DataSource = _phone;
                    query = "select phone_number.id_client,client.surname || ' '|| client.name AS ПрізвищеІмя, " +
                        " phone_number.phone_number as Телефони " +
               " from phone_number JOIN client ON phone_number.id_client = client.id_client";
                    executeQuery(query);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Прізвище Ім'я";
                    break;
                case "Розцінка":
                    comboBox2.DataSource = _price;
                    query = "select * from price";
                    executeQuery(query);
                    dataGridView1.Columns[0].HeaderText = "Тип кімнати";
                    dataGridView1.Columns[1].HeaderText = "Ціна за день";
                    break;
                case "Бронь":
                    comboBox2.DataSource = _rent;
                    query = "select distinct rent.room_number, room.room_type, rent.id_client, client.surname || ' ' || client.name, rent.check_in_datetime, " +
               "check_out_datetime from rent JOIN client on rent.id_client = client.id_client JOIN room on rent.room_number = room.room_number";
                    executeQuery(query);
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[0].HeaderText = "Номер";
                    dataGridView1.Columns[1].HeaderText = "Тип";
                    dataGridView1.Columns[3].HeaderText = "Прізвище Ім'я";
                    dataGridView1.Columns[4].HeaderText = "Дата заїзду";
                    dataGridView1.Columns[5].HeaderText = "Дата виїзду";
                    break;
            }


        }
        private void обратиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add addRoomForm = new add();
            addRoomForm.Show();
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete del = new delete();
            del.Show();
        }

        private void оновитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update up = new update();
            up.Show();
        }
    }

}

