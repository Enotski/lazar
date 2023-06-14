﻿using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Связь с филиалом
	/// </summary>
	public interface IFilialRelation {
		/// <summary>
		/// Ид филиала
		/// </summary>
		Guid FilialId { get; set; }
		/// <summary>
		/// Дата изменения
		/// </summary>
		DateTime FilialDateChange { get; set; }
	}
}
