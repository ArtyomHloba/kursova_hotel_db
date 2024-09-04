using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Hotel_db
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        List <string> _room = new List<string> {"�����", "ʳ������ ����", "���", "ֳ��/����" };
        List<string> _client = new List<string> { "������� ��'�", "г� ����������", "����� ��������", "�������(�)"};
        List<string> _rent = new List<string> { "�����", "������� ��'�", "���� �'����", "���� �����" };
        List<string> _phone = new List<string> { "������� ��'�", "����� ��������" };
        List<string> _price = new List<string> { "���", "ֳ��/����" };

        public Form1()
        {
            InitializeComponent();
            ConnectToDatabase();
            comboBox1.Items.Add("ʳ�����");
            comboBox1.Items.Add("�볺���");
            comboBox1.Items.Add("��������");
            comboBox1.Items.Add("��������");
            comboBox1.Items.Add("�����");
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
                        "|| client.name AS ���������, " +
                    "client.date_of_birth AS г�����������, client.passport_number AS �������������, " +
                    "GROUP_CONCAT(IFNULL(phone_number.phone_number, ''), ', ') AS �������� " +
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
                dataGridView1.Columns[1].HeaderText = "������� ��'�";
                dataGridView1.Columns[2].HeaderText = "г� ����������";
                dataGridView1.Columns[3].HeaderText = "����� ��������";
            }
            else if(comboBox2.DataSource == _room)
            {
                query = "select room.room_number as �����, room.number_sleeping_places as ʳ������_��������_���� , " +
                        "room.room_type as ���, " +
                "price.price_per_day ֳ��_��_���� from room JOIN price ON room.room_type = price.room_type"; ;
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
                dataGridView1.Columns[1].HeaderText = "ʳ������ �������� ����";
                dataGridView1.Columns[3].HeaderText = "ֳ�� �� ����";
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
                dataGridView1.Columns[0].HeaderText = "�����";
                dataGridView1.Columns[1].HeaderText = "���";
                dataGridView1.Columns[3].HeaderText = "������� ��'�";
                dataGridView1.Columns[4].HeaderText = "���� �����";
                dataGridView1.Columns[5].HeaderText = "���� �����";

            }
            else if(comboBox2.DataSource == _phone)
            {
                query = "select phone_number.id_client,client.surname || ' '|| client.name AS ���������, " +
                        " phone_number.phone_number as �������� " +
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
                dataGridView1.Columns[1].HeaderText = "������� ��'�";
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
                
                case "ʳ�����":
                    comboBox2.DataSource = _room;

                    query = "select room.room_number as �����, room.number_sleeping_places as ʳ������_��������_���� , " +
                        "room.room_type as ���, " +
                "price.price_per_day ֳ��_��_���� from room JOIN price ON room.room_type = price.room_type";
                    executeQuery(query);
                    dataGridView1.Columns[1].HeaderText = "ʳ������ �������� ����";
                    dataGridView1.Columns[3].HeaderText = "ֳ�� �� ����";
                    break;
                case "�볺���":
                    comboBox2.DataSource = _client;

                    query = "SELECT client.id_client, client.surname || ' '" +
                        "|| client.name AS ���������, " +
                    "client.date_of_birth AS г�����������, client.passport_number AS �������������, " +
                    "GROUP_CONCAT(IFNULL(phone_number.phone_number, ''), ', ') AS �������� " +
                    "FROM client LEFT JOIN phone_number ON client.id_client = phone_number.id_client " +
                    "GROUP BY client.id_client, ���������, г�����������, �������������";
                    executeQuery(query);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "������� ��'�";
                    dataGridView1.Columns[2].HeaderText = "г� ����������";
                    dataGridView1.Columns[3].HeaderText = "����� ��������";
                    break;
                case "��������":
                    comboBox2.DataSource = _phone;
                    query = "select phone_number.id_client,client.surname || ' '|| client.name AS ���������, " +
                        " phone_number.phone_number as �������� " +
               " from phone_number JOIN client ON phone_number.id_client = client.id_client";
                    executeQuery(query);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "������� ��'�";
                    break;
                case "��������":
                    comboBox2.DataSource = _price;
                    query = "select * from price";
                    executeQuery(query);
                    dataGridView1.Columns[0].HeaderText = "��� ������";
                    dataGridView1.Columns[1].HeaderText = "ֳ�� �� ����";
                    break;
                case "�����":
                    comboBox2.DataSource = _rent;
                    query = "select distinct rent.room_number, room.room_type, rent.id_client, client.surname || ' ' || client.name, rent.check_in_datetime, " +
               "check_out_datetime from rent JOIN client on rent.id_client = client.id_client JOIN room on rent.room_number = room.room_number";
                    executeQuery(query);
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[0].HeaderText = "�����";
                    dataGridView1.Columns[1].HeaderText = "���";
                    dataGridView1.Columns[3].HeaderText = "������� ��'�";
                    dataGridView1.Columns[4].HeaderText = "���� �����";
                    dataGridView1.Columns[5].HeaderText = "���� �����";
                    break;
            }


        }
        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add addRoomForm = new add();
            addRoomForm.Show();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete del = new delete();
            del.Show();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update up = new update();
            up.Show();
        }
    }

}

