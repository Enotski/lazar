using lazarData.Utils;

namespace lazarData.Interfaces
{
    /// <summary>
    /// Интерфейс результата
    /// </summary>
    public interface IHelperResult
    {
        /// <summary>
        /// Состояние выполнения
        /// </summary>
        ResultState State { get; }
    }
}
