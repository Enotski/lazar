﻿using Lazar.Services.Contracts.Response.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.Contracts.Response.Interfaces {
    /// <summary>
    /// Интерфейс модели ответа
    /// </summary>
    public interface IResponse {
        ResponseResultState Result { get; set; }
    }
}
