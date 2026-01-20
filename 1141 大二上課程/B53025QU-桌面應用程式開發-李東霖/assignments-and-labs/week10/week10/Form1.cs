using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace week10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        MySqlConnection connection;
        const string database = "member";//資料庫名稱
        const string databaseServer = "localhost";//資料庫伺服器
        const string databaseUser = "T00001";//資料庫使用者
        const string databasePassword = "123456";//資料庫密碼
        const string databasePort = "5678";

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString =
            $"server={databaseServer};" + $"port={databasePort};" + $"user={databaseUser};" +
            $"password={databasePassword};" + $"database={database};" +
            "charset=utf8;";
            try
            {
                using (connection = new MySqlConnection(connectionString))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM userdata", connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // 確保 DataGridView 不會因為手動欄位定義而無法顯示資料
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.AutoGenerateColumns = true;

                    if (dt.Rows.Count == 0)
                    {
                        dataGridView1.DataSource = null;
                        MessageBox.Show("查無資料。", "查詢結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 直接綁定 DataTable，欄位由 DataTable 決定
                    dataGridView1.DataSource = dt;
                    // 顯示 row count 於狀態或訊息（方便偵錯）
                    // MessageBox.Show($"查詢到 {dt.Rows.Count} 筆資料。", "查詢結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 新增資料（先驗證 id 為整數）
            if (!int.TryParse(textBox1.Text, out int idValue))
            {
                MessageBox.Show("請在 ID 輸入欄位輸入有效的整數，或改為使用資料庫的 AUTO_INCREMENT。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString =
                $"server={databaseServer};" + $"port={databasePort};" + $"user={databaseUser};" +
                $"password={databasePassword};" + $"database={database};" +
                "charset=utf8;";
            try
            {
                using (connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql =
                    "INSERT INTO userdata (id, name) VALUES (@id, @username)";
                    using (MySqlCommand insertCommand = new MySqlCommand(sql, connection))
                    {
                        insertCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = idValue;
                        insertCommand.Parameters.Add("@username", MySqlDbType.VarChar).Value = textBox2.Text ?? string.Empty;
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        MessageBox.Show($"新增完成，影響列數: {rowsAffected}", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int idValue))
            {
                MessageBox.Show("請在 ID 輸入欄位輸入有效的整數。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString =
                $"server={databaseServer};" + $"port={databasePort};" + $"user={databaseUser};" +
                $"password={databasePassword};" + $"database={database};" +
                "charset=utf8;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE userdata SET name = @name WHERE id = @id";
                using (MySqlCommand updateCommand = new MySqlCommand(sql, connection))
                {
                    updateCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text ?? string.Empty;
                    updateCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = idValue;
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    MessageBox.Show($"更新完成，影響列數: {rowsAffected}", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int idValue))
            {
                MessageBox.Show("請在 ID 輸入欄位輸入有效的整數。", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString =
                $"server={databaseServer};" + $"port={databasePort};" + $"user={databaseUser};" +
                $"password={databasePassword};" + $"database={database};" +
                "charset=utf8;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM userdata WHERE id = @id";
                using (MySqlCommand deleteCommand = new MySqlCommand(sql, connection))
                {
                    deleteCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = idValue;
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    MessageBox.Show($"刪除完成，影響列數: {rowsAffected}", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                connection.Close(); //資料庫斷線
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
