namespace Lazar.Srevices.Iterfaces.Base {
    public interface ILogService {
        /// <summary>
        /// Удаление записей из журнала
        /// </summary>
        /// <param name="ids">Код записи</param>
        /// <param name="login">Логин пользователя, инициирующего событие</param>
        /// <returns></returns>
        Task DeleteRecordsAsync(IEnumerable<Guid> ids, string login);
        /// <summary>
        /// Очистка журнала
        /// </summary>
        /// <param name="login">Логин пользователя, инициирующего событие</param>
        /// <returns></returns>
        Task ClearLogAsync(string login);
        /// <summary>
        /// Очистка устаревших данных
        /// </summary>
        /// <param name="days">Количество дней, по прошествию которого данные считаются устаревшими</param>
        /// <returns></returns>
        Task ClearLogAsync(int days);
    }
}
