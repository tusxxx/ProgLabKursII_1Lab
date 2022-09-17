using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProgLabKursII
{
    public partial class Page2 : Page
    {
        private const int MAX_LIST_LENGTH = 13;

        public Page2()
        {
            InitializeComponent();
        }

        private void GenerateButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var userInfo in GetUserInfoList(10))
            {
                if (UserInfosList.Items.Count == MAX_LIST_LENGTH) return;
                UserInfosList.Items.Add(userInfo);
            }
        }

        private List<UserInfo> GetUserInfoList(int count)
        {
            var userInfos = new List<UserInfo>();
            for (int time = 0; time < count; ++time)
            {
                userInfos.Add(new UserInfo($"{time}", $"00{time}{time}", $"00{time}{time}"));
            }

            return userInfos;
        }

        private void AddUserInfoManualClick(object sender, RoutedEventArgs e)
        {
            var phoneText = PhoneText.Text;
            var phoneSetupText = PhoneSetupText.Text;
            var surnameText = SurnameText.Text;

            if (IsValidLength(phoneText, phoneSetupText)) return;
            if (IsEmptySurname(surnameText)) return;
            if (IsMaxLengthCapped()) return;
            if (!IsValid(phoneText) || !IsValid(phoneSetupText))
            {
                MessageBox.Show("Not valid Phone or Phone Setup Year.");
                return;
            }

            var userInfo = new UserInfo(
                SurnameText.Text,
                PhoneText.Text,
                PhoneSetupText.Text
            );

            UserInfosList.Items.Add(userInfo);
        }

        private bool IsMaxLengthCapped()
        {
            if (UserInfosList.Items.Count == MAX_LIST_LENGTH)
            {
                MessageBox.Show("Out of list length");
                return true;
            }

            return false;
        }

        private static bool IsEmptySurname(string surnameText)
        {
            if (surnameText == "")
            {
                MessageBox.Show("Empty surname.");
                return true;
            }

            return false;
        }

        private static bool IsValidLength(string phoneText, string phoneSetupText)
        {
            if (phoneText.Length < 4 || phoneSetupText.Length < 4)
            {
                MessageBox.Show("Phone or PhoneSetupYear have less than 4 chars.");
                return true;
            }

            return false;
        }

        private void RemoveUserInfoManualClick(object sender, RoutedEventArgs e)
        {
            if (!UserInfosList.Items.IsEmpty)
            {
                UserInfosList.Items.RemoveAt(UserInfosList.Items.Count - 1);
            }
        }

        private bool IsValid(string str)
        {
            if (str.Length != 4) return false;
            return str[0] == str[1] && str[2] == str[3];
        }

        private void GetResultClick(object sender, RoutedEventArgs e)
        {
            var userBufferList = UserInfosList.Items;
            var resultList = new List<UserInfo>();
            
            foreach (UserInfo item in userBufferList)
            {
                if (item.Phone[2] == item.PhoneSetupYear[2] && item.Phone[3] == item.PhoneSetupYear[3])
                {
                    resultList.Add(item);
                    MessageBox.Show(item.ToString());
                }
            }

            var json = JsonSerializer.Serialize(resultList);
            File.WriteAllText(@"C:\jsonList.json", json);
        }

        private void GetJsonClick(object sender, RoutedEventArgs e)
        { 
            var jsonText = File.ReadAllText(@"C:\jsonList.json");
            var results = JsonConvert.DeserializeObject<List<UserInfo>>(jsonText);
            UserInfosList.Items.Clear();
            foreach (var userInfo in results)
            {
                UserInfosList.Items.Add(userInfo);
            }
        }
    }

    internal class UserInfo
    {
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string PhoneSetupYear { get; set; }

        public UserInfo(string surname, string phone, string phoneSetupYear)
        {
            Surname = surname;
            Phone = phone;
            PhoneSetupYear = phoneSetupYear;
        }

        public override string ToString()
        {
            return $"UserInfo({Surname},{Phone},{PhoneSetupYear})";
        }
    }
}