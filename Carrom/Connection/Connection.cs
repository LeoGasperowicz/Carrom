using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrom
{
    public abstract class Connection
    {
        public abstract void openConnection();
        public abstract void closeConnection();
        public abstract List<Player> queryUsers();

    }
}
