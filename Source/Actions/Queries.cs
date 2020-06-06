using System;

namespace Coins_Database.Actions
{
    class Queries
    {
        public static string GetRatingArtcoins(int Year, int Semestr) =>
                $"select * from get_rate_artcoins('{Year}', '{Semestr}')";
        public static string GetRatingIntellect(int Year, int Semestr) =>
                $"select * from get_rate_intellect('{Year}', '{Semestr}')";
        public static string GetRatingSocActivity(int Year, int Semestr) =>
                $"select * from get_rate_soc_activity('{Year}', '{Semestr}')";
        public static string GetRatingTalents(int Year, int Semestr) =>
                $"select * from get_rate_talents('{Year}', '{Semestr}')";
        public static string GetRatingTotal(int Year, int Semestr) =>
                $"select * from get_rate_total('{Year}', '{Semestr}')";
        public static string GetTeachersList =>
                $"select * from teachers_list";
        public static string GetEventsList(int Year, int Semestr) =>
                $"select * from get_events_table('{Year}', '{Semestr}')";
        public static string GetCB_EventTypes =>
                $"select * from cb_sort_by_types";
        public static string GetCB_EventPlaces =>
                $"select * from cb_sort_by_places";
        public static string GetAdminMessageList =>
                $"select * from messages_admin_list";
        public static string GetCBCoinEvent(int Year, int Semestr) =>
                $"select * from get_cb_add_coin_event('{Year}', '{Semestr}')";
        public static string GetLastImage =>
                $"select * from last_image";
        public static string GetAccounts =>
                $"select * from account_list";
        public static string GetTeacherCard(string TeacherName) =>
                $"select * FROM get_teacher_card('{TeacherName}')";
        public static string GetTeacherPhoto(string TeacherName) =>
                $"select * FROM get_teacher_photo('{TeacherName}')";
        public static string GetEventInfo(string EventName) =>
                $"select * FROM get_event_info('{EventName}')";
        public static string GetEventID(string EventName) =>
                $"select * FROM get_event_id('{EventName}')";
        public static string GetTeacherID(int Type, string TeacherName) =>
                $"select * FROM get_teacher_id('{Type}', '{TeacherName}')";
        public static string GetEventTypeID(string EventName) =>
                $"select * FROM get_event_type_id('{EventName}')";
        public static string GetAdminMessageListSort(string TypeSort) =>
                $"select * FROM get_admin_messages_list_sort('{TypeSort}')";
        public static string GetTeacherMessageList(string Login, int Year, int Semestr) =>
                $"select * FROM get_teacher_messages_list('{Login}', '{Year}', '{Semestr}')";
        public static string GetCoinsList(string TeacherName, int Year, int Semestr) =>
                $"select * FROM get_coins('{TeacherName}', '{Year}', '{Semestr}')";
        public static string GetViewCoinsList(string Login, int Year, int Semestr) =>
                $"select * FROM get_tcoins('{Login}', '{Year}', '{Semestr}')";
        public static string GetCoinsCount(string Login, int Year, int Semestr) =>
                $"select * FROM get_coins_count('{Login}', '{Year}', '{Semestr}')";
        public static string GetTCoinsCount(string Login, int Year, int Semestr) =>
                $"select * FROM get_tcoins_count('{Login}', '{Year}', '{Semestr}')";
        public static string GetCoinComment(int IDCoin) =>
               $"select * FROM get_coin_comment('{IDCoin}')";
        public static string GetSortedEvent(string EventType, string EventPlace) =>
               $"select * FROM get_sorted_events('{EventType}', '{EventPlace}')";
        public static string DeleteCoin(int CoinID) =>
                $"select * FROM delete_coin('{CoinID}')";
        public static string DeleteTeacher(int TeacherID) =>
                $"select * FROM delete_teacher('{TeacherID}')";
        public static string DeleteEvent(int EventID) =>
                $"select * FROM delete_event('{EventID}')";
        public static string UpdateMessageStatus(string NewStatus, int MessageID) =>
                $"select * FROM update_message_status('{NewStatus}', '{MessageID}')";
        public static string AddCoin(int IDEvent, int IDTeacher, int IDCoin_Type, string Comment) =>
            $"select * FROM add_coin('{IDEvent}', '{IDTeacher}', '{IDCoin_Type}', '{Comment}')";
        public static string AddTeacher(string TeacherName, string Speciality, string About, int IDImage) =>
               $"select * FROM add_teacher('{TeacherName}', '{Speciality}', '{About}', '{IDImage}')";
        public static string UpdateTeacher(string TeacherName, string Speciality, string About, int IDImage, int TeacherID) =>
               $"select * FROM update_teacher('{TeacherName}', '{Speciality}', '{About}', '{IDImage}', '{TeacherID}')";
        public static string GetParticipants(int EventID) =>
               $"select * FROM get_participants('{EventID}')";
        public static string AddEvent(string EventName, string EventPlace, string EventDescription, int IDType, DateTime Date) =>
              $"select * FROM add_event('{EventName}', '{EventPlace}', '{EventDescription}', '{IDType}', '{Date}')";
        public static string AddMessage(int IDTeacher, int IDEvent, DateTime Curr) =>
              $"select * FROM add_message('{IDTeacher}', '{IDEvent}', '{Curr}')";
        public static string UpdateEvent(string EventName, string EventPlace, string EventDescription, int IDType, DateTime Date, int IDEvent) =>
               $"select * FROM update_event('{EventName}', '{EventPlace}', '{EventDescription}', '{IDType}', '{Date}', '{IDEvent}')";
        public static string NewAccount(string Login, string Password, int IDTeacher) =>
               $"create user \"{Login}\" with password '{Password}'; grant teacher to \"{Login}\";" +
               $"update teacher set login = '{Login}' where teacher.id_teacher = '{IDTeacher}'";
        public static string DeleteAccount(string Login, string Account) =>
              $"drop user \"{Login}\"; update teacher set login = '' where teacher.login = '{Account}'";

    }
}