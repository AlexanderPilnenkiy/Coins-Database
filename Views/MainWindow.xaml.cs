using ClosedXML.Excel;
using Coins_Database.Actions;
using Coins_Database.Controls;
using Coins_Database.DataAccessLayer;
using Coins_Database.Operations;
using Coins_Database.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Coins_Database.Views
{
    public partial class MainWindow : Window
    {
        public string sPassword, sLogin, sTeacherName, sFilePath, sTeacher;
        private int _idEvent, _idTeacher, _iYear, _iSemestr, _idCoinType;
        private bool _bIsNewTeacher, _bIsNewEvent;

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
        DetermineCoinType DCT = new DetermineCoinType();
        AddCoinTypes ACT = new AddCoinTypes();
        #endregion

        public MainWindow(string Login, string Password)
        {
            InitializeComponent();
            CreateMainMenu.CreateRadioButtons(stackPanelMainMenu, this);
            _iYear = DateTime.Now.Year;
            _iSemestr = GetSemestr();
            CurrentUser.Text = Login;
            if (Session.Access == Session.ACCESS.Superadmin)
            {
                listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
                cbItemTotalRating.IsSelected = true;
                gridRating.Visibility = Visibility.Visible;
            }
            else
            {
                WindowHeader.Children.Remove(Settings);
                WindowHeader.Children.Remove(Export);
                lViewCoinsList.ItemsSource = CLVM.LoadCoinsList(Connection.Established, Queries.GetViewCoinsList(sLogin, _iYear, _iSemestr));
                gridCoinsImages.DataContext = CCVM.LoadCoinsCount(Connection.Established, Queries.GetTCoinsCount(sLogin, _iYear, _iSemestr));
                gridTeachersCoins.Visibility = Visibility.Visible;
            }
        }

        private int GetSemestr()
        {
            if (DateTime.Now.Month >= 9 && DateTime.Now.Month <= 12)
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
            listViewRating.ItemsSource = null;
            AllRatingHidden();
        }

        #region Виды рейтинга
        private void cbItemTotalRating_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
        }

        private void cbartcoins_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
        }

        private void cbIntellect_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingIntellect(_iYear, _iSemestr));
        }

        private void cbTalents_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTalents(_iYear, _iSemestr));
        }

        private void cbSocialActivity_Selected(object sender, RoutedEventArgs e)
        {
            ChangeRate();
            listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingSocActivity(_iYear, _iSemestr));
        }
        #endregion

        void AllRatingHidden()
        {
            imageTeachersPhoto.Visibility = Visibility.Hidden;
            textBlockTeachersName.Visibility = Visibility.Hidden;
            textBlockTeachersInfo.Visibility = Visibility.Hidden;
            textBlockTeachersSpeciality.Visibility = Visibility.Hidden;
            buttonTeachersAchievements.Visibility = Visibility.Hidden;
        }

        void AllRatingVisible()
        {
            imageTeachersPhoto.Visibility = Visibility.Visible;
            textBlockTeachersName.Visibility = Visibility.Visible;
            textBlockTeachersInfo.Visibility = Visibility.Visible;
            textBlockTeachersSpeciality.Visibility = Visibility.Visible;
            buttonTeachersAchievements.Visibility = Visibility.Visible;
        }

        private void listViewRating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewRating.SelectedItems.Count >= 1)
            {
                AllRatingVisible();
                Rating RR = (Rating)listViewRating.SelectedItems[0];
                textBlockTeachersName.Text = RR.FIO;
                textBlockTeachersSpeciality.DataContext = TCVM.LoadTeacherCard(Connection.Established, Queries.GetTeacherCard(RR.FIO));
                textBlockTeachersInfo.DataContext = TCVM.LoadTeacherCard(Connection.Established, Queries.GetTeacherCard(RR.FIO));
                imageTeachersPhoto.Source = TPVM.LoadTeacherPhoto(Connection.Established, Queries.GetTeacherPhoto(RR.FIO));
                _idTeacher = RR.ID;
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            listViewCoinsList.ItemsSource = CLVM.LoadCoinsList(Connection.Established, Queries.GetCoinsList(textBlockTeachersName.Text, _iYear, _iSemestr));
            gridBackgroundZone.Visibility = Visibility.Visible;
            colorZoneNight.Visibility = Visibility.Visible;
            gridCoins.Visibility = Visibility.Visible;
            gridBackPanel.Visibility = Visibility.Visible;
            gridCoinsImg.DataContext = CCVM.LoadCoinsCount(Connection.Established, Queries.GetCoinsCount(textBlockTeachersName.Text, _iYear, _iSemestr));
        }

        public void RatingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            gridRating.Visibility = Visibility.Visible;
            gridEvents.Visibility = Visibility.Hidden;
            gridMessages.Visibility = Visibility.Hidden;
            if (listViewRating.SelectedItems.Count >= 1)
            {
                AllRatingVisible();
            }
            else
            {
                buttonTeachersAchievements.Visibility = Visibility.Hidden;
            }
            gridTeachers.Visibility = Visibility.Hidden;
        }

        public void EventsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            gridEvents.Visibility = Visibility.Visible;
            gridRating.Visibility = Visibility.Hidden;
            gridMessages.Visibility = Visibility.Hidden;
            AllRatingHidden();
            gridTeachers.Visibility = Visibility.Hidden;
            listViewEvents.ItemsSource = EVM.LoadEvents(Connection.Established, Queries.GetEventsList(_iYear, _iSemestr));
            cbEventsType.Items.Clear();
            cbEventsType.Items.Add("Все");
            foreach (string item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCB_EventTypes, "event_type"))
            {
                cbEventsType.Items.Add(item);
            }
            cbEventsPlaces.Items.Clear();
            cbEventsPlaces.Items.Add("Все");
            foreach (string item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCB_EventPlaces, "event_place"))
            {
                cbEventsPlaces.Items.Add(item);
            }
        }

        public void TeachersRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            gridRating.Visibility = Visibility.Hidden;
            gridEvents.Visibility = Visibility.Hidden;
            gridMessages.Visibility = Visibility.Hidden;
            AllRatingHidden();
            gridTeachers.Visibility = Visibility.Visible;
            listViewTeachers.ItemsSource = TLVM.LoadTeachersList(Connection.Established, Queries.GetTeachersList);
        }

        public void ApplicationsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                gridTeachers.Visibility = Visibility.Hidden;
                gridRating.Visibility = Visibility.Hidden;
                gridEvents.Visibility = Visibility.Hidden;
                gridMessages.Visibility = Visibility.Visible;
                AllRatingHidden();
                listViewAdminMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetAdminMessageList);
                rbAllMessages.IsChecked = true;
            }
            catch { }
        }

        public void RenewTeacherCard()
        {
            buttonDeleteTeacher.Visibility = Visibility.Visible;
            buttonChangeTeacher.Visibility = Visibility.Visible;
            buttonGrantCoin.Visibility = Visibility.Visible;
            TeachersList TL = (TeachersList)listViewTeachers.SelectedItems[0];
            textBlockTeachersCardName.Text = TL.FIO;
            textBlockTeachersCardSpeciality.DataContext = TCVM.LoadTeacherCard(Connection.Established, Queries.GetTeacherCard(TL.FIO));
            textBlockTeachersCardInfo.DataContext = TCVM.LoadTeacherCard(Connection.Established, Queries.GetTeacherCard(TL.FIO));
            imageTeachersCardPhoto.Source = TPVM.LoadTeacherPhoto(Connection.Established, Queries.GetTeacherPhoto(TL.FIO));
            _idTeacher = TL.ID;
        }

        private void listViewTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewTeachers.SelectedItems.Count >= 1)
            {
                RenewTeacherCard();
                TeachersList TL = (TeachersList)listViewTeachers.SelectedItems[0];
                _idTeacher = TL.ID;
            }
        }

        private void listViewEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewEvents.SelectedItems.Count >= 1)
            {
                buttonDeleteEvent.Visibility = Visibility.Visible;
                buttonChangeEvent.Visibility = Visibility.Visible;
                buttonParticipants.Visibility = Visibility.Visible;
                Events EVE = (Events)listViewEvents.SelectedItems[0];
                textBlockEventInfo.Text = AEVM.LoadEvents(Connection.Established, Queries.GetEventInfo(EVE.Caption))[0].Description;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridBackgroundZone.Visibility = Visibility.Hidden;
            colorZoneNight.Visibility = Visibility.Hidden;
            gridCoins.Visibility = Visibility.Hidden;
            gridBackPanel.Visibility = Visibility.Hidden;
            textBlockCoinsEvent.Text = "";
            textBlockCoinsDate.Text = "";
            textBlockCoinsDescription.Text = "";
            textBlockCoinsPlace.Text = "";
            cbRating.Text = "Общий рейтинг";
        }

        private void GetEventSortRules()
        {
            string Type, Place;
            if (cbEventsType.Text == "Все" || cbEventsType.Text == "")
            {
                Type = "";
            }
            else
            {
                Type = cbEventsType.SelectedItem.ToString();
            }
            if (cbEventsPlaces.Text == "Все" || cbEventsPlaces.Text == "")
            {
                Place = "";
            }
            else
            {
                Place = cbEventsPlaces.SelectedItem.ToString();
            }
            listViewEvents.ItemsSource = SEVM.LoadSortedEvents(Connection.Established, Queries.GetSortedEvent(Type, Place));
        }

        private void SearchEvents_Click(object sender, RoutedEventArgs e)
        {
            GetEventSortRules();
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            listViewAdminMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetAdminMessageListSort(""));
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            listViewAdminMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetAdminMessageListSort("Не прочитано"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Connection.Disconnect();
            Application.Current.Shutdown();
        }

        private void listViewCoinsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewCoinsList.SelectedItems.Count >= 1)
            {
                CoinsList CoL = (CoinsList)listViewCoinsList.SelectedItems[0];
                textBlockCoinsEvent.Text = CoL.Party;
                textBlockCoinsDescription.Text = GCCVM.GetCoinComment(Connection.Established, CoL.IDCoin);
                textBlockCoinsDate.Text = CoL.Date;
                textBlockCoinsPlace.Text = CoL.Place;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listViewCoinsList.SelectedItems.Count >= 1)
            {
                CoinsList CoL = (CoinsList)listViewCoinsList.SelectedItems[0];
                MessageBoxResult Result = MessageBox.Show("Удалить коин?", "Удалить", MessageBoxButton.YesNo);

                if (Result == MessageBoxResult.Yes)
                {
                    QueriesManager.Execute(Connection.Established, Queries.DeleteCoin(CoL.IDCoin));
                    listViewCoinsList.ItemsSource = CLVM.LoadCoinsList(Connection.Established, Queries.GetCoinsList(textBlockTeachersName.Text, _iYear, _iSemestr));
                    listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
                    gridCoinsImg.DataContext = CCVM.LoadCoinsCount(Connection.Established, Queries.GetCoinsCount(textBlockTeachersName.Text, _iYear, _iSemestr));
                }
            }
            else
            {
                MessageBox.Show("Сначала нужно выбрать, что удалять");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            cbEvents.Items.Clear();
            textBoxComment.Text = "";
            stackPanelNewCoin.Visibility = Visibility.Hidden;
            colorZoneNewNight.Visibility = Visibility.Hidden;
            stackPanelNewCoinPanel.Visibility = Visibility.Hidden;
            cbAddCoinType.Items.Clear();
        }

        private void addCoin_Click_1(object sender, RoutedEventArgs e)
        {
            stackPanelNewCoin.Visibility = Visibility.Visible;
            foreach (string item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCBCoinEvent(_iYear, _iSemestr), "event_name"))
            {
                cbEvents.Items.Add(item);
            }
            ACT.ReloadCB(cbTypeOfCoin);
        }

        private void buttonDeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (listViewTeachers.SelectedItems.Count >= 1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить учителя?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    QueriesManager.Execute(Connection.Established, Queries.DeleteTeacher(_idTeacher));
                    listViewTeachers.ItemsSource = TLVM.LoadTeachersList(Connection.Established, Queries.GetTeachersList);
                    imageTeachersCardPhoto.Source = null;
                    textBlockTeachersCardName.Text = "";
                    textBlockTeachersCardInfo.DataContext = null;
                    textBlockTeachersCardSpeciality.DataContext = null;
                }
            }
            else
            {
                MessageBox.Show("Сначала нужно выбрать, что удалять");
            }
        }

        public void ClearEditTeacher()
        {
            textBoxTeachersFio.Text = "";
            textBoxTeachersSpeciality.Text = "";
            textBoxTeachersInfo.Text = "";
            imageTeachersPortrait.Source = null;
        }

        private void buttonChangeTeacher_Click(object sender, RoutedEventArgs e)
        {
            textBoxTeachersFio.Text = textBlockTeachersCardName.Text;
            textBoxTeachersSpeciality.Text = textBlockTeachersCardSpeciality.Text;
            textBoxTeachersInfo.Text = textBlockTeachersCardInfo.Text;
            imageTeachersPortrait.Source = imageTeachersCardPhoto.Source;
            gridTeacherPage.Visibility = Visibility.Visible;
            colorZoneTeacherPage.Visibility = Visibility.Visible;
            stackPanelTeacherPanel.Visibility = Visibility.Visible;
            buttonNewTeacher.Content = "Обновить";
            _bIsNewTeacher = false;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (OpenFileDialog.ShowDialog() == true)
            {
                foreach (string Filename in OpenFileDialog.FileNames)
                    imageTeachersPortrait.Source = new BitmapImage(new Uri(OpenFileDialog.FileName));
                sFilePath = OpenFileDialog.FileName;
            }
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (imageTeachersPortrait.Source != null)
                {
                    if (_bIsNewTeacher == true)
                    {
                        IIVM.InsertImage(Connection.Established, sFilePath);
                        QueriesManager.Execute(Connection.Established, Queries.AddTeacher(textBoxTeachersFio.Text, textBoxTeachersSpeciality.Text,
                            textBoxTeachersInfo.Text, LIIVM.LastImageID(Connection.Established)));
                        imageTeachersCardPhoto.Source = null;
                        textBlockTeachersCardName.Text = "";
                        textBlockTeachersCardInfo.DataContext = null;
                        textBlockTeachersCardSpeciality.DataContext = null;
                    }
                    else
                    {
                        IIVM.InsertImage(Connection.Established, sFilePath);
                        QueriesManager.Execute(Connection.Established, Queries.UpdateTeacher(textBoxTeachersFio.Text, textBoxTeachersSpeciality.Text,
                            textBoxTeachersInfo.Text, LIIVM.LastImageID(Connection.Established), _idTeacher));
                        textBlockTeachersCardName.Text = textBoxTeachersFio.Text;
                        textBlockTeachersCardSpeciality.DataContext = TCVM.LoadTeacherCard(Connection.Established, Queries.GetTeacherCard(textBoxTeachersFio.Text));
                        textBlockTeachersCardInfo.DataContext = TCVM.LoadTeacherCard(Connection.Established, Queries.GetTeacherCard(textBoxTeachersFio.Text));
                        imageTeachersCardPhoto.Source = TPVM.LoadTeacherPhoto(Connection.Established, Queries.GetTeacherPhoto(textBoxTeachersFio.Text));

                    }
                    listViewTeachers.ItemsSource = TLVM.LoadTeachersList(Connection.Established, Queries.GetTeachersList);
                    gridTeacherPage.Visibility = Visibility.Hidden;
                    colorZoneTeacherPage.Visibility = Visibility.Hidden;
                    stackPanelTeacherPanel.Visibility = Visibility.Hidden;
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
            imageTeachersPortrait.Source = null;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            gridTeacherPage.Visibility = Visibility.Hidden;
            stackPanelTeacherPanel.Visibility = Visibility.Hidden;
            ClearEditTeacher();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            gridTeacherPage.Visibility = Visibility.Visible;
            colorZoneTeacherPage.Visibility = Visibility.Visible;
            stackPanelTeacherPanel.Visibility = Visibility.Visible;
            buttonNewTeacher.Content = "Добавить";
            _bIsNewTeacher = true;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            _bIsNewEvent = true;
            gridNewEvent.Visibility = Visibility.Visible;
            gridEventParticipants.Visibility = Visibility.Hidden;
            stackPanelEventPanel.Visibility = Visibility.Visible;
            cbEventType.Items.Clear();
            cbEventType.Items.Add("Все типы");
            foreach (string Item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCB_EventTypes, "event_type"))
            {
                cbEventType.Items.Add(Item);
            }
            buttonNewEvent.Content = "Добавить";
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            gridNewEvent.Visibility = Visibility.Hidden;
            gridEventParticipants.Visibility = Visibility.Hidden;
        }

        private void Participants_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gridNewEvent.Visibility = Visibility.Visible;
                stackPanelEventPanel.Visibility = Visibility.Hidden;
                gridEventParticipants.Visibility = Visibility.Visible;
                Events EvE = (Events)listViewEvents.SelectedItems[0];
                listViewParticipants.ItemsSource = TLVM.LoadTeachersList(Connection.Established,
                    Queries.GetParticipants(GIVM.LoadID(Connection.Established, Queries.GetEventID(EvE.Caption), "id_event")));
            }
            catch
            {
                MessageBox.Show("Нужно выбрать мероприятие");
            }
        }

        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Удалить мероприятие? Все коины, связанные с ним, исчезнут", "Удалить", 
                MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Events EvE = (Events)listViewEvents.SelectedItems[0];
                QueriesManager.Execute(
                    Connection.Established, Queries.DeleteEvent(GIVM.LoadID(Connection.Established, Queries.GetEventID(EvE.Caption), "id_event")));
                listViewEvents.ItemsSource = EVM.LoadEvents(Connection.Established, Queries.GetEventsList(_iYear, _iSemestr));
                textBlockEventInfo.DataContext = null;
            }
            else
            {

            }
            cbEventsPlaces.Items.Clear();
            cbEventsPlaces.Items.Add("Все");
            foreach (string Item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCB_EventPlaces, "event_place"))
            {
                cbEventsPlaces.Items.Add(Item);
            }
        }

        private void ChangeEvent_Click(object sender, RoutedEventArgs e)
        {
            cbEventType.Items.Clear();
            _bIsNewEvent = false;
            cbEventType.Items.Add("Все типы");
            foreach (string Item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCB_EventTypes, "event_type"))
            {
                cbEventType.Items.Add(Item);
            }
            gridNewEvent.Visibility = Visibility.Visible;
            stackPanelEventPanel.Visibility = Visibility.Visible;
            gridEventParticipants.Visibility = Visibility.Hidden;
            Events EvE = (Events)listViewEvents.SelectedItems[0];
            datePickerCalendar.SelectedDate = Convert.ToDateTime(EvE.Date);
            textBoxEventCaption.Text = EvE.Caption;
            textBoxEventPlace.Text = EvE.Place;
            textBoxEventDescription.Text = textBlockEventInfo.Text;
            cbEventType.SelectedItem = EvE.Type;
            buttonNewEvent.Content = "Изменить";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            listViewAdminMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetAdminMessageListSort("Принято"));
        }

        private void listViewAdminMessageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AdminMessageList AML = (AdminMessageList)listViewAdminMessageBox.SelectedItems[0];
                if (listViewAdminMessageBox.SelectedItems.Count >= 1)
                {
                    if (AML.Status == "Не прочитано")
                    {
                        buttonAcceptRequest.Visibility = Visibility.Visible;
                        buttonDeclineRequest.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        buttonAcceptRequest.Visibility = Visibility.Hidden;
                        buttonDeclineRequest.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    buttonAcceptRequest.Visibility = Visibility.Hidden;
                    buttonDeclineRequest.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdminMessageList AML = (AdminMessageList)listViewAdminMessageBox.SelectedItems[0];
                QueriesManager.Execute(Connection.Established, Queries.UpdateMessageStatus("Отклонено", AML.ID));
                listViewAdminMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetAdminMessageListSort(""));
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
                AdminMessageList AML = (AdminMessageList)listViewAdminMessageBox.SelectedItems[0];
                colorZoneTeacherPage.Visibility = Visibility.Visible;
                gridResponce.Visibility = Visibility.Visible;
                textBlockAcceptRequestEvent.Text = AML.Event;
                sTeacher = AML.Teacher;
                ACT.ReloadCB(cbAcceptRequestCoinType);
            }
            catch
            {
                MessageBox.Show("Нужно выбрать заявку");
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (cbAcceptRequestCoinType.Text == "")
            {
                MessageBox.Show("Нужно заполнить все поля");
            }
            else
            {
                _idCoinType = DCT.CoinType(cbAddCoinType);
                AdminMessageList AML = (AdminMessageList)listViewAdminMessageBox.SelectedItems[0];
                _idEvent = GIVM.LoadID(Connection.Established, Queries.GetEventID(textBlockAcceptRequestEvent.Text), "id_event");
                _idTeacher = GIVM.LoadID(Connection.Established, Queries.GetTeacherID(1, sTeacher), "id_teacher");
                QueriesManager.Execute(Connection.Established, Queries.AddCoin(_idEvent, _idTeacher, _idCoinType, textBoxAcceptRequestComment.Text));
                textBoxAcceptRequestComment.Text = "";
                QueriesManager.Execute(Connection.Established, Queries.UpdateMessageStatus("Принято", AML.ID));
                listViewAdminMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetAdminMessageListSort(""));
                colorZoneTeacherPage.Visibility = Visibility.Hidden;
                gridResponce.Visibility = Visibility.Hidden;
                listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            listViewAccounts.ItemsSource = ALVM.LoadAccounts(Connection.Established, Queries.GetAccounts);
            gridSettings.Visibility = Visibility.Visible;
            stackPanelSettings.Visibility = Visibility.Visible;
            textBoxCurrentYear.Text = _iYear.ToString();
            if (_iSemestr == 1)
            {
                rbFirstSemestr.IsChecked = true;
                rbSecondSemestr.IsChecked = false;
            }
            else
            {
                rbFirstSemestr.IsChecked = false;
                rbSecondSemestr.IsChecked = true;
            }
        }

        private void AddfdEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account ACC = (Account)listViewAccounts.SelectedItems[0];
                if (textBoxAccountLogin.Text == "" || textBoxAccountPassword.Text == "" || (textBoxAccountLogin.Text == "" && textBoxAccountPassword.Text == ""))
                {
                    MessageBox.Show("Нужно заполнить все поля");
                }
                else
                {
                    QueriesManager.Execute(Connection.Established, Queries.NewAccount(textBoxAccountLogin.Text, textBoxAccountPassword.Text, ACC.ID));
                    SnackbarOne.IsActive = true;
                    listViewAccounts.ItemsSource = ALVM.LoadAccounts(Connection.Established, Queries.GetAccounts);
                    textBoxAccountLogin.IsEnabled = false;
                    textBoxAccountPassword.IsEnabled = false;
                    buttonCreateAccount.IsEnabled = false;
                    buttonDeleteAccount.IsEnabled = true;
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
            try
            {
                gridSettings.Visibility = Visibility.Hidden;
                stackPanelSettings.Visibility = Visibility.Hidden;
                buttonChangeEvent.Visibility = Visibility.Hidden;
                buttonDeleteEvent.Visibility = Visibility.Hidden;
                buttonParticipants.Visibility = Visibility.Hidden;
                _iYear = Convert.ToInt32(textBoxCurrentYear.Text);
                if (rbFirstSemestr.IsChecked == true)
                {
                    _iSemestr = 1;
                }
                else
                {
                    _iSemestr = 2;
                }
                cbItemTotalRating.IsSelected = true;
                listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
                listViewEvents.ItemsSource = EVM.LoadEvents(Connection.Established, Queries.GetEventsList(_iYear, _iSemestr));
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введённого года");
            }
        }

        private void AccountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Account ACC = (Account)listViewAccounts.SelectedItems[0];
                if (listViewAccounts.SelectedItems.Count >= 1)
                {
                    buttonDeleteAccount.Visibility = Visibility.Visible;
                    buttonCreateAccount.Visibility = Visibility.Visible;
                    textBoxAccountLogin.Visibility = Visibility.Visible;
                    textBoxAccountPassword.Visibility = Visibility.Visible;
                    if (ACC.Login.Length >= 1)
                    {
                        textBoxAccountLogin.Text = ACC.Login;
                        textBoxAccountLogin.IsEnabled = false;
                        textBoxAccountPassword.Text = "********";
                        textBoxAccountPassword.IsEnabled = false;
                        buttonCreateAccount.IsEnabled = false;
                        buttonDeleteAccount.IsEnabled = true;
                    }
                    else
                    {
                        textBoxAccountLogin.Text = "";
                        textBoxAccountPassword.Text = "";
                        textBoxAccountLogin.IsEnabled = true;
                        textBoxAccountPassword.IsEnabled = true;
                        buttonCreateAccount.IsEnabled = true;
                        buttonDeleteAccount.IsEnabled = false;
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
                    QueriesManager.Execute(Connection.Established, Queries.DeleteAccount(textBoxAccountLogin.Text, textBoxAccountLogin.Text));
                    SnackbarOne.IsActive = true;
                    listViewAccounts.ItemsSource = ALVM.LoadAccounts(Connection.Established, Queries.GetAccounts);
                    textBoxAccountLogin.Text = "";
                    textBoxAccountPassword.Text = "";
                    textBoxAccountLogin.IsEnabled = true;
                    textBoxAccountPassword.IsEnabled = true;
                    buttonCreateAccount.IsEnabled = true;
                    buttonDeleteAccount.IsEnabled = false;
                }
                else { }
            }
            catch { }
        }

        private void lViewCoinsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lViewCoinsList.SelectedItems.Count >= 1)
            {
                CoinsList CoL = (CoinsList)lViewCoinsList.SelectedItems[0];
                textBlockCoinEvent.Text = CoL.Party;
                textBlockCoinEventDescription.Text = GCCVM.GetCoinComment(Connection.Established, CoL.IDCoin);
                textBlockCoinEventDate.Text = CoL.Date;
                textBlockCoinEventPlace.Text = CoL.Place;
            }
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (cbRequestEvent.SelectedItem != null)
            {
                QueriesManager.Execute(Connection.Established, Queries.AddMessage(GIVM.LoadID(Connection.Established, Queries.GetTeacherID(2, sLogin), "id_teacher"),
                    GIVM.LoadID(Connection.Established, Queries.GetEventID(cbRequestEvent.SelectedItem.ToString()), "id_event"), DateTime.Now));
                listViewTeacherMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetTeacherMessageList(sLogin, _iYear, _iSemestr));
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
            gridTotalRatingBackgroung.Visibility = Visibility.Visible;
            listViewTotalRating.ItemsSource = null;
            cbRatingTypes.Text = "Общий рейтинг";
            listViewTotalRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            colorZoneTeacherPage.Visibility = Visibility.Hidden;
            gridResponce.Visibility = Visibility.Hidden;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            gridNewEvent.Visibility = Visibility.Hidden;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (datePickerCalendar.SelectedDate != null)
            {
                if (cbEventType.SelectedItem != null)
                {
                    if (textBoxEventCaption.Text == "" || textBoxEventPlace.Text == "" || (textBoxEventCaption.Text == "" && textBoxEventPlace.Text == ""))
                    {
                        MessageBox.Show("Нужно указать данные о мероприятии");
                    }
                    else
                    {
                        if (_bIsNewEvent == true)
                        {
                            QueriesManager.Execute(Connection.Established, Queries.AddEvent(textBoxEventCaption.Text, textBoxEventPlace.Text, textBoxEventDescription.Text,
                                GIVM.LoadID(Connection.Established, Queries.GetEventTypeID(cbEventType.SelectedItem.ToString()), "id"), datePickerCalendar.SelectedDate.Value));
                        }
                        else
                        {
                            Events EvE = (Events)listViewEvents.SelectedItems[0];
                            QueriesManager.Execute(Connection.Established, Queries.UpdateEvent(textBoxEventCaption.Text, textBoxEventPlace.Text, textBoxEventDescription.Text,
                                GIVM.LoadID(Connection.Established, Queries.GetEventTypeID(cbEventType.SelectedItem.ToString()), "id"), datePickerCalendar.SelectedDate.Value,
                                GIVM.LoadID(Connection.Established, Queries.GetEventID(EvE.Caption), "id_event")));
                        }
                        gridNewEvent.Visibility = Visibility.Hidden;
                        textBoxEventCaption.Text = "";
                        textBoxEventPlace.Text = "";
                        cbEventType.SelectedItem = null;
                        listViewEvents.ItemsSource = EVM.LoadEvents(Connection.Established, Queries.GetEventsList(_iYear, _iSemestr));
                        textBlockEventInfo.Text = textBoxEventDescription.Text;
                        textBoxEventDescription.Text = "";
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
            if (cbEvents.Text == "" || cbTypeOfCoin.Text == "" || (cbEvents.Text == "" && cbTypeOfCoin.Text == ""))
            {
                MessageBox.Show("Нужно заполнить все поля");
            }
            else
            {
                _idCoinType = DCT.CoinType(cbAddCoinType);
                _idEvent = GIVM.LoadID(Connection.Established, Queries.GetEventID(cbEvents.Text), "id_event");
                QueriesManager.Execute(Connection.Established, Queries.AddCoin(_idEvent, _idTeacher, _idCoinType, textBoxComment.Text));
                listViewCoinsList.ItemsSource = CLVM.LoadCoinsList(Connection.Established, Queries.GetCoinsList(textBlockTeachersName.Text, _iYear, _iSemestr));
                listViewRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
                gridCoinsImg.DataContext = CCVM.LoadCoinsCount(Connection.Established, Queries.GetCoinsCount(textBlockTeachersName.Text, _iYear, _iSemestr));
                cbEvents.Items.Clear();
                textBoxComment.Text = "";
                stackPanelNewCoin.Visibility = Visibility.Hidden;
            }
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            listViewAdminMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetAdminMessageListSort("Отклонено"));
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            gridTotalRatingBackgroung.Visibility = Visibility.Hidden;
        }

        private void buttonGrantCoin_Click(object sender, RoutedEventArgs e)
        {
            colorZoneNewNight.Visibility = Visibility.Visible;
            stackPanelNewCoinPanel.Visibility = Visibility.Visible;
            cbGrantCoinEvents.Items.Clear();
            foreach (string item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCBCoinEvent(_iYear, _iSemestr), "event_name"))
            {
                cbGrantCoinEvents.Items.Add(item);
            }
            ACT.ReloadCB(cbAddCoinType);
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            if (cbGrantCoinEvents.Text == "" || cbAddCoinType.Text == "" || (cbGrantCoinEvents.Text == "" && cbAddCoinType.Text == ""))
            {
                MessageBox.Show("Нужно заполнить все поля");
            }
            else
            {
                _idCoinType = DCT.CoinType(cbAddCoinType);
                _idEvent = GIVM.LoadID(Connection.Established, Queries.GetEventID(cbGrantCoinEvents.Text), "id_event");
                QueriesManager.Execute(Connection.Established, Queries.AddCoin(_idEvent, _idTeacher, _idCoinType, textBoxCommentCoin.Text));
                cbGrantCoinEvents.Items.Clear();
                textBoxCommentCoin.Text = "";
                stackPanelNewCoinPanel.Visibility = Visibility.Hidden;
                colorZoneNewNight.Visibility = Visibility.Hidden;
                stackPanelNewCoinPanel.Visibility = Visibility.Hidden;
                cbAddCoinType.Items.Clear();
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            gridExportToExcel.Visibility = Visibility.Visible;
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            gridExportToExcel.Visibility = Visibility.Hidden;
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            var Workbook = new XLWorkbook();
            if (cbElementExportEvents.IsChecked == true)
            {
                Excel.EventsReport(Workbook, "Мероприятия", EVM.LoadEvents(Connection.Established, Queries.GetEventsList(_iYear, _iSemestr)));
            }
            if (cbElementExportTotal.IsChecked == true)
            {
                Excel.RatingReport(Workbook, "Общий рейтинг", RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr)));
            }
            if (cbElementExportTalents.IsChecked == true)
            {
                Excel.RatingReport(Workbook, "Таланты", RWM.LoadRating(Connection.Established, Queries.GetRatingTalents(_iYear, _iSemestr)));
            }
            if (cbElementExportIntellect.IsChecked == true)
            {
                Excel.RatingReport(Workbook, "Интеллект", RWM.LoadRating(Connection.Established, Queries.GetRatingIntellect(_iYear, _iSemestr)));
            }
            if (cbElementExportArtcoins.IsChecked == true)
            {
                Excel.RatingReport(Workbook, "Арткоины", RWM.LoadRating(Connection.Established, Queries.GetRatingArtcoins(_iYear, _iSemestr)));
            }
            if (cbElementExportSocialActivity.IsChecked == true)
            {
                Excel.RatingReport(Workbook, "Соц. активность", RWM.LoadRating(Connection.Established, Queries.GetRatingSocActivity(_iYear, _iSemestr)));
            }
            if (Excel.SaveReport(Workbook) == true)
            {
                gridExportToExcel.Visibility = Visibility.Hidden;
            }
        }

        #region Учительский общий рейтинг
        private void TcbItemTotalRating_Selected(object sender, RoutedEventArgs e)
        {
            listViewTotalRating.ItemsSource = null;
            listViewTotalRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
        }

        private void Tcbartcoins_Selected(object sender, RoutedEventArgs e)
        {
            listViewTotalRating.ItemsSource = null;
            listViewTotalRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTotal(_iYear, _iSemestr));
        }

        private void TcbIntellect_Selected(object sender, RoutedEventArgs e)
        {
            listViewTotalRating.ItemsSource = null;
            listViewTotalRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingIntellect(_iYear, _iSemestr));
        }

        private void TcbTalents_Selected(object sender, RoutedEventArgs e)
        {
            listViewTotalRating.ItemsSource = null;
            listViewTotalRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingTalents(_iYear, _iSemestr));
        }

        private void TcbSocialActivity_Selected(object sender, RoutedEventArgs e)
        {
            listViewTotalRating.ItemsSource = null;
            listViewTotalRating.ItemsSource = RWM.LoadRating(Connection.Established, Queries.GetRatingSocActivity(_iYear, _iSemestr));
        }
        #endregion

        public void TCoinsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            gridTeachersCoins.Visibility = Visibility.Visible;
            gridTeachersMessageBox.Visibility = Visibility.Hidden;
            listViewCoinsList.ItemsSource = CLVM.LoadCoinsList(Connection.Established, Queries.GetViewCoinsList(sLogin, _iYear, _iSemestr));
        }

        public void TRequestsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            gridTeachersCoins.Visibility = Visibility.Hidden;
            gridTeachersMessageBox.Visibility = Visibility.Visible;
            listViewTeacherMessageBox.ItemsSource = AMLVM.LoadMAList(Connection.Established, Queries.GetTeacherMessageList(sLogin, _iYear, _iSemestr));
            labelTeachersFio.DataContext = AMLVM.LoadMAList(Connection.Established, Queries.GetTeacherMessageList(sLogin, _iYear, _iSemestr));
            cbRequestEvent.Items.Clear();
            foreach (string Item in cESBTVM.LoadTypes(Connection.Established, Queries.GetCBCoinEvent(_iYear, _iSemestr), "event_name"))
            {
                cbRequestEvent.Items.Add(Item);
            }
        }
    }
}
