using System;
using System.Linq;
using TMK.Utils.Interfaces;
using TMK.Utils.Interfaces.Base;
namespace TMK.Utils.Extensions {
	public static class IcpExtension {
		public static bool IsUniqueIcpName<T>(this IQueryable<T> query, string icpName, Guid? id = null)
			where T : class, IKeyEntity, IIcpNames {
			try {
				if (string.IsNullOrEmpty(icpName)) {
					return false;
				}
				if (id.HasValue) {
					query = query.Where(x => x.Id != id);
				}
				return !query.Where(x => !string.IsNullOrEmpty(x.IcpName)).Any(x => x.IcpName.Trim().ToUpper() == icpName.Trim().ToUpper());
			} catch (Exception ex) {
				throw ex;
			}
		}

		public static bool IsUniqueIcpCode<T>(this IQueryable<T> query, string icpCode, Guid? id = null)
			where T : class, IKeyEntity, IIcpNames {
			try {
				if (string.IsNullOrEmpty(icpCode)) {
					return false;
				}
				if (id.HasValue) {
					query = query.Where(x => x.Id != id);
				}
				return !query.Where(x => !string.IsNullOrEmpty(x.IcpCode)).Any(x => x.IcpCode.Trim().ToUpper() == icpCode.Trim().ToUpper());
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}
