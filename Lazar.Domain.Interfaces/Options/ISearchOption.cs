namespace Lazar.Domain.Interfaces.Options {
    public interface ISearchOption {
        /// <summary>
        /// Интерфейс параметров выборки
        /// </summary> 
        public interface ISearchOption {
            /// <summary>
            /// Столбец, к которому применяется условие
            /// </summary>
            string ColumnName { get; set; }
            /// <summary>
            /// Значение, в соответствии с которыми выполняется выборка
            /// </summary>
            string Value { get; set; }
        }
    }
}
