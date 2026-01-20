using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;

namespace week9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Address
        {
            public string City { get; set; }
            public string Street { get; set; }
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<string> Hobbies { get; set; }
            public Address HomeAddress { get; set; }
        }

        public class Employee
        {
            [JsonProperty("full_name")]
            public string Name { get; set; }

            [JsonProperty("hire_date")]
            [JsonConverter(typeof(IsoDateTimeConverter))]
            public DateTime HireDate { get; set; }

            [JsonIgnore]
            public string password { get; set; }
            public int Age { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var person = new Person
            {
                Name = "Alice",
                Age = 30,
                Hobbies = new List<string> { "Reading", "Hiking", "Coding" },
                HomeAddress = new Address
                {
                    City = "Taipei",
                    Street = "Xinyi Rd. Sec. 5"
                }
            };

            string json = JsonConvert.SerializeObject(person, Newtonsoft.Json.Formatting.Indented);
            richTextBox1.AppendText("Pesson 序列化結果：\n" + json);

            var person2 = JsonConvert.DeserializeObject<Person>(json);
            richTextBox1.AppendText("\nPerson 反序列化結果：\n");
            richTextBox1.AppendText($"Name: {person2.Name}");
            richTextBox1.AppendText($" City: {person2.HomeAddress.City}");
            richTextBox1.AppendText(" Hobbies: " + string.Join(", ", person2.Hobbies) + "\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var employee = new Employee
            {
                Name = "Cathy Chen",
                Age = 28,
                HireDate = new DateTime(2022, 1, 15),
                password = "qwertyuiop1234567890"
            };
            string json = JsonConvert.SerializeObject(employee, Newtonsoft.Json.Formatting.Indented);
            richTextBox1.AppendText("\nEmployee 序列化結果：\n" + json);

            var jsonInput = @"
            {
                ""full_name"": ""David Lee"",
                ""hire_date"": ""2023-03-10T00:00:00"",
                ""Age"": 35
            }";
            var emp2 = JsonConvert.DeserializeObject<Employee>(jsonInput);
            richTextBox1.AppendText("\nEmployee 反序列化結果：\n");
            richTextBox1.AppendText($"Name: {emp2.Name}");
            richTextBox1.AppendText($" HireDate: {emp2.HireDate:yyyy-MM-dd}");
            richTextBox1.AppendText($" Age: {emp2.Age}\n");
        }

        public async Task<string> CrawlerStockByDate(DateTime date)
        {
            string stockCode = textBox1.Text.Trim();
            if (stockCode == "")
            {
                stockCode = "2330";
            }
            textBox1.Text = "";
            // clear the text box after reading
            using (var client = new HttpClient())
            {
                string url = $"https://www.twse.com.tw/exchangeReport/STOCK_DAY?date={date.ToString("yyyyMMdd")}&stockNo={stockCode}";
                var response = await client.GetStringAsync(url);    
                return response;
            }
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var getResult = await CrawlerStockByDate(DateTime.Now);
                string json = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(getResult), Newtonsoft.Json.Formatting.Indented);
                richTextBox1.AppendText("\nPractice 序列化結果：\n" + json);

            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("\n發生錯誤: " + ex.Message + "\n");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ( richTextBox1.Text == "")
            {
                MessageBox.Show("文字欄位無內容");
                return;
            }
            richTextBox1.Clear();
            MessageBox.Show("內容已清除");
        }
    }
}
