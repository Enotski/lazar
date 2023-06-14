using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Данные пользователя для вывода
	/// </summary>
	public interface IUserFullName {
		/// <summary>
		/// Имя
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Фамилия
		/// </summary>
		string Surname { get; set; }
		/// <summary>
		/// Отчество
		/// </summary>
		string Patronymic { get; set; }
		/// <summary>
		/// Логин
		/// </summary>
		string Login { get; set; }
	}
}
