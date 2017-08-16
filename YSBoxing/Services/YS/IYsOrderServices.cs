using System.Collections.Generic;
using System.IO;
using YSBoxing.Core.YS;

namespace YSBoxing.Services.YS
{
    public interface IYsOrderServices
    {
        List<InputYsOrder> GetInputYsOrderByExcel(Stream stream);
    }
}