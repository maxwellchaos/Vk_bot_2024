using CefSharp.DevTools.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Vk_bot
{
    public partial class friends_add : Form
    {
         public string access_token;

        public friends_add()
        {
            InitializeComponent();
        }

        private void friends_add_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Api;
            Api = "https://api.vk.com/method/users.get?" + "fields=photo_100&" + access_token + "&v=5.199";


            WebClient client = new WebClient();
            client.DownloadData(Api);
            string Anwer = Encoding.UTF8.GetString(client.DownloadData(Api));
            string ruguest = "https://api.vk.com/method/friends.getSuggestions?" +
                                      "count=100&" +
                                      access_token +
                                       "&v=5.199";

            string Answer = Encoding.UTF8.GetString(client.DownloadData(ruguest));

            Friends_getSuggestions Friends_getSuggestions = JsonConvert.DeserializeObject<Friends_getSuggestions>(Answer);
            progressBar1.Maximum = Friends_getSuggestions.response.items.Count;

            for (int index = 0; index < Friends_getSuggestions.response.items.Count; index++)
            {
               
                    for (int j = 0; j < 1; j++) 
                    {
                        button1.Enabled = false;
                        Thread.Sleep(1);
                        Application.DoEvents();
                    }
                progressBar1.Value = index;
                ListViewItem peopleItem = new ListViewItem();
                peopleItem.ImageIndex = index;// индокс аватарки пользователя
                peopleItem.SubItems.Add(Friends_getSuggestions.response.items[index].first_name);// имя пользователя
                peopleItem.SubItems.Add(Friends_getSuggestions.response.items[index].last_name);// фамилия пользователя 

                Api = "https://api.vk.com/method/friends.add?" + "user_id=" +
                        Friends_getSuggestions.response.items[index].id.ToString() +"&"+
                                       access_token +
                                       "&v=5.199";
                Answer = Encoding.UTF8.GetString(client.DownloadData(Api));
                textBox1.Text += Answer + "\r\n\r\n\r\n\r\n";
                peopleItem.Text = Friends_getSuggestions.response.items[index].id.ToString(); //айди польлователей которые можешь добавить

                listView1.Items.Add(peopleItem);

            } 
            button1.Enabled = true;
            progressBar1.Value = progressBar1.Maximum;

        }
    }
}
        
