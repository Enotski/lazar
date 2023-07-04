namespace lazarData.Models.Response.ViewModels
{
    /// <summary>
    /// Базовая модель
    /// </summary>
    public class BaseResponseModel {
		public string? ErrorMessage { get; private set; } = string.Empty;
		public BaseResponseModel(string mess) => ErrorMessage = mess;
        public BaseResponseModel() { }

    }
}
