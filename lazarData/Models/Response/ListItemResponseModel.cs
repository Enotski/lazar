using lazarData.Models.Response.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response
{
    /// <summary>
    /// Элемент для выпадающего списка
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class ListItemResponseModel<TKey> : BaseResponseModel
        where TKey : struct
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Данные
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public ListItemResponseModel()
        {
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public ListItemResponseModel(TKey key, string value, object data = null)
        {
            Key = key;
            Text = value;
            Data = data;
        }
    }
}
