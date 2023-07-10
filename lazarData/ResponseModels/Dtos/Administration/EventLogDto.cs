using lazarData.Enums;
using lazarData.Models.Response.ViewModels;

namespace lazarData.ResponseModels.Dtos.Administration
{
    public class EventLogDto : BaseResponseModel
    {
        public Guid Id { get; set; }
        public DateTime DateChange { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        public string TypeInfo { get; set; }
        public string User { get; set; }
    }
}
