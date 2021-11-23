using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semana_7
{
    public interface Database
    {
        SQLiteAsyncConnection GetConnection();
    }
}
