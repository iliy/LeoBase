using AppData.Entities;
using AppPresentators.VModels.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IMapControl:UIComponent
    {
        event Action<MapSearchModel> FilterViolations;
        event Action<int> OpenViolation;
        List<AdminViolation> DataSource { get; set; }
        void SetPoint(MapPoint point);
        void SetPoint(AdminViolation violation);
        void ClearPoints();
    }
}
