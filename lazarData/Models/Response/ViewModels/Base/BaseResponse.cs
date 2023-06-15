using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
	public class BaseResponse : HelperResult<BaseResponseModel> {
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		public BaseResponse(BaseResponseModel res)
			: base(res, null, null) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(BaseResponseModel res, object additionalInfo)
			: this(res, null, additionalInfo) {
			this.AdditionalInfo = additionalInfo;
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(BaseResponseModel res, Exception ex, object additionalInfo)
			: base(res, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		public BaseResponse(Exception ex)
			: this(null, ex, null) {

		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(Exception ex, object additionalInfo)
			: this(null, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(string exMsg, object additionalInfo)
			: base(null, null, additionalInfo) {
			this.Error = new Exception(exMsg);
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		public BaseResponse(string exMsg)
			: this(null, null, null) {
			this.Error = new Exception(exMsg);
		}
	}

	public class BaseResponse<T> : HelperResult<T>
		where T : BaseResponseModel {
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		public BaseResponse(T res)
			: base(res, null, null) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(T res, object additionalInfo)
			: this(res, null, additionalInfo) {
			this.AdditionalInfo = additionalInfo;
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(T res, Exception ex, object additionalInfo)
			: base(res, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		public BaseResponse(Exception ex)
			: this(null, ex, null) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(Exception ex, object additionalInfo)
			: this(null, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponse(string exMsg, object additionalInfo)
			: base(null, null, additionalInfo) {
			this.Error = new Exception(exMsg);
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		public BaseResponse(string exMsg)
			: this(null, null, null) {
			this.Error = new Exception(exMsg);
		}
	}


	public class BaseResponseEnumerable : HelperResult<IEnumerable<BaseResponseModel>> {


		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		public BaseResponseEnumerable(IEnumerable<BaseResponseModel> res)
			: base(res, null, null) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(IEnumerable<BaseResponseModel> res, object additionalInfo)
			: this(res, null, additionalInfo) {
			this.AdditionalInfo = additionalInfo;
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(IEnumerable<BaseResponseModel> res, Exception ex, object additionalInfo)
			: base(res, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		public BaseResponseEnumerable(Exception ex)
			: this(null, ex, null) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(Exception ex, object additionalInfo)
			: this(null, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(string exMsg, object additionalInfo)
			: base(null, null, additionalInfo) {
			this.Error = new Exception(exMsg);
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		public BaseResponseEnumerable(string exMsg)
			: this(null, null, null) {
			this.Error = new Exception(exMsg);
		}
	}

	public class BaseResponseEnumerable<T> : HelperResult<IEnumerable<T>>
		where T : BaseResponseModel {
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		public BaseResponseEnumerable(IEnumerable<T> res)
			: base(res, null, null) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(IEnumerable<T> res, object additionalInfo)
			: this(res, null, additionalInfo) {
			this.AdditionalInfo = additionalInfo;
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="res">Рузультат</param>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(IEnumerable<T> res, Exception ex, object additionalInfo)
			: base(res, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		public BaseResponseEnumerable(Exception ex)
			: this(null, ex, null) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="ex">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(Exception ex, object additionalInfo)
			: this(null, ex, additionalInfo) {
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		/// <param name="additionalInfo">Доп информация</param>
		public BaseResponseEnumerable(string exMsg, object additionalInfo)
			: base(null, null, additionalInfo) {
			this.Error = new Exception(exMsg);
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="exMsg">Ошибка</param>
		public BaseResponseEnumerable(string exMsg)
			: this(null, null, null) {
			this.Error = new Exception(exMsg);
		}
	}
}
