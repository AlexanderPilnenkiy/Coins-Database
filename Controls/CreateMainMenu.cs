using Coins_Database.Actions;
using Coins_Database.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coins_Database.Controls
{
    class CreateMainMenu
    {
        public static void CreateRadioButtons(StackPanel stackPanelMainMenu, MainWindow Window)
        {
            if (Session.Access == Session.ACCESS.Superadmin)
            {
                RadioButton Rate = new RadioButton
                {
                    IsChecked = true,
                    Content = "Рейтинг",
                    Width = 200,
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                Rate.Checked += Window.RatingRadioButton_Checked;
                stackPanelMainMenu.Children.Add(Rate);
                RadioButton Teachers = new RadioButton
                {
                    IsChecked = false,
                    Content = "Учителя",
                    Width = 200,
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                Teachers.Checked += Window.TeachersRadioButton_Checked;
                stackPanelMainMenu.Children.Add(Teachers);
                RadioButton Events = new RadioButton
                {
                    IsChecked = false,
                    Content = "Мероприятия",
                    Width = 200,
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                Events.Checked += Window.EventsRadioButton_Checked;
                stackPanelMainMenu.Children.Add(Events);
                RadioButton Applicanions = new RadioButton
                {
                    IsChecked = false,
                    Content = "Заявки",
                    Width = 200,
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                Applicanions.Checked += Window.ApplicationsRadioButton_Checked;
                stackPanelMainMenu.Children.Add(Applicanions);
            }
            else
            {
                RadioButton TeachersAchievements = new RadioButton
                {
                    IsChecked = true,
                    Content = "Мои награды",
                    Width = 400,
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                TeachersAchievements.Checked += Window.TCoinsRadioButton_Checked;
                stackPanelMainMenu.Children.Add(TeachersAchievements);
                RadioButton TeachersRequest = new RadioButton
                {
                    IsChecked = false,
                    Content = "Мои заявки",
                    Width = 400,
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                TeachersRequest.Checked += Window.TRequestsRadioButton_Checked;
                stackPanelMainMenu.Children.Add(TeachersRequest);
            }
        }
    }
}
