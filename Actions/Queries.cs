using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.Actions
{
    class Queries
    {
        public static string GetRatingArtcoins(int year, int semestr) =>
                $"select * from get_rate_artcoins('{year}', '{semestr}')";
        public static string GetRatingIntellect(int year, int semestr) =>
                $"select * from get_rate_intellect('{year}', '{semestr}')";
        public static string GetRatingSocActivity(int year, int semestr) =>
                $"select * from get_rate_soc_activity('{year}', '{semestr}')";
        public static string GetRatingTalents(int year, int semestr) =>
                $"select * from get_rate_talents('{year}', '{semestr}')";
        public static string GetRatingTotal(int year, int semestr) =>
                $"select * from get_rate_total('{year}', '{semestr}')";
        public static string GetTeachersList =>
                $"select * from teachers_list";
        public static string GetEventsList(int year, int semestr) =>
                $"select * from get_events_table('{year}', '{semestr}')";
        public static string GetCB_EventTypes =>
                $"select * from cb_sort_by_types";
        public static string GetCB_EventPlaces =>
                $"select * from cb_sort_by_places";
        public static string GetAdminMessageList =>
                $"select * from messages_admin_list";
        public static string GetCBCoinEvent(int year, int semestr) =>
                $"select * from get_cb_add_coin_event('{year}', '{semestr}')";
        public static string GetLastImage =>
                $"select * from last_image";
        public static string GetAccounts =>
                $"select * from account_list";
        public static string GetTeacherCard(string _teacher_name)=>
                $"select * FROM get_teacher_card('{_teacher_name}')";
        public static string GetTeacherPhoto(string _teacher_name) =>
                $"select * FROM get_teacher_photo('{_teacher_name}')";
        public static string GetEventInfo(string event_name) =>
                $"select * FROM get_event_info('{event_name}')";
        public static string GetEventID(string event_name) =>
                $"select * FROM get_event_id('{event_name}')";
        public static string GetTeacherID(int type, string teacher_name) =>
                $"select * FROM get_teacher_id('{type}', '{teacher_name}')";
        public static string GetEventTypeID(string event_name) =>
                $"select * FROM get_event_type_id('{event_name}')";
        public static string GetAdminMessageListSort(string type_sort) =>
                $"select * FROM get_admin_messages_list_sort('{type_sort}')";
        public static string GetTeacherMessageList(string login, int year, int semestr) =>
                $"select * FROM get_teacher_messages_list('{login}', '{year}', '{semestr}')";
        public static string GetCoinsList(string _teacher_name, int year, int semestr) =>
                $"select * FROM get_coins('{_teacher_name}', '{year}', '{semestr}')";
        public static string GetTCoinsList(string login, int year, int semestr) =>
                $"select * FROM get_tcoins('{login}', '{year}', '{semestr}')";
        public static string GetCoinsCount(string login, int year, int semestr) =>
                $"select * FROM get_coins_count('{login}', '{year}', '{semestr}')";
        public static string GetTCoinsCount(string login, int year, int semestr) =>
                $"select * FROM get_tcoins_count('{login}', '{year}', '{semestr}')";
        public static string GetCoinComment(int id_coin) =>
               $"select * FROM get_coin_comment('{id_coin}')";
        public static string GetSortedEvent(string event_type, string event_place) =>
               $"select * FROM get_sorted_events('{event_type}', '{event_place}')";
        public static string DeleteCoin(int coin_id) =>
                $"select * FROM delete_coin('{coin_id}')";
        public static string DeleteTeacher(int teacher_id) =>
                $"select * FROM delete_teacher('{teacher_id}')";
        public static string DeleteEvent(int event_id) =>
                $"select * FROM delete_event('{event_id}')";
        public static string UpdateMessageStatus(string newstatus, int message_id) =>
                $"select * FROM update_message_status('{newstatus}', '{message_id}')";
        public static string AddCoin(int id_event, int id_teacher, int id_coin_type, string comment) =>
            $"select * FROM add_coin('{id_event}', '{id_teacher}', '{id_coin_type}', '{comment}')";
        public static string AddTeacher(string teacher_name, string speciality, string about, int id_image) =>
               $"select * FROM add_teacher('{teacher_name}', '{speciality}', '{about}', '{id_image}')";
        public static string UpdateTeacher(string teacher_name, string speciality, string about, int id_image, int teacher_id) =>
               $"select * FROM update_teacher('{teacher_name}', '{speciality}', '{about}', '{id_image}', '{teacher_id}')";
        public static string GetParticipants(int event_id) =>
               $"select * FROM get_participants('{event_id}')";
        public static string AddEvent(string event_name, string event_place, string event_description, int id_type, DateTime date) =>
              $"select * FROM add_event('{event_name}', '{event_place}', '{event_description}', '{id_type}', '{date}')";
        public static string AddMessage(int id_teacher, int id_event, DateTime curr) =>
              $"select * FROM add_message('{id_teacher}', '{id_event}', '{curr}')";
        public static string UpdateEvent(string event_name, string event_place, string event_description, int id_type, DateTime date, int id_event) =>
               $"select * FROM update_event('{event_name}', '{event_place}', '{event_description}', '{id_type}', '{date}', '{id_event}')";
        public static string NewAccount(string login, string password, int id_teacher) =>
               $"create user \"{login}\" with password '{password}'; grant teacher to \"{login}\";" +
               $"update teacher set login = '{login}' where teacher.id_teacher = '{id_teacher}'";
        public static string DeleteAccount(string login, string account) =>
              $"drop user \"{login}\"; update teacher set login = '' where teacher.login = '{account}'";

    }
}
