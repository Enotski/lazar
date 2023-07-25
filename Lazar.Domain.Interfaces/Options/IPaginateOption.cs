namespace Lazar.Domain.Interfaces.Options {
    /// <summary>
    /// Интерфейс параметров пагинации
    /// </summary>
    public interface IPaginatedOption : ISkip, ITake { }
    /// <summary>
    /// Интерфейс пропуска заданного числа элементов 
    /// </summary>
    public interface ISkip {
        /// <summary>
        /// Пропустит количество элементов 
        /// </summary>
        int? Skip { get; set; }
    }
    /// <summary>
    /// Интерфейс ограничения выборки числа записей до заданного числа
    /// </summary>
    public interface ITake {
        /// <summary>
        /// Выбрать количество элементов
        /// </summary>
        int? Take { get; set; }
    }
}
