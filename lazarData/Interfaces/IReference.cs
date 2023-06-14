using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMK.Utils.Interfaces.Base;

namespace TMK.Utils.Interfaces {
	/// <summary>
	/// Справочники Ид Name Дата изменения последняя Родительский Ид 
	/// </summary>
	public interface IReference: INameBase, IDateChange, IDeleted, IChangedUserReference
    {
	}
}
