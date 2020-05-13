using ClosedXML.Excel;
using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using Coins_Database.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Coins_Database.Views
{
    public partial class MainWindow : Window
    {
        public string _password, _login, teacher_name, file_path, teacher;
        private int id_event, id_teacher, year, semestr;
        private bool is_new_teacher, is_new_event;

        #region ViewModels
        RatingViewModel RWM = new RatingViewModel();
        TeacherCardViewModel TCVM = new TeacherCardViewModel();
        TeacherPhotoViewModel TPVM = new TeacherPhotoViewModel();
        TeachersListViewModel TLVM = new TeachersListViewModel();
        EventsViewModel EVM = new EventsViewModel();
        AboutEventViewModell AEVM = new AboutEventViewModell();
        cbEventsSortViewModel cESBTVM = new cbEventsSortViewModel();
        SortedEventsViewModel SEVM = new SortedEventsViewModel();
        AdminMessageListViewModel AMLVM = new AdminMessageListViewModel();
        CoinsListViewModel CLVM = new CoinsListViewModel();
        CoinsCountViewModel CCVM = new CoinsCountViewModel();
        GetIdViewModel GIVM = new GetIdViewModel();
        InsertImageViewModel IIVM = new InsertImageViewModel();
        AccountListViewModel ALVM = new AccountListViewModel();
        GetCoinCommentViewModel GCCVM = new GetCoinCommentViewModel();
        LastImageIdViewModel LIIVM = new LastImageIdViewModel();
        #endregion

        public MainWindow(string login, string password)
        {
            InitializeComponent();
            CreateRadioButtons();
            year = DateTime.Now.Year;
            semestr = GetSemestr();
            _login = login;
            _password = password;
            CurrentUser.Text = _login;
            if (Session.Access == Session.ACCESS.Superadmin)
            {
                adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
                cbTotal.IsSelected = true;
                RatingGrid.Visibility = Visibility.Visible;
            }
            else
            {
                WindowHeader.Children.Remove(Settings);
                WindowHeader.Children.Remove(Export);
                TCoinsList.ItemsSource = CLVM.LoadCoinsList(_login, _password, Queries.GetTCoinsList(_login, year, semestr));
                Tcoins_img.DataContext = CCVM.LoadCoinsCount(_login, _password, Queries.GetTCoinsCount(_login, year, semestr));
                teacher_coins.Visibility = Visibility.Visible;
            }
        }

        private int GetSemestr()
        {
            if (DateTime.Now.Month >=9 && DateTime.Now.Month <= 12)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public void ChangeRate()
        {
            adminTable.ItemsSource = null;
            AllRatingHidden();
        }

        #region Виды рейтинга
        private void cbTotal_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
        }

        private void cbArtcoins_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingArtcoins(year, semestr));
        }

        private void cbIntellect_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingIntellect(year, semestr));
        }

        private void cbTalents_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTalents(year, semestr));
        }

        private void cbSocialActivity_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingSocActivity(year, semestr));
        }
        #endregion

        void AllRatingHidden()
        {
            photoTeacher.Visibility = Visibility.Hidden;
            cardTName.Visibility = Visibility.Hidden;
            cardTInfo.Visibility = Visibility.Hidden;
            cardTSpeciality.Visibility = Visibility.Hidden;
            AchievementsButton.Visibility = Visibility.Hidden;
        }

        void AllRatingVisible()
        {
            photoTeacher.Visibility = Visibility.Visible;
            cardTName.Visibility = Visibility.Visible;
            cardTInfo.Visibility = Visibility.Visible;
            cardTSpeciality.Visibility = Visibility.Visible;
            AchievementsButton.Visibility = Visibility.Visible;
        }

        private void adminTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (adminTable.SelectedItems.Count >= 1)
            {
                AllRatingVisible();
                Rating RR = (Rating)adminTable.SelectedItems[0];
                cardTName.Text = RR.FIO;
                cardTSpeciality.DataContext = TCVM.LoadTeacherCard(_login, _password, Queries.GetTeacherCard(RR.FIO));
                cardTInfo.DataContext = TCVM.LoadTeacherCard(_login, _password, Queries.GetTeacherCard(RR.FIO));
                photoTeacher.Source = TPVM.LoadTeacherPhoto(_login, _password, Queries.GetTeacherPhoto(RR.FIO));
                id_teacher = RR.id_teacher;
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            coinsList.ItemsSource = CLVM.LoadCoinsList(_login, _password, Queries.GetCoinsList(cardTName.Text, year, semestr));
            BackgroundZone.Visibility = Visibility.Visible;
            night.Visibility = Visibility.Visible;
            Coins.Visibility = Visibility.Visible;
            BackPanel.Visibility = Visibility.Visible;
            coins_img.DataContext = CCVM.LoadCoinsCount(_login, _password, Queries.GetCoinsCount(cardTName.Text, year, semestr));
        }

        private void RatingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RatingGrid.Visibility = Visibility.Visible;
            EventsGrid.Visibility = Visibility.Hidden;
            MessagesGrid.Visibility = Visibility.Hidden;
            if (adminTable.SelectedItems.Count >= 1)
            {
                AllRatingVisible();
            }
            else
            {
                AchievementsButton.Visibility = Visibility.Hidden;
            }
            TeachersGrid.Visibility = Visibility.Hidden;
        }

        private void EventsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            EventsGrid.Visibility = Visibility.Visible;
            RatingGrid.Visibility = Visibility.Hidden;
            MessagesGrid.Visibility = Visibility.Hidden;
            AllRatingHidden();
            TeachersGrid.Visibility = Visibility.Hidden;
            eventsTable.ItemsSource = EVM.LoadEvents(_login, _password, Queries.GetEventsList(year, semestr));
            cbEType.Items.Clear();
            cbEType.Items.Add("Все");
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCB_EventTypes, "event_type"))
            {
                cbEType.Items.Add(item);
            }
            cbEPlace.Items.Clear();
            cbEPlace.Items.Add("Все");
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCB_EventPlaces, "event_place"))
            {
                cbEPlace.Items.Add(item);
            }
        }

        private void TeachersRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RatingGrid.Visibility = Visibility.Hidden;
            EventsGrid.Visibility = Visibility.Hidden;
            MessagesGrid.Visibility = Visibility.Hidden;
            AllRatingHidden();
            TeachersGrid.Visibility = Visibility.Visible;
            teachersTable.ItemsSource = TLVM.LoadTeachersList(_login, _password, Queries.GetTeachersList);
        }

        private void ApplicationsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                TeachersGrid.Visibility = Visibility.Hidden;
                RatingGrid.Visibility = Visibility.Hidden;
                EventsGrid.Visibility = Visibility.Hidden;
                MessagesGrid.Visibility = Visibility.Visible;
                AllRatingHidden();
                adminMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetAdminMessageList);
                AllMessages.IsChecked = true;
            }
            catch { }
        }

        public void RenewTeacherCard()
        {
            DeleteTeacher.Visibility = Visibility.Visible;
            ChangeTeacher.Visibility = Visibility.Visible;
            Loot.Visibility = Visibility.Visible;
            TeachersList TL = (TeachersList)teachersTable.SelectedItems[0];
            TCardName.Text = TL.FIO;
            TCardSpeciality.DataContext = TCVM.LoadTeacherCard(_login, _password, Queries.GetTeacherCard(TL.FIO));
            TCardInfo.DataContext = TCVM.LoadTeacherCard(_login, _password, Queries.GetTeacherCard(TL.FIO));
            TCardPhoto.Source = TPVM.LoadTeacherPhoto(_login, _password, Queries.GetTeacherPhoto(TL.FIO));
            id_teacher = TL.id_teacher;
        }

        private void teachersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teachersTable.SelectedItems.Count >= 1)
            {
                RenewTeacherCard();
                TeachersList TL = (TeachersList)teachersTable.SelectedItems[0];
                id_teacher = TL.id_teacher;
            }
        }

        private void eventsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (eventsTable.SelectedItems.Count >= 1)
            {
                DeleteEvent.Visibility = Visibility.Visible;
                ChangeEvent.Visibility = Visibility.Visible;
                Participants.Visibility = Visibility.Visible;
                Events EVE = (Events)eventsTable.SelectedItems[0];
                EventInfo.DataContext = AEVM.LoadEvents(_login, _password, Queries.GetEventInfo(EVE.caption));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BackgroundZone.Visibility = Visibility.Hidden;
            night.Visibility = Visibility.Hidden;
            Coins.Visibility = Visibility.Hidden;
            BackPanel.Visibility = Visibility.Hidden;
            achEvent.Text = "";
            achDateEvent.Text = "";
            achDescrEvent.Text = "";
            achEventPlace.Text = "";
            ratingCB.Text = "Общий рейтинг";
        }

        private void GetEventSortRules()
        {
            string type, place;
            if (cbEType.Text == "Все")
            {
                type = "";
            }
            else
            {
                type = cbEType.SelectedItem.ToString();
            }
            if (cbEPlace.Text == "Все")
            {
                place = "";
            }
            else
            {
                place = cbEPlace.SelectedItem.ToString();
            }
            eventsTable.ItemsSource = SEVM.LoadSortedEvents(_login, _password, Queries.GetSortedEvent(type, place));
        }

        private void SearchEvents_Click(object sender, RoutedEventArgs e)
        {
            GetEventSortRules();
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            adminMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetAdminMessageListSort(""));
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            adminMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetAdminMessageListSort("Не прочитано"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void coinsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (coinsList.SelectedItems.Count >= 1)
            {
                CoinsList CoL = (CoinsList)coinsList.SelectedItems[0];
                achEvent.Text = CoL.party;
                achDescrEvent.Text = GCCVM.GetCoinComment(_login, _password, CoL.id_coin);
                achDateEvent.Text = CoL.date;
                achEventPlace.Text = CoL.place;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (coinsList.SelectedItems.Count >= 1)
            {
                CoinsList CoL = (CoinsList)coinsList.SelectedItems[0];
                MessageBoxResult result = MessageBox.Show("Удалить коин?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Operations.Operations.Execute(_login, _password, Queries.DeleteCoin(CoL.id_coin));
                    coinsList.ItemsSource = CLVM.LoadCoinsList(_login, _password, Queries.GetCoinsList(cardTName.Text, year, semestr));
                    adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
                    coins_img.DataContext = CCVM.LoadCoinsCount(_login, _password, Queries.GetCoinsCount(cardTName.Text, year, semestr));
                }
            }
            else
            {
                MessageBox.Show("Сначала нужно выбрать, что удалять");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FakeEvent.Items.Clear();
            FakeComment.Text = "";
            FakePanel.Visibility = Visibility.Hidden;
            Lnight.Visibility = Visibility.Hidden;
            LPanel.Visibility = Visibility.Hidden;
            LType.Items.Clear();
        }

        private void addCoin_Click_1(object sender, RoutedEventArgs e)
        {
            FakePanel.Visibility = Visibility.Visible;
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCBCoinEvent(year, semestr), "event_name"))
            {
                FakeEvent.Items.Add(item);
            }
            FakeType.Items.Clear();
            FakeType.Items.Add("Арткоин");
            FakeType.Items.Add("Талант");
            FakeType.Items.Add("Соц. активность");
            FakeType.Items.Add("Интеллект");
        }

        private void DeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (teachersTable.SelectedItems.Count >= 1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить учителя?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Operations.Operations.Execute(_login, _password, Queries.DeleteTeacher(id_teacher));
                    teachersTable.ItemsSource = TLVM.LoadTeachersList(_login, _password, Queries.GetTeachersList);
                    TCardPhoto.Source = null;
                    TCardName.Text = "";
                    TCardInfo.DataContext = null;
                    TCardSpeciality.DataContext = null;
                }
            }
            else
            {
                MessageBox.Show("Сначала нужно выбрать, что удалять");
            }
        }

        public void ClearEditTeacher()
        {
            FakeFIO.Text = "";
            FakeSpeciality.Text = "";
            FakeInfo.Text = "";
            Portrait.Source = null;
        }

        private void ChangeTeacher_Click(object sender, RoutedEventArgs e)
        {
            FakeFIO.Text = TCardName.Text;
            FakeSpeciality.Text = TCardSpeciality.Text;
            FakeInfo.Text = TCardInfo.Text;
            Portrait.Source = TCardPhoto.Source;
            TeacherPageGrid.Visibility = Visibility.Visible;
            TeacherPageBackground.Visibility = Visibility.Visible;
            FakeTeacherPanel.Visibility = Visibility.Visible;
            AddTeacherButton.Content = "Обновить";
            is_new_teacher = false;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                Portrait.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                file_path = openFileDialog.FileName;
            }
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Portrait.Source != null)
                {
                    if (is_new_teacher == true)
                    {
                        IIVM.InsertImage(_login, _password, file_path);
                        Operations.Operations.Execute(_login, _password, Queries.AddTeacher(FakeFIO.Text, FakeSpeciality.Text,
                            FakeInfo.Text, LIIVM.LastImageID(_login, _password)));
                        TCardPhoto.Source = null;
                        TCardName.Text = "";
                        TCardInfo.DataContext = null;
                        TCardSpeciality.DataContext = null;
                    }
                    else
                    {
                        IIVM.InsertImage(_login, _password, file_path);
                        Operations.Operations.Execute(_login, _password, Queries.UpdateTeacher(FakeFIO.Text, FakeSpeciality.Text,
                            FakeInfo.Text, LIIVM.LastImageID(_login, _password), id_teacher));
                        TCardName.Text = FakeFIO.Text;
                        TCardSpeciality.DataContext = TCVM.LoadTeacherCard(_login, _password, Queries.GetTeacherCard(FakeFIO.Text));
                        TCardInfo.DataContext = TCVM.LoadTeacherCard(_login, _password, Queries.GetTeacherCard(FakeFIO.Text));
                        TCardPhoto.Source = TPVM.LoadTeacherPhoto(_login, _password, Queries.GetTeacherPhoto(FakeFIO.Text));

                    }
                    teachersTable.ItemsSource = TLVM.LoadTeachersList(_login, _password, Queries.GetTeachersList);
                    TeacherPageGrid.Visibility = Visibility.Hidden;
                    TeacherPageBackground.Visibility = Visibility.Hidden;
                    FakeTeacherPanel.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Нужно выбрать фото");
                }
            }
            catch
            {
                MessageBox.Show("Нужно выбрать/обновить фото (при обновлении можно выбрать то же самое)");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Portrait.Source = null;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            TeacherPageGrid.Visibility = Visibility.Hidden;
            FakeTeacherPanel.Visibility = Visibility.Hidden;
            ClearEditTeacher();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            TeacherPageGrid.Visibility = Visibility.Visible;
            TeacherPageBackground.Visibility = Visibility.Visible;
            FakeTeacherPanel.Visibility = Visibility.Visible;
            AddTeacherButton.Content = "Добавить";
            is_new_teacher = true;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            is_new_event = true;
            FakeEventsGrig.Visibility = Visibility.Visible;
            EventParticipants.Visibility = Visibility.Hidden;
            FakeEventPanel.Visibility = Visibility.Visible;
            FakeTypeEvent.Items.Clear();
            FakeTypeEvent.Items.Add("Все типы");
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCB_EventTypes, "event_type"))
            {
                FakeTypeEvent.Items.Add(item);
            }
            AddEvent.Content = "Добавить";
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            FakeEventsGrig.Visibility = Visibility.Hidden;
            EventParticipants.Visibility = Visibility.Hidden;
        }

        private void Participants_Click(object sender, RoutedEventArgs e)
        {
            FakeEventsGrig.Visibility = Visibility.Visible;
            FakeEventPanel.Visibility = Visibility.Hidden;
            EventParticipants.Visibility = Visibility.Visible;
            Events EvE = (Events)eventsTable.SelectedItems[0];
            PartTeachers.ItemsSource = TLVM.LoadTeachersList(_login, _password,
                Queries.GetParticipants(GIVM.LoadID(_login, _password, Queries.GetEventID(EvE.caption), "id_event")));
        }

        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Удалить мероприятие? Все коины, связанные с ним, исчезнут", "Удалить", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Events EvE = (Events)eventsTable.SelectedItems[0];
                Operations.Operations.Execute(
                    _login, _password, Queries.DeleteEvent(GIVM.LoadID(_login, _password, Queries.GetEventID(EvE.caption), "id_event")));
                eventsTable.ItemsSource = EVM.LoadEvents(_login, _password, Queries.GetEventsList(year, semestr));
                EventInfo.DataContext = null;
            }
            else
            {

            }
            cbEPlace.Items.Clear();
            cbEPlace.Items.Add("Все");
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCB_EventPlaces, "event_place"))
            {
                cbEPlace.Items.Add(item);
            }
        }

        private void ChangeEvent_Click(object sender, RoutedEventArgs e)
        {
            FakeTypeEvent.Items.Clear();
            FakeTypeEvent.Items.Add("Все типы");
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCB_EventTypes, "event_type"))
            {
                FakeTypeEvent.Items.Add(item);
            }
            FakeEventsGrig.Visibility = Visibility.Visible;
            FakeEventPanel.Visibility = Visibility.Visible;
            EventParticipants.Visibility = Visibility.Hidden;
            Events EvE = (Events)eventsTable.SelectedItems[0];
            FakeCalendar.SelectedDate = Convert.ToDateTime(EvE.date);
            FakeCaption.Text = EvE.caption;
            FakePlace.Text = EvE.place;
            FakeDescription.Text = EventInfo.Text;
            FakeTypeEvent.SelectedItem = EvE.type;
            AddEvent.Content = "Изменить";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            adminMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetAdminMessageListSort("Принято"));
        }

        private void adminMessageTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AdminMessageList AML = (AdminMessageList)adminMessageTable.SelectedItems[0];
                if (adminMessageTable.SelectedItems.Count >= 1)
                {
                    if (AML.status == "Не прочитано")
                    {
                        Accept.Visibility = Visibility.Visible;
                        Decline.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Accept.Visibility = Visibility.Hidden;
                        Decline.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    Accept.Visibility = Visibility.Hidden;
                    Decline.Visibility = Visibility.Hidden;
                }
            }
            catch
            {

            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdminMessageList AML = (AdminMessageList)adminMessageTable.SelectedItems[0];
                Operations.Operations.Execute(_login, _password, Queries.UpdateMessageStatus("Отклонено", AML.id_message));
                adminMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetAdminMessageListSort(""));
            }
            catch
            {
                MessageBox.Show("Нужно выбрать заявку");
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdminMessageList AML = (AdminMessageList)adminMessageTable.SelectedItems[0];
                TeacherPageBackground.Visibility = Visibility.Visible;
                ResponceBlock.Visibility = Visibility.Visible;
                REvent.Text = AML._event;
                teacher = AML.teacher;
                RType.Items.Clear();
                RType.Items.Add("Арткоин");
                RType.Items.Add("Талант");
                RType.Items.Add("Соц. активность");
                RType.Items.Add("Интеллект");
            }
            catch
            {
                MessageBox.Show("Нужно выбрать заявку");
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (RType.Text == "")
            {
                MessageBox.Show("Нужно заполнить все поля");
            }
            else
            {
                int id_coin_type = 0;
                if (RType.Text == "Арткоин")
                {
                    id_coin_type = 0;
                }
                else
                {
                    if (RType.Text == "Талант")
                    {
                        id_coin_type = 1;
                    }
                    else
                    {
                        if (RType.Text == "Соц. активность")
                        {
                            id_coin_type = 2;
                        }
                        else
                        {
                            if (RType.Text == "Интеллект")
                            {
                                id_coin_type = 3;
                            }
                        }
                    }
                }
                AdminMessageList AML = (AdminMessageList)adminMessageTable.SelectedItems[0];
                id_event = GIVM.LoadID(_login, _password, Queries.GetEventID(REvent.Text), "id_event");
                id_teacher = GIVM.LoadID(_login, _password, Queries.GetTeacherID(1, teacher), "id_teacher");
                Operations.Operations.Execute(_login, _password, Queries.AddCoin(id_event, id_teacher, id_coin_type, RComment.Text));
                RComment.Text = "";
                Operations.Operations.Execute(_login, _password, Queries.UpdateMessageStatus("Принято", AML.id_message));
                adminMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetAdminMessageListSort(""));
                TeacherPageBackground.Visibility = Visibility.Hidden;
                ResponceBlock.Visibility = Visibility.Hidden;
                adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            AccountList.ItemsSource = ALVM.LoadAccounts(_login, _password, Queries.GetAccounts);
            SettingsGrid.Visibility = Visibility.Visible;
            SettingsPanel.Visibility = Visibility.Visible;
            currentYear.Text = year.ToString();
            if (semestr == 1)
            {
                CS1.IsChecked = true;
                CS2.IsChecked = false;
            }
            else
            {
                CS1.IsChecked = false;
                CS2.IsChecked = true;
            }
        }

        private void AddfdEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account ACC = (Account)AccountList.SelectedItems[0];
                if (ALogin.Text == "" || APassword.Text == "" || (ALogin.Text == "" && APassword.Text == ""))
                {
                    MessageBox.Show("Нужно заполнить все поля");
                }
                else
                {
                    Operations.Operations.Execute(_login, _password, Queries.NewAccount(ALogin.Text, APassword.Text, ACC.ac_id));
                    SnackbarOne.IsActive = true;
                    AccountList.ItemsSource = ALVM.LoadAccounts(_login, _password, Queries.GetAccounts);
                    ALogin.IsEnabled = false;
                    APassword.IsEnabled = false;
                    AddAccount.IsEnabled = false;
                    DeleteAccount.IsEnabled = true;
                }
            }
            catch 
            {
                MessageBox.Show("Нужно выбрать учителя");
            }
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarOne.IsActive = false;
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Hidden;
            SettingsPanel.Visibility = Visibility.Hidden;
            year = Convert.ToInt32(currentYear.Text);
            if (CS1.IsChecked == true)
            {
                semestr = 1;
            }
            else
            {
                semestr = 2;
            }
            cbTotal.IsSelected = true;
            adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
            eventsTable.ItemsSource = EVM.LoadEvents(_login, _password, Queries.GetEventsList(year, semestr));
        }

        private void AccountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Account ACC = (Account)AccountList.SelectedItems[0];
                if (AccountList.SelectedItems.Count >= 1)
                {
                    DeleteAccount.Visibility = Visibility.Visible;
                    AddAccount.Visibility = Visibility.Visible;
                    ALogin.Visibility = Visibility.Visible;
                    APassword.Visibility = Visibility.Visible;
                    if (ACC.ac_login.Length >= 1)
                    {
                        ALogin.Text = ACC.ac_login;
                        ALogin.IsEnabled = false;
                        APassword.Text = "********";
                        APassword.IsEnabled = false;
                        AddAccount.IsEnabled = false;
                        DeleteAccount.IsEnabled = true;
                    }
                    else
                    {
                        ALogin.Text = "";
                        APassword.Text = "";
                        ALogin.IsEnabled = true;
                        APassword.IsEnabled = true;
                        AddAccount.IsEnabled = true;
                        DeleteAccount.IsEnabled = false;
                    }
                }
            }
            catch { }
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Удалить аккаунт?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Operations.Operations.Execute(_login, _password, Queries.DeleteAccount(ALogin.Text, ALogin.Text));
                    SnackbarOne.IsActive = true;
                    AccountList.ItemsSource = ALVM.LoadAccounts(_login, _password, Queries.GetAccounts);
                    ALogin.Text = "";
                    APassword.Text = "";
                    ALogin.IsEnabled = true;
                    APassword.IsEnabled = true;
                    AddAccount.IsEnabled = true;
                    DeleteAccount.IsEnabled = false;
                }
                else { }
            }
            catch { }
        }

        private void TCoinsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TCoinsList.SelectedItems.Count >= 1)
            {
                CoinsList CoL = (CoinsList)TCoinsList.SelectedItems[0];
                TachEvent.Text = CoL.party;
                TachDescrEvent.Text = GCCVM.GetCoinComment(_login, _password, CoL.id_coin);
                TachDateEvent.Text = CoL.date;
                TachEventPlace.Text = CoL.place;
            }
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestEvent.SelectedItem != null)
            {
                Operations.Operations.Execute(_login, _password, Queries.AddMessage(GIVM.LoadID(_login, _password, Queries.GetTeacherID(2, _login), "id_teacher"),
                    GIVM.LoadID(_login, _password, Queries.GetEventID(RequestEvent.SelectedItem.ToString()), "id_event"), DateTime.Now));
                teacherMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetTeacherMessageList(_login, year, semestr));
                Snackbar2.IsActive = true;
            }
            else
            {
                MessageBox.Show("Нужно выбрать мероприятие");
            }
        }

        private void SnackbarMessage_ActionClick_1(object sender, RoutedEventArgs e)
        {
            Snackbar2.IsActive = false;
        }

        private void TotalRating_Click(object sender, RoutedEventArgs e)
        {
            TBackgroundZone.Visibility = Visibility.Visible;
            TadminTable.ItemsSource = null;
            TratingCB.Text = "Общий рейтинг";
            TadminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            TeacherPageBackground.Visibility = Visibility.Hidden;
            ResponceBlock.Visibility = Visibility.Hidden;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            FakeEventsGrig.Visibility = Visibility.Hidden;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (FakeCalendar.SelectedDate != null)
            {
                if (FakeTypeEvent.SelectedItem != null)
                {
                    if (FakeCaption.Text == "" || FakePlace.Text == "" || (FakeCaption.Text == "" && FakePlace.Text == ""))
                    {
                        MessageBox.Show("Нужно указать данные о мероприятии");
                    }
                    else
                    {
                        if (is_new_event == true)
                        {
                            Operations.Operations.Execute(_login, _password, Queries.AddEvent(FakeCaption.Text, FakePlace.Text, FakeDescription.Text,
                                GIVM.LoadID(_login, _password, Queries.GetEventTypeID(FakeTypeEvent.SelectedItem.ToString()), "id"), FakeCalendar.SelectedDate.Value));
                        }
                        else
                        {
                            Events EvE = (Events)eventsTable.SelectedItems[0];
                            Operations.Operations.Execute(_login, _password, Queries.UpdateEvent(FakeCaption.Text, FakePlace.Text, FakeDescription.Text,
                                GIVM.LoadID(_login, _password, Queries.GetEventTypeID(FakeTypeEvent.SelectedItem.ToString()), "id"), FakeCalendar.SelectedDate.Value,
                                GIVM.LoadID(_login, _password, Queries.GetEventID(EvE.caption), "id_event")));
                        }
                        FakeEventsGrig.Visibility = Visibility.Hidden;
                        FakeCaption.Text = "";
                        FakePlace.Text = "";
                        FakeTypeEvent.SelectedItem = null;
                        eventsTable.ItemsSource = EVM.LoadEvents(_login, _password, Queries.GetEventsList(year, semestr));
                        EventInfo.Text = FakeDescription.Text;
                        FakeDescription.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Нужно указать тип мероприятия");
                }
            }
            else
            {
                MessageBox.Show("Нужно указать дату мероприятия");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (FakeEvent.Text == "" || FakeType.Text == "" || (FakeEvent.Text == "" && FakeType.Text == ""))
            {
                MessageBox.Show("Нужно заполнить все поля");
            }
            else
            {
                int id_coin_type = 0;
                if (FakeType.Text == "Арткоин")
                {
                    id_coin_type = 0;
                }
                else
                {
                    if (FakeType.Text == "Талант")
                    {
                        id_coin_type = 1;
                    }
                    else
                    {
                        if (FakeType.Text == "Соц. активность")
                        {
                            id_coin_type = 2;
                        }
                        else
                        {
                            if (FakeType.Text == "Интеллект")
                            {
                                id_coin_type = 3;
                            }
                        }
                    }
                }
                id_event = GIVM.LoadID(_login, _password, Queries.GetEventID(FakeEvent.Text), "id_event");
                Operations.Operations.Execute(_login, _password, Queries.AddCoin(id_event, id_teacher, id_coin_type, FakeComment.Text));
                coinsList.ItemsSource = CLVM.LoadCoinsList(_login, _password, Queries.GetCoinsList(cardTName.Text, year, semestr));
                adminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
                coins_img.DataContext = CCVM.LoadCoinsCount(_login, _password, Queries.GetCoinsCount(cardTName.Text, year, semestr));
                FakeEvent.Items.Clear();
                FakeComment.Text = "";
                FakePanel.Visibility = Visibility.Hidden;
            }
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            adminMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetAdminMessageListSort("Отклонено"));
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            TBackgroundZone.Visibility = Visibility.Hidden;
        }

        private void Loot_Click(object sender, RoutedEventArgs e)
        {
            Lnight.Visibility = Visibility.Visible;
            LPanel.Visibility = Visibility.Visible;
            LEvent.Items.Clear();
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCBCoinEvent(year, semestr), "event_name"))
            {
                LEvent.Items.Add(item);
            }
            LType.Items.Add("Арткоин");
            LType.Items.Add("Талант");
            LType.Items.Add("Соц. активность");
            LType.Items.Add("Интеллект");
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            if (LEvent.Text == "" || LType.Text == "" || (LEvent.Text == "" && LType.Text == ""))
            {
                MessageBox.Show("Нужно заполнить все поля");
            }
            else
            {
                int id_coin_type = 0;
                if (LType.Text == "Арткоин")
                {
                    id_coin_type = 0;
                }
                else
                {
                    if (LType.Text == "Талант")
                    {
                        id_coin_type = 1;
                    }
                    else
                    {
                        if (LType.Text == "Соц. активность")
                        {
                            id_coin_type = 2;
                        }
                        else
                        {
                            if (LType.Text == "Интеллект")
                            {
                                id_coin_type = 3;
                            }
                        }
                    }
                }
                id_event = GIVM.LoadID(_login, _password, Queries.GetEventID(LEvent.Text), "id_event");
                Operations.Operations.Execute(_login, _password, Queries.AddCoin(id_event, id_teacher, id_coin_type, LComment.Text));
                LEvent.Items.Clear();
                LComment.Text = "";
                LPanel.Visibility = Visibility.Hidden;
                Lnight.Visibility = Visibility.Hidden;
                LPanel.Visibility = Visibility.Hidden;
                LType.Items.Clear();
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ExportGrid.Visibility = Visibility.Visible;
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            ExportGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            var workbook = new XLWorkbook();
            if(ExpEvents.IsChecked == true)
            {
                Excel.EventsReport(workbook, "Мероприятия", EVM.LoadEvents(_login, _password, Queries.GetEventsList(year, semestr)));
            }
            if (ExpTotal.IsChecked == true)
            {
                Excel.RatingReport(workbook, "Общий рейтинг", RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr)));
            }
            if (ExpTalents.IsChecked == true)
            {
                Excel.RatingReport(workbook, "Таланты", RWM.LoadRating(_login, _password, Queries.GetRatingTalents(year, semestr)));
            }
            if (ExpIntellect.IsChecked == true)
            {
                Excel.RatingReport(workbook, "Интеллект", RWM.LoadRating(_login, _password, Queries.GetRatingIntellect(year, semestr)));
            }
            if (ExpArtcoins.IsChecked == true)
            {
                Excel.RatingReport(workbook, "Арткоины", RWM.LoadRating(_login, _password, Queries.GetRatingArtcoins(year, semestr)));
            }
            if (ExpSocActivity.IsChecked == true)
            {
                Excel.RatingReport(workbook, "Соц. активность", RWM.LoadRating(_login, _password, Queries.GetRatingSocActivity(year, semestr)));
            }
            if (Excel.SaveReport(workbook) == true)
            {
                ExportGrid.Visibility = Visibility.Hidden;
            }
        }

        #region Учительский общий рейтинг
        private void TcbTotal_Selected(object sender, RoutedEventArgs e)
        {
            TadminTable.ItemsSource = null;
            TadminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTotal(year, semestr));
        }

        private void TcbArtcoins_Selected(object sender, RoutedEventArgs e)
        {
            TadminTable.ItemsSource = null;
            TadminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingArtcoins(year, semestr));
        }

        private void TcbIntellect_Selected(object sender, RoutedEventArgs e)
        {
            TadminTable.ItemsSource = null;
            TadminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingIntellect(year, semestr));
        }

        private void TcbTalents_Selected(object sender, RoutedEventArgs e)
        {
            TadminTable.ItemsSource = null;
            TadminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingTalents(year, semestr));
        }

        private void TcbSocialActivity_Selected(object sender, RoutedEventArgs e)
        {
            TadminTable.ItemsSource = null;
            TadminTable.ItemsSource = RWM.LoadRating(_login, _password, Queries.GetRatingSocActivity(year, semestr));
        }
        #endregion

        public void CreateRadioButtons()
        {
            if (Session.Access == Session.ACCESS.Superadmin)
            {
                RadioButton rate = new RadioButton
                {
                    IsChecked = true,
                    Content = "Рейтинг",
                    Width = 200,
                    Cursor = Cursors.Hand,
                };
                rate.Checked += RatingRadioButton_Checked;
                rButtons.Children.Add(rate);
                RadioButton teachers = new RadioButton
                {
                    IsChecked = false,
                    Content = "Учителя",
                    Width = 200,
                    Cursor = Cursors.Hand
                };
                teachers.Checked += TeachersRadioButton_Checked;
                rButtons.Children.Add(teachers);
                RadioButton events = new RadioButton
                {
                    IsChecked = false,
                    Content = "Мероприятия",
                    Width = 200,
                    Cursor = Cursors.Hand
                };
                events.Checked += EventsRadioButton_Checked;
                rButtons.Children.Add(events);
                RadioButton applicanions = new RadioButton
                {
                    IsChecked = false,
                    Content = "Заявки",
                    Width = 200,
                    Cursor = Cursors.Hand
                };
                applicanions.Checked += ApplicationsRadioButton_Checked;
                rButtons.Children.Add(applicanions);
            }
            else
            {
                RadioButton teacher_coins = new RadioButton
                {
                    IsChecked = true,
                    Content = "Мои награды",
                    Width = 400,
                    Cursor = Cursors.Hand,
                };
                teacher_coins.Checked += TCoinsRadioButton_Checked;
                rButtons.Children.Add(teacher_coins);
                RadioButton teacher_requests = new RadioButton
                {
                    IsChecked = false,
                    Content = "Мои заявки",
                    Width = 400,
                    Cursor = Cursors.Hand,
                };
                teacher_requests.Checked += TRequestsRadioButton_Checked;
                rButtons.Children.Add(teacher_requests);
            }
}

        private void TCoinsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            teacher_coins.Visibility = Visibility.Visible;
            teachers_mBox.Visibility = Visibility.Hidden;
        }

        private void TRequestsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            teacher_coins.Visibility = Visibility.Hidden;
            teachers_mBox.Visibility = Visibility.Visible;
            teacherMessageTable.ItemsSource = AMLVM.LoadMAList(_login, _password, Queries.GetTeacherMessageList(_login, year, semestr));
            RequestFIO.DataContext = AMLVM.LoadMAList(_login, _password, Queries.GetTeacherMessageList(_login, year, semestr));
            RequestEvent.Items.Clear();
            foreach (string item in cESBTVM.LoadTypes(_login, _password, Queries.GetCBCoinEvent(year, semestr), "event_name"))
            {
                RequestEvent.Items.Add(item);
            }
        }
    }
}
